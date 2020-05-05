namespace ITSkills.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ITSkills.Common;
    using ITSkills.Data;
    using ITSkills.Data.Models;
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels.Administration.Lections;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    [Area("Administration")]
    public class LectionsController : AdministrationController
    {
        private const string RedirectToRoute = "/Administration/Lections/Details/{0}";
        private readonly ApplicationDbContext _context;
        private readonly ILectionsService lectionsService;
        private readonly ICoursesService coursesService;
        private readonly UserManager<ApplicationUser> userManager;

        public LectionsController(ApplicationDbContext context, ILectionsService lectionsService, ICoursesService coursesService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.lectionsService = lectionsService;
            this.coursesService = coursesService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            IEnumerable<AllLectionsViewModel> lections = this.lectionsService.GetAll<AllLectionsViewModel>();
            return this.View(lections);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lection = await _context.Lections
                .Include(l => l.Course)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lection == null)
            {
                return NotFound();
            }

            return View(lection);
        }

        public IActionResult Create()
        {
            var users = this.userManager.Users;

            IEnumerable<CoursesDropDownMenuViewModel> courses = this.coursesService.GetAll<CoursesDropDownMenuViewModel>();
            var viewModel = new CreateLectionViewModel
            {
                Courses = courses,
                Users = users,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLectionViewModel input)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                var userId = user.Id;
                await this.lectionsService.CreateAsync(input.Title, input.Description, input.CourseId, input.Url, userId);
            }

            return this.Redirect("/Administration/Lections");
        }

        public IActionResult Edit(int id)
        {
            var users = this.userManager.Users;
            IEnumerable<CoursesDropDownMenuViewModel> courses = this.coursesService.GetAll<CoursesDropDownMenuViewModel>();
            var lection = this.lectionsService.GetById<EditLectionViewModel>(id);

            if (lection == null)
            {
                return this.View(GlobalConstants.NotFoundRoute);
            }

            lection.Users = users;
            lection.Courses = courses;

            return this.View(lection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditLectionViewModel input)
        {
            if (!this.lectionsService.LectionExists(input.Id))
            {
                return this.View(GlobalConstants.NotFoundRoute);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.lectionsService.EditAsync(input.Id, input.Title, input.Description, input.Url, input.UserId, input.CourseId);

            return this.Redirect(string.Format(RedirectToRoute, input.Id));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lection = await _context.Lections
                .Include(l => l.Course)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lection == null)
            {
                return NotFound();
            }

            return View(lection);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lection = await _context.Lections.FindAsync(id);
            _context.Lections.Remove(lection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

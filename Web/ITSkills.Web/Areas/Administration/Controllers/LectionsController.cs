namespace ITSkills.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lection = await _context.Lections.FindAsync(id);
            if (lection == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", lection.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", lection.UserId);
            return View(lection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,Title,Url,Description,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Lection lection)
        {
            if (id != lection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LectionExists(lection.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", lection.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", lection.UserId);
            return View(lection);
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

        private bool LectionExists(int id)
        {
            return _context.Lections.Any(e => e.Id == id);
        }
    }
}

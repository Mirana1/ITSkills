namespace ITSkills.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ITSkills.Common;
    using ITSkills.Data;
    using ITSkills.Data.Models;
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels.Administration.Courses;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    [Area("Administration")]
    public class CoursesController : AdministrationController
    {
        private readonly ApplicationDbContext _context;
        private readonly ICoursesService coursesService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CoursesController(ApplicationDbContext context, ICoursesService coursesService, ICategoriesService categoriesService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.coursesService = coursesService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<AllCoursesViewModel> courses = this.coursesService.GetAll<AllCoursesViewModel>();
            return this.View(courses);
        }

        public async Task<IActionResult> Details(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var course = await _context.Courses
            //    .Include(c => c.Category)
            //    .Include(c => c.User)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (course == null)
            //{
            //    return NotFound();
            //}
            if (!this.coursesService.CourseExists(id))
            {
                return this.View(GlobalConstants.NotFoundRoute);
            }

            var course = this.coursesService.GetById<DetailsCourseViewModel>(id);

            if (course == null)
            {
                return this.View(GlobalConstants.NotFoundRoute);
            }

            return this.View(course);
        }

        public IActionResult Create()
        {
            var users = this.userManager.Users;

            IEnumerable<CategoryDropDownViewModel> categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
            var viewModel = new CreateCourseViewModel
            {
                Categories = categories,
                Users = users,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;
            await this.coursesService.CreateAsync(input.Title, input.Description, input.CategoryId, input.Price, userId, input.AcquiredKnowledge, input.Requirements, input.ImageUrl);

            return this.Redirect("/Administration/Courses");
        }

        public IActionResult Edit(int id)
        {
            var users = this.userManager.Users;
            IEnumerable<CategoryDropDownViewModel> categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
            var course = this.coursesService.GetById<EditCourseViewModel>(id);
            if (course == null)
            {
                return this.View(GlobalConstants.NotFoundRoute);
            }

            course.Categories = categories;
            course.Users = users;
            return this.View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCourseViewModel input)
        {
            if (!this.coursesService.CourseExists(input.Id))
            {
                return this.View(GlobalConstants.NotFoundRoute);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.coursesService.EditAsync(input.Id, input.Title, input.Description, input.Price, input.ImageUrl, input.UserId, input.Requirements, input.AcquiredKnowledge, input.CategoryId);

            return this.Redirect("/Administration/Courses");
        }

        public IActionResult Delete(int id)
        {
            if (!this.coursesService.CourseExists(id))
            {
                return this.View(GlobalConstants.NotFoundRoute);
            }

            var course = this.coursesService.GetById<DeleteCourseViewModel>(id);

            return this.View(course);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.coursesService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

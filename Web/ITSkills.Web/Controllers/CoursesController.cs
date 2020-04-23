namespace ITSkills.Web.Controllers
{
    using System.Threading.Tasks;

    using ITSkills.Data.Models;
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : BaseController
    {
        private readonly ICoursesService coursesService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CoursesController(ICoursesService coursesService, ICategoriesService categoriesService, UserManager<ApplicationUser> userManager)
        {
            this.coursesService = coursesService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.coursesService.GetById<CoursesViewModel>(id);

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
            var viewModel = new CreateCourseViewModel
            {
                Categories = categories,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseViewModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;
            var courseId = await this.coursesService.CreateAsync(input.Title, input.Description, input.CategoryId, input.Price, userId, input.AcquiredKnowledge, input.Requirements, input.ImageUrl);
            this.TempData["InfoMessage"] = "Course created!";

            return this.RedirectToAction(nameof(this.ById), new { id = courseId });
        }
    }
}

namespace ITSkills.Web.Controllers
{
    using System.Threading.Tasks;

    using ITSkills.Data.Models;
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels.Lections;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class LectionsController : BaseController
    {
        private const string NotFoundRoute = "NotFound";

        private readonly ILectionsService lectionsService;
        private readonly ICoursesService coursesService;
        private readonly UserManager<ApplicationUser> userManager;

        public LectionsController(ILectionsService lectionsService, ICoursesService coursesService, UserManager<ApplicationUser> userManager)
        {
            this.lectionsService = lectionsService;
            this.coursesService = coursesService;
            this.userManager = userManager;
        }

        public IActionResult ById(int id)
        {
            if (!this.lectionsService.TryGetById<LectionsViewModel>(id))
            {
                return this.View(NotFoundRoute);
            }

            var lections = this.lectionsService.GetAll<ListLectionsViewModel>();
            var viewModel = this.lectionsService.GetById<LectionsViewModel>(id);
            viewModel.Lections = lections;

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var courses = this.coursesService.GetAll<CoursesDropDownMenuViewModel>();
            var viewModel = new CreateLectionViewModel
            {
                Courses = courses,
            };
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateLectionViewModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;
            var lectionId = await this.lectionsService.CreateAsync(input.Title, input.Description, input.CourseId, input.Url, userId);

            return this.RedirectToAction(nameof(this.ById), new { id = lectionId });
        }
    }
}

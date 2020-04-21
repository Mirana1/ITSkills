namespace ITSkills.Web.Controllers
{
    using System.Threading.Tasks;

    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels.Lections;
    using Microsoft.AspNetCore.Mvc;

    public class LectionsController : BaseController
    {
        private readonly ILectionsService lectionsService;
        private readonly ICoursesService coursesService;

        public LectionsController(ILectionsService lectionsService, ICoursesService coursesService)
        {
            this.lectionsService = lectionsService;
            this.coursesService = coursesService;
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.lectionsService.GetById<LectionsViewModel>(id);

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            var courses = this.coursesService.GetAll<CoursesDropDownMenuViewModel>();
            var viewModel = new CreateLectionViewModel
            {
                Courses = courses,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLectionViewModel input)
        {
            var lectionId = await this.lectionsService.CreateAsync(input.Title, input.Description, input.CourseId, input.Url);

            return this.RedirectToAction(nameof(this.ById), new { id = lectionId });
        }
    }
}

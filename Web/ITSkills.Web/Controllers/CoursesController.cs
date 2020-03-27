 namespace ITSkills.Web.Controllers
{
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : BaseController
    {
        private readonly ICoursesService coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.coursesService.GetById<CoursesViewModel>(id);

            return this.View(viewModel);
        }
    }
}

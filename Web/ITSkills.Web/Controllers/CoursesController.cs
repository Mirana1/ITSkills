namespace ITSkills.Web.Controllers
{
    using System.Linq;

    using ITSkills.Common;
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

        public IActionResult Index(string searchString)
        {
            var courses = this.coursesService.GetByTitle<SearchCourseViewModel>(searchString);

            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(x => x.Title.Contains(searchString));
            }

            return this.View(courses);
        }

        public IActionResult ById(int id)
        {
            if (!this.coursesService.TryGetById<CoursesViewModel>(id))
            {
                return this.View(GlobalConstants.NotFoundRoute);
            }

            var viewModel = this.coursesService.GetById<CoursesViewModel>(id);
            return this.View(viewModel);
        }
    }
}

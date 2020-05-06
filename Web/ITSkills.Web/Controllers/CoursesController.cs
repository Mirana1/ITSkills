namespace ITSkills.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using ITSkills.Common;
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;

    public class CoursesController : BaseController
    {
        private readonly ICoursesService coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        public IActionResult Index(string title)
        {
            IEnumerable<SearchCourseViewModel> courses = this.coursesService.GetByTitle<SearchCourseViewModel>(title);

            if (!string.IsNullOrEmpty(title))
            {
                courses = courses.Where(x => x.Title.ToLower().Contains(title));
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

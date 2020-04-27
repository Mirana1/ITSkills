namespace ITSkills.Web.Controllers
{
    using System.Threading.Tasks;

    using ITSkills.Data.Models;
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels.Courses;
    using ITSkills.Web.ViewModels.MyCourses;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class MyCoursesController : BaseController
    {
        private readonly IMyCoursesService myCourseService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICoursesService courseService;

        public MyCoursesController(IMyCoursesService myCourseService, UserManager<ApplicationUser> userManager, ICoursesService courseService)
        {
            this.myCourseService = myCourseService;
            this.userManager = userManager;
            this.courseService = courseService;
        }

        [Authorize]
        public async Task<IActionResult> Payment()
        {
            var courses = this.courseService.GetAll<CoursesDropDownViewModel>();
            var viewModel = new PaymentViewModel
            {
                Courses = courses
            };

            return this.View(viewModel);
        }
    }
}

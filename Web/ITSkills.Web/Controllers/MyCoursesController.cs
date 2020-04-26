namespace ITSkills.Web.Controllers
{
    using System.Threading.Tasks;

    using ITSkills.Data.Models;
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels.Courses;
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
        [HttpPost]
        public async Task<IActionResult> AddCourseToUser(int courseId, string userId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var wantedUserId = user.Id;
            var course = this.courseService.GetById<CoursesViewModel>(courseId);

            await this.myCourseService.AddCourseToUserAsync(course.Id, wantedUserId);

            return this.RedirectToAction($"/");
        }
    }
}

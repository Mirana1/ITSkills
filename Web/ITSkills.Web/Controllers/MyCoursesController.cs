namespace ITSkills.Web.Controllers
{
    using ITSkills.Common;
    using ITSkills.Data.Models;
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels.Courses;
    using ITSkills.Web.ViewModels.MyCourses;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    public class MyCoursesController : BaseController
    {
        private readonly IMyCoursesService myCourseService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICoursesService coursesService;

        public MyCoursesController(IMyCoursesService myCourseService, UserManager<ApplicationUser> userManager, ICoursesService coursesService)
        {
            this.myCourseService = myCourseService;
            this.userManager = userManager;
            this.coursesService = coursesService;
        }

        public ActionResult Payment(int id)
        {
            if (!this.coursesService.TryGetById<CoursesViewModel>(id))
            {
                return this.View(GlobalConstants.NotFoundRoute);
            }

            var currentUser = this.HttpContext.User.Identity.Name;
            var viewModel = this.coursesService.GetById<PaymentViewModel>(id);
            viewModel.Username = currentUser;
            return this.View(viewModel);
        }

        //[HttpPost]
        //public async Task<IActionResult> Payment(int courseId, string userId, string paymentCode)
        //{
        //    var isAdded = this.myCourseService.AddCourseToUserAsync(courseId, userId, paymentCode);

        //    if (isAdded)
        //    {
        //        return this.Redirect("/Trips/All");
        //    }

        //    return this.Redirect($"/Trips/Details?tripId={tripId}");
        //}
    }
}

namespace ITSkills.Web.Controllers
{
    using ITSkills.Common;
    using ITSkills.Data.Models;
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels.Courses;
    using ITSkills.Web.ViewModels.Lections;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class LectionsController : BaseController
    {
        private readonly ILectionsService lectionsService;
        private readonly IMyCoursesService myCoursesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICoursesService coursesService;

        public LectionsController(ILectionsService lectionsService, IMyCoursesService myCoursesService, UserManager<ApplicationUser> userManager, ICoursesService coursesService)
        {
            this.lectionsService = lectionsService;
            this.myCoursesService = myCoursesService;
            this.userManager = userManager;
            this.coursesService = coursesService;
        }

        [Authorize]
        public async Task<IActionResult> ById(int id)
        {
            if (!this.lectionsService.TryGetById<LectionsViewModel>(id))
            {
                return this.View(GlobalConstants.NotFoundRoute);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;

            if (!this.myCoursesService.IsHasPayed(userId))
            {
                return this.RedirectToAction("Payment", "Courses");
            }

            var lections = this.lectionsService.GetAll<ListLectionsViewModel>();
            var viewModel = this.lectionsService.GetById<LectionsViewModel>(id);
            viewModel.Lections = lections;
            return this.View(viewModel);
        }
    }
}

namespace ITSkills.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ITSkills.Common;
    using ITSkills.Data.Models;
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels.Courses;
    using ITSkills.Web.ViewModels.MyCourses;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : BaseController
    {
        private readonly ICoursesService coursesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMyCoursesService myCoursesService;

        public CoursesController(ICoursesService coursesService, UserManager<ApplicationUser> userManager, IMyCoursesService myCoursesService)
        {
            this.coursesService = coursesService;
            this.userManager = userManager;
            this.myCoursesService = myCoursesService;
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

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Payment(PaymentViewModel input)
        {
            RandomGenerator generator = new RandomGenerator();
            string paymentCode = generator.RandomCode();

            var viewModel = this.coursesService.GetById<PaymentViewModel>(input.Id);

            var user = await this.userManager.GetUserAsync(this.User);
            var userName = user.UserName;
            var userId = user.Id;

            await this.coursesService.AddCourseToUserAsync(input.Id, userId, paymentCode, viewModel.Price, userName);

            return this.RedirectToAction("PaymentCode", "Courses", new { userId = userId, courseId = viewModel.Id});
        }

        [Authorize]
        public async Task<IActionResult> PaymentCode(string userId, int courseId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            userId = user.Id;

            var viewModel = this.myCoursesService.GetById<PaymentCodeViewModel>(userId, courseId);

            return this.View(viewModel);
        }
    }
}

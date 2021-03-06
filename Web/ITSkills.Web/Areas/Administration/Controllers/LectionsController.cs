﻿namespace ITSkills.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ITSkills.Common;
    using ITSkills.Data.Models;
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels.Administration.Lections;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Area("Administration")]
    public class LectionsController : AdministrationController
    {
        private const string EditRedirectRoute = "/Administration/Lections/Details/{0}";

        private readonly ILectionsService lectionsService;
        private readonly ICoursesService coursesService;
        private readonly UserManager<ApplicationUser> userManager;

        [TempData]
        public string StatusMessage { get; set; }

        public LectionsController(ILectionsService lectionsService, ICoursesService coursesService, UserManager<ApplicationUser> userManager)
        {
            this.lectionsService = lectionsService;
            this.coursesService = coursesService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            IEnumerable<AllLectionsViewModel> lections = this.lectionsService.GetAll<AllLectionsViewModel>();
            return this.View(lections);
        }

        public IActionResult Details(int id)
        {
            var lection = this.lectionsService.GetById<DetailsLectionViewModel>(id);

            if (lection == null)
            {
                return this.View(GlobalConstants.NotFoundRoute);
            }

            return this.View(lection);
        }

        public IActionResult Create()
        {
            var users = this.userManager.Users;

            IEnumerable<CoursesDropDownMenuViewModel> courses = this.coursesService.GetAll<CoursesDropDownMenuViewModel>();
            var viewModel = new CreateLectionViewModel
            {
                Courses = courses,
                Users = users,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLectionViewModel input)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                var userId = user.Id;
                await this.lectionsService.CreateAsync(input.Title, input.Description, input.CourseId, input.Url, userId);
            }

            this.StatusMessage = "Lection created!";
            return this.Redirect("/Administration/Lections");
        }

        public IActionResult Edit(int id)
        {
            var users = this.userManager.Users;
            IEnumerable<CoursesDropDownMenuViewModel> courses = this.coursesService.GetAll<CoursesDropDownMenuViewModel>();
            var lection = this.lectionsService.GetById<EditLectionViewModel>(id);

            if (lection == null)
            {
                return this.View(GlobalConstants.NotFoundRoute);
            }

            lection.Users = users;
            lection.Courses = courses;

            return this.View(lection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditLectionViewModel input)
        {
            if (!this.lectionsService.LectionExists(input.Id))
            {
                return this.View(GlobalConstants.NotFoundRoute);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.lectionsService.EditAsync(input.Id, input.Title, input.Description, input.Url, input.UserId, input.CourseId);

            return this.Redirect(string.Format(EditRedirectRoute, input.Id));
        }

        public IActionResult Delete(int id)
        {
            var lection = this.lectionsService.GetById<DeleteLectionViewModel>(id);
            if (lection == null)
            {
                return this.View(GlobalConstants.NotFoundRoute);
            }

            return this.View(lection);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.lectionsService.DeleteAsync(id);
            return this.Redirect("/Administration/Lections");
        }
    }
}

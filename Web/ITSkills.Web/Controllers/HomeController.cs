﻿namespace ITSkills.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using ITSkills.Data.Models;
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels;
    using ITSkills.Web.ViewModels.Home;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;

    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly ICoursesService coursesService;
        private readonly IDistributedCache distributedCache;
        private readonly IMyCoursesService myCoursesService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(ICategoriesService categoriesService, ICoursesService coursesService, IDistributedCache distributedCache, IMyCoursesService myCoursesService, UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.coursesService = coursesService;
            this.distributedCache = distributedCache;
            this.myCoursesService = myCoursesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> CacheTest()
        {
            var data = await this.distributedCache.GetStringAsync("DateTimeAsString");
            if (data == null)
            {
                data = DateTime.UtcNow.ToString();
                await this.distributedCache.SetStringAsync("DateTimeAsString", data, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(2) });
            }

            return this.Ok(data);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel();

            var categories = this.categoriesService
                .GetAll<IndexCategoryViewModel>();
            viewModel.Categories = categories;

            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                var userId = user.Id;

                var myCourses = this.myCoursesService.GetAll<MyCoursesViewModel>().Where(x => x.UserId == userId).ToList();
                var coursesTitles = this.coursesService.GetAll<CoursesInMyCoursesViewModel>();

                foreach (var course in myCourses)
                {
                    foreach (var title in coursesTitles.Where(x => x.Id == course.CourseId))
                    {
                        course.ImageUrl = title.ImageUrl;
                        course.Title = title.Title;
                    }
                }

                viewModel.MyCourses = myCourses;
            }

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult Contacts()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

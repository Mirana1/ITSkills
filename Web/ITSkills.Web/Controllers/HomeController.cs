namespace ITSkills.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using ITSkills.Data;
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels;
    using ITSkills.Web.ViewModels.Categories;
    using ITSkills.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;

    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly ICoursesService coursesService;
        private readonly ApplicationDbContext db;
        private readonly IDistributedCache distributedCache;
        private readonly IMyCoursesService myCoursesService;

        public HomeController(ICategoriesService categoriesService, ICoursesService coursesService, ApplicationDbContext db, IDistributedCache distributedCache, IMyCoursesService myCoursesService)
        {
            this.categoriesService = categoriesService;
            this.coursesService = coursesService;
            this.db = db;
            this.distributedCache = distributedCache;
            this.myCoursesService = myCoursesService;
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

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            var categories = this.categoriesService
                .GetAll<IndexCategoryViewModel>();
            viewModel.Categories = categories;
            var myCourses = this.myCoursesService.GetAll<MyCoursesViewModel>();
            viewModel.MyCourses = myCourses;
            return this.View(viewModel);
        }

        //public IActionResult Search(string title, string searchWord)
        //{
        //    var searchedCourses = new List<string>();

        //    var allCourses = from c in db.Courses
        //                     orderby c.Title
        //                     select c.Title;

        //    searchedCourses.AddRange(allCourses.Distinct());
        //    ViewBag.title = new SelectList(searchedCourses);

        //    var courses = from c in db.Courses
        //                  select c;

        //    if (!string.IsNullOrEmpty(searchWord))
        //    {
        //        courses = courses.Where(c => c.Title.Contains(searchWord));
        //    }

        //    if (!string.IsNullOrEmpty(title))
        //    {
        //        courses = courses.Where(c => c.Title == title);
        //    }

        //    return View(courses.ToList());
        //}

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

namespace ITSkills.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using ITSkills.Data;
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels;
    using ITSkills.Web.ViewModels.Categories;
    using ITSkills.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly ICoursesService coursesService;
        private readonly ApplicationDbContext db;

        public HomeController(ICategoriesService categoriesService, ICoursesService coursesService, ApplicationDbContext db)
        {
            this.categoriesService = categoriesService;
            this.coursesService = coursesService;
            this.db = db;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            var categories = this.categoriesService
                .GetAll<IndexCategoryViewModel>();
            viewModel.Categories = categories;

            return this.View(viewModel);
        }

        public IActionResult Search(string title, string searchWord)
        {
            var searchedCourses = new List<string>();

            var allCourses = from c in db.Courses
                             orderby c.Title
                             select c.Title;

            searchedCourses.AddRange(allCourses.Distinct());
            ViewBag.title = new SelectList(searchedCourses);

            var courses = from c in db.Courses
                          select c;

            if (!string.IsNullOrEmpty(searchWord))
            {
                courses = courses.Where(c => c.Title.Contains(searchWord));
            }

            if (!string.IsNullOrEmpty(title))
            {
                courses = courses.Where(c => c.Title == title);
            }

            return View(courses.ToList());
        }

        public IActionResult Privacy()
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

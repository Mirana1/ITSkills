namespace ITSkills.Web.Controllers
{
    using System.Diagnostics;

    using ITSkills.Data;
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels;
    using ITSkills.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly ApplicationDbContext db;

        public HomeController(ICategoriesService categoriesService, ApplicationDbContext db)
        {
            this.categoriesService = categoriesService;
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

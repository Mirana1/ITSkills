namespace ITSkills.Web.Controllers
{
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class CategoriesController : BaseController
    {
        private const string NotFoundRoute = "NotFound";
        private readonly ICategoriesService categoriesService;
        private readonly ILogger<CategoriesController> logger;

        public CategoriesController(ICategoriesService categoriesService, ILogger<CategoriesController> logger)
        {
            this.categoriesService = categoriesService;
            this.logger = logger;
        }

        public IActionResult ByName(string name)
        {
            if (!this.categoriesService.TryGetCategoryById<CategoryViewModel>(name))
            {
                return this.View(NotFoundRoute);
            }

            var viewModel = this.categoriesService.GetByName<CategoryViewModel>(name);
            return this.View(viewModel);
        }
    }
}

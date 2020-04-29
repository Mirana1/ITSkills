namespace ITSkills.Web.Controllers
{
    using ITSkills.Common;
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult ByName(string name)
        {
            if (!this.categoriesService.TryGetCategoryById<CategoryViewModel>(name))
            {
                return this.View(GlobalConstants.NotFoundRoute);
            }

            var viewModel = this.categoriesService.GetByName<CategoryViewModel>(name);
            return this.View(viewModel);
        }
    }
}

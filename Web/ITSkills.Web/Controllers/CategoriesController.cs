namespace ITSkills.Web.Controllers
{
    using System.Threading.Tasks;

    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult ByName(string name)
        {
            var viewModel = this.categoriesService.GetByName<CategoryViewModel>(name);

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel input)
        {
            var category = await this.categoriesService.CreateAsync(input.Name, input.ImageUrl, input.Description);
            return this.Redirect("/");
        }
    }
}

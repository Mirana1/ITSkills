namespace ITSkills.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ITSkills.Common;
    using ITSkills.Services.Data;
    using ITSkills.Web.InputModels;
    using ITSkills.Web.ViewModels.Administration.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Area("Administration")]
    public class CategoriesController : AdministrationController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            IEnumerable<AllCategoriesViewModel> categories = this.categoriesService.GetAll<AllCategoriesViewModel>();
            return this.View(categories);
        }

        public IActionResult Details(int id)
        {
            var category = this.categoriesService.GetById<CategoryDetailsViewModel>(id);

            if (category == null)
            {
                return this.View(GlobalConstants.NotFoundRoute);
            }

            return this.View(category);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryViewModel input)
        {
            if (this.ModelState.IsValid)
            {
                await this.categoriesService.CreateAsync(input.Name, input.ImageUrl, input.Description);
                return this.RedirectToAction(nameof(this.Index));
            }

            this.TempData["Success"] = "Successfully created course!";
            return this.View(input);
        }

        public IActionResult Edit(int id)
        {
            var category = this.categoriesService.GetById<CategoryEditInputModel>(id);

            if (category == null)
            {
                return this.View(GlobalConstants.NotFoundRoute);
            }

            return this.View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryEditInputModel input)
        {
            if (!this.categoriesService.CategoryExists(input.Id))
            {
                return this.View(GlobalConstants.NotFoundRoute);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.categoriesService.EditAsync(input.Id, input.Name, input.ImageUrl, input.Description);

            return this.Redirect("/Administration/Categories");
        }

        public IActionResult Delete(int id)
        {
            var category = this.categoriesService.GetById<DeleteCategoryViewModel>(id);

            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.categoriesService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

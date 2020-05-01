namespace ITSkills.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ITSkills.Data;
    using ITSkills.Data.Models;
    using ITSkills.Services.Data;
    using ITSkills.Services.Models;
    using ITSkills.Web.InputModels;
    using ITSkills.Web.ViewModels.Administration.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    [Area("Administration")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ApplicationDbContext context, ICategoriesService categoriesService)
        {
            this.dbContext = context;
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index()
        {
            return this.View(await this.dbContext.Categories.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.View("NotFound");
            }

            var category = await this.dbContext.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return this.NotFound();
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

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = this.categoriesService.GetById<EditCategoryViewModel>(id);

            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryEditInputModel input)
        {
            if (!this.categoriesService.CategoryExists(input.Name))
            {
                return this.NotFound();
            }
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.categoriesService.EditAsync(input.Id, input.Name, input.ImageUrl, input.Description);

            return this.Redirect("/Administration/Categories");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.dbContext.Categories
                .FirstOrDefaultAsync(m => m.Id == id);

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

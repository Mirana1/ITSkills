namespace ITSkills.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ITSkills.Data;
    using ITSkills.Data.Models;
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly ApplicationDbContext dbContext;

        public CategoriesController(ICategoriesService categoriesService, ApplicationDbContext dbContext)
        {
            this.categoriesService = categoriesService;
            this.dbContext = dbContext;
        }

        public IActionResult ByName(string name)
        {
            var viewModel = this.categoriesService.GetByName<CategoryViewModel>(name);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel input)
        {
            await this.categoriesService.CreateAsync(input.Name, input.ImageUrl, input.Description);
            return this.Redirect("/");
        }

        public async Task<IActionResult> Edit(string name)
        {
            if (name == null)
            {
                return NotFound();
            }

            var category = await dbContext.Categories.FindAsync(name);

            if (category == null)
            {
                return NotFound();
            }

            return this.View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string name, [Bind("CreatedOn,ModifiedOn,IsDeleted,DeletedOn,Name, Description,ImageUrl")] Category category)
        {
            if (name != category.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Update(category);
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Name))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/");
            }
            return View(category);
        }

        private bool CategoryExists(string name)
        {
            return dbContext.Categories.Any(c => c.Name == name);
        }
    }
}

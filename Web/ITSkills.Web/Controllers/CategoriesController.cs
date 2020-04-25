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
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.categoriesService.CreateAsync(input.Name, input.ImageUrl, input.Description);

            return this.Redirect($"/Category/{input.Name}");
        }


        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.dbContext.Categories.FindAsync(id);

            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        [Route("/Category/Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CreatedOn,ModifiedOn,IsDeleted,DeletedOn,Name, Description,ImageUrl")] Category category)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            if (id != category.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.dbContext.Update(category);
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CategoryExists(category.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return this.Redirect("/");
            }
            return this.View(category);
        }

        private bool CategoryExists(int id)
        {
            return this.dbContext.Categories.Any(c => c.Id == id);
        }
    }
}

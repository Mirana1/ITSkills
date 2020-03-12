namespace ITSkills.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ITSkills.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<string> { "C#", "Machine Learning", "SQL", "Network Secutiry", "Unix" };

            foreach (var category in categories)
            {
                await dbContext.Categories.AddAsync(new Category
                {
                Name = category,
                Description = category,
                });
            }
        }
    }
}

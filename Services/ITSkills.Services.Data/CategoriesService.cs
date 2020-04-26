namespace ITSkills.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ITSkills.Data.Common.Repositories;
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public const string InvalidCategoryTitleErrorMessage = "Category with name {0} does not exist.";

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<int> CreateAsync(string name, string imageUrl, string description)
        {
            var category = new Category
            {
                Name = name,
                ImageUrl = imageUrl,
                Description = description,
            };

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();

            return category.Id;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query = this.categoryRepository
            .All()
            .OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            return this.categoryRepository.All()
                .Where(x => x.Name.Replace(" ", "-") == name.Replace(" ", "-"))
                .To<T>()
                .FirstOrDefault();
        }

        public T GetById<T>(int id)
        {
            return this.categoryRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task<int> GeIdByTitleAsync(string categoryTitle)
        {
            var category = await this.categoryRepository.AllAsNoTracking().SingleOrDefaultAsync(c => c.Name == categoryTitle);

            if (category == null)
            {
                throw new ArgumentNullException(string.Format(InvalidCategoryTitleErrorMessage, categoryTitle));
            }

            return category.Id;
        }

        public bool TryGetCategoryById<T>(string name)
        {
            return this.categoryRepository.AllAsNoTracking().Any(c => c.Name == name);
        }

    }
}

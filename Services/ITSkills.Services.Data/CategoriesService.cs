namespace ITSkills.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using ITSkills.Data.Common.Repositories;
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;
    using ITSkills.Services.Models;
    using Microsoft.EntityFrameworkCore;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

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
            var category = this.categoryRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return category;
        }

        public async Task DeleteAsync(int id)
        {
            var category = this.categoryRepository
                .All()
                .Where(c => c.Id == id)
                .FirstOrDefault();

            this.categoryRepository.Delete(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public bool TryGetCategoryById<T>(string name)
        {
            return this.categoryRepository.AllAsNoTracking().Any(c => c.Name == name);
        }

        public async Task EditAsync(int id, string name, string imageUrl, string description)
        {
            var category = this.categoryRepository.All().Where(c => c.Id == id).FirstOrDefault();
            category.Name = name;
            category.ImageUrl = imageUrl;
            category.Description = description;

            this.categoryRepository.Update(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public bool CategoryExists(int id)
        {
            return this.categoryRepository
                .All()
                .Any(n => n.Id == id);
        }
    }
}

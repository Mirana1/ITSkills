namespace ITSkills.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ITSkills.Data.Common.Repositories;
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

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
    }
}

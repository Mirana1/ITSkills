namespace ITSkills.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ITSkills.Services.Models;

    public interface ICategoriesService
    {
        Task<bool> Create(CategoryServiceModel categoryServiceModel);

        Task<int> CreateAsync(string name, string imageUrl, string description);

        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(int? id);

        Task<int> GeIdByTitleAsync(string categoryTitle);

        bool TryGetCategoryById<T>(string name);
    }
}

namespace ITSkills.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        Task<int> CreateAsync(string name, string imageUrl, string description);

        T GetById<T>(int id);

        Task<int> GeIdByTitleAsync(string categoryTitle);

        bool TryGetCategoryById<T>(string name);
    }
}

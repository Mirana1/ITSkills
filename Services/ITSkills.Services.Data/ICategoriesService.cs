namespace ITSkills.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        Task<int> CreateAsync(string name, string imageUrl, string description);

        Task EditAsync(int id, string name, string imageUrl, string description);

        Task DeleteAsync(int id);

        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(int id);

        bool TryGetCategoryById<T>(string name);

        bool CategoryExists(int id);
    }
}

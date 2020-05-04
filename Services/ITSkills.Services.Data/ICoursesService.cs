namespace ITSkills.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICoursesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Task<int> CreateAsync(string title, string description, int categoryId, decimal? price, string usedId, string acquiredKnowledge, string requirements, string imageUrl);

        Task EditAsync(int id, string title, string description, decimal? price, string imageUrl, string userId, string requirements, string acquiredKnowledge, int categoryId);

        Task DeleteAsync(int id);

        IEnumerable<T> GetByTitle<T>(string title);

        T GetById<T>(int id);

        bool TryGetById<T>(int id);

        bool CourseExists(int? id);
    }
}

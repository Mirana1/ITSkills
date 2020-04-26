namespace ITSkills.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICoursesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Task<int> CreateAsync(string title, string description, int categoryId, decimal? price, string usedId, string acquiredKnowledge, string requirements, string imageUrl);

        T GetByTitle<T>(string title);

        IEnumerable<T> Search<T>(string searchWord, string title);

        T GetById<T>(int id);

        bool TryGetById<T>(int id);
    }
}

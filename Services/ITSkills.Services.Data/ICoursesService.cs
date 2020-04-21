namespace ITSkills.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICoursesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Task<int> CreateAsync(string title, string description, int categoryId, decimal? price, string acquiredKnowledge, string requirements, string imageUrl);

        IEnumerable<T> Search<T>(string searchWord);

        T GetById<T>(int id);
    }
}

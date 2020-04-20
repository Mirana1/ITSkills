namespace ITSkills.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICoursesService
    {
        IEnumerable<T> GetAll<T>(int? count);

        Task<int> CreateAsync(string title, string description, int categoryId, decimal? price, string acquiredKnowledge, string requirements, string imageUrl);

        T GetById<T>(int id);
    }
}

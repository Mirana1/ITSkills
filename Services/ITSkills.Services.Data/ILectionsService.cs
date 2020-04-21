namespace ITSkills.Services.Data
{
    using System.Threading.Tasks;

    public interface ILectionsService
    {
        T GetById<T>(int id);

        Task<int> CreateAsync(string title, string description, int courseId, string url);
    }
}

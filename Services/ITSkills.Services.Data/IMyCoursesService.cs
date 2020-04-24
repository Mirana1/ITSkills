namespace ITSkills.Services.Data
{
    using System.Threading.Tasks;

    public interface IMyCoursesService
    {
        Task<int> AddCourseToUserAsync(int courseId, string userId);
    }
}

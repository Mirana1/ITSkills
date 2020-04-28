namespace ITSkills.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMyCoursesService
    {
        Task<int> AddCourseToUserAsync(int courseId, string userId);

        IEnumerable<T> GetAll<T>(int? count = null);
    }
}

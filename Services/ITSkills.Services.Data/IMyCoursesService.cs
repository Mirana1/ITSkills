namespace ITSkills.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMyCoursesService
    {
        Task<int> AddCourseToUserAsync(int courseId, string userId, string paymentCode);

        IEnumerable<T> GetAll<T>(int? count = null);
    }
}

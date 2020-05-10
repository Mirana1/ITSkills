namespace ITSkills.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMyCoursesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(string userId, int courseId);
    }
}

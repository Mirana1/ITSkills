namespace ITSkills.Services.Data
{
    using System.Collections.Generic;

    public interface IMyCoursesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(string userId, int courseId);

        bool IsHasPayed(string userId);
    }
}

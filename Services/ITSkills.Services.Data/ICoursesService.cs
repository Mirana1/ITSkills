namespace ITSkills.Services.Data
{
    using System.Collections.Generic;

    public interface ICoursesService
    {
        IEnumerable<T> GetAll<T>(int? count);

        T GetById<T>(int id);
    }
}

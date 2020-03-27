namespace ITSkills.Services.Data
{
    using System.Collections.Generic;

    public interface ICoursesService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);
    }
}

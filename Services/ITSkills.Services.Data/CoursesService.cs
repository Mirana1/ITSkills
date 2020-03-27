namespace ITSkills.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using ITSkills.Data.Common.Repositories;
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class CoursesService : ICoursesService
    {
        private readonly IRepository<Course> coursesRepository;

        public CoursesService(IRepository<Course> coursesRepository)
        {
            this.coursesRepository = coursesRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Course> query = this.coursesRepository
            .All()
            .OrderBy(x => x.Title);

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            return this.coursesRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }
    }
}

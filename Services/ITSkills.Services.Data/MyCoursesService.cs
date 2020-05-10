namespace ITSkills.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ITSkills.Data.Common.Repositories;
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class MyCoursesService : IMyCoursesService
    {
        private readonly IRepository<MyCourse> myCoursesRepository;

        public MyCoursesService(IRepository<MyCourse> myCoursesRepository)
        {
            this.myCoursesRepository = myCoursesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<MyCourse> query = this.myCoursesRepository
            .All();

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            return this.myCoursesRepository
                        .All()
                        .Where(mc => mc.Id == id)
                        .To<T>()
                        .FirstOrDefault();
        }
    }
}

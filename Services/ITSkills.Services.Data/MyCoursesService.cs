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

        public async Task<int> AddCourseToUserAsync(int courseId, string userId)
        {
            var userTrip = new MyCourse
            {
                CourseId = courseId,
                UserId = userId,
            };

            await this.myCoursesRepository.AddAsync(userTrip);
            await this.myCoursesRepository.SaveChangesAsync();
            return userTrip.Id;
        }
    }
}

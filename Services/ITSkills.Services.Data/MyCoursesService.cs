namespace ITSkills.Services.Data
{
    using System.Threading.Tasks;

    using ITSkills.Data.Common.Repositories;
    using ITSkills.Data.Models;

    public class MyCoursesService : IMyCoursesService
    {
        private readonly IRepository<MyCourse> myCoursesRepository;

        public MyCoursesService(IRepository<MyCourse> myCoursesRepository)
        {
            this.myCoursesRepository = myCoursesRepository;
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

namespace ITSkills.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task<int> CreateAsync(string title, string description, int categoryId, decimal? price, string userId, string acquiredKnowledge, string requirements, string imageUrl)
        {
            var course = new Course
            {
                Title = title,
                Description = description,
                CategoryId = categoryId,
                Price = price,
                UserId = userId,
                AcquiredKnowledge = acquiredKnowledge,
                Requirements = requirements,
                ImageUrl = imageUrl,
            };

            await this.coursesRepository.AddAsync(course);
            await this.coursesRepository.SaveChangesAsync();
            return course.Id;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Course> query = this.coursesRepository
            .All()
            .OrderBy(x => x.Title);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

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

        public T GetByTitle<T>(string title)
        {
            return this.coursesRepository.All()
                .Where(x => x.Title.Replace(" ", "-") == title.Replace(" ", "-"))
                .To<T>()
                .FirstOrDefault();
        }

        public bool TryGetById<T>(int id)
        {
            return this.coursesRepository.AllAsNoTracking().Any(x => x.Id == id);
        }
    }
}

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

        public async Task EditAsync(int id, string title, string description, decimal? price, string imageUrl, string userId, string requirements, string acquiredKnowledge, int categoryId)
        {
            var course = this.coursesRepository.All().Where(c => c.Id == id).FirstOrDefault();

            course.Title = title;
            course.Description = description;
            course.Price = price;
            course.ImageUrl = imageUrl;
            course.UserId = userId;
            course.Requirements = requirements;
            course.AcquiredKnowledge = acquiredKnowledge;
            course.CategoryId = categoryId;

            this.coursesRepository.Update(course);
            await this.coursesRepository.SaveChangesAsync();
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

        public IEnumerable<T> GetByTitle<T>(string title)
        {
            return this.coursesRepository
            .AllAsNoTracking()
            .Where(x => x.Title.Contains(title))
            .To<T>()
            .ToList();

        }

        public bool TryGetById<T>(int id)
        {
            return this.coursesRepository.AllAsNoTracking().Any(x => x.Id == id);
        }

        public bool CourseExists(int? id)
        {
            return this.coursesRepository
                .All()
                .Any(n => n.Id == id);
        }
    }
}

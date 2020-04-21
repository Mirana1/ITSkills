namespace ITSkills.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using ITSkills.Data.Common.Repositories;
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class LectionsService : ILectionsService
    {
        private readonly IDeletableEntityRepository<Lection> lectionsRepository;

        public LectionsService(IDeletableEntityRepository<Lection> lectionsRepository)
        {
            this.lectionsRepository = lectionsRepository;
        }

        public async Task<int> CreateAsync(string title, string description, int courseId, string url)
        {
            var lection = new Lection
            {
                Title = title,
                Description = description,
                CourseId = courseId,
                Url = url,
            };

            await this.lectionsRepository.AddAsync(lection);
            await this.lectionsRepository.SaveChangesAsync();

            return lection.Id;
        }

        public T GetById<T>(int id)
        {
            return this.lectionsRepository
                 .All()
                 .Where(x => x.Id == id)
                 .To<T>()
                 .FirstOrDefault();
        }
    }
}

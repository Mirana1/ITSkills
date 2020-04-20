namespace ITSkills.Services.Data
{
    using System.Linq;

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

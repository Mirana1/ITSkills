﻿namespace ITSkills.Services.Data
{
    using System.Collections.Generic;
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

        public async Task<int> CreateAsync(string title, string description, int courseId, string url, string userId)
        {
            var lection = new Lection
            {
                Title = title,
                Description = description,
                CourseId = courseId,
                Url = url,
                UserId = userId,
            };

            await this.lectionsRepository.AddAsync(lection);
            await this.lectionsRepository.SaveChangesAsync();

            return lection.Id;
        }

        public IEnumerable<T> GetAll<T>(int? count)
        {
            IQueryable<Lection> query = this.lectionsRepository
            .All()
            .OrderBy(x => x.Title);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task EditAsync(int id, string title, string description, string url, string userId, int courseId)
        {
            var lection = this.lectionsRepository.All().Where(l => l.Id == id).FirstOrDefault();

            lection.Title = title;
            lection.Description = description;
            lection.Url = url;
            lection.UserId = userId;
            lection.CourseId = courseId;

            this.lectionsRepository.Update(lection);
            await this.lectionsRepository.SaveChangesAsync();
        }

        public T GetById<T>(int id)
        {
            return this.lectionsRepository
                 .All()
                 .Where(x => x.Id == id)
                 .To<T>()
                 .FirstOrDefault();
        }

        public bool TryGetById<T>(int id)
        {
            return this.lectionsRepository.AllAsNoTracking().Any(l => l.Id == id);
        }

        public bool LectionExists(int? id)
        {
            return this.lectionsRepository.All().Any(e => e.Id == id);
        }

        public async Task DeleteAsync(int id)
        {
            var searchedLection = this.lectionsRepository.All().Where(l => l.Id == id).FirstOrDefault();
            this.lectionsRepository.Delete(searchedLection);
            await this.lectionsRepository.SaveChangesAsync();
        }
    }
}

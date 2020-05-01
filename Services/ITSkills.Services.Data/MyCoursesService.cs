namespace ITSkills.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
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

        public async Task<int> AddCourseToUserAsync(int courseId, string userId, string paymentCode)
        {
            var courseToUser = new MyCourse
            {
                CourseId = courseId,
                UserId = userId,
                PaymentCode = this.Hash(paymentCode),
            };

            await this.myCoursesRepository.AddAsync(courseToUser);
            await this.myCoursesRepository.SaveChangesAsync();
            return courseToUser.Id;
        }

        private string Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}

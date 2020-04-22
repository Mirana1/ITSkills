﻿namespace ITSkills.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILectionsService
    {
        T GetById<T>(int id);

        IEnumerable<T> GetAll<T>(int? count = null);

        Task<int> CreateAsync(string title, string description, int courseId, string url);
    }
}

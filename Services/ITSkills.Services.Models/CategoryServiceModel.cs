﻿namespace ITSkills.Services.Models
{
    using System;

    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class CategoryServiceModel : IMapFrom<Course>, IMapTo<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}

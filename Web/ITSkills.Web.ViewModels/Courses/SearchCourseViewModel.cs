namespace ITSkills.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class SearchCourseViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal? Price { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}

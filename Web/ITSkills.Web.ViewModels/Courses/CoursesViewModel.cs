namespace ITSkills.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;

    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class CoursesViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime CreatedOn { get; set; }

        public int LectionsCount { get; set; }

        public string Requirements { get; set; }

        public string AcquiredKnowledge { get; set; }

        public IEnumerable<LectionsInCourseViewModel> Lections { get; set; }
    }
}

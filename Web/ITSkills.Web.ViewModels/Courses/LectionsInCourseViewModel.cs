﻿namespace ITSkills.Web.ViewModels.Courses
{
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class LectionsInCourseViewModel : IMapFrom<Lection>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}

﻿namespace ITSkills.Web.ViewModels.Administration.Lections
{
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class CoursesDropDownMenuViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}

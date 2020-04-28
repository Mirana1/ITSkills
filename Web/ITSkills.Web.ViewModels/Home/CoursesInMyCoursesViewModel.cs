using ITSkills.Data.Models;
using ITSkills.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITSkills.Web.ViewModels.Home
{
    public class CoursesInMyCoursesViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }
    }
}

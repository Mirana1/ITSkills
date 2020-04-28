using ITSkills.Data.Models;
using ITSkills.Services.Mapping;
using System;

namespace ITSkills.Services.Models
{
    public class CoursesListingServiceViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}

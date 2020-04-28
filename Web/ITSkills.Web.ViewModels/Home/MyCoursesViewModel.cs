namespace ITSkills.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class MyCoursesViewModel : IMapFrom<MyCourse>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public IEnumerable<CoursesInMyCoursesViewModel> Courses { get; set; }
    }
}

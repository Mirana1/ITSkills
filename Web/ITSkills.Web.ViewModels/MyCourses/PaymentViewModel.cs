namespace ITSkills.Web.ViewModels.MyCourses
{
    using System.Collections.Generic;

    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class PaymentViewModel : IMapFrom<MyCourse>
    {
        public IEnumerable<CoursesDropDownViewModel> Courses { get; set; }
    }
}

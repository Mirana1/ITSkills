namespace ITSkills.Web.ViewModels.MyCourses
{
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class CoursesDropDownViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal? Price { get; set; }
    }
}

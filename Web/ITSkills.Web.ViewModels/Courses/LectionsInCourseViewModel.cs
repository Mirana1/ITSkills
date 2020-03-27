namespace ITSkills.Web.ViewModels.Courses
{
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class LectionsInCourseViewModel : IMapFrom<Lection>
    {
        public string Title { get; set; }

        public string Url { get; set; }
    }
}

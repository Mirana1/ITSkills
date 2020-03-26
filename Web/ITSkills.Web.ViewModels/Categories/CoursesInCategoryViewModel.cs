namespace ITSkills.Web.ViewModels.Categories
{
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class CoursesInCategoryViewModel : IMapFrom<Course>
    {
        public string Title { get; set; }

        public string UserUserName { get; set; }
    }
}

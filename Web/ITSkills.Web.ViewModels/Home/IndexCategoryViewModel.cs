namespace ITSkills.Web.ViewModels.Home
{
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class IndexCategoryViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int? CoursesCount { get; set; }

        public string Url => $"/Category/{this.Name.Replace(' ', '-')}";
    }
}

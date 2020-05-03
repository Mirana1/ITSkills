namespace ITSkills.Web.ViewModels.Administration.Courses
{
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

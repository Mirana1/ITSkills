namespace ITSkills.Web.ViewModels.Categories
{
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class CreateCategoryViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}

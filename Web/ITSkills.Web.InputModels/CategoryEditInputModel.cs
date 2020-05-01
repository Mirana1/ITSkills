namespace ITSkills.Web.InputModels
{
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class CategoryEditInputModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}

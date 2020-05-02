namespace ITSkills.Web.InputModels
{
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class CategoryEditInputModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}

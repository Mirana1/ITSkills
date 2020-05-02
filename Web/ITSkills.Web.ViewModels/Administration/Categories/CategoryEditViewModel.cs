namespace ITSkills.Web.ViewModels.Administration.Categories
{
    using System.ComponentModel.DataAnnotations;

    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class CategoryEditViewModel : IMapTo<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
    }
}

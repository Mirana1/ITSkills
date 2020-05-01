namespace ITSkills.Web.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ITSkills.Services.Mapping;
    using ITSkills.Services.Models;

    public class CategoryCreateInputModel : IMapTo<CategoryServiceModel>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}

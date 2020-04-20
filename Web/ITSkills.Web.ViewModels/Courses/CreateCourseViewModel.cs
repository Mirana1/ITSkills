namespace ITSkills.Web.ViewModels.Courses
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class CreateCourseViewModel : IMapFrom<Course>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public decimal? Price { get; set; }

        [Required]
        public string AcquiredKnowledge { get; set; }

        [Required]
        public string Requirements { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }
    }
}

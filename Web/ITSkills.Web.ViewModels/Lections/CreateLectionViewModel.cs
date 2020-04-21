namespace ITSkills.Web.ViewModels.Lections
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class CreateLectionViewModel : IMapFrom<Lection>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [Required]
        public string Url { get; set; }

        public IEnumerable<CoursesDropDownMenuViewModel> Courses { get; set; }
    }
}

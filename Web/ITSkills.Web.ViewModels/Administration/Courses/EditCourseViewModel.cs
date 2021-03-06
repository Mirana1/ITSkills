﻿namespace ITSkills.Web.ViewModels.Administration.Courses
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class EditCourseViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Title should be at least {2} characters", MinimumLength = 10)]
        public string Title { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Description should be at least {2} characters", MinimumLength = 50)]
        public string Description { get; set; }

        public decimal? Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }

        [Required]
        public string Requirements { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public string AcquiredKnowledge { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }
    }
}

﻿namespace ITSkills.Web.ViewModels.Administration.Lections
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class EditLectionViewModel : IMapFrom<Lection>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Title should be at least {2} characters!", MinimumLength = 10)]
        public string Title { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Title should be at least {2} characters!", MinimumLength = 50)]
        public string Description { get; set; }

        [Required]
        public string Url { get; set; }

        public string UserId { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }

        public int CourseId { get; set; }

        public IEnumerable<CoursesDropDownMenuViewModel> Courses { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}

namespace ITSkills.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ITSkills.Data.Common.Models;

    public class MyCourse : BaseDeletableModel<int>
    {
        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        public string PaymentCode { get; set; }

        public bool HasPayed { get; set; }

        public decimal? Price { get; set; }

        public string Username { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}

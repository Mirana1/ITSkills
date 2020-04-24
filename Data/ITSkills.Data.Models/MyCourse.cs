namespace ITSkills.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ITSkills.Data.Common.Models;

    public class MyCourse : BaseDeletableModel<int>
    {
        public MyCourse()
        {
            this.Courses = new HashSet<Course>();
        }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public int CourseId { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}

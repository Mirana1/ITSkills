namespace ITSkills.Data.Models
{
    using System.Collections.Generic;

    using ITSkills.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Courses = new HashSet<Course>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}

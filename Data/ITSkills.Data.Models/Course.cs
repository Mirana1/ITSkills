namespace ITSkills.Data.Models
{
    using System.Collections.Generic;

    using ITSkills.Data.Common.Models;

    public class Course : BaseModel<int>
    {
        public Course()
        {
            this.Lections = new HashSet<Lection>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Lection> Lections { get; set; }
    }
}

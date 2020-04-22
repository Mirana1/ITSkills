namespace ITSkills.Web.ViewModels.Categories
{
    using System;

    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class CoursesInCategoryViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal? Price { get; set; }

        public DateTime CreatedOn { get; set; }

        public ApplicationUser User { get; set; }
    }
}

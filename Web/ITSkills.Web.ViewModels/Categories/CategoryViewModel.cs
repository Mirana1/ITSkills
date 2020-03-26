namespace ITSkills.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? CoursesCount { get; set; }

        public IEnumerable<CoursesInCategoryViewModel> Courses { get; set; }
    }
}

namespace ITSkills.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using ITSkills.Web.ViewModels.Categories;

    public class SearchCourseViewModel
    {
        public IEnumerable<CoursesInCategoryViewModel> Courses { get; set; }
    }
}

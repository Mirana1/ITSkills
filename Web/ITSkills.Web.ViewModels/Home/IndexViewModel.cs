namespace ITSkills.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<IndexCategoryViewModel> Categories { get; set; }

        public IEnumerable<SearchCourseViewModel> Courses { get; set; }

        public string Search { get; set; }
    }
}

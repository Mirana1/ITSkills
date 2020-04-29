namespace ITSkills.Web.ViewModels.MyCourses
{
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class PaymentViewModel : IMapFrom<MyCourse>, IMapFrom<Course>
    {
        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public Course Course { get; set; }

        public string Title { get; set; }

        public decimal? Price { get; set; }
    }
}

namespace ITSkills.Web.ViewModels.Administration.Courses
{
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class UsersViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }
    }
}

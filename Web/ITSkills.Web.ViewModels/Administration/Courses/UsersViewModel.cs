using ITSkills.Data.Models;
using ITSkills.Services.Mapping;

namespace ITSkills.Web.ViewModels.Administration.Courses
{
    public class UsersViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }
    }
}

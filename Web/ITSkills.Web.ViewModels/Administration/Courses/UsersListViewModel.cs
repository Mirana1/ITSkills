using ITSkills.Data.Models;
using ITSkills.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITSkills.Web.ViewModels.Administration.Courses
{
    public class UsersListViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }
    }
}

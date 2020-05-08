using ITSkills.Data.Models;
using ITSkills.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITSkills.Web.ViewModels.Courses
{
    public class UserPaymentViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }
    }
}

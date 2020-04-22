namespace ITSkills.Web.Areas.Identity.Data
{
    using Microsoft.AspNetCore.Identity;

    public class ITSkillsUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string LastName { get; set; }

        [PersonalData]
        public string AboutInfo { get; set; }

        [PersonalData]
        public string ImageUrl { get; set; }
    }
}

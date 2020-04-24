namespace ITSkills.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area("Admin")]
    public class DashboardController : AdministrationController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}

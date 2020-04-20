namespace ITSkills.Web.Controllers
{
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels.Lections;
    using Microsoft.AspNetCore.Mvc;

    public class LectionsController : BaseController
    {
        private readonly ILectionsService lectionsService;

        public LectionsController(ILectionsService lectionsService)
        {
            this.lectionsService = lectionsService;
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.lectionsService.GetById<LectionsViewModel>(id);

            return this.View(viewModel);
        }
    }
}

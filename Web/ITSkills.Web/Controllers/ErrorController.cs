namespace ITSkills.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using ITSkills.Common;
    using ITSkills.Services.Data;
    using ITSkills.Web.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class ErrorController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public ErrorController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [Route("/Error/500")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult InternalServerError()
        {
            var errorViewModel = new ErrorViewModel
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
            };

            return this.View(errorViewModel);
        }

        [Route("/Error/404")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public async Task<IActionResult> NotFoundError()
        {
            var errorViewModel = new ErrorViewModel();
            errorViewModel.StatusCode = StatusCodes.Status404NotFound;
            this.ViewData["Id"] = await this.categoriesService.GeIdByTitleAsync(GlobalConstants.CategoryTitle);

            if (this.TempData["ErrorParams"] is Dictionary<string, string> dictionary)
            {
                errorViewModel.RequestId = dictionary["RequestId"];
                errorViewModel.RequestPath = dictionary["RequestPath"];
            }

            if (errorViewModel.RequestId == null)
            {
                errorViewModel.RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier;
            }

            return this.View(errorViewModel);
        }
    }
}

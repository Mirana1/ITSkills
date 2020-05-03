namespace ITSkills.Web.ViewModels.Administration.Categories
{
    using System;

    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class CreateCategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}

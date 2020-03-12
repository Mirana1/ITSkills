namespace ITSkills.Web.ViewModels.Home
{
    public class IndexCategoryViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Url => $"/Category/{this.Name.Replace(" ", "-")}";
    }
}

namespace ITSkills.Web.ViewModels.Lections
{
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class ListLectionsViewModel : IMapFrom<Lection>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}

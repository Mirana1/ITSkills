namespace ITSkills.Web.ViewModels.Home
{
    using AutoMapper;
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class MyCoursesViewModel : IMapFrom<MyCourse>, IHaveCustomMappings
    {
        public string UserId { get; set; }

        public int CourseId { get; set; }

        public decimal? Price { get; set; }

        public string Title { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MyCoursesViewModel, ApplicationUser>()
                .ForMember(x => x.Id, y => y.MapFrom(s => s.UserId));
            configuration.CreateMap<MyCoursesViewModel, Course>()
                .ForMember(x => x.Id, y => y.MapFrom(s => s.CourseId))
                .ForMember(x => x.Id, y => y.MapFrom(s => s.Title));
        }
    }
}

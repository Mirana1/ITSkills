namespace ITSkills.Web.ViewModels.MyCourses
{
    using AutoMapper;
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class PaymentCodeViewModel : IMapFrom<MyCourse>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string PaymentCode { get; set; }

        public int CourseId { get; set; }

        public string UserId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PaymentCodeViewModel, ApplicationUser>()
                .ForMember(u => u.Id, x => x.MapFrom(y => y.UserId));
            configuration.CreateMap<PaymentCodeViewModel, Course>()
                .ForMember(u => u.Id, x => x.MapFrom(y => y.CourseId));
        }
    }
}

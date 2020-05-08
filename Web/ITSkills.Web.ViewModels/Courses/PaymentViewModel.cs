namespace ITSkills.Web.ViewModels.Courses
{
    using AutoMapper;

    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class PaymentViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public string Title { get; set; }

        public decimal? Price { get; set; }

        public string PaymentCode { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PaymentViewModel, UserPaymentViewModel>()
                .ForMember(id => id.Id, user => user.MapFrom(u => u.UserId))
                .ForMember(un => un.Username, user => user.MapFrom(u => u.Username));
        }
    }
}

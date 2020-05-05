namespace ITSkills.Web.ViewModels.Administration.Courses
{
    using System;

    using AutoMapper;

    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;
    using ITSkills.Web.ViewModels.Courses;

    public class DeleteCourseViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public string Requirements { get; set; }

        public int CategoryId { get; set; }

        public string AcquiredKnowledge { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<DeleteCourseViewModel, UsersViewModel>().ForMember(id => id.Id, user => user.MapFrom(md => md.UserId));
            configuration.CreateMap<DeleteCourseViewModel, CoursesViewModel>().ForMember(id => id.Id, category => category.MapFrom(c => c.CategoryId));
        }
    }
}

﻿namespace ITSkills.Web.ViewModels.Administration.Lections
{
    using System;
    using AutoMapper;
    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;
    using ITSkills.Web.ViewModels.Administration.Courses;

    public class DetailsLectionViewModel : IMapFrom<Lection>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string UserId { get; set; }

        public int CourseId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<DetailsLectionViewModel, UsersViewModel>().ForMember(id => id.Id, user => user.MapFrom(u => u.UserId));
            configuration.CreateMap<DetailsLectionViewModel, CourseViewModel>().ForMember(id => id.Id, course => course.MapFrom(c => c.CourseId));
        }
    }
}

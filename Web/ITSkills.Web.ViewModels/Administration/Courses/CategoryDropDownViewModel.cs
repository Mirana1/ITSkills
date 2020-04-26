using ITSkills.Data.Models;
using ITSkills.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITSkills.Web.ViewModels.Administration.Courses
{
    public class CategoryDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

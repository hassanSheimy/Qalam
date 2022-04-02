using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class EducationYearViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubjectViewModel> Subjects { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }
}

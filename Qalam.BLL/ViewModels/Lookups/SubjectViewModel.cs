using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class SubjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LanguageViewModel Language { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }
}

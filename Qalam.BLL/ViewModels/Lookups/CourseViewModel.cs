using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string EducationYearName { get; set; }
        public string SubjectName { get; set; }
        public int EducationYearId { get; set; }
        public int SubjectId { get; set; }
        public List<LessonViewModel> Lessons { get; set; }
    }
}

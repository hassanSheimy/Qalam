using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class TeacherCourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
    }
}

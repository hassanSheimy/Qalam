using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class LessonVideoMinimalViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ShowDate { get; set; }
        public bool IsLive { get; set; }
        public bool IsFree { get; set; }
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
        public string Country { get; set; }
        public string EducationYear { get; set; }
    }
}

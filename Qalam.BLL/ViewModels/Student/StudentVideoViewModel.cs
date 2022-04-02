using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class StudentVideoViewModel
    {
        public int Id { get; set; }
        public DateTime ViewDate { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public int StudentId { get; set; }
        public int LessonVideoId { get; set; }
    }
}

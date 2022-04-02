using Qalam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class ExamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int TimeInMinutes { get; set; }
        public DateTime Date { get; set; }
        public EStartTime StartTime { get; set; }
        public int LessonVideoId { get; set; }
        public int SetterId { get; set; }
    }
}

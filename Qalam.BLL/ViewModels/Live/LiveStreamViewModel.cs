using Qalam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class LiveStreamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ShowDate { get; set; }
        public ELiveState? State { get; set; }
        public bool IsFree { get; set; }
        public string CountryName { get; set; }
        public string EducationYearName { get; set; }
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
        public int? MyProperty { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public int? TimetableId { get; set; }
    }
}

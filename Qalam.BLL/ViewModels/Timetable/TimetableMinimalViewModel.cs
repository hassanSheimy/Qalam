using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class TimetableMinimalViewModel
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string CountryName { get; set; }
        public string EducationTypeName { get; set; }
        public string EducationYearName { get; set; }
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
        public int TimetableId { get; set; }
    }
}

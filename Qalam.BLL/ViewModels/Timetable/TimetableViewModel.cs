using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class TimetableViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DayOfWeek Day { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int CourseId { get; set; }
        public bool IsInterval { get; set; }
        public CountryViewModel Country { get; set; }
        public EducationYearViewModel EducationYear { get; set; }
        public CourseViewModel Course { get; set; }
    }
}

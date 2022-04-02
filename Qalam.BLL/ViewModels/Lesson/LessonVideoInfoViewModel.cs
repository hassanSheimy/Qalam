using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class LessonVideoInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ShowDate { get; set; }
        public string EducationYearName { get; set; }
        public int Views { get; set; }
        public double Rating { get; set; }
    }
}

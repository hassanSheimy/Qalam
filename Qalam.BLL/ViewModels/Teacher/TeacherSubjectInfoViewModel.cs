using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class TeacherSubjectInfoViewModel
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int YearId { get; set; }
        public string YearName { get; set; }
    }
}

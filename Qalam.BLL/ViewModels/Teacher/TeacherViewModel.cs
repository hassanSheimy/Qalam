using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class TeacherViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int ProfileImageId { get; set; }
        public string CountryName { get; set; }
        public string SubjectName { get; set; }
        public List<string> EducationYears { get; set; }
        public string ImagePath { get; set; }
        public string About { get; set; }
        public string Facebook { get; set; }
    }
}

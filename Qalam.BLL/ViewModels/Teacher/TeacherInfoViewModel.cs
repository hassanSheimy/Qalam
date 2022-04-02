using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class TeacherInfoViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int ProfileImageId { get; set; }
        public string ImagePath { get; set; }
        public string SubjectName { get; set; }
        public string EducationYear { get; set; }
        public bool IsConfirmed { get; set; }
    }
}

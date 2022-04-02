using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class StudentExamsDegreeModel
    {
        public int Id { get; set; }
        public DateTime ExamDate { get; set; }
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
        public int Dgree { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
    }
}

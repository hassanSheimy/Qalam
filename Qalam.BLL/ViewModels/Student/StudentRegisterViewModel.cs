using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class StudentRegisterViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ParentPhoneNumber { get; set; }
        public int EducationYearId { get; set; }
        public string Referalcode { get; set; }
    }
}

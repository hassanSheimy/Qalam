using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class UserViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string ImagePath { get; set; }
        public string About { get; set; }
        public string Facebook { get; set; }
        public string UniqueKey { get; set; }
        public int? CountryId { get; set; }
        public int? EducationYearId { get; set; }
        public int? EducationTypeId { get; set; }
        public int? SubjectId { get; set; }
        public string Token { get; set; }
        public RoleViewModel Role { get; set; }
    }
}

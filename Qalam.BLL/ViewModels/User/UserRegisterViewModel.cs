using Newtonsoft.Json;
using Qalam.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class UserRegisterViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public EUserTypes Type { get; set; }
        public StudentRegisterViewModel Student { get; set; }
        public TeacherRegisterViewModel Teacher { get; set; }
        public int CountryId { get; set; }
        public RoleViewModel Role { get; set; }
    }
}

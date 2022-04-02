using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class FollowTeacherViewModel
    {
        public int Id { get; set; }
        public DateTime FollowingDate { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
    }
}

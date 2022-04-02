using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Qalam.MYSQL.Entities
{
    public class Teacher
    {
        public Teacher()
        {
            Courses = new HashSet<TeacherCourse>();
            Videos = new HashSet<LiveStream>();
            Exams = new HashSet<Exam>();
            Timetables = new HashSet<TeacherTimetable>();
            Followers = new HashSet<TeacherFollow>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string StreamKey { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public virtual ICollection<TeacherCourse> Courses { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<LiveStream> Videos { get; set; }
        public virtual ICollection<TeacherTimetable> Timetables { get; set; }
        public virtual ICollection<TeacherFollow> Followers { get; set; }
    }
}

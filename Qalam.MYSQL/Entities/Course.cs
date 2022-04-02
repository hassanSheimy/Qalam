using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Qalam.MYSQL.Entities
{
    public class Course
    {
        public Course()
        {
            Lessons = new HashSet<Lesson>();
            Timetable = new HashSet<Timetable>();
            Videos = new HashSet<LiveStream>();
            Teachers = new HashSet<TeacherCourse>();
            Exams = new HashSet<Exam>();
            ExcludedContents = new HashSet<ExcludedContent>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("EducationYear")]
        public int EducationYearId { get; set; }
        public virtual EducationYear EducationYear { get; set; }

        [Required]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<LiveStream> Videos { get; set; }
        public virtual ICollection<Timetable> Timetable { get; set; }
        public virtual ICollection<TeacherCourse> Teachers { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<ExcludedContent> ExcludedContents { get; set; }
    }
}

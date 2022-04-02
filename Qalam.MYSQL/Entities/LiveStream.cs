using Qalam.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Qalam.MYSQL.Entities
{
    public class LiveStream
    {
        public LiveStream()
        {
            Students = new HashSet<StudentVideo>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime ShowDate { get; set; }
        public ELiveState? State { get; set; }
        public bool IsFree { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        [Required]
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        [ForeignKey("Timetable")]
        public int? TimetableId { get; set; }
        public virtual TeacherTimetable Timetable { get; set; }
        
        public virtual Exam Exam { get; set; }

        public virtual ICollection<StudentVideo> Students { get; set; }
    }
}

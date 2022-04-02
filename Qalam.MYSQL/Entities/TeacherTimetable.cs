using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Qalam.MYSQL.Entities
{
    public class TeacherTimetable
    {
        public TeacherTimetable()
        {
            Videos = new HashSet<LiveStream>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public TimeSpan Start { get; set; }

        [Required]
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        [Required]
        [ForeignKey("Timetable")]
        public int TimetableId { get; set; }
        public virtual Timetable Timetable { get; set; }

        public virtual ICollection<LiveStream> Videos { get; set; }
    }
}

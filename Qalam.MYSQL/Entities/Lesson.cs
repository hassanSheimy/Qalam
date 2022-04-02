using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Qalam.MYSQL.Entities
{
    public class Lesson
    {
        public Lesson()
        {
            Questions = new HashSet<Question>();
            ExcludedContents = new HashSet<ExcludedContent>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int SemesterNumber { get; set; }
        [Required]
        public int UnitNumber { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<ExcludedContent> ExcludedContents { get; set; }
    }
}

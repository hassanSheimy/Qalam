using Qalam.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Qalam.MYSQL.Entities
{
    public class ExcludedContent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public EContentType Type { get; set; }

        [ForeignKey("Lesson")]
        public int? LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        [ForeignKey("Course")]
        public int? CourseId { get; set; }
        public virtual Course Course { get; set; }

        [Required]
        [ForeignKey("EducationType")]
        public int EducationTypeId { get; set; }
        public virtual EducationType EducationType { get; set; }
    }
}

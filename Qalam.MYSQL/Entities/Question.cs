using Qalam.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Qalam.MYSQL.Entities
{
    public class Question
    {
        public Question()
        {
            Exams = new HashSet<ExamQuestion>();
            Answers = new HashSet<QuestionAnswer>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public EQuestionTypes Type { get; set; }
        [Required]
        public EDifficultyLevel DifficultyLevel { get; set; }
        public bool IsSystem { get; set; }

        [Required]
        [ForeignKey("Lesson")]
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        [Required]
        [ForeignKey("Setter")]
        public int SetterId { get; set; }
        public virtual User Setter { get; set; }

        public virtual ICollection<ExamQuestion> Exams { get; set; }
        public virtual ICollection<QuestionAnswer> Answers { get; set; }
    }
}

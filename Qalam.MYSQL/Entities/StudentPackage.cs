using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Qalam.MYSQL.Entities
{
    public class StudentPackage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime SubscribeDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        [Required]
        public int VideoCount { get; set; }
        [Required]
        public int ExamCount { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        [Required]
        [ForeignKey("PackageItem")]
        public int PackageItemId { get; set; }
        public virtual PackageItem PackageItem { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Qalam.MYSQL.Entities
{
    public class Student
    {
        public Student()
        {
            InverseReferredBy = new HashSet<Student>();
            Packages = new HashSet<StudentPackage>();
            FollowingTeachers = new HashSet<TeacherFollow>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public int Points { get; set; }
        [Required]
        public string ParentPhoneNumber { get; set; }
        [Required]
        public string Referalcode { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("ReferalByStudent")]
        public int? ReferalById { get; set; }
        public virtual Student ReferalByStudent { get; set; }

        [Required]
        [ForeignKey("EducationType")]
        public int EducationTypeId { get; set; }
        public virtual EducationType EducationType { get; set; }
        
        [Required]
        [ForeignKey("EducationYear")]
        public int EducationYearId { get; set; }
        public virtual EducationYear EducationYear { get; set; }

        public virtual ICollection<Student> InverseReferredBy { get; set; }
        public virtual ICollection<StudentPackage> Packages { get; set; }        
        public virtual ICollection<StudentExam> Exams { get; set; }
        public virtual ICollection<StudentVideo> Videos { get; set; }
        public virtual ICollection<TeacherFollow> FollowingTeachers { get; set; }
    }
}

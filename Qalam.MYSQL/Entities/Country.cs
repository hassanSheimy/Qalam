using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Qalam.MYSQL.Entities
{
    public class Country
    {
        public Country()
        {
            EducationTypes = new HashSet<EducationType>();
            EducationYears = new HashSet<EducationYear>();
            Subjects = new HashSet<Subject>();
            Users = new HashSet<User>();
            PackagePrices = new HashSet<PackagePrice>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Currency { get; set; }
        public virtual ICollection<EducationType> EducationTypes { get; set; }
        public virtual ICollection<EducationYear> EducationYears { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<PackagePrice> PackagePrices { get; set; }
    }
}

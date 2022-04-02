using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Qalam.MYSQL.Entities
{
    public class Package
    {
        public Package()
        {
            Items = new HashSet<PackageItem>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Color { get; set; }
        public int? PeriodInDays { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        [Required]
        [ForeignKey("Image")]
        public int ImageId { get; set; }
        public virtual DataFile Image { get; set; }

        public virtual ICollection<PackageItem> Items { get; set; }
    }
}

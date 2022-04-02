using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Qalam.MYSQL.Entities
{
    public class PackageItem
    {
        public PackageItem()
        {
            Students = new HashSet<StudentPackage>();
            Offers = new HashSet<PackageOffer>();
            Prices = new HashSet<PackagePrice>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasOffer { get; set; }
        [Required]
        public int NumberOfExams { get; set; }
        [Required]
        public int NumberOfVideos { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }

        [Required]
        [ForeignKey("Package")]
        public int PackageId { get; set; }
        public virtual Package Package { get; set; }

        public virtual ICollection<StudentPackage> Students { get; set; }
        public virtual ICollection<PackageOffer> Offers { get; set; }
        public virtual ICollection<PackagePrice> Prices { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Qalam.MYSQL.Entities
{
    public class PackageOffer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        [Required]
        public int NumberOfExams { get; set; }
        [Required]
        public int NumberOfVideos { get; set; }
        [Required]
        public int PriceInDollar { get; set; }
        public int? PriceInPoint { get; set; }
        public DateTime ExpiryDate { get; set; }

        [Required]
        [ForeignKey("PackageItem")]
        public int PackageItemId { get; set; }
        public virtual PackageItem PackageItem { get; set; }
    }
}
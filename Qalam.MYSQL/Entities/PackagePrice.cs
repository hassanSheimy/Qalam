using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Qalam.MYSQL.Entities
{
    public class PackagePrice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public double PriceInMoney { get; set; }
        public double? PriceInPoint { get; set; }

        [Required]
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        [Required]
        [ForeignKey("PackageItem")]
        public int PackageItemId { get; set; }
        public PackageItem PackageItem { get; set; }
    }
}

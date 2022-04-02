using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class PackageItemViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasOffer { get; set; }
        public int NumberOfExams { get; set; }
        public int NumberOfVideos { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public double PriceInMoney { get; set; }
        public double? PriceInPoint { get; set; }
        public PackageOfferViewModel Offer { get; set; }
    }
}

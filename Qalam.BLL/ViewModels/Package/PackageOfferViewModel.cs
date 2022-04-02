using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class PackageOfferViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public int NumberOfExams { get; set; }
        public int NumberOfVideos { get; set; }
        public int PriceInMoney { get; set; }
        public int? PriceInPoint { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class PackageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int? PeriodInDays { get; set; }
        public int ImageId { get; set; }
        public string ImagePath { get; set; }
        public List<PackageItemViewModel> Items { get; set; }
    }
}

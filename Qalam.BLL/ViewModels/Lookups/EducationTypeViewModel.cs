using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class EducationTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EducationYearViewModel> EducationYears { get; set; }
        public virtual List<ExcludedContentViewModel> ExcludedContents { get; set; }
    }
}

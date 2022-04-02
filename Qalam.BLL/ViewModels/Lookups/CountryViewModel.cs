using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class CountryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public int EducationTypesCount { get; set; }
        public int EducationYearsCount { get; set; }
        public int SubjectsCount { get; set; }
        public List<EducationTypeViewModel> EducationTypes { get; set; }
        public List<EducationYearViewModel> EducationYears { get; set; }
        public List<SubjectViewModel> Subjects { get; set; }
    }
}

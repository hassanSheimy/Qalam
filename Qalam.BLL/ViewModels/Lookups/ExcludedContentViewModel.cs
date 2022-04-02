using Qalam.Common.Enums;

namespace Qalam.BLL.ViewModels
{
    public class ExcludedContentViewModel
    {
        public int Id { get; set; }
        public EContentType Type { get; set; }

        public int? LessonId { get; set; }
        public int? CourseId { get; set; }
        public int EducationTypeId { get; set; }
    }
}
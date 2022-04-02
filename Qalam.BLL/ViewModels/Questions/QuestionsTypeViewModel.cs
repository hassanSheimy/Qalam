using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class QuestionsTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<QuestionExamViewModel> Questions { get; set; }
    }
}

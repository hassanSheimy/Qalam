using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class QuestionExamViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<QuestionAnswerExamViewModel> Answers { get; set; }
    }
}

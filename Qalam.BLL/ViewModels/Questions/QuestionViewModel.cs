using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int DifficultyLevel { get; set; }
        public List<QuestionAnswerViewModel> Answers { get; set; }
    }
}

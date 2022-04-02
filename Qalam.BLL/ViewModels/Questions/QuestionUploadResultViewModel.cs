using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class QuestionUploadResultViewModel
    {
        public int NumberOfSuccessRows { get; set; }
        public int NumberOfFaildRows { get; set; }
        public List<int> FaildRows { get; set; }
    }
}

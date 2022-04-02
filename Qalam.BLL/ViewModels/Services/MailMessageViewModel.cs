﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qalam.BLL.ViewModels
{
    public class MailMessageViewModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public bool IsBodyHtml { get; set; }
        public string Body { get; set; }
        public List<string> ReplyToList { get; set; }
        public List<string> Attachments { get; set; }
    }
}

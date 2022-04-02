using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.ViewModels
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsRead { get; set; }
        public DateTime ReadTime { get; set; }
        public string Url { get; set; }
        public string Message { get; set; }

        public int UserId { get; set; }

        public int SenderId { get; set; }
        public NotificationSenderViewModel Sender { get; set; }

    }
}

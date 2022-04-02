using Qalam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.Common.Helper
{
    public class RequestAttributes
    {
        public RequestAttributes()
        {
            ResetValues();
        }

        public int UserId { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public string UserEmail { get; set; }
        public EUserTypes UserRole { get; set; }
        public string Language { get; set; }
        public static string AppBaseUrl { get; set; }
        public int CountryId { get; set; }

        public void ResetValues()
        {
            UserId = TeacherId = StudentId = -1;
            UserEmail = null;
            CountryId = 0;
        }

        public void CopyFrom(RequestAttributes requestAttribute)
        {
            UserId = requestAttribute.UserId;
            TeacherId = requestAttribute.TeacherId;
            StudentId = requestAttribute.StudentId;
            UserEmail = requestAttribute.UserEmail;
            UserRole = requestAttribute.UserRole;
        }
    }
}

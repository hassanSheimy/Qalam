using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Qalam.Common.Enums
{
    public enum EStatusCode : Int32
    {
        [Description("Process done successfully")]
        ProcessSuccess = 1,
        [Description("Process Faild")]
        ProcessFailed = 2,
        [Description("Unauthorized")]
        Unauthorized = 3,
        [Description("Forbidden")]
        Forbidden = 4,
        [Description("Not Found")]
        NotFound = 5,
        [Description("Internal server error")]
        InternalServerError = 6,
        [Description("Missed data")]
        MissedData = 7,
        [Description("Invalid data")]
        InvalidData = 8,
        [Description("Repeated data")]
        RepeatedData = 9,
        [Description("Database case an error")]
        DatabaseError = 10,
        [Description("Faild to generate token")]
        GenerateTokenFaild = 11,
        [Description("User not exist or not active")]
        UserNotExist = 12,
        [Description("This data is duplicated")]
        DuplicateData = 13,
        [Description("Email already exists")]
        EmailExists = 15,
        [Description("Stream key isn't valid or live doesn't started yet")]
        InValidStreamKey = 17,
        [Description("Email or password is wrong")]
        EmailOrPasswordWrong = 18,
        [Description("Email isn't confirmed")]
        NotConfirmedUser = 19,
        [Description("User isn't active")]
        InActiveUser = 20,
        [Description("The confirmation email has been sent")]
        ConfirmationEmailSent = 21,
        [Description("This user already confirmed")]
        AlreadyConfirmed = 22,
        [Description("This token has been expired")]
        TokenExpired = 23,
        [Description("The reset email has been sent")]
        ResetPasswordEmailSent = 24,
        [Description("This password is wrong")]
        WrongPassword = 25,
        [Description("This token is invalid")]
        InvalidToken = 26
    }
}

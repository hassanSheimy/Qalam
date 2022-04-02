using Qalam.Common.Enums;
using Qalam.Common.Extensions;
using System.Collections.Generic;
using System.Net;

namespace Qalam.Common.Helper
{
    public class Result<T>
    {
        public Result(T Data, bool IsSucceeded, EStatusCode StatusCode, string Message)
        {
            if (Message == null)
                Message = StatusCode.Description();

            this.Data = Data;
            this.IsSucceeded = IsSucceeded;
            this.StatusCode = StatusCode;
            this.Message = Message;
        }

        public bool IsSucceeded { get; private set; }
        public EStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; private set; }
    }

    public class PageResult<T>
    {
        public int Count { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public List<T> PageData { get; set; }
    }

    public static class ResultHelper
    {
        public static Result<TData> Succeeded<TData>(TData data = default,
            string message = "Process done successfuly",
            EStatusCode statusCode = EStatusCode.ProcessSuccess)
        {
            return new Result<TData>(data, true, statusCode, message);
        }

        public static Result<TData> Failed<TData>(TData data = default,
            string message = "Process Failed!",
            EStatusCode statusCode = EStatusCode.InternalServerError)
        {
            var tempStatus = message.ToEnum<EStatusCode>();
            if (tempStatus != default)
                statusCode = tempStatus;
            return new Result<TData>(data, false, statusCode, message);
        }
    }
}

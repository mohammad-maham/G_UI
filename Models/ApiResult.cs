using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_APIs.Models
{

    public class ApiResult
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }

        public ApiResult()
        {
        }

        public ApiResult(int resultCode, string message = "", dynamic data = null)
        {
            this.StatusCode = resultCode;
            this.Message = message;
            this.Data = data;
        }
    }
}
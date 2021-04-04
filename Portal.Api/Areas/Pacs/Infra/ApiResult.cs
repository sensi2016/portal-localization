using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace His.Reception.Api
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public ApiResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static implicit operator ApiResult(OkResult result)
        {
            return new ApiResult(true, " Success ");
        }

        public static implicit operator ApiResult(BadRequestResult result)
        {
            return new ApiResult(false, "parameter Not Valid .");
        }

    }

    public class ApiResult<TData> : ApiResult 
    {
        public TData Data { get; set; }

        public ApiResult(bool isSuccess, string message, TData data)
            : base(isSuccess, message)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }


        public static implicit operator ApiResult<TData>(TData data)
        {
            return new ApiResult<TData>(true, " Success ", data);
        }


        public static implicit operator ApiResult<TData>(OkResult result)
        {
            return new ApiResult<TData>(true, " Success ", default);
        }

        public static implicit operator ApiResult<TData>(BadRequestResult result)
        {
            return new ApiResult<TData>(false, "parameter Not Valid .", default);
        }
    }
}

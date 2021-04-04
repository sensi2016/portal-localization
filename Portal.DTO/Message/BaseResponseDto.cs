using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public interface IBaseResponse
    {
         ResponseStatus Status { get; set; }
         string Message { get; set; }
         string Errors { get; set; }
         object Data { get; set; }
    }
    public class BaseResponseDto: IBaseResponse
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public string Errors { get; set; }
        public object Data { get; set; }
        public static BaseResponseDto Success(string message)
        {
            return new BaseResponseDto
            {
                Message = message,
                Status = ResponseStatus.Success
            };
        }

        public static BaseResponseDto NoTValid(string message)
        {
            return new BaseResponseDto
            {
                Message = message,
                Status = ResponseStatus.NotValid
            };
        }

        public static BaseResponseDto Unknow(string message) {
            return new BaseResponseDto
            {
                Message = message,
                Status = ResponseStatus.Unknow
            };
        }

        public static BaseResponseDto Fail(string message)
        {
            return new BaseResponseDto
            {
                Message = message,
                Status = ResponseStatus.Fail
            };
        }
    }

    public enum ResponseStatus
    {
        Unknow,
        Fail,
        Success,
        NotValid,
        NotFound
    }
}


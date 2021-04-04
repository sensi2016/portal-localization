using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Portal.DAL
{
    public class HttpStatusCodeException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsDeleted { get; set; }


        public HttpStatusCodeException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCodeException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

    }

    public class CanNotDeleteException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }


        public CanNotDeleteException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public CanNotDeleteException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

    }

    public class UnauthorizedException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ResourceKey { get; set; }


        public UnauthorizedException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public UnauthorizedException(HttpStatusCode statusCode, string message, string key) : base(message)
        {
            StatusCode = statusCode;
            ResourceKey = key;
        }

    }
}
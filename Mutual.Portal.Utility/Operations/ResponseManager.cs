using Mutual.Portal.Utility.Models;
using System;
using System.Collections.Generic;

namespace Mutual.Portal.Utility.Operations
{
    public class ResponseManager
    {
        public static ResponseObject GetExceptionResponse(string message, Exception exception, string errorCode)
        {
            return new ResponseObject()
            {
                MetaData = new MetaData()
                {
                    IsSucceeded = false,
                    HttpResponseCode=500,
                    Message = message,
                    Exception = exception,
                    ErrorCode = errorCode
                }
            };
        }

        public static ResponseObject GetSuccessResponse(object successObj, string message, int httpResponseCode)
        {
            return new ResponseObject()
            {
                MetaData = new MetaData()
                {
                    IsSucceeded = true,
                    HttpResponseCode = httpResponseCode,
                    Message = message
                },

                Data = successObj
            };
        }

        public static ResponseObject GetLogicalErrorResponse(string message, string errorCode, int httpResponseCode)
        {
            return new ResponseObject()
            {
                MetaData = new MetaData()
                {
                    IsSucceeded = false,
                    HttpResponseCode = httpResponseCode,
                    Message = message,
                    ErrorCode = errorCode
                }
            };
        }
    }
}

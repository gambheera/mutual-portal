using Mutual.Portal.Utility.Enums;
using Mutual.Portal.Utility.Models;
using System;
using System.Collections.Generic;

namespace Mutual.Portal.Utility.Operations
{
    public class ResponseManager
    {
        public static ResponseObject GetExceptionResponse(string message, Exception exception, string errorCode, ResponseType errorResponse)
        {
            return new ResponseObject()
            {
                MetaData = new MetaData()
                {
                    IsSucceeded = false,
                    HttpResponse=errorResponse,
                    Message = message,
                    Exception = exception,
                    ErrorCode = errorCode
                }
            };
        }

        public static ResponseObject GetSuccessResponse(object successObj, string message, ResponseType httpResponseCode)
        {
            return new ResponseObject()
            {
                MetaData = new MetaData()
                {
                    IsSucceeded = true,
                    HttpResponse = httpResponseCode,
                    Message = message
                },

                Data = successObj
            };
        }

        public static ResponseObject GetLogicalErrorResponse(string message, string errorCode, ResponseType httpResponseCode)
        {
            return new ResponseObject()
            {
                MetaData = new MetaData()
                {
                    IsSucceeded = false,
                    HttpResponse = httpResponseCode,
                    Message = message,
                    ErrorCode = errorCode
                }
            };
        }
    }
}

using Mutual.Portal.Utility.Enums;
using System;

namespace Mutual.Portal.Utility.Models
{
    public class ResponseObject
    {
        public object Data { get; set; }
        public MetaData MetaData { get; set; }
        // public T SuccessObject { get; set; } // the object if IsSucceeded==true
        // public List<T> SuccessobjectList { get; set; } // the object list if IsSucceeded==true

    }

    public class MetaData
    {
        public bool IsSucceeded { get; set; } // if operation valid to be processed
        public ResponseType HttpResponse { get; set; }
        public string Message { get; set; } // The message to pass client side
        public string ErrorCode { get; set; } // The error code to give client side
        public Exception Exception { get; set; } // the exception object if exception ocured
    }
}

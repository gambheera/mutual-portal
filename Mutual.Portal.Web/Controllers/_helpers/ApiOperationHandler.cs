
using Mutual.Portal.Utility.Enums;
using Mutual.Portal.Utility.Models;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace Mutual.Portal.Web.Controllers._helpers
{
    public class ApiOperationHandler : ApiController
    {
        private static ApiOperationHandler _singletonObj;

        private ApiOperationHandler()
        { }

        public static ApiOperationHandler GetSingltonInstance()
        {
            if (_singletonObj == null)
            {
                _singletonObj = new ApiOperationHandler();
                return _singletonObj;
            }

            return _singletonObj;
        }

        public IHttpActionResult GetHttpClientResponse(ResponseObject responseObject)
        {
            if (responseObject.MetaData.HttpResponse == ResponseType.InternalServerError)
            {
                // Then this is an exception. return 500
                var exp = responseObject.MetaData.Exception;
                exp.Source = responseObject.MetaData.Message + "\nError Code: " + responseObject.MetaData.ErrorCode;
                return InternalServerError(exp);
            }

            if (responseObject.MetaData.HttpResponse == ResponseType.Created)
            {
                return Created("", responseObject);   
            }
            
            return Ok(responseObject);
        }

        public IHttpActionResult CheckAuthority()
        {
            return Unauthorized();
        }

        public string GetUserEmail()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userEmail = identity.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault();
            return userEmail;
        }
    }
}
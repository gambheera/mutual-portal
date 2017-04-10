
using Mutual.Portal.Utility.Enums;
using Mutual.Portal.Utility.Models;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

using System;
using System.Collections.Generic;

using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http.Controllers;

namespace Mutual.Portal.Web.Controllers._helpers
{
    public class ApiOperationHandler : ApiController
    {
        private static ApiOperationHandler _singletonObj;

        private ApiOperationHandler(HttpConfiguration config)
        {
            Configuration = config;
        }

        public static ApiOperationHandler GetSingltonInstance(HttpConfiguration config)
        {
            
            if (_singletonObj == null)
            {
                _singletonObj = new ApiOperationHandler(config);
                return _singletonObj;
            }

            return _singletonObj;
        }

        public IHttpActionResult GetHttpClientResponse(ResponseObject responseObject, HttpConfiguration config, HttpRequestMessage req)
        {
            try
            {

            
            if (responseObject.MetaData.HttpResponse == ResponseType.InternalServerError)
            {
                //Configuration = config;
                // Then this is an exception. return 500
                    var r = HttpContext.Current.Request;
                var exp = responseObject.MetaData.Exception;
                var message = responseObject.MetaData.Message + "\nError Code: " + responseObject.MetaData.ErrorCode;

                if (exp == null) exp = new System.Exception(message);
                
                exp.Source = message;
                var err = InternalServerError(exp);

                return err;
            }

            if (responseObject.MetaData.HttpResponse == ResponseType.Created)
            {
                return Created("", responseObject);   
            }
            
            return Ok(responseObject);
            }
            catch (Exception ex)
            {
                return null;
            }
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
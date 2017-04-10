using Mutual.Portal.Service.BusinessLogic.UserManagement;
using Mutual.Portal.Utility.Enums;
using Mutual.Portal.Utility.Models;
using Mutual.Portal.Web.Controllers._helpers;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace Mutual.Portal.Web.Controllers.BusinessApi
{
    [Authorize]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager usermanager)
        {
            _userManager = usermanager;
        }

        [Authorize]
        [Route("get-user-status-by-guid")]
        public IHttpActionResult GetUserStatusByGuid(string userGuid)
        {
            var obj = _userManager.GetUserStatusByGuid("");
            return _getHttpClientResponse(obj);
        }

        [Authorize]
        [Route("get-user-info")]
        public IHttpActionResult GetUserInfo()
        {
            var guid = _getUserGuid();
            var userObj = _userManager.GetUserInfo(guid);
            return _getHttpClientResponse(userObj);
        }

        // [Authorize]
        [Route("get-user-employee-type")]
        public IHttpActionResult GetUserEmployeeType()
        {
            string guid = _getUserGuid();
            var obj = _userManager.GetUserEmployeeType(guid);
            return _getHttpClientResponse(obj);
        }

        #region Helpers

        private IHttpActionResult _getHttpClientResponse(ResponseObject responseObject)
        {
            if (responseObject.MetaData.HttpResponse == ResponseType.InternalServerError)
            {
                // Then this is an exception. return 500
                var exp = responseObject.MetaData.Exception;
                var message = responseObject.MetaData.Message + "\nError Code: " + responseObject.MetaData.ErrorCode;

                if (exp == null) exp = new Exception(message);

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

        public string _getUserGuid()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userEmail = identity.Claims.Where(c => c.Type == ClaimTypes.SerialNumber).Select(c => c.Value).SingleOrDefault();
            return userEmail;
        }

        #endregion
    }
}

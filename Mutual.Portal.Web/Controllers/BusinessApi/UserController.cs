using Mutual.Portal.Service.BusinessLogic.UserManagement;
using Mutual.Portal.Web.Controllers._helpers;
using System.Web.Http;

namespace Mutual.Portal.Web.Controllers.BusinessApi
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private IUserManager _userManager;
        private readonly ApiOperationHandler _apiOperationHandler;

        public UserController(IUserManager usermanager)
        {
            _userManager = usermanager;
            _apiOperationHandler = ApiOperationHandler.GetSingltonInstance();
        }

        [Authorize]
        [Route("get-user-status-by-guid")]
        public IHttpActionResult GetUserStatusByGuid(string userGuid)
        {
            var obj = _userManager.GetUserStatusByGuid("");
            return _apiOperationHandler.GetHttpClientResponse(obj);
        }

        [Authorize]
        [Route("get-user-info")]
        public IHttpActionResult GetUserInfo()
        {
            var email = _apiOperationHandler.GetUserEmail();
            return Ok(email);
        }
    }
}

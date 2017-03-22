using Mutual.Portal.Service.BusinessLogic.UserManagement;
using System.Web.Http;

namespace Mutual.Portal.Web.Controllers.BusinessApi
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private IUserManager _userManager;

        public UserController(IUserManager usermanager)
        {
            _userManager = usermanager;
        }

        //ResponseObject GetUserStatusByGuid(string userGuid);

        [Route("get-user-status-by-guid")]
        public IHttpActionResult GetUserStatusByGuid(string userGuid)
        {
            var obj = _userManager.GetUserStatusByGuid("");
            return Ok(obj);
        }
    }
}

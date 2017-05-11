using Mutual.Portal.Core.Persistence;
using Mutual.Portal.Service.BusinessLogic.NurseManagement;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace Mutual.Portal.Web.Controllers.ViewControlers
{
    public class ViewProfileController : Controller
    {

        // GET: ViewProfile
        public ActionResult Index()
        {
            return View();
        }

    }
}
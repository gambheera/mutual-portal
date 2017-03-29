using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mutual.Portal.Web.Controllers.ViewControlers
{
    public class UserDashboardController : Controller
    {
        // GET: UserDashboard
        //[Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
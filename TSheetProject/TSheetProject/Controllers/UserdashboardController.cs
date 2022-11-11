using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TSheetProject.Controllers
{
    public class UserdashboardController : Controller
    {
        // GET: Userdashboard
        public ActionResult UserDashboard()
        {
            return View();
        }

        public ActionResult AllTimeSheet()
        {
            return View();
        }
    }
}
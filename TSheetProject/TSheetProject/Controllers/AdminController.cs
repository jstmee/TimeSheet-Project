using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TSheetProject.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public AdminController()
        {

        }
        // GET: Admin
        public ActionResult DashBoard()
        {
            return View();
        }

        public ActionResult CreateUser()
        {

            return View();
        }


        public ActionResult AllTimeSheet()
        {
            return View();
        }

       public ActionResult ProjectList()
       {
            return View();
       }
       

    }
}
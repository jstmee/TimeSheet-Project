using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSheet.Data;

namespace TSheetProject.Controllers
{
    public class UserController : Controller
    {
        public UserController()
        {

        }
        // GET: User
        public ActionResult UserTimeSheet()
        {
            
            TSheetDB dB= new TSheetDB();
            ViewBag.ProjectData = dB.ProjectMasters.ToList();
            
            return View();
        }



        public ActionResult UserLogin()
        {
            return View();
        }

        public ActionResult AllTimeSheet()
        {
            return View();
        }

        public ActionResult DashBoard()
        {
            
            // we will fetch 
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

    }
}
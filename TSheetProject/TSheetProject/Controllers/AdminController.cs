using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TSheetProject.Controllers
{
    public class AdminController : Controller
    {
        public AdminController()
        {

        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Admin() {
            return View();
                }
    }
}
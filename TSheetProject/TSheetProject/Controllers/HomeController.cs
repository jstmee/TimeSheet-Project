using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSheet.BL;
using TSheet.Data;
using TSheet.Modals;

namespace TSheetMangement.Controllers
{
    public class HomeController : Controller
    {
        RegistrationRepository _RegistrationRepository;
        
        public HomeController()
        {
            _RegistrationRepository = new RegistrationRepository();
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
       
        

    }
}
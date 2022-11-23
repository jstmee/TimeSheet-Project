using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSheet.BL;
using TSheet.Data;
using TSheet.Models;

namespace TSheetProject.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private RegistrationRepository _registrationRepository;
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
        [HttpPost]
        public ActionResult CreateUser(RegistrationModel user)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                _registrationRepository.AddRegistration(user);
                message = "User Added successfully";
            }
            else
            {
                message = "Invalid Request";
                return View(user);
            }
            ViewBag.Message = message;
            return View();


        }

        public ActionResult AllTimeSheet()
        {
            TSheetDB dB= new TSheetDB();
            var alltsheetdata= dB.TimeSheetDetails.ToList();

            return View(alltsheetdata);
        }


       public ActionResult ProjectList()
       {
            TSheetDB dB= new TSheetDB();
            var projectlist = dB.ProjectMasters.ToList();
            return View(projectlist);
       }

        public ActionResult ApproveTimesheet()
        {
            return View();
        }
        public ActionResult RejectTimeSheet()
        {
            return View();
        }

    }
}
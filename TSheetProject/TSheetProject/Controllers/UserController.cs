﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TSheet.Data;
using TSheet.Modals;

namespace TSheetProject.Controllers
{
    [Authorize]
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
        [HttpPost]
        public ActionResult ChangePassword(ChangeUserPassword userPassword)
        {
            string message = "";
            using (TSheetDB dB = new TSheetDB())
            {

                var v = dB.Registrations.Where(x => x.Email == userPassword.Email).FirstOrDefault();
                v.Password= userPassword.Password;
                dB.SaveChanges();
                message = "Password Changed successfully";
            }
            ViewBag.Message = message;
            return View();
        }

    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSheet.Data;
using TSheet.Modals;

namespace TSheetMangement.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            using (TSheetDB db = new TSheetDB())
            {
                var b = db.Registrations.Where(a => a.Email == login.Email).FirstOrDefault();
                if (b != null)
                {
                    if (b.Password == login.Password)
                    {
                        var c = b.UserID;
                         var d= db.AssignedRoles.Where(a => a.UserID == c).FirstOrDefault();
                        var HisRoleId = d.RoleID;
                        var RoleRow=db.Roles.Where(a => a.RoleID == HisRoleId).FirstOrDefault();
                        if (RoleRow.RoleName == "Admin")
                        {
                            return RedirectToAction("Admin", "Admin");
                        }

                    }
                }

               
            }
                return View();
        }
        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult forgetPassword()
        {
            return View();
        }
        

    }
}
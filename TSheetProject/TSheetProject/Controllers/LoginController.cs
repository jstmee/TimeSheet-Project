using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TSheet.Data;
using TSheet.Modals;

namespace TSheetProject.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel login)
        {
            using (TSheetDB db = new TSheetDB())
            {
                if (ModelState.IsValid)
                {
                    bool isValidUser=db.Registrations.Any(u=>u.Email== login.Email && u.Password==login.Password);
                    if (isValidUser)
                    {
                        FormsAuthentication.SetAuthCookie(login.Email, false);
                        var b = db.Registrations.Where(a => a.Email == login.Email).FirstOrDefault();
                        if (b != null)
                        {
                            if (b.Password == login.Password)
                            {
                                var c = b.UserID;
                                var d = db.AssignedRoles.Where(a => a.UserID == c).FirstOrDefault();
                                var HisRoleId = d.RoleID;
                                var RoleRow = db.Roles.Where(a => a.RoleID == HisRoleId).FirstOrDefault();
                                if (RoleRow.RoleName == "Admin")
                                {
                                    return RedirectToAction("DashBoard", "Admin");
                                }
                                else if (RoleRow.RoleName == "SuperAdmin")
                                {
                                    return RedirectToAction("DashBoard", "SuperAdmin");
                                }
                                else if (RoleRow.RoleName == "User")
                                {
                                    return RedirectToAction("Dashboard", "User");
                                }

                            }
                        }

                    }
                    ModelState.AddModelError("", "invalid Username or Password");
                    return View();
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login");
        }
    }
}
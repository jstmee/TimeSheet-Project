using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        [httpPost]
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
                        var d = db.AssignedRoles.Where(a => a.UserID == c).FirstOrDefault();
                        var HisRoleId = d.RoleID;
                        var RoleRow = db.Roles.Where(a => a.RoleID == HisRoleId).FirstOrDefault();
                        if (RoleRow.RoleName == "Admin")
                        {
                            return RedirectToAction("SuperAdmin", "SuperAdmin");
                        }
                        else if (RoleRow.RoleName == "SuperAdmin")
                        {
                            return RedirectToAction("SuperAdmin", "SuperAdmin");
                        }
                        else if (RoleRow.RoleName == "User")
                        {
                            return RedirectToAction("User", "UserDashboard");
                        }

                    }
                }


            }
            return View();
        }
    }
}
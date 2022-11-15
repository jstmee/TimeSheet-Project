using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSheet.Data;
using TSheet.Modals;

namespace TSheetProject.Controllers
{
    public class SuperAdminController : Controller
    {
        // GET: SuperAdmin
        public ActionResult SuperAdmin()
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
            if (ModelState.IsValid)
            {
                using (TSheetDB db = new TSheetDB())
                {

                    Registration registration = new Registration();
                    registration.FirstName = user.FirstName;
                    registration.LastName = user.LastName;
                    registration.Email = user.Email;
                    registration.EditedBy = user.EditedBy;
                    registration.CreatedBy = user.CreatedBy;
                    registration.DateOfJoining = user.DateOfJoining;
                    registration.DateOfLeaving = user.DateOfLeaving;
                    registration.DateOfbirth = user.DateOfBirth;
                    registration.CreatedOn = user.CreatedOn;
                    registration.Gender = user.Gender;
                    registration.IsActive = user.IsActive;
                    registration.Password = user.Password;
                    registration.MobileNumber = user.MobileNumber;
                    registration.UpdatedOn = user.UpdatedOn;

                    db.Registrations.Add(registration);
                    db.SaveChanges();

                }
            }
            else
            {
                return View();
            }


            return View(user);
        }
        public ActionResult AssignRoles()
        {
            TSheetDB db = new TSheetDB();

            var alluser = db.Registrations.ToList();
            return View(alluser);
           
        }
        public ActionResult edit(RegistrationModel res)
        {
            return View(res);
        }

        /*[HttpPost]
        public ActionResult AssignRoles(AssignedRolesModel assigned)
        {
            TSheetDB db = new TSheetDB();

            var alluser = db.Registrations.ToList();
            return View(alluser);
        }
*/
    }
}
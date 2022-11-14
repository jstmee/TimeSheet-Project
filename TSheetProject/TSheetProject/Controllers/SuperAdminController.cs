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



            return View(user);
        }
    }
}
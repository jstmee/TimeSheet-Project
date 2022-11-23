using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TSheet.BL;
using TSheet.Data;
using TSheet.Models;

namespace TSheetProject.Controllers
{
    
    public class SuperAdminController : Controller
    {
        private RegistrationRepository _registrationRepository;
        public SuperAdminController()
        {
            _registrationRepository = new RegistrationRepository();

        }
        // GET: SuperAdmin
        public ActionResult DashBoard()
        {
            using (TSheetDB dB = new TSheetDB())
            {
                ViewBag.NoOfUsers = dB.Registrations.Count();
                ViewBag.NoOfProjects= dB.ProjectMasters.Count();
            }
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
        public ActionResult AssignRoles()
        {
            TSheetDB db = new TSheetDB();

            var alluser = db.Registrations.ToList();
            return View(alluser);
           
        }
        public ActionResult EditUser(RegistrationModel user)
        {
            if (user != null)
            {
                return View(user);
            }
            return View();
        }
        [HttpPost]
        public ActionResult edit1(RegistrationModel user)
        {
            /*_registrationRepository.EditUser(user);*/


            return RedirectToAction("edit","SuperAdmin");
        }

        /*[HttpPost]
        public ActionResult AssignRoles(AssignedRolesModel assigned)
        {
            TSheetDB db = new TSheetDB();

            var alluser = db.Registrations.ToList();
            return View(alluser);
        }
*/
        public ActionResult AddProjects()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProjects(ProjectsModel projects)
        {

            using(TSheetDB dB= new TSheetDB())
            {
                ProjectMaster projectMaster = new ProjectMaster();
                projectMaster.ProjectName = projects.ProjectName;
                projectMaster.ProjectDescription = projects.ProjectDescription;
                dB.ProjectMasters.Add(projectMaster);
                dB.SaveChanges();

            }
            return View(projects);
        }
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel resetPassword,int id)
        {
            string message = "";
            using (TSheetDB dB = new TSheetDB())
            {
                var v = dB.Registrations.Where(a => a.UserID == id).FirstOrDefault();
                v.Password = resetPassword.Password;
                dB.SaveChanges();
                message = "Password Changed successfully";
               
            }
            ViewBag.message = message;
            return View();
        }
       
    }
}
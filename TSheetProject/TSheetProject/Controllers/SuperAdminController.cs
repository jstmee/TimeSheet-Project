using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSheet.BL;
using TSheet.Data;
using TSheet.Modals;

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
        public ActionResult SuperAdmin()
        {
            using (TSheetDB dB = new TSheetDB())
            {
                var v=dB.Registrations.ToList();
                ViewBag.NoOfUsers=v.Count;
                /*var countuser = 0;
                foreach (var item in v)
                {
                    countuser++;
                }
                ViewBag.NoOfUers = countuser;*/
                

                var p =dB.ProjectMasters.ToList();
                ViewBag.NoOfProjects=p.Count;
            }
            /*ViewBag.countuser = _registrationRepository.count(user);
            ViewBag.countuser = _registrationRepository.count(admin);
            ViewBag.countuser = _registrationRepository.count(Projects);*/


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
                _registrationRepository.AddRegistration(user);
            }
            else
            {
                return View(user);
            }
            return View();

        }
        public ActionResult AssignRoles()
        {
            TSheetDB db = new TSheetDB();

            var alluser = db.Registrations.ToList();
            return View(alluser);
           
        }
        public ActionResult edit(RegistrationModel user)
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
                /* projects.ProjectName = projectMaster.ProjectName;
                 projects.ProjectDescription = projectMaster.ProjectDescription;*/
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
        public ActionResult ResetPassword(ResetPassword resetPassword,int id)
        {
            using (TSheetDB dB = new TSheetDB())
            {
                var v = dB.Registrations.Where(a => a.UserID == id).FirstOrDefault();
                v.Password = resetPassword.Password;
                dB.SaveChanges();
                /*Registration registration= new Registration();  
                registration.Email = resetPassword.Email;
                registration.Password = resetPassword.Password;
                dB.SaveChanges();*/
            }

                return View();
        }

    }
}
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
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private RegistrationRepository _registrationRepository;
        public AdminController()
        {
            _registrationRepository = new RegistrationRepository();

        }
        // GET: Admin
        public ActionResult DashBoard()
        {
            using (TSheetDB dB = new TSheetDB())
            {
                ViewBag.NoOfProject = dB.ProjectMasters.Count();
                ViewBag.NoOfUser = dB.AssignedRoles.Where(a => a.RoleID == 3).ToList().Count();
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

     

       public ActionResult ProjectList()
       {
            TSheetDB dB= new TSheetDB();
            var projectlist = dB.ProjectMasters.ToList();
            return View(projectlist);
       }
    
       
      public ActionResult DeleteProject(int id)
      {
            TSheetDB dB = new TSheetDB();
            var delproject=dB.ProjectMasters.Where(x=>x.ProjectID==id).First();
            dB.ProjectMasters.Remove(delproject);
            dB.SaveChanges();
            var list = dB.ProjectMasters.ToList();
            return View("ProjectList", list);
      }

        public ActionResult ShowUser()
        {
            TSheetDB dB=new TSheetDB();
            var showuser = dB.Registrations.ToList();

            return View(showuser);
        }
        public ActionResult ViewUser(int id)
        {
            TSheetDB dB =new TSheetDB();
            var seedetail=dB.Registrations.Where(x=>x.UserID==id).First();
            return View(seedetail);
        }

  }
}
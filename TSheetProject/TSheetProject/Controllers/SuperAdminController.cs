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
    //[Authorize(Roles="SuperAdmin")]
    public class SuperAdminController : Controller
    {
        private RegistrationRepository _registrationRepository;
        private ProjectRepository _projectrepository;
        public SuperAdminController()
        {
            _registrationRepository = new RegistrationRepository();
            _projectrepository = new ProjectRepository();

        }
        // GET: SuperAdmin
        [HttpGet]
        public ActionResult DashBoard()
        {
            using (TSheetDB dB = new TSheetDB())
            {
                ViewBag.NoOfUsers = dB.Registrations.Count();
                ViewBag.NoOfProjects= dB.ProjectMasters.Count();
                ViewBag.NoOfAdmin=dB.AssignedRoles.Where(a=>a.RoleID==2).ToList().Count();
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
        [HttpGet]
        public ActionResult AssignRoles()
        {
            TSheetDB db = new TSheetDB();

            var alluser = db.Registrations.ToList();
            AssignedRole assignedRole = new AssignedRole();
            

            return View(alluser);
           
        }
        [HttpGet]
        public ActionResult EditUser(RegistrationModel user)
        {
            TSheetDB dB = new TSheetDB();
            
            ViewBag.projectlist = GetProjectList();
            ViewBag.Roles = GetRolesList();

            if (user != null)
            {
                return View(user);
            }
            return View();
        }
        [HttpPost]
        public ActionResult edit1(RegistrationModel user)
        {
            
            ViewBag.projectlist = GetProjectList();
            ViewBag.Roles = GetRolesList();
            TSheetDB db=new TSheetDB();
            if (user.FirstName!=null && user.LastName!=null && user.DateOfBirth!=null && user.Gender!=null && user.Email!=null && user.MobileNumber!=null  && user.AssignProject!=null && user.AssignProject!=null)
            {

                var v = db.Registrations.Where(a => a.UserID == user.Id).FirstOrDefault();
                v.DateOfbirth = user.DateOfBirth;
                v.FirstName = user.FirstName;
                v.LastName = user.LastName;
                v.Email = user.Email;
                v.MobileNumber = user.MobileNumber;
                v.IsActive = user.IsActive;
                v.Gender = user.Gender;
                v.DateOfLeaving = user.DateOfLeaving;
                

                
                AssignedRole assignedRole = new AssignedRole();
                assignedRole.RoleID = (int)user.AssignedRole;
                assignedRole.UserID = user.Id;

                var LoggedUser = HttpContext.User?.Identity.Name;
                
                var userrow = db.Registrations.Where(r => r.Email == LoggedUser).FirstOrDefault();
                var UserIdLogged = userrow.UserID;

                v.UpdatedOn = DateTime.Now;
                v.EditedBy = UserIdLogged.ToString();
                db.SaveChanges();
                assignedRole.CreatedById = UserIdLogged;
                assignedRole.UpdatedById = 0;
                db.AssignedRoles.Add(assignedRole);

                db.SaveChanges();

                DescriptionAndProjectMapping projectMapping = new DescriptionAndProjectMapping();
                projectMapping.ProjectID = (int)user.AssignProject;
                projectMapping.UserID = user.Id;
                db.DescriptionAndProjectMappings.Add(projectMapping);
                db.SaveChanges();
                return RedirectToAction("AssignRoles", "SuperAdmin");



            }
            else
            {
                return View("EditUser");
            }

            
        }

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
                /*Crypto crypto=new Crypto();*/
                v.Password = Crypto.Hash(resetPassword.Password);
                dB.SaveChanges();
                message = "Password Changed successfully";
               
            }
            ViewBag.message = message;
            return View();
        }



        //a non action method for initializing the dropdownlist in view of timehseetadd
        [NonAction]
        public List<ProjectModel> GetProjectList()
        {
            List<ProjectModel> ListProjects = new List<ProjectModel>();

            var getallprojects = _projectrepository.GetAllProjects();



            foreach (var project in getallprojects)
            {
                ListProjects.Add(new ProjectModel { Id = project.ProjectID, Name = project.ProjectName });
            }
            return ListProjects;
        }

        [NonAction]
        public List<RolesModel> GetRolesList()
        {
            List<RolesModel> ListRoles = new List<RolesModel>();
            TSheetDB dB= new TSheetDB();
            var roles=dB.Roles.ToList();
            foreach (var addrole in roles)
            {
                ListRoles.Add(new RolesModel { Id = addrole.RoleID, RoleName = addrole.RoleName });
            }
            return ListRoles;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.EnterpriseServices;
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
                ViewBag.NoOfUsers = dB.AssignedRoles.Where(a=>a.RoleID==3).Count();

                ViewBag.NoOfProjects= dB.ProjectMasters.Count();
                ViewBag.NoOfAdmin=dB.AssignedRoles.Where(a=>a.RoleID==2).Count();
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
            /*List<Registration> registrations = new List<Registration>();*/
            
            TSheetDB db = new TSheetDB();

            var alluser = db.Registrations.ToList();
            List<AssignRoleViewModel> Detaillist = new List<AssignRoleViewModel>();

            foreach (var item in alluser)
            {
                AssignRoleViewModel assignRoleViewModel= new AssignRoleViewModel();
                
                assignRoleViewModel.FirstName = item.FirstName;
                assignRoleViewModel.LastName = item.LastName;
                assignRoleViewModel.Email = item.Email;
                assignRoleViewModel.UserID= item.UserID;
                assignRoleViewModel.DateOfBirth = item.DateOfbirth;
                assignRoleViewModel.DateOfJoining=item.DateOfJoining;
                assignRoleViewModel.MobileNumber= item.MobileNumber;
                assignRoleViewModel.Gender= item.Gender;
                var userrow = item.UserID;
                var findrole = db.AssignedRoles.Where(x => x.UserID == userrow).FirstOrDefault();
                if (findrole == null)
                {
                    assignRoleViewModel.RoleName = "Not Assigned";
                }
                else
                {
                    assignRoleViewModel.RoleName = findrole.Role.RoleName;
                }
                
                
                Detaillist.Add(assignRoleViewModel);


            }
            return View(Detaillist);
           
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

            string message = "";
            ViewBag.projectlist = GetProjectList();
            ViewBag.Roles = GetRolesList();
            TSheetDB db=new TSheetDB();
            if (user.FirstName != null && user.LastName != null && user.DateOfBirth != null && user.Gender != null && user.Email != null && user.MobileNumber != null && ((user.AssignProject != null && user.AssignedRole != null) || (user.AssignedRole != null && user.AssignProject == null)))
            {

                var registereduser = db.Registrations.Where(a => a.UserID == user.Id).FirstOrDefault();
                registereduser.DateOfbirth = user.DateOfBirth;
                registereduser.FirstName = user.FirstName;
                registereduser.LastName = user.LastName;
                registereduser.Email = user.Email;
                registereduser.MobileNumber = user.MobileNumber;
                registereduser.IsActive = user.IsActive;
                registereduser.Gender = user.Gender;
                registereduser.DateOfLeaving = user.DateOfLeaving;
                

                
                AssignedRole assignedRole = new AssignedRole();
                
                assignedRole.RoleID = (int)user.AssignedRole;
                assignedRole.UserID = user.Id;

                var LoggedUser = HttpContext.User?.Identity.Name;
                
                var userrow = db.Registrations.Where(r => r.Email == LoggedUser).FirstOrDefault();
                var UserIdLogged = userrow.UserID;

                registereduser.UpdatedOn = DateTime.Now;
                registereduser.EditedBy = UserIdLogged.ToString();
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
                message = "Edited Successfully";
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
            string message = "";
            using(TSheetDB dB= new TSheetDB())
            {
                ProjectMaster projectMaster = new ProjectMaster();
                projectMaster.ProjectName = projects.ProjectName;
                projectMaster.ProjectDescription = projects.ProjectDescription;
                dB.ProjectMasters.Add(projectMaster);
                dB.SaveChanges();
                message = "Project added";

            }
            ViewBag.Message = message;
            return View(projects);
        }
        public ActionResult ProjectList()
        {
            TSheetDB dB = new TSheetDB();
            var projectlist = dB.ProjectMasters.ToList();
            return View(projectlist);
        }


        public ActionResult DeleteProject(int id)
        {
            TSheetDB dB = new TSheetDB();
            var deleteproject = dB.ProjectMasters.Where(x => x.ProjectID == id).First();
            dB.ProjectMasters.Remove(deleteproject);
            dB.SaveChanges();
            var list = dB.ProjectMasters.ToList();
            return View("ProjectList", list);
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
                if (ModelState.IsValid)
                {
                    var user = dB.Registrations.Where(a => a.UserID == id).SingleOrDefault();
                    /*Crypto crypto=new Crypto();*/
                    user.Password = Crypto.Hash(resetPassword.Password);
                    dB.SaveChanges();
                    message = "Password Changed successfully";
                }
                else
                {
                    message = "Invalid Request";
                    return View(resetPassword);
                }
            }
            ViewBag.message = message;
            return View();
        }
        public ActionResult ViewUser(int id)
        {
            TSheetDB dB = new TSheetDB();
            var seedetail = dB.Registrations.Where(x => x.UserID == id).First();
            return View(seedetail);
        }
        public ActionResult ProjectsAssigned()
        {
            TSheetDB dB= new TSheetDB();
            var allassignedprojects=dB.DescriptionAndProjectMappings.ToList();

            return View(allassignedprojects);
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
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSheet.BL;
using TSheet.Data;
using TSheet.IBL;
using TSheet.Models;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace TSheetProject.Controllers
{
    public class TotalCountsController : Controller
    {
        private ProjectRepository _projectRepository;
        private TimeSheetMasterRepository _timesheetmasterRepository;
        private RegistrationRepository _RegistrationRepository;
        private TimeSheetDetailRepository _TimeSheetDetailRepository;
        private DescriptionAndProjectMappingRepository _descriptionAndProjectMappingRepository;
        public TotalCountsController()
        {
            _projectRepository = new ProjectRepository();
            _timesheetmasterRepository = new TimeSheetMasterRepository();
            _RegistrationRepository = new RegistrationRepository();
            _TimeSheetDetailRepository = new TimeSheetDetailRepository();
            _descriptionAndProjectMappingRepository = new DescriptionAndProjectMappingRepository();

        }
        // GET: TotalCounts
        TSheetDB dB = new TSheetDB();
        public ActionResult Totaladmin()
        {
            var admin = dB.AssignedRoles.Where(x => x.RoleID == 2).ToList();
            return View(admin);
        }
        public ActionResult TotalUsers()
        {
            var users = dB.AssignedRoles.Where(x => x.RoleID == 3).ToList();
            return View(users);
        }
        public ActionResult TotalProjects()
        {
            var projects = dB.ProjectMasters.ToList();
            return View(projects);
        }
        public ActionResult TotalProjectsSuperAdmin()
        {
            var projects = dB.ProjectMasters.ToList();
            return View(projects);
        }
        public ActionResult TotalTimesheet()
        {
            var tsheet = dB.TimeSheetDetails.Where(x => x.TimeSheetMaster.Registration.Email == User.Identity.Name).ToList();
            return View(tsheet);
        }



        public ActionResult TotalApproved()
        {
            var approved=dB.TimeSheetAuditTBs.Where(x=>x.Status=="Approved" && x.Registration.Email== User.Identity.Name).ToList();
           
            return View(approved);
        }
        public ActionResult TotalRejected()
        {
            var rejected = dB.TimeSheetAuditTBs.Where(x => x.Status == "Rejected"&& x.Registration.Email==User.Identity.Name).ToList();
            return View(rejected);
        }


        public ActionResult TotalTimeSheets1()
        
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult LoadData()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            int recordsTotal = 0;
            

            var UserIdLogged = _RegistrationRepository.GetRegistrationByEmail(HttpContext.User?.Identity.Name).UserID;

            var v = _timesheetmasterRepository.ShowTimeSheet(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(UserIdLogged));
            recordsTotal = v.Count();
            var data = v.Skip(skip).Take(pageSize).ToList();
            var vv = Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
            return vv;





        }
    }

}
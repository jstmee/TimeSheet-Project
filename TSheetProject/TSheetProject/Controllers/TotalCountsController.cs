using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSheet.Data;

namespace TSheetProject.Controllers
{
    public class TotalCountsController : Controller
    {
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
    }

}
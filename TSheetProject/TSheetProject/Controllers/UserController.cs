using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TSheet.Data;
using TSheet.Models;

namespace TSheetProject.Controllers
{
    /*Authorize(Roles ="User")]*/
    public class UserController : Controller
    {
        public UserController()
        {

        }
        // GET: User
        [HttpGet]
        public ActionResult DashBoard()
        {
            ViewBag.TotalsheetUser = TotalSheet();
            ViewBag.Approved = ApprovedSheet();
            ViewBag.Rejected = RejectedSheet();
            return View();
        }
        [HttpGet]
        public ActionResult AllTimeSheet()
        {
            List<UserTimeSheetModel> userTimeSheetModels= new List<UserTimeSheetModel>();
            var LoggedUser = HttpContext.User?.Identity.Name;
            TSheetDB db = new TSheetDB();
            var userrow = db.Registrations.Where(r => r.Email == LoggedUser).FirstOrDefault();
            var UserIdLogged = userrow.UserID;
            var matchedId=db.TimeSheetMasters.Where(a => a.UserID == UserIdLogged).ToList();
            foreach(var item in matchedId)
            {
                UserTimeSheetModel obj = new UserTimeSheetModel();
                obj.Id = item.ProjectId;
                obj.TotalHrs = item.TotalHours;
                obj.FromDate = (DateTime)item.FromDate;
                if(item.ToDate!= null)
                {
                    obj.ToDate = (DateTime)item.ToDate;

                }
                
                obj.Status = item.TimeSheetStatus;
                obj.Comment = item.Comment;
                var ProjectListRow=db.ProjectMasters.Where(a => a.ProjectID == item.ProjectId).FirstOrDefault();
                obj.ProjectName = ProjectListRow.ProjectName;
                userTimeSheetModels.Add(obj);

            }
            


            return View(userTimeSheetModels);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangeUserPassword userPassword)
        {
            string message = "";
            using (TSheetDB dB = new TSheetDB())
            {

                var getuser = dB.Registrations.Where(x => x.Email == userPassword.Email).FirstOrDefault();
                getuser.Password= Crypto.Hash(userPassword.Password);
                dB.SaveChanges();
                message = "Password Changed successfully";
            }
            ViewBag.Message = message;
            return View();
        }

        [NonAction]
        public int TotalSheet()
        {
            int Number;
            TSheetDB db = new TSheetDB();
            var LoggedUser = HttpContext.User?.Identity.Name;
            var userrow = db.Registrations.Where(r => r.Email == LoggedUser).FirstOrDefault();
            var UserIdLogged = userrow.UserID;
            var UserIdMatchedRow=db.TimeSheetMasters.Where(a => a.UserID == UserIdLogged).ToList();
            int sum = 0;
            foreach(var data in UserIdMatchedRow)
            {
                sum+= db.TimeSheetDetails.Where(b => b.TimeSheetMasterID == data.TimeSheetMasterID).ToList().Count();
                
            }
            Number= sum;

            return Number;
        }
        [NonAction]
        public int ApprovedSheet()
        {
            int Number;
            TSheetDB db = new TSheetDB();
            var LoggedUser = HttpContext.User?.Identity.Name;
            var userrow = db.Registrations.Where(r => r.Email == LoggedUser).FirstOrDefault();
            var UserIdLogged = userrow.UserID;
            var UserIdMatchedRow=db.TimeSheetAuditTBs.Where(b=>b.UserID== UserIdLogged && b.Status=="Approved").ToList().Count();
            Number = UserIdMatchedRow;

            return Number;
        }
        [NonAction]
        public int RejectedSheet()
        {
            int Number;
            TSheetDB db = new TSheetDB();
            var LoggedUser = HttpContext.User?.Identity.Name;
            var userrow = db.Registrations.Where(r => r.Email == LoggedUser).FirstOrDefault();
            var UserIdLogged = userrow.UserID;
            var UserIdMatchedRow = db.TimeSheetAuditTBs.Where(b => b.UserID == UserIdLogged && b.Status == "Rejected").ToList().Count();
            Number = UserIdMatchedRow;
            return Number;
        }

    }
}
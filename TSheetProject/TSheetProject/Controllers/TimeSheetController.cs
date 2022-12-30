using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TSheet.BL;
using TSheet.Data;
using TSheet.Models;

namespace TSheetProject.Controllers
{
    [Authorize]
    public class TimeSheetController : Controller
    {
        private ProjectRepository _projectrepository;
        public TimeSheetController()
        {
            _projectrepository = new ProjectRepository();
        }
        // GET: TimeSheet
        [HttpGet]
        public ActionResult Add()
        {
            //for initializing the dropdownlist with project
            ViewBag.Projects = DisplayProjectList();
            return View();
            
        }
        [HttpPost]
        public ActionResult Add(TimeSheetModel obj)
        {
            //for initializing the dropdownlist with project
            ViewBag.Projects = DisplayProjectList();
            ViewBag.DataSaved = false;
            if (ModelState.IsValid == true)
            {

                List<ProjectRow> projectRows = Rowfun(obj);
                foreach (var allrowdata in projectRows)
                {
                    TimeSheetMaster masterobj = new TimeSheetMaster();
                    masterobj.ProjectId = allrowdata.Id;
                    /*HttpContext.Current.User.Identity.Name*/
                    var LoggedUser = HttpContext.User?.Identity.Name;
                    TSheetDB db = new TSheetDB();
                    var userrow=db.Registrations.Where(r => r.Email == LoggedUser).FirstOrDefault();
                    var UserIdLogged = userrow.UserID;

                    masterobj.UserID = UserIdLogged;
                    masterobj.FromDate = obj.Date1;
                    masterobj.ToDate = obj.Date7;
                    TimeSheetAuditTB timeSheetAuditTB=new TimeSheetAuditTB();
                   /* masterobj.TimeSheetStatus = "Not Approved";*/
                   masterobj.TimeSheetStatus=timeSheetAuditTB.Status;
                    if (masterobj.TimeSheetStatus== null)
                    {
                        masterobj.TimeSheetStatus = "Not Approved";
                    }
                    masterobj.Comment = allrowdata.comment;
                    List<int> listOfHrs = li(allrowdata);
                    var totalhrs = 0;
                    var count = 0;
                    foreach (var addhours in listOfHrs)
                    {
                        count++;
                        totalhrs += addhours;
                    }
                    if (count == 5)
                    {
                        obj.Date7 = obj.Date1.AddDays(7);
                    }
                    masterobj.TotalHours = totalhrs;
                    /*TSheetDB db = new TSheetDB();*/
                    db.TimeSheetMasters.Add(masterobj);
                    db.SaveChanges();
                    var days = 0;
                    for (int hours = 0; hours < count; hours++)
                    {
                        days++;
                        TimeSheetDetail detailobj = new TimeSheetDetail();
                        detailobj.Hours = listOfHrs[hours];

                        detailobj.TimeSheetMasterID = masterobj.TimeSheetMasterID;
                        detailobj.Date = obj.Date1.AddDays(days - 1);
                        detailobj.CreatedOn = DateTime.Now;
                        db.TimeSheetDetails.Add(detailobj);
                        db.SaveChanges();
                    }
                    ViewBag.DataSaved = true;
                }

            }
            
            return View(obj);
        }


        [HttpGet]
        public ActionResult AllTimeSheet()
        {
            ViewBag.b = false;
            var alldatatsheet = alltsheetdata();
            return View(alldatatsheet);
        }

        public ActionResult AllTimeSheetAdmin()
        {
            ViewBag.b = false;
            var alldatatsheet = alltsheetdata();
            return View(alldatatsheet);
        }

        public ActionResult AllTimeSheetSuperAdmin()
        {
            ViewBag.b = false;
            var alldatatsheet = alltsheetdata();
            return View(alldatatsheet);
        }


        public ActionResult ApproveTimeSheet()
         {
             ViewBag.b = true;
             var alldatatsheet = alltsheetdata();
             return View("AllTimeSheet", alldatatsheet);
         }
        [HttpPost]
        public ActionResult ApproveTimesheet(int Id, int Id2, string Identity)
        {
            TSheetDB db = new TSheetDB();
            var v=db.Registrations.Where(a => a.Email == Identity).FirstOrDefault();
            TimeSheetAuditTB obj= new TimeSheetAuditTB();
            obj.UserID= Id2;
            obj.Status = "Approved";
            obj.ApprovedBy = Identity;
            obj.TimeSheetDetailID = Id;
           /* obj.Status=allTimeSheetModel.Status;*/
            db.TimeSheetAuditTBs.Add(obj);
            db.SaveChanges();
            return RedirectToAction("ApproveTimeSheet");
        }
        [HttpGet]
        public ActionResult RejectTimeSheet()
        {
            ViewBag.rejectview = true;
            var alldata = alltsheetdata();
            return View("AllTimeSheet",alldata);
        }
        [HttpPost]
        public ActionResult RejectTimeSheet(int Id, string Identity,int Id2)
        {
            TSheetDB db = new TSheetDB();
            var v = db.Registrations.Where(a => a.Email == Identity).FirstOrDefault();

            TimeSheetAuditTB obj = new TimeSheetAuditTB();
            obj.UserID = Id2;
            obj.Status = "Rejected";
            obj.ApprovedBy = Identity;
            obj.TimeSheetDetailID = Id;
            /*obj.Status= allTimeSheetModel.Status;*/
            
            db.TimeSheetAuditTBs.Add(obj);
            db.SaveChanges();
            return RedirectToAction("RejectTimeSheet");
        }

        [HttpGet]
        public ActionResult Approve(int masterid)
        {

            TSheetDB dB = new TSheetDB();
            var alldatesdata = dB.TimeSheetMasters.Where(x => x.TimeSheetMasterID == masterid).SingleOrDefault().TimeSheetDetails.ToList();
            foreach (var data in alldatesdata)
            {
                TimeSheetAuditTB timeSheetAuditTB = new TimeSheetAuditTB();
                if (timeSheetAuditTB.Status == "Approved" || timeSheetAuditTB.Status=="Rejected") 
                {
                    continue;
                }
                timeSheetAuditTB.Status = "Approved";
                timeSheetAuditTB.ApprovedBy = HttpContext.User.Identity.Name;
                timeSheetAuditTB.TimeSheetDetailID = data.TimeSheetDetailID;
                timeSheetAuditTB.UserID = data.TimeSheetMaster.UserID;

                var rowexist = dB.TimeSheetAuditTBs.Where(x => x.TimeSheetDetailID == data.TimeSheetDetailID).SingleOrDefault();
                if (rowexist != null)
                {
                    timeSheetAuditTB.TimeSheetAuditID = rowexist.TimeSheetAuditID;
                    dB.TimeSheetAuditTBs.AddOrUpdate(timeSheetAuditTB);
                    //dB.Entry(timeSheetAuditTB).State = EntityState.Modified;
                    dB.SaveChanges();
                }
                else
                {
                    dB.TimeSheetAuditTBs.Add(timeSheetAuditTB);
                    dB.SaveChanges();
                }
            }
            return RedirectToAction("WeeklyStatus");

        }


        public ActionResult ApproveSuperAdmin(int masterid)
        {

            TSheetDB dB = new TSheetDB();
            var alldatesdata = dB.TimeSheetMasters.Where(x => x.TimeSheetMasterID == masterid).SingleOrDefault().TimeSheetDetails.ToList();
            foreach (var data in alldatesdata)
            {
                TimeSheetAuditTB timeSheetAuditTB = new TimeSheetAuditTB();
                if (timeSheetAuditTB.Status == "Approved" || timeSheetAuditTB.Status == "Rejected")
                {
                    continue;
                }
                timeSheetAuditTB.Status = "Approved";
                timeSheetAuditTB.ApprovedBy = HttpContext.User.Identity.Name;
                timeSheetAuditTB.TimeSheetDetailID = data.TimeSheetDetailID;
                timeSheetAuditTB.UserID = data.TimeSheetMaster.UserID;

                var rowexist = dB.TimeSheetAuditTBs.Where(x => x.TimeSheetDetailID == data.TimeSheetDetailID).SingleOrDefault();
                if (rowexist != null)
                {
                    timeSheetAuditTB.TimeSheetAuditID = rowexist.TimeSheetAuditID;
                    dB.TimeSheetAuditTBs.AddOrUpdate(timeSheetAuditTB);
                    //dB.Entry(timeSheetAuditTB).State = EntityState.Modified;
                    dB.SaveChanges();
                }
                else
                {
                    dB.TimeSheetAuditTBs.Add(timeSheetAuditTB);
                    dB.SaveChanges();
                }
            }
            return RedirectToAction("WeeklyStatusSuperAdmin");
        }

        [HttpGet]
        public ActionResult Reject(int masterid)
        {

            TSheetDB dB = new TSheetDB();
            var alldatesdata = dB.TimeSheetMasters.Where(x => x.TimeSheetMasterID == masterid).SingleOrDefault().TimeSheetDetails.ToList();
            foreach (var data in alldatesdata)
            {
                TimeSheetAuditTB timeSheetAuditTB = new TimeSheetAuditTB();
                timeSheetAuditTB.Status = "Rejected";
                timeSheetAuditTB.ApprovedBy = HttpContext.User.Identity.Name;
                timeSheetAuditTB.TimeSheetDetailID = data.TimeSheetDetailID;
                timeSheetAuditTB.UserID = data.TimeSheetMaster.UserID;
                var rowexist = dB.TimeSheetAuditTBs.Where(x => x.TimeSheetDetailID == data.TimeSheetDetailID).SingleOrDefault();
                if (rowexist != null)
                {
                    timeSheetAuditTB.TimeSheetAuditID = rowexist.TimeSheetAuditID;
                    dB.TimeSheetAuditTBs.AddOrUpdate(timeSheetAuditTB);
                    /*dB.Entry(timeSheetAuditTB).State = EntityState.Modified;*/
                    dB.SaveChanges();
                }
                else
                {
                    dB.TimeSheetAuditTBs.Add(timeSheetAuditTB);
                    dB.SaveChanges();
                }
            }
            return RedirectToAction("WeeklyStatusSuperAdmin");
        }

        public ActionResult RejectSuperAdmin(int masterid)
        {

            TSheetDB dB = new TSheetDB();
            var alldatesdata = dB.TimeSheetMasters.Where(x => x.TimeSheetMasterID == masterid).SingleOrDefault().TimeSheetDetails.ToList();
            foreach (var data in alldatesdata)
            {
                TimeSheetAuditTB timeSheetAuditTB = new TimeSheetAuditTB();
                timeSheetAuditTB.Status = "Rejected";
                timeSheetAuditTB.ApprovedBy = HttpContext.User.Identity.Name;
                timeSheetAuditTB.TimeSheetDetailID = data.TimeSheetDetailID;
                timeSheetAuditTB.UserID = data.TimeSheetMaster.UserID;
                var rowexist = dB.TimeSheetAuditTBs.Where(x => x.TimeSheetDetailID == data.TimeSheetDetailID).SingleOrDefault();
                if (rowexist != null)
                {
                    timeSheetAuditTB.TimeSheetAuditID = rowexist.TimeSheetAuditID;
                    dB.TimeSheetAuditTBs.AddOrUpdate(timeSheetAuditTB);
                    /*dB.Entry(timeSheetAuditTB).State = EntityState.Modified;*/
                    dB.SaveChanges();
                }
                else
                {
                    dB.TimeSheetAuditTBs.Add(timeSheetAuditTB);
                    dB.SaveChanges();
                }
            }
            return RedirectToAction("WeeklyStatusSuperAdmin");
        }

        [HttpGet]
        public ActionResult WeeklyStatus()
        {
            TSheetDB db = new TSheetDB();
            //Retriving The Data Of The Master Table For The Users
            var masterdata = db.TimeSheetMasters.ToList();

            TimeSheetDetail timeSheetDetail= new TimeSheetDetail();
            

            List<WeeklyApproveRejectModel> viewmodellists = new List<WeeklyApproveRejectModel>();
            foreach (var masterdataitem in masterdata)
            {
                WeeklyApproveRejectModel modellist = new WeeklyApproveRejectModel();
                modellist.TotalHours = masterdataitem.TotalHours;
                modellist.FromDate = masterdataitem.FromDate;
                modellist.ToDate = masterdataitem.ToDate;
                modellist.FirstName = masterdataitem.Registration.FirstName;
                modellist.LastName = masterdataitem.Registration.LastName;
                modellist.Id = masterdataitem.TimeSheetMasterID;
                modellist.Project = masterdataitem.ProjectMaster.ProjectName;
                modellist.Comment = masterdataitem.Comment;
                
               var oneweeklog = db.TimeSheetDetails.Where(x=>x.TimeSheetMasterID==masterdataitem.TimeSheetMasterID).ToList();
                int approvedcounter = 0,rejectedcounter=0;
                foreach(var day in oneweeklog)
                {
                    var timesheetauditstatus = db.TimeSheetAuditTBs.Where(x => x.TimeSheetDetailID == day.TimeSheetDetailID).FirstOrDefault();
                    if (timesheetauditstatus!=null)
                    {
                        if (timesheetauditstatus.Status == "Approved")
                        {
                            approvedcounter++;
                        }

                        if (timesheetauditstatus.Status == "Rejected")
                        {
                            rejectedcounter++;
                        }
                    }
                }

                if (approvedcounter == oneweeklog.Count() && approvedcounter>0)
                {
                    modellist.Status = "Week Approved";
                }
                else if(rejectedcounter == oneweeklog.Count() && rejectedcounter>0) 
                {
                    modellist.Status = "Week Rejected";
                }
                else if(approvedcounter>0 || rejectedcounter>0)
                {
                    modellist.Status = "Some days are approved/rejected";
                }
                else
                {
                    modellist.Status = "No Action";
                }




                
                
                


                
                


                viewmodellists.Add(modellist);
            }
             return View(viewmodellists);
        }

        public ActionResult WeeklyStatusSuperAdmin()
        {
            TSheetDB db = new TSheetDB();

            //Retriving The Data Of The Master Table For The Users
            var masterdata = db.TimeSheetMasters.ToList();

            TimeSheetDetail timeSheetDetail = new TimeSheetDetail();


            List<WeeklyApproveRejectModel> viewmodellists = new List<WeeklyApproveRejectModel>();
            foreach (var masterdataitem in masterdata)
            {
                WeeklyApproveRejectModel modellist = new WeeklyApproveRejectModel();
                modellist.TotalHours = masterdataitem.TotalHours;
                modellist.FromDate = masterdataitem.FromDate;
                modellist.ToDate = masterdataitem.ToDate;
                modellist.FirstName = masterdataitem.Registration.FirstName;
                modellist.LastName = masterdataitem.Registration.LastName;
                modellist.Id = masterdataitem.TimeSheetMasterID;
                modellist.Project = masterdataitem.ProjectMaster.ProjectName;
                modellist.Comment = masterdataitem.Comment;

                var oneweeklog = db.TimeSheetDetails.Where(x => x.TimeSheetMasterID == masterdataitem.TimeSheetMasterID).ToList();
                int approvedcounter = 0, rejectedcounter = 0;
                foreach (var day in oneweeklog)
                {
                    var timesheetauditstatus = db.TimeSheetAuditTBs.Where(x => x.TimeSheetDetailID == day.TimeSheetDetailID).FirstOrDefault();
                    if (timesheetauditstatus != null)
                    {
                        if (timesheetauditstatus.Status == "Approved")
                        {
                            approvedcounter++;
                        }

                        if (timesheetauditstatus.Status == "Rejected")
                        {
                            rejectedcounter++;
                        }
                    }
                }

                if (approvedcounter == oneweeklog.Count() && approvedcounter > 0)
                {
                    modellist.Status = "Week Approved";
                }
                else if (rejectedcounter == oneweeklog.Count() && rejectedcounter > 0)
                {
                    modellist.Status = "Week Rejected";
                }
                else if (approvedcounter > 0 || rejectedcounter > 0)
                {
                    modellist.Status = "Some days are approved/rejected";
                }
                else
                {
                    modellist.Status = "No Action";
                }













                viewmodellists.Add(modellist);
            }
            return View(viewmodellists);
        }


        [HttpGet]
        public ActionResult WeekApproveReject(int? id2)
        {

            TSheetDB dB= new TSheetDB();
            /*if(id2 == null)
            {
                id2 = masterid;
            }*/
            var detail= dB.TimeSheetDetails.Where(x=>x.TimeSheetMasterID==id2).ToList();
            List<WeekInfoModel> weekInfoModels= new List<WeekInfoModel>();
            
            foreach(var detailitem in detail)
            {
                WeekInfoModel model=new WeekInfoModel();
                model.Id = detailitem.TimeSheetDetailID;
                model.Date = detailitem.Date;
                model.Hours = (int?)detailitem.Hours;
                var onedaylog = dB.TimeSheetAuditTBs.Where(x => x.TimeSheetDetailID == detailitem.TimeSheetDetailID).FirstOrDefault();

                if (onedaylog!=null)
                {
                    if (onedaylog.Status != null)
                    {
                        model.Status= onedaylog.Status;
                    }
                }
                
                 weekInfoModels.Add(model);
            }

            return View(weekInfoModels);
        }

        public ActionResult WeekApproveRejectSuperAdmin(int? id2)
        {
            TSheetDB dB = new TSheetDB();
            /*if(id2 == null)
            {
                id2 = masterid;
            }*/
            var detail = dB.TimeSheetDetails.Where(x => x.TimeSheetMasterID == id2).ToList();
            List<WeekInfoModel> weekInfoModels = new List<WeekInfoModel>();

            foreach (var detailitem in detail)
            {
                WeekInfoModel model = new WeekInfoModel();
                model.Id = detailitem.TimeSheetDetailID;
                model.Date = detailitem.Date;
                model.Hours = (int?)detailitem.Hours;
                var onedaylog = dB.TimeSheetAuditTBs.Where(x => x.TimeSheetDetailID == detailitem.TimeSheetDetailID).FirstOrDefault();

                if (onedaylog != null)
                {
                    if (onedaylog.Status != null)
                    {
                        model.Status = onedaylog.Status;
                    }
                }

                weekInfoModels.Add(model);
            }

            return View(weekInfoModels);
        }
        [HttpGet]
        public ActionResult ApproveDay(int? id)
        {
            TSheetDB dB= new TSheetDB();
            var oneauditrow= dB.TimeSheetAuditTBs.Where(x=>x.TimeSheetDetailID==id).FirstOrDefault();
            int? masterid=null;
            if(oneauditrow==null)
            {
                    TimeSheetAuditTB timeSheetAuditTB = new TimeSheetAuditTB();
                    timeSheetAuditTB.ApprovedBy = HttpContext.User.Identity.Name;
                var user = dB.TimeSheetDetails.Where(x => x.TimeSheetDetailID == id).FirstOrDefault().TimeSheetMasterID;
                var userid = dB.TimeSheetMasters.Where(x => x.TimeSheetMasterID == user).FirstOrDefault().UserID;
                timeSheetAuditTB.UserID = userid;
                    timeSheetAuditTB.Status = "Approved";
                timeSheetAuditTB.TimeSheetDetailID= id;
                masterid = user;
                var rowexist = dB.TimeSheetAuditTBs.Where(x => x.TimeSheetDetailID==id).SingleOrDefault();
                    dB.TimeSheetAuditTBs.Add(timeSheetAuditTB);
                    dB.SaveChanges();
                
                
            }
            if (masterid == null)
            {
                var user = dB.TimeSheetDetails.Where(x => x.TimeSheetDetailID == id).FirstOrDefault().TimeSheetMasterID;
                masterid = user;
            }
            
            return RedirectToAction("WeekApproveReject", new { id2 = masterid });
        }
        [HttpGet]
        public ActionResult RejectDay(int? id)
        {
            TSheetDB dB = new TSheetDB();
            var oneauditrow = dB.TimeSheetAuditTBs.Where(x => x.TimeSheetDetailID == id).FirstOrDefault();
            int? masterid = null;
            if (oneauditrow == null)
            {
                TimeSheetAuditTB timeSheetAuditTB = new TimeSheetAuditTB();
                timeSheetAuditTB.ApprovedBy = HttpContext.User.Identity.Name;
                var user = dB.TimeSheetDetails.Where(x => x.TimeSheetDetailID == id).FirstOrDefault().TimeSheetMasterID;
                masterid = user;
                var userid = dB.TimeSheetMasters.Where(x => x.TimeSheetMasterID == user).FirstOrDefault().UserID;
                timeSheetAuditTB.UserID = userid;
                timeSheetAuditTB.TimeSheetDetailID = id;
                timeSheetAuditTB.Status = "Rejected";
                dB.TimeSheetAuditTBs.Add(timeSheetAuditTB);
                dB.SaveChanges();
            }
            return RedirectToAction("WeekApproveReject", new { id2 = masterid });
        }


        public ActionResult approve1(int id)
        {
            TSheetDB dB = new TSheetDB();
            var details=dB.TimeSheetAuditTBs.Where(a=>a.TimeSheetDetailID==id).SingleOrDefault();
            if (details != null)
            {
                details.Status = "Approved";
                dB.SaveChanges();

            }
            var getmasterid = dB.TimeSheetDetails.Where(a => a.TimeSheetDetailID == id).SingleOrDefault().TimeSheetMasterID;
            /*return RedirectToAction("WeeklyStatus", "TimeSheet");*/
            return RedirectToAction("WeekApproveReject", "TimeSheet", new { id2 = getmasterid });
            /*return View();*/
        }
        public ActionResult reject1(int id)
        {
            TSheetDB dB = new TSheetDB();
            var details = dB.TimeSheetAuditTBs.Where(a => a.TimeSheetDetailID == id).SingleOrDefault();
            if (details != null)
            {
                details.Status = "Rejected";
                dB.SaveChanges();

            }
            var getmasterid = dB.TimeSheetDetails.Where(a => a.TimeSheetDetailID == id).SingleOrDefault().TimeSheetMasterID;
            return RedirectToAction("WeekApproveReject", "TimeSheet", new { id2 = getmasterid });
        }
        [NonAction]
        public List<AllTimeSheetModel> alltsheetdata()
        {
            List<AllTimeSheetModel> viewmodellists = new List<AllTimeSheetModel>();
            TSheetDB db = new TSheetDB();
            /*AssignedRole assigned = new AssignedRole();*/
            var logged= db.AssignedRoles.Where(a=>a.Registration.Email== User.Identity.Name).SingleOrDefault().RoleID;
            if (logged == 3)
            {
                ViewBag.Showname = false;
                var tsheetdetailtb = db.TimeSheetDetails.Where(x => x.TimeSheetMaster.Registration.Email == User.Identity.Name).ToList();
                foreach (var detail in tsheetdetailtb)
                {
                    AllTimeSheetModel viewmodel = new AllTimeSheetModel();
                    viewmodel.Hours = detail.Hours;
                    viewmodel.CreatedOn = detail.CreatedOn;
                    viewmodel.Date = detail.Date;
                    viewmodel.AllTimesheetId = detail.TimeSheetDetailID;
                    viewmodel.TimeSheetDetailID = detail.TimeSheetDetailID;
                   
                    /*viewmodel.UserUniqueId=v.*/



                    var masterid = detail.TimeSheetMasterID;
                    var masteridmatchedrow = db.TimeSheetMasters.Where(a => a.TimeSheetMasterID == masterid).FirstOrDefault();
                    var userid = masteridmatchedrow.UserID;
                    viewmodel.UserUniqueId = userid;
                    var projectid = masteridmatchedrow.ProjectId;
                    var projectmatchedrow = db.ProjectMasters.Where(a => a.ProjectID == projectid).FirstOrDefault();
                    var useridmatchedrow = db.Registrations.Where(a => a.UserID == userid).FirstOrDefault();
                    viewmodel.FirstName = useridmatchedrow.FirstName;
                    viewmodel.LastName = useridmatchedrow.LastName;
                    viewmodel.ProjectName = projectmatchedrow.ProjectName;



                    viewmodellists.Add(viewmodel);
                }
            }
            else
            {
                var tsheetdetailtb = db.TimeSheetDetails.ToList();
                ViewBag.Showname = true;
                foreach (var detail in tsheetdetailtb)
                {
                    AllTimeSheetModel viewmodel = new AllTimeSheetModel();
                    viewmodel.Hours = detail.Hours;
                    viewmodel.CreatedOn = detail.CreatedOn;
                    viewmodel.Date = detail.Date;
                    /*viewmodel.AllTimesheetId = v.TimeSheetDetailID;*/
                    viewmodel.TimeSheetDetailID = detail.TimeSheetDetailID;
                    TimeSheetAuditTB obj= new TimeSheetAuditTB();
                    /*viewmodel.UserUniqueId=v.*/
                    var auditdetailid= db.TimeSheetAuditTBs.Where(x => x.TimeSheetDetailID == detail.TimeSheetDetailID).FirstOrDefault();
                    if ( auditdetailid!= null)
                    {

                        viewmodel.Status = auditdetailid.Status;

                    }
                    var masterid = detail.TimeSheetMasterID;
                    var masteridmatchedrow = db.TimeSheetMasters.Where(a => a.TimeSheetMasterID == masterid).FirstOrDefault();
                    var userid = masteridmatchedrow.UserID;
                    viewmodel.UserUniqueId = userid;
                    var projectid = masteridmatchedrow.ProjectId;
                    var projectmatchedrow = db.ProjectMasters.Where(a => a.ProjectID == projectid).FirstOrDefault();
                    var useridmatchedrow = db.Registrations.Where(a => a.UserID == userid).FirstOrDefault();
                    viewmodel.FirstName = useridmatchedrow.FirstName;
                    viewmodel.LastName = useridmatchedrow.LastName;
                    viewmodel.ProjectName = projectmatchedrow.ProjectName;



                    viewmodellists.Add(viewmodel);
                }

            }
           

            
            return viewmodellists;

        }

        [NonAction]
        public List<int> ProjectForIterate(TimeSheetModel obj)
        {
            var projectslist = new List<int>();
            projectslist.Add((obj.ProjectID1));
            if (obj.ProjectID2 != null)
            {
                projectslist.Add(((int)obj.ProjectID2));

            }

            if (obj.ProjectID3 != null)
            {
                projectslist.Add(((int)obj.ProjectID3));

            }

            if (obj.ProjectID4 != null)
            {
                projectslist.Add(((int)obj.ProjectID4));

            }
            return projectslist;
        }

        [NonAction]
        public List<int> li(ProjectRow vv)
        {
            var l= new List<int>();
            l.Add((int)vv.day1);
            if (vv.day2 != null)
            {
                l.Add((int)vv.day2);
            }
            if (vv.day3 != null)
            {
                l.Add((int)vv.day3);
            }
            if (vv.day4 != null)
            {
                l.Add((int)vv.day4);
            }
            if (vv.day5 != null)
            {
                l.Add((int)vv.day5);
            }
            if (vv.day6 != null)
            {
                l.Add((int)vv.day6);
            }
            if (vv.day7 != null)
            {
                l.Add((int)vv.day7);
            }

            return l;

        }

        [NonAction]
        public List<ProjectRow> Rowfun(TimeSheetModel obj)
        {


            List<ProjectRow> projectRows = new List<ProjectRow>();
            int p = 0;
            for (int i = 0; i < 4; i++)
            {
                p++;
                if (p == 1)
                {
                    ProjectRow row1 = new ProjectRow();
                    row1.Id = obj.ProjectID1;
                    if (obj.Text1_ProjectID1 == null)
                    {
                        return projectRows;
                    }
                    row1.day1 = (int)obj.Text1_ProjectID1;
                    if (obj.Text2_ProjectID1 != null)
                    {
                        row1.day2 = (int)obj.Text2_ProjectID1;

                    }
                    if (obj.Text3_ProjectID1 != null)
                    {
                        row1.day3 = (int)obj.Text3_ProjectID1;

                    }
                    if (obj.Text4_ProjectID1 != null)
                    {
                        row1.day4 = (int)obj.Text4_ProjectID1;


                    }
                    if (obj.Text5_ProjectID1 != null)
                    {
                        row1.day5 = (int)obj.Text5_ProjectID1;

                    }

                    if (obj.Text6_ProjectID1 != null)
                    {
                        row1.day6 = (int)obj.Text6_ProjectID1;

                    }
                    if (obj.Text7_ProjectID1 != null)
                    {
                        row1.day7 = (int)obj.Text7_ProjectID1;

                    }
                    if (obj.Description_ProjectID1 != null)
                    {
                        row1.comment = (string)obj.Description_ProjectID1;

                    }


                    projectRows.Add(row1);
                }
                if (p == 2)
                {
                    if (obj.ProjectID2 != null)
                    {
                        ProjectRow row2 = new ProjectRow();
                        row2.Id = (int)obj.ProjectID2;
                        row2.day1 = (int)obj.Text1_ProjectID2;
                        if (obj.Text2_ProjectID2 != null)
                        {
                            row2.day2 = (int)obj.Text2_ProjectID2;

                        }
                        if (obj.Text3_ProjectID2 != null)
                        {
                            row2.day3 = (int)obj.Text3_ProjectID2;

                        }
                        if (obj.Text4_ProjectID2 != null)
                        {
                            row2.day4 = (int)obj.Text4_ProjectID2;


                        }
                        if (obj.Text5_ProjectID2 != null)
                        {
                            row2.day5 = (int)obj.Text5_ProjectID2;

                        }

                        if (obj.Text6_ProjectID2 != null)
                        {
                            row2.day6 = (int)obj.Text6_ProjectID2;

                        }
                        if (obj.Text7_ProjectID2 != null)
                        {
                            row2.day7 = (int)obj.Text7_ProjectID2;

                        }

                        if (obj.Description_ProjectID2 != null)
                        {
                            row2.comment = (string)obj.Description_ProjectID2;

                        }
                        projectRows.Add(row2);

                    }

                }

                if (p == 3)
                {
                    if (obj.ProjectID3 != null)
                    {
                        ProjectRow row3 = new ProjectRow();
                        row3.Id = (int)obj.ProjectID3;
                        row3.day1 = (int)obj.Text1_ProjectID3;
                        if (obj.Text2_ProjectID3 != null)
                        {
                            row3.day2 = (int)obj.Text2_ProjectID3;

                        }
                        if (obj.Text3_ProjectID3 != null)
                        {
                            row3.day3 = (int)obj.Text3_ProjectID3;

                        }
                        if (obj.Text4_ProjectID3 != null)
                        {
                            row3.day4 = (int)obj.Text4_ProjectID3;


                        }
                        if (obj.Text5_ProjectID3 != null)
                        {
                            row3.day5 = (int)obj.Text5_ProjectID3;

                        }

                        if (obj.Text6_ProjectID3 != null)
                        {
                            row3.day6 = (int)obj.Text6_ProjectID3;

                        }
                        if (obj.Text7_ProjectID3 != null)
                        {
                            row3.day7 = (int)obj.Text7_ProjectID3;

                        }

                        if (obj.Description_ProjectID3 != null)
                        {
                            row3.comment = (string)obj.Description_ProjectID3;

                        }
                        projectRows.Add(row3);

                    }

                }

                if (p == 4)
                {
                    if (obj.ProjectID4 != null)
                    {
                        ProjectRow row4 = new ProjectRow();
                        row4.Id = (int)obj.ProjectID4;
                        row4.day1 = (int)obj.Text1_ProjectID4;
                        if (obj.Text2_ProjectID4 != null)
                        {
                            row4.day2 = (int)obj.Text2_ProjectID4;

                        }
                        if (obj.Text3_ProjectID4 != null)
                        {
                            row4.day3 = (int)obj.Text3_ProjectID4;

                        }
                        if (obj.Text4_ProjectID4 != null)
                        {
                            row4.day4 = (int)obj.Text4_ProjectID4;


                        }
                        if (obj.Text5_ProjectID4 != null)
                        {
                            row4.day5 = (int)obj.Text5_ProjectID4;

                        }

                        if (obj.Text6_ProjectID4 != null)
                        {
                            row4.day6 = (int)obj.Text6_ProjectID4;

                        }
                        if (obj.Text7_ProjectID4 != null)
                        {
                            row4.day7 = (int)obj.Text7_ProjectID4;

                        }

                        if (obj.Description_ProjectID4 != null)
                        {
                            row4.comment = (string)obj.Description_ProjectID4;

                        }
                        projectRows.Add(row4);

                    }

                }

            }
            return projectRows;

        }

        //a non action method for initializing the dropdownlist in view of timehseetadd
        [NonAction]
        public List<ProjectModel> DisplayProjectList()
        {
            List<ProjectModel> ListProjects = new List<ProjectModel>();

            var getprojects = _projectrepository.GetAllProjects();



            foreach (var project in getprojects)
            {
                ListProjects.Add(new ProjectModel { Id = project.ProjectID, Name = project.ProjectName });
            }
            return ListProjects;
        }

    }
    
}
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
            ViewBag.Projects = MyCustom();



            return View();
            
        }
        [HttpPost]
        public ActionResult Add(TimeSheetModel obj)
        {
            //for initializing the dropdownlist with project
            ViewBag.Projects = MyCustom();
            ViewBag.DataSaved = false;
            if (ModelState.IsValid == true)
            {

                List<ProjectRow> projectRows = Rowfun(obj);
                foreach (var vv in projectRows)
                {
                    TimeSheetMaster masterobj = new TimeSheetMaster();
                    masterobj.ProjectId = vv.Id;
                    /*HttpContext.Current.User.Identity.Name*/
                    var LoggedUser = HttpContext.User?.Identity.Name;
                    TSheetDB db = new TSheetDB();
                    var userrow=db.Registrations.Where(r => r.Email == LoggedUser).FirstOrDefault();
                    var UserIdLogged = userrow.UserID;

                    masterobj.UserID = UserIdLogged;
                    masterobj.FromDate = obj.Date1;
                    masterobj.ToDate = obj.Date7;
                    masterobj.TimeSheetStatus = "Not Approved";
                    masterobj.Comment = vv.comment;
                    List<int> listOfHrs = li(vv);
                    var totalhrs = 0;
                    var count = 0;
                    foreach (var v in listOfHrs)
                    {

                        count++;
                        totalhrs += v;
                    }
                    if (count == 5)
                    {
                        obj.Date7 = obj.Date1.AddDays(7);
                    }
                    masterobj.TotalHours = totalhrs;
                    /*TSheetDB db = new TSheetDB();*/
                    db.TimeSheetMasters.Add(masterobj);
                    db.SaveChanges();
                    var dayys = 0;
                    for (int i = 0; i < count; i++)
                    {
                        dayys++;
                        TimeSheetDetail detailobj = new TimeSheetDetail();
                        detailobj.Hours = listOfHrs[i];

                        detailobj.TimeSheetMasterID = masterobj.TimeSheetMasterID;
                        detailobj.Date = obj.Date1.AddDays(dayys - 1);
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
            var v = alltsheetdata();
            return View(v);
        }
        public ActionResult ApproveSheet()
        {
            ViewBag.b = true;
            var v = alltsheetdata();
            return View("AllTimeSheet", v);
        }
        public ActionResult ApproveTimesheet(int id,string Email)
        {
            TSheetDB db = new TSheetDB();
            var v=db.Registrations.Where(a => a.Email == Email).FirstOrDefault();
            
            TimeSheetAuditTB obj= new TimeSheetAuditTB();
            
            obj.UserID= v.UserID;
            obj.Status = "Approved";
            obj.ApprovedBy = Email;
            db.TimeSheetAuditTBs.Add(obj);
            db.SaveChanges();
           return RedirectToAction("ApproveSheet");
        }
        public ActionResult RejectTimeSheet()
        {
            
            var v = alltsheetdata();
            return View("AllTimeSheet", v);
           
        }
        public ActionResult RejectTimeSheet(int id, string Email)
        {
            TSheetDB db = new TSheetDB();
            var v = db.Registrations.Where(a => a.Email == Email).FirstOrDefault();

            TimeSheetAuditTB obj = new TimeSheetAuditTB();
            obj.UserID = v.UserID;
            obj.Status = "Rejected";
            obj.ApprovedBy = Email;
            db.TimeSheetAuditTBs.Add(obj);
            db.SaveChanges();
            return RedirectToAction("RejectSheet");
        }


        [NonAction]
        public List<AllTimeSheetModel> alltsheetdata()
        {
            List<AllTimeSheetModel> viewmodellists = new List<AllTimeSheetModel>();



            TSheetDB db = new TSheetDB();
            var tsheetdetailtb = db.TimeSheetDetails.ToList();
            foreach (var v in tsheetdetailtb)
            {
                AllTimeSheetModel viewmodel = new AllTimeSheetModel();
                viewmodel.Hours = v.Hours;
                viewmodel.CreatedOn = v.CreatedOn;
                viewmodel.Date = v.Date;
                viewmodel.AllTimesheetId = v.TimeSheetDetailID;



                var masterid = v.TimeSheetMasterID;
                var masteridmatchedrow = db.TimeSheetMasters.Where(a => a.TimeSheetMasterID == masterid).FirstOrDefault();
                var userid = masteridmatchedrow.UserID;
                var projectid = masteridmatchedrow.ProjectId;
                var projectmatchedrow = db.ProjectMasters.Where(a => a.ProjectID == projectid).FirstOrDefault();
                var useridmatchedrow = db.Registrations.Where(a => a.UserID == userid).FirstOrDefault();
                viewmodel.FirstName = useridmatchedrow.FirstName;
                viewmodel.LastName = useridmatchedrow.LastName;
                viewmodel.ProjectName = projectmatchedrow.ProjectName;



                viewmodellists.Add(viewmodel);
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
        public List<ProjectModel> MyCustom()
        {
            List<ProjectModel> ListProjects = new List<ProjectModel>();

            var v = _projectrepository.GetAllProjects();



            foreach (var p in v)
            {
                ListProjects.Add(new ProjectModel { Id = p.ProjectID, Name = p.ProjectName });
            }
            return ListProjects;
        }

    }
    
}
﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TSheet.BL;
using TSheet.Data;
using TSheet.Models;

namespace TSheetProject.Controllers
{
    [Authorize(Roles="Admin, SuperAdmin")]
    public class TimeSheetController : Controller
    {
        private ProjectRepository _projectrepository;
        public TimeSheetController()
        {
            _projectrepository = new ProjectRepository();
        }
        // GET: TimeSheet
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

            //initializing the total for all field of the project
            //and the dates of subsequent 6 days from date started
            Initilizing(obj);

            return View();
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

        //a function to initilize the total hours of all the projects
        [NonAction]
        public void Initilizing(TimeSheetModel obj)
        {
            // initilization of all the dates which should not be null

            obj.Date2 = obj.Date1.AddDays(1);
            obj.Date3 = obj.Date1.AddDays(2);
            obj.Date4 = obj.Date1.AddDays(3);
            obj.Date5 = obj.Date1.AddDays(4);
            obj.Date6 = obj.Date1.AddDays(5);
            obj.Date7 = obj.Date1.AddDays(6);

            /*NullFieldToZero(obj);*/
            obj.totalhr_ProjectID1=obj.Text1_ProjectID1+obj.Text2_ProjectID1+ obj.Text3_ProjectID1+ obj.Text4_ProjectID1+ obj.Text5_ProjectID1+ obj.Text6_ProjectID1+ obj.Text7_ProjectID1;

            if (obj.ProjectID2 != null)
            {
                obj.totalhr_ProjectID2 = obj.Text1_ProjectID2 + obj.Text2_ProjectID2 + obj.Text3_ProjectID2 + obj.Text4_ProjectID2 + obj.Text5_ProjectID2 + obj.Text6_ProjectID2 + obj.Text7_ProjectID2;

            }
            
            if(obj.ProjectID3 != null)
            {
                obj.totalhr_ProjectID3 = obj.Text1_ProjectID3 + obj.Text2_ProjectID3 + obj.Text3_ProjectID3 + obj.Text4_ProjectID3 + obj.Text5_ProjectID3 + obj.Text6_ProjectID3 + obj.Text7_ProjectID3;
            }
            
            if(obj.ProjectID4 != null)
            {
                obj.totalhr_ProjectID4 = obj.Text1_ProjectID4 + obj.Text2_ProjectID4 + obj.Text3_ProjectID4 + obj.Text4_ProjectID4 + obj.Text5_ProjectID4 + obj.Text6_ProjectID4 + obj.Text7_ProjectID4;

            }

            


        }

        [NonAction]
        public void nullfieldtozero(TimeSheetModel obj)
        {
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

    }
    
}
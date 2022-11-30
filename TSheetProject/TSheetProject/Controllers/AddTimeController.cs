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
    public class AddTimeController : Controller
    {
        private ProjectRepository _projectRepository;

        public AddTimeController()
        {
            _projectRepository = new ProjectRepository();
        }
        // GET: AddTime
        [HttpGet]
        public ActionResult AddTime()
        {
            ViewBag.Projects = DisplayProjectList();
            List<AddTimeSheetModel> addTimeSheetModels = new List<AddTimeSheetModel>();
            var projectcount = _projectRepository.GetAllProjects().Count();
            for (int i = 0; i < projectcount; i++)
            {
                AddTimeSheetModel addTimeSheetobj = new AddTimeSheetModel();
                addTimeSheetobj.id = i + 1;
                addTimeSheetModels.Add(addTimeSheetobj);
            }

            return View(addTimeSheetModels);
        }
        [HttpPost]
        public ActionResult AddTime(List<AddTimeSheetModel> addTime, DateTime userdate)
        {
            ViewBag.Projects = DisplayProjectList();
            if (ModelState.IsValid)
            {
                TSheetDB dB = new TSheetDB();
                foreach(var userrowdata in addTime) 
                {
                    /*if(userrowdata != null) { }*/
                    if (userrowdata.ProjectId != null)
                    {
                        TimeSheetMaster timesheetmasterobj = new TimeSheetMaster();

                        timesheetmasterobj.ProjectId = (int)userrowdata.ProjectId;
                        var LoggedUser = HttpContext.User?.Identity.Name;
                        var userrow = dB.Registrations.Where(r => r.Email == LoggedUser).FirstOrDefault();
                        var UserIdLogged = userrow.UserID;
                        timesheetmasterobj.UserID = UserIdLogged;
                        timesheetmasterobj.FromDate = userdate;
                        timesheetmasterobj.ToDate = userdate.AddDays(6);
                        timesheetmasterobj.Comment = userrowdata.Description;
                        timesheetmasterobj.TimeSheetStatus = "Not Approved";
                        timesheetmasterobj.TotalHours = (int)CalculateTotalHours(userrowdata);
                        dB.TimeSheetMasters.Add(timesheetmasterobj);
                        dB.SaveChanges();
                        List<int>Days= new List<int>();
                        if (userrowdata.MondayLogTime != null)
                        {
                            Days.Add((int)userrowdata.MondayLogTime);
                        }
                        if(userrowdata.TuesdayLogTime != null)
                        {
                            Days.Add((int)userrowdata.TuesdayLogTime);
                        }
                        if(userrowdata.WednesdayLogTime != null)
                        {
                            Days.Add((int)userrowdata.WednesdayLogTime);
                        }
                        if(userrowdata.ThursdayLogTime != null)
                        {
                            Days.Add((int)userrowdata.ThursdayLogTime);
                        }
                        if (userrowdata.FridayLogTime != null)
                        {
                            Days.Add((int)userrowdata.FridayLogTime);
                        }
                        if(userrowdata.SaturdayLogTime != null)
                        {
                            Days.Add((int)userrowdata.SaturdayLogTime);
                        }
                        if(userrowdata.SundayLogTime != null)
                        {
                            Days.Add((int)userrowdata.SundayLogTime);
                        }
                        for (int i = 0; i <Days.Count(); i++)
                        {
                            TimeSheetDetail timeSheetDetail = new TimeSheetDetail();
                            timeSheetDetail.TimeSheetMasterID = timesheetmasterobj.TimeSheetMasterID;
                            timeSheetDetail.Hours =Days[i]; 

                            timeSheetDetail.Date = userdate;
                            timeSheetDetail.CreatedOn = DateTime.Now;
                            dB.TimeSheetDetails.Add(timeSheetDetail);
                            dB.SaveChanges();

                        }


                    }
                }






            }
            return View(addTime);
        }
        [NonAction]
        public List<ProjectModel> DisplayProjectList()
        {
            List<ProjectModel> ListProjects = new List<ProjectModel>();

            var getprojects = _projectRepository.GetAllProjects();



            foreach (var project in getprojects)
            {
                ListProjects.Add(new ProjectModel { Id = project.ProjectID, Name = project.ProjectName });
            }
            return ListProjects;
        }
        [NonAction]
        public int? CalculateTotalHours(AddTimeSheetModel totalhours)
        {
            
            int? Total = 0;
            /*var val1 = TimeSheetModel.texttotal_p1 == null ? 0 : TimeSheetModel.texttotal_p1;
            var val2 = TimeSheetModel.texttotal_p2 == null ? 0 : TimeSheetModel.texttotal_p2;
            var val3 = TimeSheetModel.texttotal_p3 == null ? 0 : TimeSheetModel.texttotal_p3;
            var val4 = TimeSheetModel.texttotal_p4 == null ? 0 : TimeSheetModel.texttotal_p4;
            var val5 = TimeSheetModel.texttotal_p5 == null ? 0 : TimeSheetModel.texttotal_p5;
            var val6 = TimeSheetModel.texttotal_p6 == null ? 0 : TimeSheetModel.texttotal_p6;*/
            /*Total = val1 + val2 + val3 + val4 + val5 + val6;*/
            var val1 = totalhours.MondayLogTime == null ? 0 : totalhours.MondayLogTime;
            var val2 = totalhours.TuesdayLogTime == null ? 0 : totalhours.TuesdayLogTime;
            var val3 = totalhours.WednesdayLogTime == null ? 0 : totalhours.WednesdayLogTime;
            var val4 = totalhours.ThursdayLogTime == null ? 0 : totalhours.ThursdayLogTime;
            var val5 = totalhours.FridayLogTime == null ? 0 : totalhours.FridayLogTime;
            var val6 = totalhours.SaturdayLogTime == null ? 0 : totalhours.SaturdayLogTime;
            var val7 = totalhours.SundayLogTime == null ? 0 : totalhours.SundayLogTime;
            Total=val1+val2+val3+val4+val5+val6+val7;
            return Total;
            

        }
    }
}
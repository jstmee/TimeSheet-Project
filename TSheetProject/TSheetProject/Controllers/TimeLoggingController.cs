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
    public class TimeLoggingController : Controller
    {

        private ProjectRepository _projectRepository;
        private TimeSheetMasterRepository _timesheetmasterRepository;
        private RegistrationRepository _RegistrationRepository;
        private TimeSheetDetailRepository _TimeSheetDetailRepository;
        public TimeLoggingController()
        {
            _projectRepository = new ProjectRepository();
            _timesheetmasterRepository=new TimeSheetMasterRepository();
            _RegistrationRepository=new RegistrationRepository();
            _TimeSheetDetailRepository=new TimeSheetDetailRepository();

        }

        //Get Method for user time logging page
        [HttpGet]
        public ActionResult AddTime()
        {
            //fetching list of projects from database for showing it on dropdownlist in view
            ViewBag.Projects = DisplayProjectList();

            //initializing the empty timesheetmodal for use in view
            List<AddTimeSheetModel> addTimeSheetModels = new List<AddTimeSheetModel>();

            //getting the total count of the projects
            var projectcount = _projectRepository.GetAllProjects().Count();
            for (int i = 0; i < projectcount; i++)
            {
                AddTimeSheetModel addTimeSheetobj = new AddTimeSheetModel();
                addTimeSheetobj.id = i + 1;

                //initializing the row of the time logging by no of projects
                addTimeSheetModels.Add(addTimeSheetobj);
            }

            //passing list which is initially has no of row equal to the no of projects in the database
            return View(addTimeSheetModels);
        }

        //post method for the user time logging ie submiting the time logging by the user
        [HttpPost]
        public ActionResult AddTime(List<AddTimeSheetModel> addTime, DateTime userdate)
        {
            // again initializing the dropdownlist so the if anything goes wrong he can again select them
            ViewBag.Projects = DisplayProjectList();

            //checking views model state is valid or not
            if (ModelState.IsValid)
            {
                foreach (var userrowdata in addTime)
                {
                    if (userrowdata.ProjectId != null)
                    {
                        TimeSheetMaster timesheetmasterobj = new TimeSheetMaster();
                        timesheetmasterobj.ProjectId = (int)userrowdata.ProjectId;
                        var LoggedUser = HttpContext.User?.Identity.Name;
                        var userrow = _RegistrationRepository.GetRegistrationByEmail(LoggedUser);
                        var UserIdLogged = userrow.UserID;
                        timesheetmasterobj.UserID = UserIdLogged;
                        timesheetmasterobj.FromDate = userdate;
                        timesheetmasterobj.ToDate = userdate.AddDays(6);
                        timesheetmasterobj.Comment = userrowdata.Description;
                        timesheetmasterobj.TimeSheetStatus = "Not Approved";
                        timesheetmasterobj.TotalHours = (int)CalculateTotalHours(userrowdata);
                        _timesheetmasterRepository.AddTimeSheetMaster(timesheetmasterobj);
                        Dictionary<DateTime, int> DaysWiseHrs = GettingDayWiseHrs1(userrowdata, userdate);
                        foreach(var DictionaryDaywiseHrs in DaysWiseHrs)
                        {
                            TimeSheetDetail timeSheetDetail = new TimeSheetDetail();
                            timeSheetDetail.TimeSheetMasterID = timesheetmasterobj.TimeSheetMasterID;
                            timeSheetDetail.Hours = DictionaryDaywiseHrs.Value;
                            timeSheetDetail.Date = DictionaryDaywiseHrs.Key;
                            timeSheetDetail.CreatedOn = DateTime.Now;
                            _TimeSheetDetailRepository.AddTimeSheetDetail(timeSheetDetail);
                        }
                    }
                }
            }
            else
            {
                return View(addTime);
            }
            return View(addTime);
        }

        //non action method for getting the list of project for displaying in dropdownlist of the use view
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

        //non action method for calculating the total hours of the user time logging for a particular project
        [NonAction]
        public int? CalculateTotalHours(AddTimeSheetModel totalhours)
        {
            int? Total = 0;
            var val1 = totalhours.MondayLogTime == null ? 0 : totalhours.MondayLogTime;
            var val2 = totalhours.TuesdayLogTime == null ? 0 : totalhours.TuesdayLogTime;
            var val3 = totalhours.WednesdayLogTime == null ? 0 : totalhours.WednesdayLogTime;
            var val4 = totalhours.ThursdayLogTime == null ? 0 : totalhours.ThursdayLogTime;
            var val5 = totalhours.FridayLogTime == null ? 0 : totalhours.FridayLogTime;
            var val6 = totalhours.SaturdayLogTime == null ? 0 : totalhours.SaturdayLogTime;
            var val7 = totalhours.SundayLogTime == null ? 0 : totalhours.SundayLogTime;
            Total = val1 + val2 + val3 + val4 + val5 + val6 + val7;
            return Total;
        }


        // a non action method for getting daywise hrs in form of dicionary
        [NonAction]
        public Dictionary<DateTime, int> GettingDayWiseHrs1(AddTimeSheetModel userrowdata, DateTime userdate)
        {
            Dictionary<DateTime, int> intMap = new Dictionary<DateTime, int>();
            if (userrowdata.MondayLogTime != null)
            {
                intMap.Add(userdate,(int)userrowdata.MondayLogTime);
            }
            if (userrowdata.TuesdayLogTime != null)
            {
                intMap.Add(userdate.AddDays(1), (int)userrowdata.TuesdayLogTime);
            }
            if (userrowdata.WednesdayLogTime != null)
            {
                intMap.Add(userdate.AddDays(2), (int)userrowdata.WednesdayLogTime);
            }
            if (userrowdata.ThursdayLogTime != null)
            {
                intMap.Add(userdate.AddDays(3),(int)userrowdata.ThursdayLogTime);
            }
            if (userrowdata.FridayLogTime != null)
            {
                intMap.Add(userdate.AddDays(4),(int)userrowdata.FridayLogTime);
            }
            if (userrowdata.SaturdayLogTime != null)
            {
                intMap.Add(userdate.AddDays(5),(int)userrowdata.SaturdayLogTime);
            }
            if (userrowdata.SundayLogTime != null)
            {
                intMap.Add(userdate.AddDays(6) ,(int)userrowdata.SundayLogTime);
            }
            if (userrowdata.SundayLogTime != null)
            {
                intMap.Add(userdate.AddDays(7), (int)userrowdata.SundayLogTime);
            }
            return intMap;

        }
    }
}
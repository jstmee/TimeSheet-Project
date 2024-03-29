﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using TSheet.BL;
using TSheet.Data;
using TSheet.IBL;
using TSheet.Models;

namespace TSheetProject.Controllers
{
    public class TimeLoggingController : Controller
    {

        private ProjectRepository _projectRepository;
        private TimeSheetMasterRepository _timesheetmasterRepository;
        private RegistrationRepository _RegistrationRepository;
        private TimeSheetDetailRepository _TimeSheetDetailRepository;
        private DescriptionAndProjectMappingRepository _descriptionAndProjectMappingRepository;
        public TimeLoggingController()
        {
            _projectRepository = new ProjectRepository();
            _timesheetmasterRepository = new TimeSheetMasterRepository();
            _RegistrationRepository = new RegistrationRepository();
            _TimeSheetDetailRepository = new TimeSheetDetailRepository();
            _descriptionAndProjectMappingRepository = new DescriptionAndProjectMappingRepository();

        }

        public ActionResult TimeLog(string userweek)
        {
            if (userweek == null)
            {
                userweek = GetCurrentWeek();
            }
            //convert userweek to dates
            int year = int.Parse(userweek.Substring(0, 4));
            Session["Year"] = year;
            int week = int.Parse(userweek.Substring(6));
            Session["WeekNo"] = week;
            var FirstDays = FirstDateOfWeek(year, week);
            Session["FirstDateOfWeek"]=FirstDays;
            List<DateTime> ListOfDates = GetListOfDates(FirstDays);
            Session["LastDateOfWeek"] = ListOfDates;
            var UserIdLogged = _RegistrationRepository.GetRegistrationByEmail(HttpContext.User?.Identity.Name).UserID;
            var timeSheetMasterlist = _timesheetmasterRepository.GetTimeSheetMasterByUserIDFromDate(UserIdLogged, FirstDays);
            //if user has already data
            //fetching of data required
            if (timeSheetMasterlist.Count == 0)
            {
                Session["userDates"] = ListOfDates;
                Session["UserTimeLogData"] = null;
                return RedirectToAction("AddTime");

            }
            else
            {
                List<AddTimeSheetModel> addTimeSheetModelsList = new List<AddTimeSheetModel>();
                //fetch the user data and pass that data to addtime controller
                foreach (var timeSheetMaster in timeSheetMasterlist)
                {
                     AddTimeSheetModel addTimeSheetModel = new AddTimeSheetModel();
                     var timesheetmasterid = timeSheetMaster.TimeSheetMasterID;
                     addTimeSheetModel.ProjectId = timeSheetMaster.ProjectId;
                     addTimeSheetModel.ProjectName = timeSheetMaster.ProjectMaster.ProjectName;

                     if (timeSheetMaster.Comment != null)
                     {


                        addTimeSheetModel.Description = timeSheetMaster.Comment;
                        addTimeSheetModel.DescriptionId = 1;
                     }
                     var timeSheetDetail = _TimeSheetDetailRepository.GetAllTimeSheetDetailByMasterId(timesheetmasterid);

                     if (timeSheetDetail != null)
                     {
                         Dictionary<DateTime?, decimal?> DaysWiseHrsUserDataInDB = GetDaysWiseHrsUserDataInDB(timeSheetDetail);
                            #region 
                            if (DaysWiseHrsUserDataInDB.TryGetValue(FirstDays, out decimal? value1) != false)
                            {
                                addTimeSheetModel.MondayLogTime = (float?)DaysWiseHrsUserDataInDB[FirstDays];
                                addTimeSheetModel.MondayLogTimeId = 1;
                            }

                            if (DaysWiseHrsUserDataInDB.TryGetValue(FirstDays.AddDays(1), out decimal? value2) != false)
                            {
                                addTimeSheetModel.TuesdayLogTime = (float?)DaysWiseHrsUserDataInDB[FirstDays.AddDays(1)];
                                addTimeSheetModel.TuesdayLogTimeId = 1;
                            }

                            if (DaysWiseHrsUserDataInDB.TryGetValue(FirstDays.AddDays(2), out decimal? value3) != false)
                            {
                                addTimeSheetModel.WednesdayLogTime = (float?)DaysWiseHrsUserDataInDB[FirstDays.AddDays(2)];
                                addTimeSheetModel.WednesdayLogTimeId = 1;
                            }

                            if (DaysWiseHrsUserDataInDB.TryGetValue(FirstDays.AddDays(3), out decimal? value4) != false)
                            {
                                addTimeSheetModel.ThursdayLogTime = (float?)DaysWiseHrsUserDataInDB[FirstDays.AddDays(3)];
                                addTimeSheetModel.ThursdayLogTimeId = 1;
                            }

                            if (DaysWiseHrsUserDataInDB.TryGetValue(FirstDays.AddDays(4), out decimal? value5) != false)
                            {
                                addTimeSheetModel.FridayLogTime = (float?)DaysWiseHrsUserDataInDB[FirstDays.AddDays(4)];
                                addTimeSheetModel.FridayLogTimeId = 1;
                            }

                            if (DaysWiseHrsUserDataInDB.TryGetValue(FirstDays.AddDays(5), out decimal? value6) != false)
                            {
                                addTimeSheetModel.SaturdayLogTime = (float?)DaysWiseHrsUserDataInDB[FirstDays.AddDays(5)];
                                addTimeSheetModel.SaturdayLogTimeId = 1;
                            }

                            if (DaysWiseHrsUserDataInDB.TryGetValue(FirstDays.AddDays(6), out decimal? value7) != false)
                            {
                                addTimeSheetModel.SundayLogTime = (float?)DaysWiseHrsUserDataInDB[FirstDays.AddDays(6)];
                                addTimeSheetModel.SundayLogTimeId = 1;
                            }
                            #endregion
                            addTimeSheetModelsList.Add(addTimeSheetModel);

                     }
                }
                Session["userDates"] = ListOfDates;
                Session["UserTimeLogData"] = addTimeSheetModelsList;
                return RedirectToAction("AddTime");
            }
        }
        //Get Method for user time logging page
        [HttpGet]
        public ActionResult AddTime()
        {
            List<ProjectModel> projectModels = DisplayProjectList();
            ViewBag.Projects = projectModels;
            if (Session["userDates"] == null)
            {
                return RedirectToAction("Timelog", "TimeLogging", new { userweek = GetCurrentWeek() });
            }
            ViewBag.userDates = Session["userDates"];
            Session["Dates"] = Session["userDates"];
            ViewBag.userWeek = Session["WeekNo"];
            Session["WeekNo"] = ViewBag.userWeek;
            ViewBag.year = Session["Year"];
            ViewBag.FirstDayOfWeek = Session["FirstDateOfWeek"];
            ViewBag.LastDateOfWeek = Session["LastDateOfWeek"];
            ViewBag.monthName = GetMonthName(ViewBag.userWeek);
            //initializing the empty timesheetmodal for use in view
            List<AddTimeSheetModel> addTimeSheetModels = new List<AddTimeSheetModel>();
            for (int projectadd = 0; projectadd< projectModels.Count(); projectadd++)
            {
                AddTimeSheetModel addTimeSheetobj = new AddTimeSheetModel();
                addTimeSheetobj.id = 0;
                addTimeSheetobj.ProjectId = projectModels[projectadd].Id;
                addTimeSheetobj.ProjectName = projectModels[projectadd].Name;

                //initializing the row of the time logging by no of projects
                addTimeSheetModels.Add(addTimeSheetobj);
            }
            if (Session["UserTimeLogData"] != null)
            {
                List<AddTimeSheetModel> userLogData = (List<AddTimeSheetModel>)Session["UserTimeLogData"];
                if (userLogData != null)
                {
                    //1st working method
                    foreach (var filleddata in userLogData)
                    {
                        for (int i = 0; i < projectModels.Count(); i++)
                        {
                            if (addTimeSheetModels[i].ProjectName == filleddata.ProjectName)
                            {
                                addTimeSheetModels[i] = filleddata;
                                addTimeSheetModels[i].id = 5;

                            }

                        }

                    }

                    

                }

            }

            else
            {

            } 

            //passing list which is initially has no of row equal to the no of projects in the database
            return View(addTimeSheetModels);
        }

        //post method for the user time logging ie submiting the time logging by the user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTime(List<AddTimeSheetModel> addTime)
        {
            string message = "";
            // again initializing the dropdownlist so the if anything goes wrong he can again select them
            ViewBag.Projects = DisplayProjectList();
            
            ViewBag.userDates = Session["Dates"];
            var userdate = ViewBag.userDates[0];
            ViewBag.userWeek = Session["WeekNo"];

            ViewBag.monthName = GetMonthName(ViewBag.userWeek);
            ViewBag.year = Session["Year"];

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
                        if (userrowdata.Description == null)
                        {
                            userrowdata.Description = "";

                        }
                        else
                        {
                            timesheetmasterobj.Comment = userrowdata.Description;
                        }

                        timesheetmasterobj.TimeSheetStatus = "Not Approved";
                        timesheetmasterobj.TotalHours = (decimal)CalculateTotalHours(userrowdata);

                        TSheetDB db = new TSheetDB();

                        var getTimeSheetMasterByUserIDFromDate = _timesheetmasterRepository.GetTimeSheetMasterByUserIDFromDate(UserIdLogged, userdate, (int)userrowdata.ProjectId);

                        if (getTimeSheetMasterByUserIDFromDate != null)
                        {
                            timesheetmasterobj.TimeSheetMasterID = getTimeSheetMasterByUserIDFromDate.TimeSheetMasterID;
                            db.Entry(timesheetmasterobj).State = EntityState.Modified;
                            db.SaveChanges();

                        }
                        else
                        {
                            _timesheetmasterRepository.AddTimeSheetMaster(timesheetmasterobj);
                        }


                        Dictionary<DateTime, decimal?> DaysWiseHrs = GettingDayWiseHrs1(userrowdata, userdate);
                        foreach (var DictionaryDaywiseHrs in DaysWiseHrs)
                        {
                            TimeSheetDetail timeSheetDetail = new TimeSheetDetail();
                            timeSheetDetail.TimeSheetMasterID = timesheetmasterobj.TimeSheetMasterID;
                            timeSheetDetail.Hours = DictionaryDaywiseHrs.Value;
                            timeSheetDetail.Date = DictionaryDaywiseHrs.Key;
                            timeSheetDetail.CreatedOn = DateTime.Now;
                            var getTimeSheetDetailByMasterIDDate = _TimeSheetDetailRepository.GetAllTimeSheetDetailByMasterIdDate(timesheetmasterobj.TimeSheetMasterID, DictionaryDaywiseHrs.Key);

                            if (getTimeSheetDetailByMasterIDDate != null)
                            {


                                
                                timeSheetDetail.TimeSheetDetailID = getTimeSheetDetailByMasterIDDate.TimeSheetDetailID;
                                db.Entry(timeSheetDetail).State = EntityState.Modified;
                                db.SaveChanges();
                                message = "TimeSheet filled !";
                                
                                

                            }
                            else
                            {
                                _TimeSheetDetailRepository.AddTimeSheetDetail(timeSheetDetail);
                                message = "TimeSheet filled !";
                            }

                        }
                    }
                }

            }
            else
            {
                 return View(addTime);
            }
            ViewBag.Message = message;
            return View(addTime);
        }

        //non action method for getting the list of project for displaying in dropdownlist of the use view
        [NonAction]
        public List<ProjectModel> DisplayProjectList()
        {

            List<ProjectModel> ListProjects = new List<ProjectModel>();
            var getprojects = _projectRepository.GetAllProjects();
            var userid = _RegistrationRepository.GetRegistrationByEmail(HttpContext.User.Identity.Name).UserID;
            var getUserProjects = _descriptionAndProjectMappingRepository.GetAllProjectsOfUser(userid);
            foreach (var project in getUserProjects)
            {
                ListProjects.Add(new ProjectModel { Id = project.ProjectID, Name = project.ProjectName });
            }

            return ListProjects;
        }

        //non action method for calculating the total hours of the user time logging for a particular project
        [NonAction]
        public float? CalculateTotalHours(AddTimeSheetModel totalhours)
        {
            float? Total = 0;
            var val1 = totalhours.MondayLogTime == null ? 0 : totalhours.MondayLogTime;
            var val2 = totalhours.TuesdayLogTime == null ? 0 : totalhours.TuesdayLogTime;
            var val3 = totalhours.WednesdayLogTime == null ? 0 : totalhours.WednesdayLogTime;
            var val4 = totalhours.ThursdayLogTime == null ? 0 : totalhours.ThursdayLogTime;
            var val5 = totalhours.FridayLogTime == null ? 0 : totalhours.FridayLogTime;
            var val6 = totalhours.SaturdayLogTime == null ? 0 : totalhours.SaturdayLogTime;
            var val7 = totalhours.SundayLogTime == null ? 0 : totalhours.SundayLogTime;
            float myFloat1 = Convert.ToSingle(val1);
            float myFloat2 = Convert.ToSingle(val2);
            float myFloat3 = Convert.ToSingle(val3);
            float myFloat4 = Convert.ToSingle(val4);
            float myFloat5 = Convert.ToSingle(val5);
            float myFloat6 = Convert.ToSingle(val6);
            float myFloat7 = Convert.ToSingle(val7);
            Total = val1 + val2 + val3 + val4 + val5 + val6 + val7;
            return Total;
        }

        // a non action method for getting daywise hrs in form of dicionary
        [NonAction]
        public Dictionary<DateTime, decimal?> GettingDayWiseHrs1(AddTimeSheetModel userrowdata, DateTime userdate)
        {
            Dictionary<DateTime, decimal?> intMap = new Dictionary<DateTime, decimal?>();
            if (userrowdata.MondayLogTime != null)
            {

                intMap.Add(userdate, (decimal)userrowdata.MondayLogTime);
            }
            else
            {
                if (userrowdata.MondayLogTimeId != null)
                {
                    intMap.Add(userdate, null);
                }
            }

            if (userrowdata.TuesdayLogTime != null)
            {
                intMap.Add(userdate.AddDays(1), (decimal)userrowdata.TuesdayLogTime);
            }
            else
            {
                if (userrowdata.TuesdayLogTimeId != null)
                {
                    intMap.Add(userdate.AddDays(1), null);
                }
            }
            if (userrowdata.WednesdayLogTime != null)
            {
                intMap.Add(userdate.AddDays(2), (decimal)userrowdata.WednesdayLogTime);
            }
            else
            {
                if (userrowdata.WednesdayLogTimeId != null)
                {
                    intMap.Add(userdate.AddDays(2), null);
                }
            }
            if (userrowdata.ThursdayLogTime != null)
            {
                intMap.Add(userdate.AddDays(3), (decimal)userrowdata.ThursdayLogTime);
            }
            else
            {
                if (userrowdata.ThursdayLogTimeId != null)
                {
                    intMap.Add(userdate.AddDays(3), null);
                }
            }
            if (userrowdata.FridayLogTime != null)
            {
                intMap.Add(userdate.AddDays(4), (decimal)userrowdata.FridayLogTime);
            }
            else
            {
                if (userrowdata.FridayLogTimeId != null)
                {
                    intMap.Add(userdate.AddDays(4), null);
                }
            }
            if (userrowdata.SaturdayLogTime != null)
            {
                intMap.Add(userdate.AddDays(5), (decimal)userrowdata.SaturdayLogTime);
            }
            else
            {
                if (userrowdata.SaturdayLogTimeId != null)
                {
                    intMap.Add(userdate.AddDays(5), null);
                }
            }
            if (userrowdata.SundayLogTime != null)
            {
                intMap.Add(userdate.AddDays(6), (decimal)userrowdata.SundayLogTime);
            }
            else
            {
                if (userrowdata.SundayLogTimeId != null)
                {
                    intMap.Add(userdate.AddDays(6), null);
                }
            }
            return intMap;

        }

        [NonAction]
        public static DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;
            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }
            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }

        [NonAction]
        public List<DateTime> GetListOfDates(DateTime FirstDays)
        {
            List<DateTime> ListOfDates = new List<DateTime>();
            for (int i = 0; i < 7; i++)
            {
                ListOfDates.Add(FirstDays.AddDays(i));
            }

            return ListOfDates;

        }

        [NonAction]
        public Dictionary<DateTime?, decimal?> GetDaysWiseHrsUserDataInDB(IList<TimeSheetDetail> timeSheetDetail)
        {
            Dictionary<DateTime?, decimal?> DaysWiseHrsUserDataInDB = new Dictionary<DateTime?, decimal?>();
            foreach (var detaildata in timeSheetDetail)
            {
                DaysWiseHrsUserDataInDB[detaildata.Date] = detaildata.Hours;
            }
            return DaysWiseHrsUserDataInDB;

        }

        [NonAction]
        public string GetMonthName(int weekNumber)
        {
            // Create a DateTime object for the first week of the year
            DateTime firstWeek = new DateTime(DateTime.Now.Year, 1, 1);

            // Add the number of weeks to the first week of the year
            DateTime targetWeek = firstWeek.AddDays(weekNumber * 7);

            // Get the name of the month that the target week belongs to
            return targetWeek.ToString("MMMM");
        }

        [NonAction]
        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        [NonAction]
        public string GetCurrentWeek()
        {
            
            DateTime now = DateTime.Now;
            int GetcurrentWeekNo = GetIso8601WeekOfYear(now);
            int curyear = now.Year;
            string userweek = string.Format("{0:0000}-W{1:00}", curyear, GetcurrentWeekNo);
            return userweek;    
        }





    }
}
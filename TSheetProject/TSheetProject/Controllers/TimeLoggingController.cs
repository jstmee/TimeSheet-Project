using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
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
        public TimeLoggingController()
        {
            _projectRepository = new ProjectRepository();
            _timesheetmasterRepository = new TimeSheetMasterRepository();
            _RegistrationRepository = new RegistrationRepository();
            _TimeSheetDetailRepository = new TimeSheetDetailRepository();

        }

        [HttpGet]
        public ActionResult TimeLog()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TimeLog(string userweek)
        {

            if (ModelState.IsValid)
            {
                //convert userweek to dates
                int year = int.Parse(userweek.Substring(0, 4));
                int week = int.Parse(userweek.Substring(6));
                TempData["WeekNo"] = week;
                var FirstDays = FirstDateOfWeek(year, week);
                List<DateTime> ListOfDates = GetListOfDates(FirstDays);

                var UserIdLogged = _RegistrationRepository.GetRegistrationByEmail(HttpContext.User?.Identity.Name).UserID;

                var timeSheetMasterlist = _timesheetmasterRepository.GetTimeSheetMasterByUserIDFromDate(UserIdLogged, FirstDays);

                //if user has already data
                //fetching of data required
                if (timeSheetMasterlist.Count == 0)
                {
                    TempData["userDates"] = ListOfDates;
                    TempData["UserTimeLogData"] = null;
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
                            Dictionary<DateTime?, int?> DaysWiseHrsUserDataInDB = GetDaysWiseHrsUserDataInDB(timeSheetDetail);
                            #region 
                            if (DaysWiseHrsUserDataInDB.TryGetValue(FirstDays, out int? value1) != false)
                            {
                                addTimeSheetModel.MondayLogTime = DaysWiseHrsUserDataInDB[FirstDays];
                                addTimeSheetModel.MondayLogTimeId = 1;
                            }

                            if (DaysWiseHrsUserDataInDB.TryGetValue(FirstDays.AddDays(1), out int? value2) != false)
                            {
                                addTimeSheetModel.TuesdayLogTime = DaysWiseHrsUserDataInDB[FirstDays.AddDays(1)];
                                addTimeSheetModel.TuesdayLogTimeId = 1;
                            }

                            if (DaysWiseHrsUserDataInDB.TryGetValue(FirstDays.AddDays(2), out int? value3) != false)
                            {
                                addTimeSheetModel.WednesdayLogTime = DaysWiseHrsUserDataInDB[FirstDays.AddDays(2)];
                                addTimeSheetModel.WednesdayLogTimeId = 1;
                            }

                            if (DaysWiseHrsUserDataInDB.TryGetValue(FirstDays.AddDays(3), out int? value4) != false)
                            {
                                addTimeSheetModel.ThursdayLogTime = DaysWiseHrsUserDataInDB[FirstDays.AddDays(3)];
                                addTimeSheetModel.ThursdayLogTimeId = 1;
                            }

                            if (DaysWiseHrsUserDataInDB.TryGetValue(FirstDays.AddDays(4), out int? value5) != false)
                            {
                                addTimeSheetModel.FridayLogTime = DaysWiseHrsUserDataInDB[FirstDays.AddDays(4)];
                                addTimeSheetModel.FridayLogTimeId = 1;
                            }

                            if (DaysWiseHrsUserDataInDB.TryGetValue(FirstDays.AddDays(5), out int? value6) != false)
                            {
                                addTimeSheetModel.SaturdayLogTime = DaysWiseHrsUserDataInDB[FirstDays.AddDays(5)];
                                addTimeSheetModel.SaturdayLogTimeId = 1;
                            }

                            if (DaysWiseHrsUserDataInDB.TryGetValue(FirstDays.AddDays(6), out int? value7) != false)
                            {
                                addTimeSheetModel.SundayLogTime = DaysWiseHrsUserDataInDB[FirstDays.AddDays(6)];
                                addTimeSheetModel.SundayLogTimeId = 1;
                            }
                            #endregion
                            addTimeSheetModelsList.Add(addTimeSheetModel);

                        }
                    }
                    TempData["userDates"] = ListOfDates;
                    TempData["UserTimeLogData"] = addTimeSheetModelsList;
                    return RedirectToAction("AddTime");

                }
            }
            return View(userweek);
        }

        //Get Method for user time logging page
        [HttpGet]
        public ActionResult AddTime()
        {
            //fetching list of projects from database for showing it on dropdownlist in view
            List<ProjectModel> projectModels = DisplayProjectList();

            ViewBag.Projects = projectModels;
            ViewBag.userDates = TempData["userDates"];
            TempData["Dates"] = TempData["userDates"];
            ViewBag.userWeek = TempData["WeekNo"];
            TempData["WeekNo"] = ViewBag.userWeek;


            //initializing the empty timesheetmodal for use in view
            List<AddTimeSheetModel> addTimeSheetModels = new List<AddTimeSheetModel>();

            for (int i = 0; i < projectModels.Count(); i++)
            {
                AddTimeSheetModel addTimeSheetobj = new AddTimeSheetModel();
                addTimeSheetobj.id = 0;
                addTimeSheetobj.ProjectId = projectModels[i].Id;
                addTimeSheetobj.ProjectName = projectModels[i].Name;

                //initializing the row of the time logging by no of projects
                addTimeSheetModels.Add(addTimeSheetobj);


            }
            if (TempData["UserTimeLogData"] != null)
            {
                List<AddTimeSheetModel> userLogData = (List<AddTimeSheetModel>)TempData["UserTimeLogData"];

                if (userLogData != null)
                {

                    foreach (var filleddata in userLogData)
                    {
                        for (int i = 0; i < projectModels.Count(); i++)
                        {
                            if (addTimeSheetModels[i].ProjectName == filleddata.ProjectName)
                            {
                                addTimeSheetModels[i].id = 5;
                                addTimeSheetModels[i].ProjectId = filleddata.ProjectId;
                                addTimeSheetModels[i].ProjectName = filleddata.ProjectName;
                                addTimeSheetModels[i].MondayLogTime = filleddata.MondayLogTime;
                                addTimeSheetModels[i].MondayLogTimeId = filleddata.MondayLogTimeId;
                                addTimeSheetModels[i].TuesdayLogTime = filleddata.TuesdayLogTime;
                                addTimeSheetModels[i].TuesdayLogTimeId = filleddata.TuesdayLogTimeId;
                                addTimeSheetModels[i].WednesdayLogTime = filleddata.WednesdayLogTime;
                                addTimeSheetModels[i].WednesdayLogTimeId = filleddata.WednesdayLogTimeId;
                                addTimeSheetModels[i].ThursdayLogTime = filleddata.ThursdayLogTime;
                                addTimeSheetModels[i].ThursdayLogTimeId = filleddata.ThursdayLogTimeId;
                                addTimeSheetModels[i].FridayLogTime = filleddata.FridayLogTime;
                                addTimeSheetModels[i].FridayLogTimeId = filleddata.FridayLogTimeId;
                                addTimeSheetModels[i].SaturdayLogTime = filleddata.SaturdayLogTime;
                                addTimeSheetModels[i].SaturdayLogTimeId = filleddata.SaturdayLogTimeId;
                                addTimeSheetModels[i].SundayLogTime = filleddata.SundayLogTime;
                                addTimeSheetModels[i].SundayLogTimeId = filleddata.SundayLogTimeId;
                                addTimeSheetModels[i].Description = filleddata.Description;
                                addTimeSheetModels[i].DescriptionId = filleddata.DescriptionId;
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
        public ActionResult AddTime(List<AddTimeSheetModel> addTime)
        {
            string message = "";
            // again initializing the dropdownlist so the if anything goes wrong he can again select them
            ViewBag.Projects = DisplayProjectList();
            ViewBag.userDates = TempData["Dates"];
            var userdate = ViewBag.userDates[0];
            ViewBag.userWeek = TempData["WeekNo"];
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
                        timesheetmasterobj.TotalHours = (int)CalculateTotalHours(userrowdata);

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


                        Dictionary<DateTime, int> DaysWiseHrs = GettingDayWiseHrs1(userrowdata, userdate);
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
            foreach (var project in getprojects)
            {
                ListProjects.Add(new ProjectModel { Id = project.ProjectID, Name = project.ProjectName });
            }

            return ListProjects;
        }

        //non action method for calculating the total hours of the user time logging for a particular project
        [NonAction]
        public decimal? CalculateTotalHours(AddTimeSheetModel totalhours)
        {
            decimal? Total = 0;
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
                intMap.Add(userdate, (int)userrowdata.MondayLogTime);
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
                intMap.Add(userdate.AddDays(3), (int)userrowdata.ThursdayLogTime);
            }
            if (userrowdata.FridayLogTime != null)
            {
                intMap.Add(userdate.AddDays(4), (int)userrowdata.FridayLogTime);
            }
            if (userrowdata.SaturdayLogTime != null)
            {
                intMap.Add(userdate.AddDays(5), (int)userrowdata.SaturdayLogTime);
            }
            if (userrowdata.SundayLogTime != null)
            {
                intMap.Add(userdate.AddDays(6), (int)userrowdata.SundayLogTime);
            }
            if (userrowdata.SundayLogTime != null)
            {
                intMap.Add(userdate.AddDays(7), (int)userrowdata.SundayLogTime);
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
        public Dictionary<DateTime?, int?> GetDaysWiseHrsUserDataInDB(IList<TimeSheetDetail> timeSheetDetail)
        {
            Dictionary<DateTime?, int?> DaysWiseHrsUserDataInDB = new Dictionary<DateTime?, int?>();
            foreach (var vv in timeSheetDetail)
            {
                DaysWiseHrsUserDataInDB[vv.Date] = vv.Hours;
            }

            return DaysWiseHrsUserDataInDB;

        }




    }
}
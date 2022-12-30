using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using TSheet.Data;
using TSheet.IBL;
using TSheet.Models;

namespace TSheet.BL
{
    public class TimeSheetMasterRepository : ITimeSheetMaster
    {
        private TSheetDB _TSheetDB;
        public TimeSheetMasterRepository()
        {
            _TSheetDB = new TSheetDB();
        }
        public int AddTimeSheetMaster(TimeSheetMaster obj)
        {
            if (obj.Comment == null)
            {
                obj.Comment = "Write here";
            }
            _TSheetDB.TimeSheetMasters.Add(obj);
            return _TSheetDB.SaveChanges();

        }

        public IList<TimeSheetMaster> GetAllTimeSheetMasterRecord()
        {
            throw new NotImplementedException();
        }

        public TimeSheetMaster GetTimeSheetMaster(int id)
        {
            throw new NotImplementedException();
        }

        public IList<TimeSheetMaster> GetTimeSheetMasterByUserIDFromDate(int UserId, DateTime fromDate)
        {
            return _TSheetDB.TimeSheetMasters.Where(a => a.UserID == UserId && a.FromDate == fromDate).ToList();

        }

        public TimeSheetMaster GetTimeSheetMasterByUserIDFromDate(int UserId, DateTime fromDate, int id)
        {
            return _TSheetDB.TimeSheetMasters.Where(a => a.UserID == UserId && a.FromDate == fromDate && a.ProjectId == id).SingleOrDefault();

        }

        public IQueryable<TimeSheetMasterView> ShowTimeSheet(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var IQueryabletimesheet= (from timeSheetMaster in _TSheetDB.TimeSheetMasters
                                      where
                                    timeSheetMaster.UserID == UserID
                                      select new TimeSheetMasterView
                                      {
                                          TimeSheetStatus = timeSheetMaster.TimeSheetStatus,
                                          Comment = timeSheetMaster.Comment,
                                          TimeSheetMasterID = timeSheetMaster.TimeSheetMasterID,
                                          FromDate = SqlFunctions.DateName("day", timeSheetMaster.FromDate).Trim() + "/" +
                     SqlFunctions.StringConvert((double)timeSheetMaster.FromDate.Value.Month).TrimStart() + "/" +
                     SqlFunctions.DateName("year", timeSheetMaster.FromDate),
                                          ToDate = SqlFunctions.DateName("day", timeSheetMaster.ToDate).Trim() + "/" +
                     SqlFunctions.StringConvert((double)timeSheetMaster.ToDate.Value.Month).TrimStart() + "/" +
                     SqlFunctions.DateName("year", timeSheetMaster.ToDate),
                                          projectName = timeSheetMaster.ProjectMaster.ProjectName,
                                          TotalHours = timeSheetMaster.TotalHours
                                      }) ;
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
                
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet = IQueryabletimesheet.Where(m => m.FromDate.ToLower().Contains(Search.ToLower())|| m.Comment.ToLower().Contains(Search.ToLower())|| m.projectName.ToLower().Contains(Search.ToLower())|| m.FromDate.ToLower().Contains(Search.ToLower()));
            }
          
            return IQueryabletimesheet;
        }
    }
}

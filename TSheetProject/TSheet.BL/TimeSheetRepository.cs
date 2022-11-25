using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.IBL;
using TSheet.Data;
using TSheet.Models;

namespace TSheet.BL
{
    public class TimeSheetRepository : IUserTimeSheet
    {
        TSheetDB db;
        TimeSheetRepository()
        {
            db= new TSheetDB();
        }


        public IList<AllTimeSheetModel> AllTimeSheet()
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

        public IEnumerable<TimeSheetMaster> GetAllTimeSheets()
        {
            throw new NotImplementedException();
        }

        public int GetTimeSheetCount(TimeSheetMaster timeSheet)
        {
            throw new NotImplementedException();
        }

        public TimeSheetMaster GetTimeSheetMaster(int id)
        {
            throw new NotImplementedException();
        }
    }
}

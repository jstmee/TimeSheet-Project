using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.Data;
using TSheet.IBL;

namespace TSheet.BL
{
    public class TimeSheetDetailRepository : ITimeSheetDetail
    {
        private TSheetDB _tsheetdb;
        public TimeSheetDetailRepository() 
        {
            _tsheetdb=new TSheetDB();
        }
        public int AddTimeSheetDetail(TimeSheetDetail obj)
        {
            
            _tsheetdb.TimeSheetDetails.Add(obj);
            return _tsheetdb.SaveChanges();

        }

        public IList<TimeSheetDetail> GetAllTimeSheetDetailRecord()
        {
            throw new NotImplementedException();
        }

        public TimeSheetDetail GetTimeSheetDetail(int id)
        {
            throw new NotImplementedException();
        }

        public IList<TimeSheetDetail> GetAllTimeSheetDetailByMasterId(int id)
        {
            return _tsheetdb.TimeSheetDetails.Where(a => a.TimeSheetMasterID == id).ToList();
            
        }
    }
}

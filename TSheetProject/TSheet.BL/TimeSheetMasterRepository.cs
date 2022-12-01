﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.Data;
using TSheet.IBL;

namespace TSheet.BL
{
    public class TimeSheetMasterRepository : ITimeSheetMaster
    {
        private TSheetDB _TSheetDB;
        public TimeSheetMasterRepository()
        {
            _TSheetDB= new TSheetDB();
        }
        public int AddTimeSheetMaster(TimeSheetMaster obj)
        {
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
    }
}
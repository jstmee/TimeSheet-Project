using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.Data;

namespace TSheet.IBL
{
    public interface ITimeSheetMaster
    {
        IList<TimeSheetMaster> GetAllTimeSheetMasterRecord();

        int AddTimeSheetMaster(TimeSheetMaster obj);

        TimeSheetMaster GetTimeSheetMaster(int id);
    }
}

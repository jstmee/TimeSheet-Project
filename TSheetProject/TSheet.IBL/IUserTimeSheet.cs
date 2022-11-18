using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.Data;

namespace TSheet.IBL
{
    internal interface IUserTimeSheet
    {
        IEnumerable<TimeSheetMaster> GetAllTimeSheets();

        int GetTimeSheetCount(TimeSheetMaster timeSheet);

        TimeSheetMaster GetTimeSheetMaster(int id);

    }
}

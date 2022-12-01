using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.Data;

namespace TSheet.IBL
{
    public interface ITimeSheetDetail
    {
        IList<TimeSheetDetail> GetAllTimeSheetDetailRecord();

        int AddTimeSheetDetail(TimeSheetDetail obj);

        TimeSheetDetail GetTimeSheetDetail(int id);
    }
}

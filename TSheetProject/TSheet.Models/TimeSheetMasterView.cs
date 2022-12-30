using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSheet.Models
{
    public class TimeSheetMasterView
    {
        public int TimeSheetMasterID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public decimal? TotalHours { get; set; }
        public int UserID { get; set; }
        public string projectName { get; set; }
        public string CreatedOn { get; set; }
        public string Username { get; set; }
        public string SubmittedMonth { get; set; }
        public string TimeSheetStatus { get; set; }
        public string Comment { get; set; }
    }
}

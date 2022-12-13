using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSheet.Models
{
    public class ApproveRejectViewModel
    {
        public int? UserUniqueId { get; set; }
        public int AllTimesheetId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal? Hours { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string Status { get; set; }
        public string ProjectName { get; set; }
        public string Approve { get; set; }
        public string Reject { get; set; }



    }
}

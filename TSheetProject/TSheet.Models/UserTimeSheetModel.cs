using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSheet.Models
{
    public class UserTimeSheetModel
    {
        public int Id { get; set; }

        public Decimal? TotalHrs { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string Comment { get; set; }

        public string Status { get; set; }

        public string ProjectName { get; set; }
    }
}

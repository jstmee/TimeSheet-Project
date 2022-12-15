using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSheet.Models
{
    public class WeekInfoModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
        /*List<string> Project { get; set; }*/
       public int? Hours { get; set; }
       public string Status { get; set; }
       
    }
}

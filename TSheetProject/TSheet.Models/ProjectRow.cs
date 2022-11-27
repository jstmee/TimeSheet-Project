using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSheet.Models
{
    public class ProjectRow
    {

        public int Id { get; set; }
        public int? day1 { get; set; }
        public int? day2 { get; set; }
        public int? day3 { get; set; }
        public int? day4 { get; set; }
        public int? day5 { get; set; }
        public int? day6 { get; set; }
        public int? day7 { get; set; }
        public string comment { get; set; }
    }
}

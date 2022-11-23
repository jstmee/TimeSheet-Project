using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TSheet.Models
{
    internal class UserTimeModel
    {
        public string ProjectName { get; set; } 

        public int Hours { get; set; }  

        public DateTime Date { get; set; }

        public DateTime CreateOn { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TSheet.Modals
{
    internal class UserTimeModel
    {
        public string ProjectName { get; set; } 

        public int hours { get; set; }  

        public DateTime Date { get; set; }

        public DateTime CreateOn { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSheet.Models
{
    public class AllTimeSheetModel
    {
        public int? UserUniqueId { get; set; }
        public int AllTimesheetId { get; set; }
       public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal? Hours { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int TimeSheetDetailID { get; set; }

        public string Status { get; set; }
        public string ProjectName { get; set; }


    }
}

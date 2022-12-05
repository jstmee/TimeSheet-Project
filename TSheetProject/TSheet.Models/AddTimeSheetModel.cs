using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TSheet.Models
{
    public class AddTimeSheetModel
    {
        public int id { get; set; }

        public int? ProjectId { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "Enter Valid Hours")]
        public int? MondayLogTime { get; set; }
        public int? MondayLogTimeId { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "Enter Valid Hours")]
        public int? TuesdayLogTime { get; set; }
        public int? TuesdayLogTimeId { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "Enter Valid Hours")]
        public int? WednesdayLogTime { get; set; }

        public int? WednesdayLogTimeId { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "Enter Valid Hours")]
        public int? ThursdayLogTime { get; set; }

        public int? ThursdayLogTimeId { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "Enter Valid Hours")]
        public int? FridayLogTime { get; set; }

        public int? FridayLogTimeId { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "Enter Valid Hours")]
        public int? SaturdayLogTime { get; set; }

        public int? SaturdayLogTimeId { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "Enter Valid Hours")]
        public int? SundayLogTime { get; set; }

        public int? SundayLogTimeId { get; set; }


        [StringLength(50, ErrorMessage = "Length Exceeds")]
        public string Description { get; set; }

        public int? DescriptionId { get; set; }

    }
}

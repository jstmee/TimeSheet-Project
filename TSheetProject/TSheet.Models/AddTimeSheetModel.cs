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
    
        public string ProjectName { get; set; }


        [Range(0.0, 24.0, ErrorMessage = "0 - 24")]
        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "0 - 24")]
        public float? MondayLogTime { get; set; }

        /*[Range(0.0, 24.0, ErrorMessage = "Enter Valid Hours")]
        [RegularExpression(@"^\d+(\.\d+)?$")]*/
        public int? MondayLogTimeId { get; set; }

        /*[RegularExpression(@"^\d+(\.\d+)?$" , ErrorMessage = "Enter Only Decimal")]*/
        [Range(0.0, 24.0, ErrorMessage = "0 - 24")]
        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "0 - 24")]
        public float? TuesdayLogTime { get; set; }
        public int? TuesdayLogTimeId { get; set; }

        [Range(0.0, 24.0, ErrorMessage = "0 - 24")]
        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "0 - 24")]
        public float? WednesdayLogTime { get; set; }

        public int? WednesdayLogTimeId { get; set; }


        [Range(0.0, 24.0, ErrorMessage = "0 - 24")]
        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "0 - 24")]
        public float? ThursdayLogTime { get; set; }

        public int? ThursdayLogTimeId { get; set; }

        [Range(0.0, 24.0, ErrorMessage = "0 - 24")]
        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "0 - 24")]
        public float? FridayLogTime { get; set; }

        public int? FridayLogTimeId { get; set; }

        [Range(0.0, 24.0, ErrorMessage = "0-24")]
        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "0 - 24")]
        public float? SaturdayLogTime { get; set; }

        public int? SaturdayLogTimeId { get; set; }

        [Range(0.0, 24.0, ErrorMessage = "0 - 24")]
        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "0 - 24")]
        public float? SundayLogTime { get; set; }

        public int? SundayLogTimeId { get; set; }


        [StringLength(50, ErrorMessage = "Length Exceeds")]
        public string Description { get; set; }

        public int? DescriptionId { get; set; }

    }
}

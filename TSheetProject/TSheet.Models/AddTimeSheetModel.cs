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

        /*[RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "Enter Valid Hours")]*/
        /*[DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]*/
        public float? MondayLogTime { get; set; }
        public int? MondayLogTimeId { get; set; }
        /*[DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]*/

        /*[RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "Enter Valid Hours")]*/
        public float? TuesdayLogTime { get; set; }
        public int? TuesdayLogTimeId { get; set; }

        /*[RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "Enter Valid Hours")]*/
        /*[DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]*/
        public float? WednesdayLogTime { get; set; }

        public int? WednesdayLogTimeId { get; set; }

        public float? ThursdayLogTime { get; set; }

        public int? ThursdayLogTimeId { get; set; }

        public float? FridayLogTime { get; set; }

        public int? FridayLogTimeId { get; set; }

        public float? SaturdayLogTime { get; set; }

        public int? SaturdayLogTimeId { get; set; }

        public float? SundayLogTime { get; set; }

        public int? SundayLogTimeId { get; set; }


        [StringLength(50, ErrorMessage = "Length Exceeds")]
        public string Description { get; set; }

        public int? DescriptionId { get; set; }

    }
}

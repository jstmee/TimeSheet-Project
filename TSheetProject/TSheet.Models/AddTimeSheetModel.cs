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

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "Enter Valid Hours")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public decimal? MondayLogTime { get; set; }
        public int? MondayLogTimeId { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "Enter Valid Hours")]
        public decimal? TuesdayLogTime { get; set; }
        public int? TuesdayLogTimeId { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "Enter Valid Hours")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public decimal? WednesdayLogTime { get; set; }

        public int? WednesdayLogTimeId { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "Enter Valid Hours")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public decimal? ThursdayLogTime { get; set; }

        public int? ThursdayLogTimeId { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "Enter Valid Hours")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public decimal? FridayLogTime { get; set; }

        public int? FridayLogTimeId { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "Enter Valid Hours")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public decimal? SaturdayLogTime { get; set; }

        public int? SaturdayLogTimeId { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "Enter Valid Hours")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public decimal? SundayLogTime { get; set; }

        public int? SundayLogTimeId { get; set; }


        [StringLength(50, ErrorMessage = "Length Exceeds")]
        public string Description { get; set; }

        public int? DescriptionId { get; set; }

    }
}

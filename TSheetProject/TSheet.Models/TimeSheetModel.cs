using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSheet.Models
{
    public class TimeSheetModel
    {
        [Required]
        [Display(Name = "Select Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [RestrictedDate]
        public DateTime Date1 { get; set; }

        public DateTime? Date2 { get; set; }
        public DateTime? Date3 { get; set; }
        public DateTime? Date4 { get; set; }
        public DateTime? Date5 { get; set; }
        public DateTime? Date6 { get; set; }
        public DateTime? Date7 { get; set; }

        [Required(ErrorMessage = "Choose Project")]
        public int ProjectID1 { get; set; }
        public int? ProjectID2 { get; set; }
        public int? ProjectID3 { get; set; }
        public int? ProjectID4 { get; set; }

        //for project 1
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        [Required(ErrorMessage = "Enter Hours")]
        public int? Text1_ProjectID1 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text2_ProjectID1 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text3_ProjectID1 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text4_ProjectID1 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text5_ProjectID1 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text6_ProjectID1 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text7_ProjectID1 { get; set; }

        [StringLength(50, ErrorMessage = "Length Exceeds")]
        public string Description_ProjectID1 { get; set; }


        //for project 2

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text1_ProjectID2 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text2_ProjectID2 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text3_ProjectID2 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text4_ProjectID2 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text5_ProjectID2 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text6_ProjectID2 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "P0 to 24")]
        public int? Text7_ProjectID2 { get; set; }

        [StringLength(50, ErrorMessage = "Length Exceeds")]
        public string Description_ProjectID2 { get; set; }


        //start of project 3

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text1_ProjectID3 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text2_ProjectID3 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text3_ProjectID3 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text4_ProjectID3 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text5_ProjectID3 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text6_ProjectID3 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "P0 to 24")]
        public int? Text7_ProjectID3 { get; set; }

        [StringLength(50, ErrorMessage = "Length Exceeds")]
        public string Description_ProjectID3 { get; set; }

        //start of project4

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text1_ProjectID4 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text2_ProjectID4 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text3_ProjectID4 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text4_ProjectID4 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text5_ProjectID4 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "0 to 24")]
        public int? Text6_ProjectID4 { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        [Range(0, 24, ErrorMessage = "P0 to 24")]
        public int? Text7_ProjectID4 { get; set; }

        [StringLength(50, ErrorMessage = "Length Exceeds")]
        public string Description_ProjectID4 { get; set; }

        public int? totalhr_ProjectID1 { get; set; }
        public int? totalhr_ProjectID2 { get; set; }
        public int? totalhr_ProjectID3 { get; set; }
        public int? totalhr_ProjectID4 { get; set; }



    }

    public class RestrictedDate : ValidationAttribute
    {
        public override bool IsValid(object date)
        {
            DateTime date1 = (DateTime)date;
            return date1 < DateTime.Now;
        }
    }
}

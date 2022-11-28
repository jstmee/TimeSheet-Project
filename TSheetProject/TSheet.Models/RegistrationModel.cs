using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace TSheet.Models
{
    public class RegistrationModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Must be at least 6 characters")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password field is required")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]

        public string ConfirmPassword { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Phone Number Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string MobileNumber { get; set; }
        [Required(ErrorMessage = "Gender Required")]
        public string Gender { get; set; }
        [Required(ErrorMessage ="DateOfBirth is Required")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        /*[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]*/
        public DateTime? DateOfBirth { get; set; }
        [Display(Name = "Created On")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Updated On")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime UpdatedOn { get; set; }
        
        [Display(Name = "Edited By")]
        public string EditedBy { get; set; }
        /*[Required(ErrorMessage = "This field is required")]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }*/
        [Required(ErrorMessage = "This field is required")]
        public bool IsActive { get; set; }
        [Display(Name = "Date of Joining")]
        [DataType(DataType.Date)]
        /*[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:yyyy-MM-dd}")]*/
        public DateTime DateOfJoining { get; set; }
        [Display(Name = "Date of Leaving")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateOfLeaving { get; set; }
        public string CreatedBy { get; set; }

        [Display(Name = "Assign Role")]
        public int? AssignedRole { get; set; }

        [Display(Name = "Assign Project")]
        public int? AssignProject { get; set; }

    }


}



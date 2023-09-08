using AuthenticationJWT.Models.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationJWT.Models
{
    public class AlterPassword : User
    {
        [Required(ErrorMessage = "Please enter your current password")]
        [Display(Name = "Current Password")]
        [CompareUserPassword]
        public string Current { get; set; }

        [Required(ErrorMessage = "Please enter a new password")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Password should be of length 8-15 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Please enter a valid password.")]
        [Display(Name = "New Password")]
        public string New { get; set; }

        [Required(ErrorMessage = "Please enter your new password again")]
        [Display(Name = "Confirm Password")]
        [Compare("New", ErrorMessage = "Password doesn't match.")]
        public string Confirm { get; set; }
    }
}
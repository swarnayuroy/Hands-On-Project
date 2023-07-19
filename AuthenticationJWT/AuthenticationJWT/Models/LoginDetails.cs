using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationJWT.Models
{
    public class LoginDetails
    {
        [Required(ErrorMessage = "Please enter your email-id")]
        [RegularExpression(@"^[a-z][\w.]+@[a-z]+\.[a-z]{3}$", ErrorMessage = "Please enter a valid email-id.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
    }
}
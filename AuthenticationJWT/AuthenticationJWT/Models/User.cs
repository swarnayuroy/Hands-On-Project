using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthenticationJWT.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Name should be of charcaters less than 30")]
        [RegularExpression(@"^[A-Z]{1}[a-z]+((\s[A-Z]{1}[a-z]*)*)?$", ErrorMessage = "Please enter your name correctly.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email-id")]
        [StringLength(30, ErrorMessage = "Your email should be of charcaters less than 30")]
        [RegularExpression(@"^[a-z][\w.]+@[a-z]+\.[a-z]{3}$", ErrorMessage = "Please enter a valid email-id.")]
        [Remote("IsEmailInUse", "Login", ErrorMessage = "This email is already in use!")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please set a password")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Password should be of length 8-15 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Please enter a valid password.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
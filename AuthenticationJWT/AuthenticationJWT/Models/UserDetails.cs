using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationJWT.Models
{
    public class UserDetails: User
    {
        [Required(ErrorMessage = "Please enter your gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please enter your date of birth")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please enter your contact number")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Please enter your gender")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter your city")]
        public string City { get; set; }        

        [Required(ErrorMessage = "Please enter your gender")]
        public string Zip { get; set; }
        public DateTime RegisteredTime { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationJWT.Models
{
    public class UserDetails: User
    {
        public string Gender { get; set; }
        private DateTime DateOfBirth { get; set; }
        private string ContactNo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public DateTime RegisteredTime { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationJWT.Models
{
    public class Form
    {
        public LoginDetails Login { get; set; }
        public User Register { get; set; }
    }
}
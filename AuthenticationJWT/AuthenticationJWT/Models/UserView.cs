using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationJWT.Models
{
    public class UserView
    {
        private bool _isEditEnabled;
        public bool IsEditEnabled
        {
            get { return _isEditEnabled; }
            set { _isEditEnabled = value; }
        }
        public UserDetails User { get; set; }
    }
}
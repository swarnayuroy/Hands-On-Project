using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationJWT.Models
{
    public class UserView
    {
        private bool _isEditEnabaled;
        public bool IsEditEnabled
        {
            get { return _isEditEnabaled; }
            set { _isEditEnabaled = value; }
        }
        public UserDetails User { get; set; }
    }
}
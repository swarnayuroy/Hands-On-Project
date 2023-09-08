using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationJWT.Models.CustomValidation
{
    public class CompareUserPassword: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var requestChangePassword = (AlterPassword)validationContext.ObjectInstance;
            if (requestChangePassword.Password != requestChangePassword.Current)
            {
                return new ValidationResult("Doesn't match with your password");
            }
            return ValidationResult.Success;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIStudent.Models.Account
{
    public class LoginModel
    {
        [Display(Name = "User Name:")]
        [Required(ErrorMessage = "User Name is required.")]
        public string UserName { get; set; }

        [Display(Name = "Password:")]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me:")]
        public bool RememberMe { get; set; }
    }
}
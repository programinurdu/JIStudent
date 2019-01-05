using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIStudent.Models.Account
{
    public class UserProfileModel
    {
        [Display(Name = "Full Name:")]
        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; }

        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Display(Name = "Address:")]
        public string Address { get; set; }
    }
}
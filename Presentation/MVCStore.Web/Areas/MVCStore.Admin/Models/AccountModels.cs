using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCStore.Admin.Models
{
    public class LoginModel
    {

        [Display(Name = "User name")]
        [Required(ErrorMessage = "Please enter an email address to log in.")]
        [StringLength(64, ErrorMessage = "Invalid email address. Please try again.")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter a Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
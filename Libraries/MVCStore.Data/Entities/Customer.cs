﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;

namespace MVCStore.Data.Entities
{
    public class Customer
    {
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 6)]
        [Required(ErrorMessage = "Please enter a user name")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8)]
        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8)]
        [Required(ErrorMessage = "Password and Confirm Password should be same")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(128)]
        [Required(ErrorMessage = "Please enter a email")]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 6)]        
        [Required(ErrorMessage = "Please enter a question")]
        public string Question { get; set; }

        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 6)]        
        [Required(ErrorMessage = "Please enter a answer")]
        public string Answer { get; set; }

        public bool IsApproved { get; set; }

        public bool IsLockedOut { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? SelectedIndex { get; set; }
    }
}
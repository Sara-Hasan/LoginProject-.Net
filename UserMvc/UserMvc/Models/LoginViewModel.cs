﻿using System.ComponentModel.DataAnnotations;

namespace UserMvc.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter UserName")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

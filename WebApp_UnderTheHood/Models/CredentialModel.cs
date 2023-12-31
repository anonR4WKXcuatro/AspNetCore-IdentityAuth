﻿using System.ComponentModel.DataAnnotations;

namespace WebApp_UnderTheHood.Models
{
    public class CredentialModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}

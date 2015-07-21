using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.ComponentModel.DataAnnotations;

namespace JB2.AuthorizationServer
{

    public class LoginViewModel
    {
        public RegisterViewModel RegisterUser { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Birthday")]
        public System.DateTime Birthdate { get; set; }

        [Required]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "AcceptTerms")]
        public bool AcceptTerms { get; set; }

    }
}
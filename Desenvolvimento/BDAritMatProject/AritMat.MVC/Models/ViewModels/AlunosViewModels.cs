using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AritMat.MVC.Models.ViewModels
{
    public class AlunoLoginModel
    {
        [StringLength(75)]
        [Required]
        [Display(Name = "Nome de Utilizador")]
        public string Username { get; set; }

        [Required]
        [StringLength(75, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    public class AlunoRegisterModel
    {
        [StringLength(75)]
        [Required]
        [Display(Name = "Nome de Utilizador")]
        public string Username { get; set; }

        [Required]
        [StringLength(75, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date, ErrorMessage = "Insere data no formato AAAA-MM-DD")]
        public Nullable<System.DateTime> DataNasc { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
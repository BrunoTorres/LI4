using System;
using System.ComponentModel.DataAnnotations;

namespace AritMat.MVC.Models.ViewModels
{
    public class AlunoLoginModel
    {
        [StringLength(75)]
        [Required]
        [Display(Name = "Nome de Utilizador")]
        public string Username { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 3, ErrorMessage = "The {0} must be at least {2} characters long.")]
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
        [StringLength(75, MinimumLength = 10, ErrorMessage = "The {0} must be at least {2} characters long.")]
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
        public DateTime? DataNasc { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AlunoEditModel
    {
        [Required]
        public int IdAluno { get; set; }

        [Required]
        public string Username { get; set; }

        [StringLength(150)]
        [Required]
        public string Nome { get; set; }

        [StringLength(75,MinimumLength = 3)]
        [Required]
        public string Password { get; set; }


        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "Insere data no formato AAAA-MM-DD")]
        public DateTime? DataNasc { get; set; }

        [Range(0, 1)]
        [Required]
        [Display(Name = "Apresentar dica?")]
        public Boolean Dica { get; set; }

        [Range(1, 3)]
        [Required]
        [Display(Name = "Tema escolhido")]
        public int Tema { get; set; }

        [Range(0, 1)]
        [Required]
        [Display(Name = "Recomendar explicações?")]
        public Boolean Explicacao { get; set; }

        public AlunoEditModel(Aluno a)
        {
            IdAluno = a.IdAluno;
            Username = a.Username;
            Nome = a.Nome;
            Password = a.Password;
            DataNasc = a.DataNasc;
            Dica = a.Dica == 1;
            Tema = a.Tema;
            Explicacao = a.Explicacao == 1;
        }
    }

    public class AlunoDetailsModel
    {
        [Required]
        public int IdAluno { get; set; }

        [StringLength(150)]
        [Required]
        public string Nome { get; set; }


        [StringLength(75)]
        [Required]
        public string Username { get; set; }

        [StringLength(75)]
        [Required]
        public string Password { get; set; }

        
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "Insere data no formato AAAA-MM-DD")]
        public DateTime? DataNasc { get; set; }

        [Range(0, 1)]
        [Required]
        [Display(Name = "Apresentar dica?")]
        public Boolean Dica { get; set; }

        [Range(1, 3)]
        [Required]
        [Display(Name = "Tema escolhido")]
        public int Tema { get; set; }

        [Range(0, 1)]
        [Required]
        [Display(Name = "Recomendar explicações?")]
        public Boolean Explicacao { get; set; }

        public AlunoDetailsModel(Aluno a)
        {
            IdAluno = a.IdAluno;
            Username = a.Username;
            Nome = a.Nome;
            Password = a.Password;
            DataNasc = a.DataNasc;
            Dica = a.Dica == 1;
            Tema = a.Tema;
            Explicacao = a.Explicacao == 1;
        }

    }
}
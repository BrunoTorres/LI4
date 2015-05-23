using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AritMat.MVC.Models
{
    public class AlunoListMetadata
    {
        [StringLength(150)]
        [Required]
        public string Nome { get; set; }
        [StringLength(75)]
        [Required]
        [Display(Name = "Nome de Utilizador")]
        public string Username { get; set; }
        [StringLength(75)]
        [Required]
        public string Password { get; set; }
        [Display(Name = "Data de Nascimento" )]
        public DateTime? DataNasc { get; set; }
        [Range(0, 1)]
        [Required]
        [Display(Name = "Apresentar dica?")]
        public byte Dica { get; set; }
        [Range(1, 3)]
        [Required]
        [Display(Name = "Tema escolhido")]
        public int Tema { get; set; }
        [Range(0, 1)]
        [Required]
        [Display(Name = "Recomendar explicações?")]
        public byte Explicacao { get; set; }
        [Display(Name = "Pontuação")]
        [Required]
        public int Pontuacao { get; set; }
    }

   

}
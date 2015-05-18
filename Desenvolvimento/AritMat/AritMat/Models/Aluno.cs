namespace AritMat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Aluno")]
    public partial class Aluno
    {
        public Aluno()
        {
            AlunoExercicioLicao = new HashSet<AlunoExercicioLicao>();
            AlunoLicao = new HashSet<AlunoLicao>();
            AlunoTesteExercicio = new HashSet<AlunoTesteExercicio>();
            Aprendizagem = new HashSet<Aprendizagem>();
        }

        [Key]
        public int IdAluno { get; set; }

        [Required]
        [StringLength(75)]
        public string Nome { get; set; }

        [Required]
        [StringLength(75)]
        public string Username { get; set; }

        [Required]
        [StringLength(75)]
        public string Password { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataNasc { get; set; }

        public byte Dica { get; set; }

        public int Tema { get; set; }

        public byte Explicacao { get; set; }

        public int Pontuacao { get; set; }

        public virtual ICollection<AlunoExercicioLicao> AlunoExercicioLicao { get; set; }

        public virtual ICollection<AlunoLicao> AlunoLicao { get; set; }

        public virtual ICollection<AlunoTesteExercicio> AlunoTesteExercicio { get; set; }

        public virtual ICollection<Aprendizagem> Aprendizagem { get; set; }
    }
}

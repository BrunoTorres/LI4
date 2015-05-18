namespace AritMat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Exercicio")]
    public partial class Exercicio
    {
        public Exercicio()
        {
            AlunoExercicioLicao = new HashSet<AlunoExercicioLicao>();
            AlunoTesteExercicio = new HashSet<AlunoTesteExercicio>();
            Dica = new HashSet<Dica>();
            Resposta = new HashSet<Resposta>();
            Licao = new HashSet<Licao>();
            Teste = new HashSet<Teste>();
        }

        [Key]
        public int IdExercicio { get; set; }

        public int Tipo { get; set; }

        public int? Administrador { get; set; }

        [Column(TypeName = "text")]
        public string Texto { get; set; }

        public int Dificuldade { get; set; }

        public byte[] Imagem { get; set; }

        public TimeSpan TempoEx { get; set; }

        public virtual Administrador Administrador1 { get; set; }

        public virtual ICollection<AlunoExercicioLicao> AlunoExercicioLicao { get; set; }

        public virtual ICollection<AlunoTesteExercicio> AlunoTesteExercicio { get; set; }

        public virtual ICollection<Dica> Dica { get; set; }

        public virtual Tipo Tipo1 { get; set; }

        public virtual ICollection<Resposta> Resposta { get; set; }

        public virtual ICollection<Licao> Licao { get; set; }

        public virtual ICollection<Teste> Teste { get; set; }
    }
}

namespace AritMat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Licao")]
    public partial class Licao
    {
        public Licao()
        {
            AlunoExercicioLicao = new HashSet<AlunoExercicioLicao>();
            AlunoLicao = new HashSet<AlunoLicao>();
            Exercicio = new HashSet<Exercicio>();
        }

        [Key]
        [Column(Order = 0)]
        public int IdLicao { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumExpl { get; set; }

        public int Tipo { get; set; }

        public int? Administrador { get; set; }

        [Column(TypeName = "text")]
        public string Texto { get; set; }

        [Column(TypeName = "text")]
        public string Video { get; set; }

        public byte[] Imagem { get; set; }

        public TimeSpan TempoLicao { get; set; }

        public virtual Administrador Administrador1 { get; set; }

        public virtual ICollection<AlunoExercicioLicao> AlunoExercicioLicao { get; set; }

        public virtual ICollection<AlunoLicao> AlunoLicao { get; set; }

        public virtual Tipo Tipo1 { get; set; }

        public virtual ICollection<Exercicio> Exercicio { get; set; }
    }
}

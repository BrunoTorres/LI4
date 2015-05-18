namespace AritMat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AlunoExercicioLicao")]
    public partial class AlunoExercicioLicao
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Aluno { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Licao { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Explicacao { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Exercicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data { get; set; }

        public double? Resposta { get; set; }

        public virtual Aluno Aluno1 { get; set; }

        public virtual Licao Licao1 { get; set; }

        public virtual Exercicio Exercicio1 { get; set; }
    }
}

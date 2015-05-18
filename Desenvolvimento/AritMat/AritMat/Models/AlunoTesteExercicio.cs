namespace AritMat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AlunoTesteExercicio")]
    public partial class AlunoTesteExercicio
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Aluno { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Teste { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Exercicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data { get; set; }

        public double Nota { get; set; }

        public virtual Aluno Aluno1 { get; set; }

        public virtual Teste Teste1 { get; set; }

        public virtual Exercicio Exercicio1 { get; set; }
    }
}

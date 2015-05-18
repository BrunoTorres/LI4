namespace AritMat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Aprendizagem")]
    public partial class Aprendizagem
    {
        [Key]
        public int IdAprendizagem { get; set; }

        public int Aluno { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data { get; set; }

        public double Estado { get; set; }

        public int? Tipo { get; set; }

        public virtual Aluno Aluno1 { get; set; }
    }
}

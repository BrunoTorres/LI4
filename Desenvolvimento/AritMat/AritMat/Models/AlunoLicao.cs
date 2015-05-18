namespace AritMat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AlunoLicao")]
    public partial class AlunoLicao
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

        [Column(TypeName = "date")]
        public DateTime Data { get; set; }

        public int? RespErradas { get; set; }

        public virtual Aluno Aluno1 { get; set; }

        public virtual Licao Licao1 { get; set; }
    }
}

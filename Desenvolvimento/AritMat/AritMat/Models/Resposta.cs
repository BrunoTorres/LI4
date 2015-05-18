namespace AritMat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Resposta")]
    public partial class Resposta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdResposta { get; set; }

        public int Exercicio { get; set; }

        public int Pontuacao { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Texto { get; set; }

        public virtual Exercicio Exercicio1 { get; set; }
    }
}

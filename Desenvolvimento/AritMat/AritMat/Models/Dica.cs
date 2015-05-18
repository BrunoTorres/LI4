namespace AritMat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Dica")]
    public partial class Dica
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdDica { get; set; }

        public int Exercicio { get; set; }

        [Column(TypeName = "text")]
        public string Texto { get; set; }

        public byte[] Imagem { get; set; }

        public virtual Exercicio Exercicio1 { get; set; }
    }
}

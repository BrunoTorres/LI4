namespace AritMat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tipo")]
    public partial class Tipo
    {
        public Tipo()
        {
            Exercicio = new HashSet<Exercicio>();
            Licao = new HashSet<Licao>();
        }

        [Key]
        public int IdTipo { get; set; }

        [Required]
        [StringLength(150)]
        public string Area { get; set; }

        public virtual ICollection<Exercicio> Exercicio { get; set; }

        public virtual ICollection<Licao> Licao { get; set; }
    }
}

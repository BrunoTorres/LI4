namespace AritMat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Administrador")]
    public partial class Administrador
    {
        public Administrador()
        {
            Exercicio = new HashSet<Exercicio>();
            Licao = new HashSet<Licao>();
        }

        [Key]
        public int IdAdministrador { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataNasc { get; set; }

        [Required]
        [StringLength(75)]
        public string Username { get; set; }

        [Required]
        [StringLength(75)]
        public string Password { get; set; }

        [Required]
        [StringLength(75)]
        public string Nome { get; set; }

        public virtual ICollection<Exercicio> Exercicio { get; set; }

        public virtual ICollection<Licao> Licao { get; set; }
    }
}

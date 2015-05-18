namespace AritMat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Teste")]
    public partial class Teste
    {
        public Teste()
        {
            AlunoTesteExercicio = new HashSet<AlunoTesteExercicio>();
            Exercicio = new HashSet<Exercicio>();
        }

        [Key]
        public int IdTeste { get; set; }

        public double Dificuldade { get; set; }

        public virtual ICollection<AlunoTesteExercicio> AlunoTesteExercicio { get; set; }

        public virtual ICollection<Exercicio> Exercicio { get; set; }
    }
}

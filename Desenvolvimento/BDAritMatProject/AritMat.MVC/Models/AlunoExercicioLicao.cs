//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AritMat.MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AlunoExercicioLicao
    {
        public int Aluno { get; set; }
        public int Licao { get; set; }
        public int Explicacao { get; set; }
        public int Exercicio { get; set; }
        public System.DateTime Data { get; set; }
        public Nullable<double> Resposta { get; set; }
    
        public virtual Aluno Aluno1 { get; set; }
        public virtual Licao Licao1 { get; set; }
        public virtual Exercicio Exercicio1 { get; set; }
    }
}
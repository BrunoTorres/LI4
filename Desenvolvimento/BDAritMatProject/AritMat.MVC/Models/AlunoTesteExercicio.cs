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
    
    public partial class AlunoTesteExercicio
    {
        public int Aluno { get; set; }
        public int Teste { get; set; }
        public int Exercicio { get; set; }
        public System.DateTime Data { get; set; }
        public double Nota { get; set; }
    
        public virtual Aluno Aluno1 { get; set; }
        public virtual Teste Teste1 { get; set; }
        public virtual Exercicio Exercicio1 { get; set; }
    }
}

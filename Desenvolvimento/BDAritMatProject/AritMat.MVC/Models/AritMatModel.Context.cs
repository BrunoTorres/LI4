﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BDAritMatProjectEntities : DbContext
    {
        public BDAritMatProjectEntities()
            : base("name=BDAritMatProjectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Administrador> administradores { get; set; }
        public virtual DbSet<Aluno> Alunos { get; set; }
        public virtual DbSet<AlunoExercicioLicao> AlunoExercicioLicoes { get; set; }
        public virtual DbSet<AlunoLicao> AlunoLicoes { get; set; }
        public virtual DbSet<AlunoTesteExercicio> AlunoTesteExercicios { get; set; }
        public virtual DbSet<Aprendizagem> Aprendizagens { get; set; }
        public virtual DbSet<Dica> Dicas { get; set; }
        public virtual DbSet<Exercicio> Exercicios { get; set; }
        public virtual DbSet<Licao> Licoes { get; set; }
        public virtual DbSet<Resposta> Respostas { get; set; }
        public virtual DbSet<Teste> Testes { get; set; }
        public virtual DbSet<Tipo> Tipos { get; set; }
    }
}

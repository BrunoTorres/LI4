namespace AritMat.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AritMatDBEntities : DbContext
    {
        public AritMatDBEntities()
            : base("name=AritMatDBEntities")
        {
        }

        public virtual DbSet<Administrador> Administrador { get; set; }
        public virtual DbSet<Aluno> Aluno { get; set; }
        public virtual DbSet<AlunoExercicioLicao> AlunoExercicioLicao { get; set; }
        public virtual DbSet<AlunoLicao> AlunoLicao { get; set; }
        public virtual DbSet<AlunoTesteExercicio> AlunoTesteExercicio { get; set; }
        public virtual DbSet<Aprendizagem> Aprendizagem { get; set; }
        public virtual DbSet<Dica> Dica { get; set; }
        public virtual DbSet<Exercicio> Exercicio { get; set; }
        public virtual DbSet<Licao> Licao { get; set; }
        public virtual DbSet<Resposta> Resposta { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Teste> Teste { get; set; }
        public virtual DbSet<Tipo> Tipo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Administrador>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Administrador>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Administrador>()
                .HasMany(e => e.Exercicio)
                .WithOptional(e => e.Administrador1)
                .HasForeignKey(e => e.Administrador);

            modelBuilder.Entity<Administrador>()
                .HasMany(e => e.Licao)
                .WithOptional(e => e.Administrador1)
                .HasForeignKey(e => e.Administrador);

            modelBuilder.Entity<Aluno>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Aluno>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Aluno>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Aluno>()
                .HasMany(e => e.AlunoExercicioLicao)
                .WithRequired(e => e.Aluno1)
                .HasForeignKey(e => e.Aluno)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Aluno>()
                .HasMany(e => e.AlunoLicao)
                .WithRequired(e => e.Aluno1)
                .HasForeignKey(e => e.Aluno)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Aluno>()
                .HasMany(e => e.AlunoTesteExercicio)
                .WithRequired(e => e.Aluno1)
                .HasForeignKey(e => e.Aluno)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Aluno>()
                .HasMany(e => e.Aprendizagem)
                .WithRequired(e => e.Aluno1)
                .HasForeignKey(e => e.Aluno)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dica>()
                .Property(e => e.Texto)
                .IsUnicode(false);

            modelBuilder.Entity<Exercicio>()
                .Property(e => e.Texto)
                .IsUnicode(false);

            modelBuilder.Entity<Exercicio>()
                .HasMany(e => e.AlunoExercicioLicao)
                .WithRequired(e => e.Exercicio1)
                .HasForeignKey(e => e.Exercicio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Exercicio>()
                .HasMany(e => e.AlunoTesteExercicio)
                .WithRequired(e => e.Exercicio1)
                .HasForeignKey(e => e.Exercicio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Exercicio>()
                .HasMany(e => e.Dica)
                .WithRequired(e => e.Exercicio1)
                .HasForeignKey(e => e.Exercicio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Exercicio>()
                .HasMany(e => e.Resposta)
                .WithRequired(e => e.Exercicio1)
                .HasForeignKey(e => e.Exercicio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Licao>()
                .Property(e => e.Texto)
                .IsUnicode(false);

            modelBuilder.Entity<Licao>()
                .Property(e => e.Video)
                .IsUnicode(false);

            modelBuilder.Entity<Licao>()
                .HasMany(e => e.AlunoExercicioLicao)
                .WithRequired(e => e.Licao1)
                .HasForeignKey(e => new { e.Licao, e.Explicacao })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Licao>()
                .HasMany(e => e.AlunoLicao)
                .WithRequired(e => e.Licao1)
                .HasForeignKey(e => new { e.Licao, e.Explicacao })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Licao>()
                .HasMany(e => e.Exercicio)
                .WithMany(e => e.Licao)
                .Map(m => m.ToTable("LicaoExercicio").MapLeftKey(new[] { "Licao", "Explicacao" }).MapRightKey("Exercicio"));

            modelBuilder.Entity<Resposta>()
                .Property(e => e.Texto)
                .IsUnicode(false);

            modelBuilder.Entity<Teste>()
                .HasMany(e => e.AlunoTesteExercicio)
                .WithRequired(e => e.Teste1)
                .HasForeignKey(e => e.Teste)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teste>()
                .HasMany(e => e.Exercicio)
                .WithMany(e => e.Teste)
                .Map(m => m.ToTable("TesteExercicio").MapLeftKey("Teste").MapRightKey("Exercicio"));

            modelBuilder.Entity<Tipo>()
                .Property(e => e.Area)
                .IsUnicode(false);

            modelBuilder.Entity<Tipo>()
                .HasMany(e => e.Exercicio)
                .WithRequired(e => e.Tipo1)
                .HasForeignKey(e => e.Tipo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tipo>()
                .HasMany(e => e.Licao)
                .WithRequired(e => e.Tipo1)
                .HasForeignKey(e => e.Tipo)
                .WillCascadeOnDelete(false);
        }
    }
}

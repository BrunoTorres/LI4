using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AritMat.MVC.Models.ViewModels
{
    public class LicoesViewModel
    {
        public LicoesViewModel()
        {
            AlunoExercicioLicoes = new HashSet<AlunoExercicioLicao>();
            AlunoLicoes = new HashSet<AlunoLicao>();
            Exercicios = new HashSet<Exercicio>();
        }

        public LicoesViewModel(Licao l)
        {
            IdLicao = l.idLicao;
            NumExpl = l.NumExpl;
            Tipo = l.Tipo;
            Administrador = l.Administrador;
            Texto = l.Texto;
            Video = l.Video;
            TempoLicao = l.TempoLicao;
            Imagem = l.Imagem;

            AlunoExercicioLicoes = new HashSet<AlunoExercicioLicao>();
            AlunoLicoes = new HashSet<AlunoLicao>();
            Exercicios = new HashSet<Exercicio>();
        }
    
        public int IdLicao { get; set; }
        public int NumExpl { get; set; }
        public int Tipo { get; set; }
        public string Area { get; set; }
        public int? Administrador { get; set; }
        public string Texto { get; set; }
        public string Video { get; set; }
        public byte[] Imagem { get; set; }
        public int TempoLicao { get; set; }
    
        public virtual Administrador Administrador1 { get; set; }
        public virtual ICollection<AlunoExercicioLicao> AlunoExercicioLicoes { get; set; }
        public virtual ICollection<AlunoLicao> AlunoLicoes { get; set; }
        public virtual Tipo Tipo1 { get; set; }
        public virtual ICollection<Exercicio> Exercicios { get; set; }
    }
}
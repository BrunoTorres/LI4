using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AritMat.BOL
{
    public class Exercicio
    {
        private readonly int IdExer;
        private int Tipo;
        private int Administrador;
        private ElementoEstudo ElmEstudo;
        private int Dificuldade;
        private DateTime TempoEx;
        private Dictionary<int,Resposta> Respostas;
        private Dictionary<int,Dica> Dicas;


        //Falta
        //Verificar construtor
        public Exercicio(int id, int tipo, int adm, int dif, DateTime tmp, Dictionary<int,Resposta> r, Dica d, ElementoEstudo ee)
        {
            IdExer=id;
            Tipo=tipo;
            Administrador=adm;
            Dificuldade=dif;
            TempoEx=tmp;
            Respostas=r;
            Dica=d;
            ElmEstudo=ee;
        }

        //FALTAM GETS E SETS

        public int GetId(){return IdExer;}
        public int GetTipo(){return Tipo;}
        public int GetAdmin(){return Administrador;}
        public int GetDif(){return Dificuldade;}
        public DateTime GetTempo(){return TempoEx;}

        public void SetTipo(int t){Tipo=t;}
        public void SetAdmin(int a){Administrador=a;}
        public void SetDif(int dif){Dificuldade=dif;}
        public void SetTempo(DateTime t){TempoEx=t;}
    }
}
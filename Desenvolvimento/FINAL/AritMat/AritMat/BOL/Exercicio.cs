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
        private int Dificuldade;
        private DateTime TempoEx;
        private Dictionary<int,Resposta> Respostas;
        private Dica Dica;
        private ElementoEstudo ElmEstudo;

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

        public int getId(){return IdExer;}
        public int getTipo(){return Tipo;}
        public int getAdmin(){return Administrador;}
        public int getDif(){return Dificuldade;}
        public DateTime getTempo(){return TempoEx;}

        public void setTipo(int t){Tipo=t;}
        public void setAdmin(int a){Administrador=a;}
        public void setDif(int dif){Dificuldade=dif;}
        public void setTempo(DateTime t){TempoEx=t;}
    }
}
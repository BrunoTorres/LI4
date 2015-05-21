using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AritMat.BOL
{
    public class Licao
    {
        private ChaveLicao Id;
        private int Tipo;
        private int Administrador;
        private ElementoEstudo ElemEstudo;
        private DateTime TempoLicao;
        private string Area;
        private Dictionary<int,Exercicio> Exers;

        //Falta

        //Verificar construtor
        public Licao(ChaveLicao id, int tipo, int admin, DateTime tempo, string area)
        {
            Id=id;
            Tipo=tipo;
            Administrador=admin;
            TempoLicao=tempo;
            Area = area;
        }

        //FALTAM GETS E SETS

        public int GetTipo(){return Tipo;}
        public int GetAdministrador(){return Administrador;}
        public DateTime GetTempoLicao(){return TempoLicao;}

        public void SetTipo(int t){Tipo=t;}
        public void SetAdmin(int ad){Administrador=ad;}
    }
}
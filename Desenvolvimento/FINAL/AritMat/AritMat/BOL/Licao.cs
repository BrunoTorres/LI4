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
        private DateTime TempoLicao;
        private string Area;
        private ElementoEstudo ElemEstudo;
        private Exercicio Exrc;

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

        public int getTipo(){return Tipo;}
        public int getAdministrador(){return Administrador;}
        public DateTime getTempoLicao(){return TempoLicao;}

        public void setTipo(int t){Tipo=t;}
        public void setAdmin(int ad){Administrador=ad;}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AritMat.BOL
{
    public class Licao
    {
        private ChaveLicao id;
        private int tipo;
        private int administrador;
        private ElementoEstudo elemEstudo;
        private DateTime tempoLicao;
        private string area;
        private Dictionary<int, Exercicio> exercicios;

        //Falta

        //Verificar construtor
        public Licao(ChaveLicao idL, int t, int admin, DateTime tempo, string a)
        {
            id = idL;
            tipo = t;
            administrador = admin;
            tempoLicao = tempo;
            area = a;
        }

        //FALTAM GETS E SETS

        public int GetTipo()
        {
            return tipo;
        }

        public int GetAdministrador()
        {
            return administrador;
        }

        public DateTime GetTempoLicao()
        {
            return tempoLicao;
        }

        public void SetTipo(int t)
        {
            tipo = t;
        }

        public void SetAdmin(int ad)
        {
            administrador = ad;
        }
    }
}
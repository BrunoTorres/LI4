using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AritMat.BOL
{
    public class Dica
    {
        private readonly int IdDica;
        private int Exercicio;          
        private ElementoEstudo ElemEstudo;

        public Dica(int id)
        {
            IdDica=id;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AritMat.BOL
{
    public class Dica
    {
        private readonly int IdDica;
        private int Exercicio;          // TANTO NESTA COMO NO RESTO DAS CLASSES NAO TEM NADA PARA AS CHAVES ESTRANGEIRAS EU PUS NA MESMA MAS NAO SEI SE É PARA FICAR
        private ElementoEstudo ElemEstudo;

        public Dica(int id)
        {
            IdDica=id;
        }
    }
}
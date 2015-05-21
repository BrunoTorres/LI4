using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AritMat.BOL
{
    public class Dica
    {
        private readonly int idDica;
        private int exercicio;
        private ElementoEstudo elemEstudo;

        public Dica(int id, int exer, ElementoEstudo elem)
        {
            idDica = id;
            exercicio = exer;
            elemEstudo = elem;
        }

        public int GetId()
        {
            return idDica;
        }

        public int GetExercicio()
        {
            return exercicio;;
        }

        public ElementoEstudo GetElementoEstudo()
        {
            return elemEstudo;
        }

        public void SetExercicio(int ex)
        {
            exercicio = ex;
        }

        public void SetElementoEstudo(ElementoEstudo elem)
        {
            elemEstudo = elem;
        }
    }
}
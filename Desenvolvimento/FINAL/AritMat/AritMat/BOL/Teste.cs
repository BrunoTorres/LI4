using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AritMat.BOL
{
    public class Teste
    {

        private readonly int IdTeste;

        private float DificuldadeTeste;

        public Teste(int id, float dif)
        {
            IdTeste = id;
            DificuldadeTeste = dif;
        }

        public int GetId()
        {
            return IdTeste;
        }

        public float getDif()
        {
            return DificuldadeTeste;
        }

        public void SetDif(float dif)
        {
            DificuldadeTeste = dif;
        }

    }
}
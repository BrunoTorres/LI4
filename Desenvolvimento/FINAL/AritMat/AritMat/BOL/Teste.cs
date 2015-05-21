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
        private Dictionary<int, Exercicio> Exercicios;

        public Teste(int id, float dif)
        {
            IdTeste = id;
            DificuldadeTeste = dif;
        }

        public Teste(int id, float dif, Dictionary<int,Exercicio> exs)
        {
            IdTeste = id;
            DificuldadeTeste = dif;
            Exercicios = exs;
        }

        public int GetId() { return IdTeste; }

        public float GetDif() { return DificuldadeTeste; }

        public Dictionary<int, Exercicio> GetExs() { return Exercicios; }

        public void SetDif(float dif)
        {
            DificuldadeTeste = dif;
        }
        public void AddExercicio(Exercicio e)
        {
            Exercicios.Add(e.GetId(), e);
        }

    }
}
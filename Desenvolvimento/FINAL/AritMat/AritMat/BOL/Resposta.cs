using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AritMat.BOL
{
    public class Resposta
    {
        private readonly int IdResp;
        private int Exercicio;
        private int Pontuacao;
        private string Texto;

        public Resposta(int id, int ex, int pont, string text)
        {
            IdResp = id;
            Exercicio = ex;
            Pontuacao = pont;
            Texto = text;
        }
        //Falta get e set

        public int GetId() { return IdResp; }
        public int GetPontuacao() { return Pontuacao; }
        public string GetText() { return Texto; }

        public void SetPontuacao(int p) { Pontuacao = p; }
        public void SetText(string t) { Texto = t; }
    }
}
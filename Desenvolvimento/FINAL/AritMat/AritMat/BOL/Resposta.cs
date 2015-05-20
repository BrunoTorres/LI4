using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AritMat.BOL
{
    public class Resposta
    {
        private readonly int IdResp;
        private int Pontuacao;
        private string Texto;

        public Resposta(int id, int pont, string text)
        {
            IdResp = id;
            Pontuacao = pont;
            Texto = text;
        }

        public int getId() { return IdResp; }
        public int getPontuacao() { return Pontuacao; }
        public string getText() { return Texto; }

        public void setPontuacao(int p) { Pontuacao = p; }
        public void setText(string t) { Texto = t; }
    }
}
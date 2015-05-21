using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AritMat.BOL
{
    public class Aluno
    {
        private readonly int idAluno;
        private string nome;
        private string username;
        private string password;
        private DateTime dataNasc;
        private byte dica;
        private int tema;
        private byte explicacao;
        private int pontuacao;
        private Dictionary<int, Conhecimento> aprendizagem;
        private Dictionary<ChaveLicao, Licao> licoes;
        private Dictionary<int, Teste> testes; //ver 

        public Aluno(int id, string n, string uname, string pass, DateTime data, byte d, int t,
            byte exp, int pont)
        {
            idAluno = id;
            nome = n;
            username = uname;
            password = pass;
            dataNasc = data;
            dica = d;
            tema = t;
            explicacao = exp;
            pontuacao = pont;
        }

        public int GetId()
        {
            return idAluno;
        }

        public string GetNome()
        {
            return nome;
        }

        public string GetUsername()
        {
            return username;
        }

        public string GetPassword()
        {
            return password;
        }

        public DateTime GetDataNascimento()
        {
            return dataNasc;
        }

        public byte GetDica()
        {
            return dica;
        }

        public int GetTema()
        {
            return tema;
        }

        public byte GetExplicacao()
        {
            return explicacao;
        }

        public int GetPontuacao()
        {
            return pontuacao;
        }

        public Dictionary<int, Conhecimento> GetAprendizagem()
        {
            return aprendizagem;
        }

        //public int SetId() { return IdAluno; }
        public void SetNome(string n)
        {
            nome = n;
        }

        public void SetUsername(string uname)
        {
            username = uname;
        }

        public void SetPassword(string pass)
        {
            password = pass;
        }

        public void SetDataNascimento(DateTime data)
        {
            dataNasc = data;
        }

        public void SetDica(byte d)
        {
            dica = d;
        }

        public void SetTema(int t)
        {
            tema = t;
        }

        public void SetExplicacao(byte exp)
        {
            explicacao = exp;
        }

        public void SetPontuacao(int pont)
        {
            pontuacao = pont;
        }

        public void SetAprendizagem(Dictionary<int, Conhecimento> aprend)
        {
            aprendizagem = aprend;
        }
    }
}
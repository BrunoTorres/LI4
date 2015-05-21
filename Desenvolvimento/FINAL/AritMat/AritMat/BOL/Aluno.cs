using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AritMat.BOL
{
    public class Aluno
    {

        private readonly int IdAluno;
        private string Nome;
        private string Username;
        private string Password;
        private DateTime DataNasc;
        private byte Dica;
        private int Tema;
        private byte Explicacao;
        private int Pontuacao;
        private Dictionary<int, Conhecimento> Aprendizagem;
        private Dictionary<ChaveLicao, Licao> Licoes;
        private Dictionary<int, Teste> Testes;  //ver 

        public Aluno(int id, string nome, string username, string password, DateTime data, byte dica, int tema,
            byte explicacao, int pontuacao)
        {
            IdAluno = id;
            Nome = nome;
            Username = username;
            Password = password;
            DataNasc = data;
            Dica = dica;
            Tema = tema;
            Explicacao = explicacao;
            Pontuacao = pontuacao;
        }

        public int GetId() { return IdAluno; }
        public string GetNome() { return Nome; }
        public string GetUsername() { return Username; }
        public string GetPassword() { return Password; }
        public DateTime GetDataNascimento() { return DataNasc; }
        public byte GetDica() { return Dica; }
        public int GetTema(){ return Tema; }
        public byte GetExplicacao(){ return Explicacao; }
        public int GetPontuacao() { return Pontuacao; }

        //public int GetId() { return IdAluno; }
        public void SetNome(string nome) { Nome = nome; }
        public void SetUsername(string username) { Username = username; }
        public void SetPassword(string pass) { Password = pass; }
        public void SetDataNascimento(DateTime data) { DataNasc = data; }
        public void SetDica(byte dica) { Dica = dica; }
        public void SetTema(int tema) { Tema = tema; }
        public void SetExplicacao(byte explicacao) { Explicacao = explicacao; }
        public void SetPontuacao(int pontuacao) { Pontuacao = pontuacao; }
    }
}
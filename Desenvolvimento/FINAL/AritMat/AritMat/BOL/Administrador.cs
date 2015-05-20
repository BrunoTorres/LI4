using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AritMat.BOL
{
    public class Administrador
    {
        private readonly int IdAdmin;

        private DateTime DataNasc;
        
        private string Username;

        private string Password;

        private string Nome;

         public Administrador(int id, string nome, string username, string password, DateTime data)
        {
            IdAdmin = id;
            Nome = nome;
            Username = username;
            Password = password;
            DataNasc = data;
        }

        public int GetId() { return IdAdmin; }
        public string GetNome() { return Nome; }
        public string GetUsername() { return Username; }
        public string GetPassword() { return Password; }
        public DateTime GetDataNascimento() { return DataNasc; }

        public void SetNome(string nome) { Nome = nome; }
        public void SetUsername(string username) { Username = username; }
        public void SetPassword(string pass) { Password = pass; }
        public void SetDataNascimento(DateTime data) { DataNasc = data; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AritMat.BOL
{
    public class Conhecimento
    {
        private readonly int IdConhecimento;
        private int Aluno;
        private DateTime Data;
        private int Tipo;
        private float Estado;

        public Conhecimento(int id, int aluno, DateTime data, int tipo, float estado)
        {
            IdConhecimento = id;
            Aluno = aluno;
            Data = data;
            Tipo = tipo;
            Estado = estado;
        }

        public int GetId() { return IdConhecimento; }
        public int GetAluno() { return Aluno; }
        public DateTime GetData() { return Data; }
        public int GetTipo() { return Tipo; }
        public float GetEstado() { return Estado; }

        public void SetAluno(int aluno) { Aluno = aluno; }
        public void SetData(DateTime d) { Data = d; }
        public void SetTipo(int t) { Tipo = t; }
        public void SetEstado(float est) { Estado = est; }


    }
}
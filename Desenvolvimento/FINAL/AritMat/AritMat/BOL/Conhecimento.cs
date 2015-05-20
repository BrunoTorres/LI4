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

        public int getId() { return IdConhecimento; }
        public int getAluno() { return Aluno; }
        public DateTime getData() { return Data; }
        public int getTipo() { return Tipo; }
        public float getEstado() { return Estado; }

        public void setAluno(int aluno) { Aluno = aluno; }
        public void setData(DateTime d) { Data = d; }
        public void setTipo(int t) { Tipo = t; }
        public void setEstado(float est) { Estado = est; }


    }
}
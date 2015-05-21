using System.Collections.Generic;

namespace AritMat.BOL
{
    public class Exercicio
    {
        private readonly int idExercicio;
        private int administrador;
        private Dictionary<int, Dica> dicas;
        private int dificuldade;
        private ElementoEstudo elemEstudo;
        private Dictionary<int, Resposta> respostas;
        private int tempoEx;
        private int tipo;
        //Falta
        //Verificar construtor
        public Exercicio(int id, int t, int adm, int dif, int tmp, Dictionary<int, Resposta> r, Dictionary<int, Dica> d,
            ElementoEstudo ee)
        {
            idExercicio = id;
            tipo = t;
            administrador = adm;
            dificuldade = dif;
            tempoEx = tmp;
            respostas = r;
            dicas = d;
            elemEstudo = ee;
        }

        //FALTAM GETS E SETS

        public int GetId()
        {
            return idExercicio;
        }

        public int GetTipo()
        {
            return tipo;
        }

        public int GetAdmin()
        {
            return administrador;
        }

        public int GetDif()
        {
            return dificuldade;
        }

        public int GetTempo()
        {
            return tempoEx;
        }

        public void SetTipo(int t)
        {
            tipo = t;
        }

        public void SetAdmin(int a)
        {
            administrador = a;
        }

        public void SetDif(int dif)
        {
            dificuldade = dif;
        }

        public void SetTempo(int t)
        {
            tempoEx = t;
        }
    }
}
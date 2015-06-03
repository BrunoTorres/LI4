using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AritMat.MVC.Models;
using Microsoft.Ajax.Utilities;

namespace AritMat.MVC.DataAccess
{
    public class LicaoDAO
    {
        private BDAritMatProjectEntities db;
        private ExercicioDAO exercicioDAO;
        private TipoDAO tipoDAO;


        public LicaoDAO()
        {
            db = new BDAritMatProjectEntities();
            //exercicioDAO = new ExercicioDAO();
            //tipoDAO = new TipoDAO();
        }


        public Licao GetNextLicaoAluno(int idAluno)
        {
            int lastId = db.AlunoLicoes.OrderByDescending(a => a.Data).First(a => a.Aluno == idAluno).Licao;
            List<AlunoLicao> explsVistas = db.AlunoLicoes.OrderByDescending(a => a.Data).ToList();
            Licao l = new Licao();

            bool todasNull = true;
            int i = 0;

            while (i < explsVistas.Count && todasNull)
            {
                if (explsVistas.ElementAt(i).RespErradas != null)
                    todasNull = false;
                i++;
            }


            // ainda não fez nenhum exercício da última lição que viu
            if (todasNull)
            {
                l.idLicao = explsVistas.ElementAt(0).Licao;
                l.NumExpl = explsVistas.ElementAt(0).Explicacao;
                return l;
            }

            List<AlunoExercicioLicao> lista =
                db.AlunoExercicioLicoes.Where(a => a.Aluno == idAluno && a.Licao == lastId).ToList();

            int totExs = new ExercicioDAO().GetNumExerciciosTipo(GetTipoLicao(lastId));

            int total = 0, certas = 0;
            foreach (var aeel in lista)
            {
                total++;
                if (aeel.Resposta > 0)
                    certas++;
            }

            float percentCertas = certas/total;

            if (percentCertas > 0.75 && lista.Count > (0.5*totExs))
            {
                l.idLicao = lastId + 1;
                l.NumExpl = 1;
                return l;
            }

            bool existeProx = GetNumExplicacoesLicao(lastId) > explsVistas.ElementAt(0).Explicacao;

            if (existeProx)
                return db.Licoes.Find(lastId, explsVistas.ElementAt(0).Explicacao + 1);

            return db.Licoes.Find(lastId, 1);
        }

        private int GetNumExplicacoesLicao(int idLicao)
        {
            return db.Licoes.Count(l => l.idLicao == idLicao);
        }

        public int GetTipoLicao(int nextLicao)
        {
            return db.Licoes.SqlQuery("SELECT * FROM Licao WHERE IdLicao = " + nextLicao).First().Tipo;
        }

        public List<Licao> GetLicoesAdd()
        {
            return db.Licoes.Distinct().Where(l => l.Tipo == 1).ToList();
        }

        public List<Licao> GetLicoesSub()
        {
            return db.Licoes.Where(l => l.Tipo == 2).ToList();
        }
        /*public Exercicio GetNextExercicioLicaoAluno(int idAluno, int idLicao)
        {
            List<AlunoExercicioLicao> exercicios = exercicioDAO.GetExerciciosAlunoLicao(idAluno, idLicao);

            if(exercicios.Count == 0)
                return exercicioDAO.GetFirstExercicioTipo(GetTipoLicao(idLicao));

            AlunoExercicioLicao ael = new ExercicioDAO().GetLastExercicioDone(idAluno, idLicao);

            int dificuldade = db.Exercicios.Find(ael.Exercicio).Dificuldade;

            if (ael.Resposta > 0) // acertou resposta do último exercício
                // não está a verificar se já fez o exercício selecionado
                return
                    db.Exercicios.Where(ex => ex.Tipo == GetTipoLicao(idLicao) && ex.Dificuldade > dificuldade)
                        .OrderBy(ex => ex.Dificuldade)
                        .First();

            return db.Exercicios.Where(ex => ex.Dificuldade <= dificuldade && ex.IdExercicio != ael.Exercicio)
                        .OrderByDescending(ex => ex.Dificuldade)
                        .First();

        }*/

        public Licao GetLicao(int id, int exp)
        {
            return db.Licoes.Find(id, exp);
        }
    }
}
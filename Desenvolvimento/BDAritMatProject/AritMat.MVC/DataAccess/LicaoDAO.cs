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


        public int GetNextLicaoAluno(int idAluno)
        {
            AlunoExercicioLicao l = db.AlunoExercicioLicoes.SqlQuery("SELECT TOP 1 * FROM AlunoExercicioLicao WHERE Aluno = " + idAluno + " ORDER BY Data DESC").First();
            List<AlunoExercicioLicao> lista = db.AlunoExercicioLicoes.SqlQuery("SELECT * FROM AlunoExercicioLicao WHERE Aluno = " + idAluno + 
                " AND Licao = " + l.Licao).ToList();

            List<Exercicio> exs =
                db.Exercicios.SqlQuery("SELECT * FROM Exercicio WHERE Tipo = (SELECT DISTINCT Tipo FROM Licao WHERE IdLicao = " +
                                       l.Licao + ")").ToList();

            int totExs = exs.Count;

            int total = 0, certas = 0;
            foreach (var ael in lista)
            {
                total++;
                if (ael.Resposta > 0)
                    certas++;
            }

            float percentCertas = certas/total;

            if (percentCertas > 0.75 && lista.Count > (0.5*totExs))
                return l.Licao + 1;

            return l.Licao;
        }

        public int GetTipoLicao(int nextLicao)
        {
            return db.Licoes.SqlQuery("SELECT * FROM Licao WHERE IdLicao = " + nextLicao).First().Tipo;
        }

        public List<Licao> GetLicoesAdd()
        {
            return db.Licoes.Where(l => l.Tipo == 1).ToList();
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
    }
}
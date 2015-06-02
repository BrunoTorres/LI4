using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AritMat.MVC.Models;

namespace AritMat.MVC.DataAccess
{
    public class ExercicioDAO
    {
        private BDAritMatProjectEntities db;
        private LicaoDAO licaoDAO;

        public ExercicioDAO()
        {
            db = new BDAritMatProjectEntities();
            //licaoDAO = new LicaoDAO();
        }
        public List<AlunoExercicioLicao> GetExerciciosAlunoLicao(int idAluno, int idLicao)
        {
            return db.AlunoExercicioLicoes.Where(ael => ael.Licao == idLicao && ael.Aluno == idAluno).ToList();
        }

        public Exercicio GetFirstExercicioTipo(int tipoLicao)
        {
            /*return
                db.Exercicios.SqlQuery("SELECT * FROM Exercicio WHERE Tipo = " + tipoLicao + " ORDER BY Dificuldade ASC")
                    .ToList()
                    .First();*/

            return db.Exercicios.Where(ex => ex.Tipo == tipoLicao).OrderBy(ex => ex.Dificuldade).First();
        }

        public Exercicio GetNextExercicioLicaoAluno(int idAluno, int idLicao)
        {
            AlunoExercicioLicao ael =
                db.AlunoExercicioLicoes.Where(ex => ex.Aluno == idAluno && ex.Licao == idLicao)
                    .OrderByDescending(alexli => alexli.Data)
                    .First();

            if (ael == null)
                return GetFirstExercicioTipo(new LicaoDAO().GetTipoLicao(idLicao));

            int dificuldade = db.Exercicios.Find(ael.Exercicio).Dificuldade;

            if (ael.Resposta > 0)  // acertou resposta do último exercício
                
            {
                // não está a verificar se já fez o exercício selecionado
                int tipoLicao = new LicaoDAO().GetTipoLicao(idLicao);
                Exercicio exer =
                    db.Exercicios.Where(ex => ex.Tipo == tipoLicao && ex.Dificuldade > dificuldade)
                        .OrderBy(ex => ex.Dificuldade)
                        .First();

                exer.Respostas = new RespostaDAO().GetRespostasExercicio(exer.IdExercicio);

                return exer;

            }

            Exercicio exe = db.Exercicios.Where(ex => ex.Dificuldade <= dificuldade && ex.IdExercicio != ael.Exercicio)
                        .OrderByDescending(ex => ex.Dificuldade)
                        .First();

            exe.Respostas = new RespostaDAO().GetRespostasExercicio(exe.IdExercicio);

            return exe;
        }
    }
}
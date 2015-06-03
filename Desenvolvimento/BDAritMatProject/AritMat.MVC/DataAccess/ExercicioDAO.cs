using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

                List<Resposta> lResp = new RespostaDAO().GetRespostasExercicio(exer.IdExercicio);
                System.Diagnostics.Debug.WriteLine("R Count LISTA: " + lResp.Count);
                //exer.Respostas = new HashSet<Resposta>();

                //exer.Respostas.A

                System.Diagnostics.Debug.WriteLine("R Count IF: " + exer.Respostas.Count);

                return exer;

            }

            Exercicio exe = db.Exercicios.Where(ex => ex.Dificuldade <= dificuldade && ex.IdExercicio != ael.Exercicio)
                        .OrderByDescending(ex => ex.Dificuldade)
                        .First();

           // exe.Respostas = new HashSet<Resposta>(new RespostaDAO().GetRespostasExercicio(exe.IdExercicio));

            System.Diagnostics.Debug.WriteLine("R Count OUTSIDE IF: " + exe.Respostas.Count);
            
            return exe;
        }

        public int GetNumExerciciosTipo(int tipo)
        {
            return db.Exercicios.Count(a => a.Tipo == tipo);
        }

        public Exercicio GetExercicio(int id)
        {
            return db.Exercicios.Find(id);
        }

        public void UpdateExercicio(Exercicio exe)
        {
            db.Exercicios.AddOrUpdate(exe);
            db.SaveChanges();
        }
    }
}
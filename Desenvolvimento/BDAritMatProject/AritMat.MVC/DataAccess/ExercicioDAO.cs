using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using AritMat.MVC.Models;
using WebGrease.Css.Extensions;

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

        public Exercicio GetNextExercicioLicaoAluno(int idAluno, int idLicao, int exp)
        {
            Aluno a = db.Alunos.Find(idAluno);
            Licao l = db.Licoes.Find(idLicao, exp);

            System.Diagnostics.Debug.WriteLine("ID: " + l.idLicao + " | EXP: " + l.NumExpl);
            if (a.ExerciciosEmLicao.Count(li => li.Licao == idLicao) > 0)
            {

                List<AlunoExercicioLicao> ael = a.ExerciciosEmLicao.Where(ex => ex.Licao == idLicao)
                    .OrderByDescending(alexli => alexli.Data).ToList();

                if (!ael.Any())
                    return GetFirstExercicioLicao(idLicao);

                AlunoExercicioLicao ae = ael.First();

                int dificuldade = db.Exercicios.Find(ael.First().Exercicio).Dificuldade;

               // não está a verificar se já fez o exercício selecionado
                List<Exercicio> exerciciosLicao = new List<Exercicio>();
                List<Licao> expls = db.Licoes.Where(ll => ll.idLicao == l.idLicao).ToList();
                if (ae.Resposta > 0) // acertou resposta do último exercício
                {
                    
                    foreach (var lic in expls)
                    {
                        exerciciosLicao.AddRange(lic.ExerciciosDaLicao.OrderBy(e => e.Dificuldade).Where(e => e.Dificuldade > dificuldade).ToList());
                    }

                    // existem exercicios mais dificeis
                    if (exerciciosLicao.Count > 0)
                        return exerciciosLicao.First();

                    int r1 = new Random().Next(1, expls.Count);
                    System.Diagnostics.Debug.WriteLine("R1: " + r1);
                    Licao l1 = db.Licoes.Find(idLicao, r1);
                    int r2 = new Random().Next(0, l1.ExerciciosDaLicao.Count - 1);


                    return l1.ExerciciosDaLicao.ElementAt(r2);

                }

                // falhou resposta no último exercício
                // ir buscar todos os exercícios da lição atual em que a dificuldade é <= que o anterior e ainda nao tenha realizado
                foreach (var lic in expls)
                {
                    exerciciosLicao.AddRange(lic.ExerciciosDaLicao.OrderByDescending(e => e.Dificuldade).Where(e => e.Dificuldade <= dificuldade && e.IdExercicio != ae.Exercicio).ToList());
                }

                if (exerciciosLicao.Any())
                    return exerciciosLicao.First();

                int r11 = new Random().Next(1, expls.Count);
                Licao l11 = db.Licoes.Find(idLicao, r11);
                int r22 = new Random().Next(0, l11.ExerciciosDaLicao.Count - 1);


                return l11.ExerciciosDaLicao.ElementAt(r22);
            }

           return GetFirstExercicioLicao(idLicao);
        }

        private Exercicio GetFirstExercicioLicao(int idLicao)
        {
            return db.Licoes.Find(idLicao, 1).ExerciciosDaLicao.OrderBy(ex => ex.Dificuldade).First();
        }

        public int GetNumExerciciosTipo(int tipo)
        {
            return db.Exercicios.Count(a => a.Tipo == tipo);
        }

        public int GetNumExerciciosLicao(int idLicao)
        {
            int num = 0;
            foreach (var l in db.Licoes.Where(l => l.idLicao == idLicao))
            {
                num += l.ExerciciosDaLicao.Count;
            }
            return num;
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
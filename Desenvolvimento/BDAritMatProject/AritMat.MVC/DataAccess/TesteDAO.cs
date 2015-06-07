using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using AritMat.MVC.Models;
using Microsoft.Ajax.Utilities;

namespace AritMat.MVC.DataAccess
{
    public class TesteDAO
    {
        private BDAritMatProjectEntities db;
        ExercicioDAO exercicioDAO;

        public TesteDAO(BDAritMatProjectEntities bd)
        {
            db = bd;
            exercicioDAO = new ExercicioDAO(db);
        }
        
        public Teste GetTeste()
        {
            List<Exercicio> todosExs = exercicioDAO.GetTodosExercicios();
            Dictionary<int, Exercicio> exs = new Dictionary<int, Exercicio>();

            for (int i = 0; i < 8; i++)
            {
                int rNumber = new Random().Next(0, todosExs.Count - 1);
                while(exs.ContainsKey(todosExs.ElementAt(rNumber).IdExercicio))
                    rNumber = new Random().Next(0, todosExs.Count - 1);
                exs.Add(todosExs.ElementAt(rNumber).IdExercicio, todosExs.ElementAt(rNumber));
            }

            double dif = (double) exs.Values.Sum(ex => ex.Dificuldade) / 8;

            Teste t = new Teste
            {
                Dificuldade = dif,
                ExerciciosDoTeste = exs.Values
            };

            db.Testes.AddOrUpdate(t);

            foreach (var ex in exs.Values)
            {
                ex.TestesEmQueEsta.Add(t);
            }

            db.SaveChanges();

            

            return t;
        }

        public void RegistaRespostasTeste(int aluno, int teste, List<int> resps)
        {
            var dbTransaction = db.Database.BeginTransaction();
            try
            {
                foreach (var r in resps)
                {
                    Resposta rs = db.Respostas.Find(r);
                    AlunoTesteExercicio ate = new AlunoTesteExercicio
                    {
                        Aluno = aluno,
                        Data = DateTime.Now,
                        Exercicio = rs.Exercicio,
                        Teste = teste,
                        Nota = rs.Pontuacao
                    };
                    db.AlunoTesteExercicios.AddOrUpdate(ate);
                }
                db.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ROOOLLBACK!");
                dbTransaction.Rollback();
            }
        }

        public int GetNumTestesFeitosAluno(int aluno)
        {
            return db.AlunoTesteExercicios.DistinctBy(al => al.Teste).Count(al => al.Aluno == aluno);
        }

        public double GetPercentTestesAluno(int aluno)
        {
            Aluno a = db.Alunos.Find(aluno);
            Dictionary<int, List<AlunoTesteExercicio>> testes = new Dictionary<int, List<AlunoTesteExercicio>>();

            foreach (var ext in a.ExerciciosEmTestes)
            {
                if (!testes.ContainsKey(ext.Teste))
                {
                    List<AlunoTesteExercicio> l = new List<AlunoTesteExercicio>();
                    l.Add(ext);
                    testes.Add(ext.Teste, l);
                }
                else
                {
                    testes[ext.Teste].Add(ext);
                }
            }

            Dictionary<int, double> pontuacoes = new Dictionary<int, double>();
            foreach (var t in testes.Keys)
            {
                pontuacoes.Add(t, 0.0);
                foreach (var listElem in testes[t])
                {
                    pontuacoes[t] += listElem.Nota > 0 ? 1 : 0;
                }
            }

            // medias dos testes
            double somaMedias = 0.0;
            Dictionary<int, double> medias = new Dictionary<int, double>();
            foreach (var pont in pontuacoes.Keys)
            {
                medias.Add(pont, pontuacoes[pont] / 8);
                somaMedias += medias[pont];
            }

            double mediaTestes = somaMedias / pontuacoes.Count;

            return mediaTestes;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using AritMat.MVC.Models;

namespace AritMat.MVC.DataAccess
{
    public class TesteDAO
    {
        BDAritMatProjectEntities db = new BDAritMatProjectEntities();
        ExercicioDAO exercicioDAO;

        public TesteDAO()
        {
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
                dbTransaction.Rollback();
            }
        }
    }
}
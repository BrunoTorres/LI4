using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using AritMat.MVC.Models;
using AritMat.MVC.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace AritMat.MVC.DataAccess
{
    public class AlunoDAO
    {
        public Boolean CheckLogin(AlunoLoginModel m)
        {
            using (var context = new BDAritMatProjectEntities())
            {
                var query = from a in context.Alunos
                    where a.Username.Equals(m.Username) && a.Password.Equals(m.Password)
                    select a;

                try
                {
                    var aluno = query.Single();

                    return true;
                }
                catch (InvalidOperationException ex)
                {
                    return false;
                }
            }
        }

        public Aluno GetAluno(string username)
        {
            BDAritMatProjectEntities db = new BDAritMatProjectEntities();
            return db.Alunos.First(al => al.Username.Equals(username));
        }

        public Boolean AddAluno(AlunoRegisterModel m)
        {

            using (var context = new BDAritMatProjectEntities())
            {
                Aluno al = new Aluno();
                al.Username = m.Username;
                al.Nome = m.Nome;
                al.DataNasc = m.DataNasc;
                al.Password = m.Password;
                al.Pontuacao = 0;
                al.Explicacao = 1;
                al.Dica = 1;
                al.Tema = 1;
                try
                {
                    context.Entry(al).State = EntityState.Added;
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
        }
      

        public bool ChangeAluno(Aluno aluno)
        {
            using (var context = new BDAritMatProjectEntities())
            {
                Aluno al = new Aluno
                {
                    Username = aluno.Username,
                    Nome = aluno.Nome,
                    DataNasc = aluno.DataNasc,
                    Password = aluno.Password,
                    Pontuacao = aluno.Pontuacao,
                    Explicacao = aluno.Explicacao,
                    Dica = aluno.Dica,
                    Tema = aluno.Tema
                };
                try
                {
                    context.Entry(aluno).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
                catch (DbEntityValidationException ex)
                {
                    List<string> error = new List<string>();
                    foreach (DbEntityValidationResult db in ex.EntityValidationErrors)
                    {
                        String entityname = db.Entry.Entity.GetType().Name;
                        foreach (DbValidationError dbe in db.ValidationErrors)
                        {
                            //error.Add(entityname + "." + dbe.PropertyName + ": " + dbe.ErrorMessage);
                            System.Diagnostics.Debug.WriteLine(entityname + "." + dbe.PropertyName + ": " + dbe.ErrorMessage);
                        }
                    }
                    
                     return false;
                }
            }

        }
    }
}
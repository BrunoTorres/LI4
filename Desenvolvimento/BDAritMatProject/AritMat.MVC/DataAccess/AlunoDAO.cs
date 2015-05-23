using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using AritMat.MVC.Models;
using AritMat.MVC.Models.ViewModels;

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
    

}
}
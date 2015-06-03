using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AritMat.MVC.Models;

namespace AritMat.MVC.DataAccess
{
    public class RespostaDAO
    {
        private BDAritMatProjectEntities db;

        public RespostaDAO()
        {
            db = new BDAritMatProjectEntities();
        }

        public List<Resposta> GetRespostasExercicio(int idExercicio)
        {
            System.Diagnostics.Debug.WriteLine("SELECT * FROM Resposta WHERE Exercicio = " + idExercicio + " COUNT: " + db.Respostas.Where(ex => ex.Exercicio == idExercicio).ToList().Count);
            return db.Respostas.SqlQuery("SELECT * FROM Resposta WHERE Exercicio = " + idExercicio).ToList();
        } 
    }
}
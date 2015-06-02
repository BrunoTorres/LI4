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
            return db.Respostas.Where(r => r.Exercicio == idExercicio).ToList();
        } 
    }
}
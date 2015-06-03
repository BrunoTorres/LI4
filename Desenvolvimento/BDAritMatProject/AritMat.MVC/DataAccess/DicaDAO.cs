using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AritMat.MVC.Models;

namespace AritMat.MVC.DataAccess
{
    public class DicaDAO
    {

        BDAritMatProjectEntities db = new BDAritMatProjectEntities();

        public List<Dica> GetDicasExercicio(int idExercicio)
        {
            return db.Dicas.Where(d => d.Exercicio == idExercicio).ToList();
        }
    }

    
}
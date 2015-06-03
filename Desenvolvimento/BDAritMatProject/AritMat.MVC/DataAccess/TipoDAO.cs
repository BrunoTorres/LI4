using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AritMat.MVC.Models;

namespace AritMat.MVC.DataAccess
{
    public class TipoDAO
    {
        private BDAritMatProjectEntities db;

        public TipoDAO()
        {
            db = new BDAritMatProjectEntities();
        }

        public Tipo GetTipoLicao(int idTipo)
        {
            return db.Tipos.Find(idTipo);
        }
    }
}
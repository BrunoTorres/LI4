using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlServerCe;
using System.Data;
using System.Drawing;
using AritMat.BOL;

namespace AritMat.DAL
{
    public class TesteDAO
    {
        public TesteDAO()
        {
        }

        private static readonly string SELECT_TESTE_ID = "SELECT * FROM Teste WHERE IdTeste = @id";

        public Teste GetById(SqlCeConnection conn, int id)
        {
            Teste teste = null;

            DataTable dt = null;

            dt = GeralDAO.Query("SELECT * FROM Teste WHERE IdTeste = " + id, conn);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    teste = new Teste(int.Parse(dt.Rows[0][0].ToString()), float.Parse(dt.Rows[0][1].ToString()));
                }
            }

            return teste;
        }

        public void AddTeste(SqlCeConnection conn, Teste t)
        {
            string q = "INSERT INTO Teste " + // AQUI NÃO É INSERT INTO TABLE
                       "VALUES (" + t.GetDif() + ")";

            GeralDAO.Execute(q, conn);
        }
    }
}
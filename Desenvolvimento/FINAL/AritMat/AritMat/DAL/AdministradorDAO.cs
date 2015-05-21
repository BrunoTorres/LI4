using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Web;
using AritMat.BOL;

namespace AritMat.DAL
{
    public class AdministradorDAO
    {
        public Administrador GetById(SqlCeConnection conn, int id)
        {
            Administrador Administrador = null;

            DataTable dt = null;

            dt = GeralDAO.Query("SELECT * FROM Administrador WHERE IdAdministrador = " + id, conn);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Administrador = new Administrador(int.Parse(dt.Rows[0][0].ToString()),
                        DateTime.Parse(dt.Rows[0][1].ToString()), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), dt.Rows[0][4].ToString());
                }
            }

            return Administrador;
        }

        public Administrador GetByUsername(SqlCeConnection conn, string admin)
        {
            Administrador Administrador = null;

            DataTable dt = null;

            dt = GeralDAO.Query("SELECT * FROM Administrador WHERE Username = " + admin, conn);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Administrador = new Administrador(int.Parse(dt.Rows[0][0].ToString()),
                        DateTime.Parse(dt.Rows[0][1].ToString()), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), dt.Rows[0][4].ToString());
                }
            }

            return Administrador;
        }

        public void AddAdministrador(SqlCeConnection conn, Administrador a)
        {
            string q = "INSERT INTO Administrador " +
                       "VALUES (" +
                       a.GetNome() + "," + a.GetDataNascimento() + "," + a.GetUsername() + "," + a.GetPassword() +
                       ")";

            GeralDAO.Execute(q, conn);
        }
    }
}
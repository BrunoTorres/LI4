using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Web;

namespace AritMat.DAL
{
    public class TipoDAO
    {
        public string GetAreaById(int id, SqlCeConnection conn)
        {
            DataTable dt = null;

            dt = GeralDAO.Query("SELECT Area FROM Tipo WHERE IdTipo = " + id, conn);

            return (dt != null && dt.Rows.Count > 0) ? dt.Rows[0][0].ToString() : null;
        }
    }
}
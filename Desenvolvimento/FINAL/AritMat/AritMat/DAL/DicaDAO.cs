using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Web;
using AritMat.BOL;

namespace AritMat.DAL
{
    public class DicaDAO
    {

        public Dictionary<int, Dica> GetDicasByExercicioId(int idEx, SqlCeConnection conn)
        {
            DataTable dtResp = GeralDAO.Query("SELECT * FROM Dica WHERE Exercicio = " + idEx, conn);

            Dictionary<int, Dica> dicas = new Dictionary<int, Dica>();
            foreach (DataRow row in dtResp.Rows)
            {
                Dica d = new Dica(int.Parse(row[0].ToString()),
                        int.Parse(row[1].ToString()), GeralDAO.GetElementoEstudo(row));
                dicas.Add(d.GetId(), d);
            }

            return dicas;
        }
    }
}
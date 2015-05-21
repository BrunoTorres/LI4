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
    public class RespostaDAO
    {
         public RespostaDAO() {}

        private static readonly string SELECT_RESPOSTA_ID = "SELECT * FROM Resposta WHERE IdResposta = @id";

        public Resposta GetById(SqlCeConnection conn, int id)
        {
            Resposta resposta = null;

            DataTable dt = null;//        public Resposta(int id, int pont, string text)

            dt = GeralDAO.Query("SELECT * FROM Resposta WHERE IdResposta = " + id, conn);
            
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    resposta = new Resposta(int.Parse(dt.Rows[0][0].ToString()), int.Parse(dt.Rows[0][1].ToString()),
                        int.Parse(dt.Rows[0][2].ToString()), dt.Rows[0][3].ToString()); //Acho que está mal porque ainda falta o exercicio
                }
            }

            return resposta;
        }

        public Dictionary<int, Resposta> GetRespostasByExercicioId(int idEx, SqlCeConnection conn)
        {
            DataTable dtResp = GeralDAO.Query("SELECT * FROM Resposta WHERE Exercicio = " + idEx, conn);

            Dictionary<int, Resposta> resps = new Dictionary<int, Resposta>();
            foreach (DataRow row in dtResp.Rows)
            {
                Resposta r = new Resposta(int.Parse(row[0].ToString()),
                    int.Parse(row[1].ToString()), int.Parse(row[2].ToString()),
                    row[0].ToString());
                resps.Add(r.GetId(), r);
            }

            return resps;
        }
        

        public void AddResposta(SqlCeConnection conn, Resposta r)
        {
            string q = "INSERT INTO Resposta " +
                       "VALUES (" + r.GetPontuacao() + "," + r.GetText() + 
                       ")";

            GeralDAO.Execute(q, conn);
        }

    }
}
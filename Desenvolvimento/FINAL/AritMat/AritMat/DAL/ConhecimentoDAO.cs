using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Web;
using AritMat.BOL;

namespace AritMat.DAL
{
    public class ConhecimentoDAO
    {
        public Conhecimento GetById(SqlCeConnection conn, int id)
        {
            Conhecimento c = null;

            DataTable dt = null;

            dt = GeralDAO.Query("SELECT * FROM Conhecimento WHERE IdConhecimento = " + id, conn);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    c = new Conhecimento(int.Parse(dt.Rows[0][0].ToString()), int.Parse(dt.Rows[0][1].ToString()),
                        DateTime.Parse(dt.Rows[0][2].ToString()), int.Parse(dt.Rows[0][3].ToString()),
                        float.Parse(dt.Rows[0][4].ToString()));
                }
            }

            return c;
        }

        public Dictionary<int, Conhecimento> GetAprendizagemByAlunoId(int id, SqlCeConnection conn)
        {
            DataTable dt = GeralDAO.Query("SELECT * FROM Aprendizagem WHERE Aluno = " + id, conn);

            Dictionary<int, Conhecimento> aprend = new Dictionary<int, Conhecimento>();
            foreach (DataRow row in dt.Rows)
            {
                Conhecimento c = new Conhecimento(int.Parse(dt.Rows[0][0].ToString()), int.Parse(dt.Rows[0][1].ToString()),
                        DateTime.Parse(dt.Rows[0][2].ToString()), int.Parse(dt.Rows[0][3].ToString()),
                        float.Parse(dt.Rows[0][4].ToString()));
                aprend.Add(c.GetId(), c);
            }

            return aprend;
        }

        public float GetEstadoAtualAluno(int id, SqlCeConnection conn)
        {
            float r = 0.0f;
            DataTable dt = GeralDAO.Query("SELECT Estado FROM Aprendizagem WHERE Aluno = " + id, conn);

            return dt != null ? float.Parse(dt.Rows[0][0].ToString()) : r;
        }
    }
}
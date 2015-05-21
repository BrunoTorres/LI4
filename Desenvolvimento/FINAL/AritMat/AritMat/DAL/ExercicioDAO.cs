using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Web;
using AritMat.BOL;
using System.Drawing;
using System.IO;

namespace AritMat.DAL
{
    public class ExercicioDAO
    {
        private readonly DicaDAO dicaDAO = new DicaDAO();
        private TipoDAO tipoDAO = new TipoDAO();
        private readonly RespostaDAO respostaDAO = new RespostaDAO();

        public ExercicioDAO()
        {
            dicaDAO = new DicaDAO();
            tipoDAO = new TipoDAO();
            respostaDAO = new RespostaDAO();
        }

        public Exercicio GetById(SqlCeConnection conn, int id)
        {
            Exercicio e = null;

            DataTable dt = null;

            dt = GeralDAO.Query("SELECT * FROM Exercicio WHERE IdExercicio = " + id, conn);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    int idEx = int.Parse(dt.Rows[0][0].ToString());

                    Dictionary<int, Resposta> resps = respostaDAO.GetRespostasByExercicioId(idEx, conn);
                    Dictionary<int, Dica> dicas = dicaDAO.GetDicasByExercicioId(idEx, conn);

                    e = new Exercicio(int.Parse(dt.Rows[0][0].ToString()),
                        int.Parse(dt.Rows[0][1].ToString()), int.Parse(dt.Rows[0][2].ToString()),
                        int.Parse(dt.Rows[0][3].ToString()), int.Parse(dt.Rows[0][4].ToString()),
                        resps, dicas, GeralDAO.GetElementoEstudo(dt.Rows[0]));
                }
            }

            return e;
        }

        public void AddExercicio(SqlCeConnection conn, Exercicio e)
        {
            throw
                new
                    NotImplementedException("NO ADD EXERCICIO");
        }
    }
}
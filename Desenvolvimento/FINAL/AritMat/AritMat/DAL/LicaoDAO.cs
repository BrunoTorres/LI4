using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Web;
using AritMat.BOL;
using AritMat.Exceptions;

namespace AritMat.DAL
{
    public class LicaoDAO
    {
        private TipoDAO tipoDAO;

        public LicaoDAO()
        {
            tipoDAO = new TipoDAO();
        }

        public Licao GetById(SqlCeConnection conn, ChaveLicao id)
        {
            Licao l = null;

            DataTable dt = null;

            dt =
                GeralDAO.Query(
                    "SELECT * FROM Licao WHERE IdLicao = " + id.GetIdLicao() + " AND NumExpl = " + id.GetIdExpl(), conn);

            if (dt != null && dt.Rows.Count > 0)
            {
                int tipo = int.Parse(dt.Rows[0][2].ToString());

                string area = tipoDAO.GetAreaById(tipo, conn);
                l = new Licao(new ChaveLicao(int.Parse(dt.Rows[0][0].ToString()), int.Parse(dt.Rows[0][1].ToString())),
                    tipo, int.Parse(dt.Rows[0][3].ToString()),
                    DateTime.Parse(dt.Rows[0][4].ToString()), area);
            }

            return l;
        }

        public Licao GetNextExplicacao(SqlCeConnection conn, int idLicao, int idAluno)
        {
            Licao l = null;

            DataTable dt = GeralDAO.Query("SELECT MAX(Explicacao) FROM AlunoLicao WHERE Aluno = " + idAluno +
                                          " AND Licao = " + idLicao, conn);

            int nextExpl = (dt.Rows.Count > 0) ? (int.Parse(dt.Rows[0][0].ToString()) + 1) : 0;

            dt = GeralDAO.Query(
                    "SELECT * FROM Licao WHERE IdLicao = " + idLicao + " AND NumExpl = " + nextExpl, conn);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    int tipo = int.Parse(dt.Rows[0][2].ToString());

                    string area = tipoDAO.GetAreaById(tipo, conn);
                    l =
                        new Licao(
                            new ChaveLicao(int.Parse(dt.Rows[0][0].ToString()), int.Parse(dt.Rows[0][1].ToString())),
                            tipo, int.Parse(dt.Rows[0][3].ToString()),
                            DateTime.Parse(dt.Rows[0][4].ToString()), area);
                }
                else
                {
                    throw new LastExplicacaoException("Não existem mais explicações para esta lição");
                }
            }

            return l;
        }
    }
}

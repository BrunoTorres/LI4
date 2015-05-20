using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Linq;
using System.Web;
using AritMat.BOL;

namespace AritMat.DAL
{
    public class AlunoDAO
    {

        public AlunoDAO() {}

        private static readonly string SELECT_ALUNO_ID = "SELECT * FROM Aluno WHERE IdAluno = @id";

        public Aluno GetById(SqlCeConnection conn, int id)
        {
            Aluno aluno = null;

            DataTable dt = null;

            dt = GeralDAO.Query("SELECT * FROM Aluno WHERE IdAluno = " + id, conn);
            
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    aluno = new Aluno(int.Parse(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), DateTime.Parse(dt.Rows[0][4].ToString()), byte.Parse(dt.Rows[0][5].ToString()), int.Parse(dt.Rows[0][6].ToString()), byte.Parse(dt.Rows[0][7].ToString()), int.Parse(dt.Rows[0][7].ToString());
                }
            }

            return aluno;
        }

        public Aluno GetByUsername(SqlCeConnection conn, string user)
        {
            Aluno aluno = null;

            DataTable dt = null;

            dt = GeralDAO.Query("SELECT * FROM Aluno WHERE Username = " + user, conn);
            
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    aluno = new Aluno(int.Parse(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), DateTime.Parse(dt.Rows[0][4].ToString()), byte.Parse(dt.Rows[0][5].ToString()), int.Parse(dt.Rows[0][6].ToString()), byte.Parse(dt.Rows[0][7].ToString()), int.Parse(dt.Rows[0][7].ToString());
                }
            }

            return aluno;
        }

        public void AddAluno(SqlCeConnection conn, Aluno a)
        {
            string q = "INSERT INTO Aluno " +
                       "VALUES (" +
                       a.GetNome() + "," + a.GetUsername() + "," + a.GetPassword() + "," + a.GetDataNascimento() + "," +
                       a.GetDica() + "," + a.GetTema() + "," + a.GetExplicacao() + "," + a.GetPontuacao() +
                       ")";

            GeralDAO.Execute(q, conn);
        }

    }
}
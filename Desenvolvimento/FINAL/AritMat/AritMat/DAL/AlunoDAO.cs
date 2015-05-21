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

        private readonly ConhecimentoDAO conhecimentoDAO;

        public AlunoDAO()
        {
            conhecimentoDAO = new ConhecimentoDAO();
        }

        public Aluno AlunoLogin(SqlCeConnection conn, string user, string pw)
        {
            Aluno a = null;
            DataTable dt = GeralDAO.Query("SELECT * FROM Aluno WHERE Username = '" + user + "' AND Password = '" + pw + "'" , conn);

            if (dt != null && dt.Rows.Count > 0)
            {
                a = new Aluno(int.Parse(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), DateTime.Parse(dt.Rows[0][4].ToString()), byte.Parse(dt.Rows[0][5].ToString()), int.Parse(dt.Rows[0][6].ToString()), byte.Parse(dt.Rows[0][7].ToString()), int.Parse(dt.Rows[0][7].ToString()));
            }

            return a;
        }

        public Aluno GetById(SqlCeConnection conn, int id)
        {
            Aluno aluno = null;

            DataTable dt = null;

            dt = GeralDAO.Query("SELECT * FROM Aluno WHERE IdAluno = " + id, conn);
            
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    aluno = new Aluno(int.Parse(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), DateTime.Parse(dt.Rows[0][4].ToString()), byte.Parse(dt.Rows[0][5].ToString()), int.Parse(dt.Rows[0][6].ToString()), byte.Parse(dt.Rows[0][7].ToString()), int.Parse(dt.Rows[0][7].ToString()));
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
                    aluno = new Aluno(int.Parse(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), DateTime.Parse(dt.Rows[0][4].ToString()), byte.Parse(dt.Rows[0][5].ToString()), int.Parse(dt.Rows[0][6].ToString()), byte.Parse(dt.Rows[0][7].ToString()), int.Parse(dt.Rows[0][7].ToString()));
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

        public Dictionary<int, Conhecimento> GetAprendizagemByAlunoId(int id, SqlCeConnection conn)
        {
            return conhecimentoDAO.GetAprendizagemByAlunoId(id, conn);
        }

    }
}
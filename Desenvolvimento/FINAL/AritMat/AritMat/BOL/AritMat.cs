﻿using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Web;
using AritMat.DAL;

namespace AritMat.BOL
{
    public class AritMat
    {
        private GeralDAO geralDAO;
        private AlunoDAO alunoDAO;

        public AritMat()
        {
            geralDAO = new GeralDAO();
            alunoDAO = new AlunoDAO();
        }

        public Aluno UserLogin(string user, string pw)
        {
            SqlCeConnection conn = geralDAO.GetConnection();
            Aluno a = alunoDAO.AlunoLogin(conn, user, pw);

            conn.Close();

            return a;
        }
    }
}
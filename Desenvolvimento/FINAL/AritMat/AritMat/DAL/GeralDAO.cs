using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Reflection.Emit;

namespace AritMat.DAL
{
    public class GeralDAO
    {

        private static string Path = @"C:\Users\John\Documents\Repos\LI4\Desenvolvimento\FINAL\AritMat\AritMat\App_Data";
        private static string Db;
        private static SqlCeConnection Conn;

        /*public GeralDAO()
        {
            GeralDAO.Path = @"C:\Users\John\Documents\Repos\LI4\Desenvolvimento\FINAL\AritMat\AritMat\App_Data";
            GeralDAO.Conn = null;
            GeralDAO.Db = "none";
        }*/

        private static void OpenDB()
        {
           GeralDAO.Conn = null;
            var s = GeralDAO.Path + "\\AritMat.sdf";

            if(!File.Exists(s))
                return;

            try
            {
                GeralDAO.Conn = new SqlCeConnection("Data Source=" + s + "'; LCID=1033; Case Sensitive = TRUE");
                GeralDAO.Db = "AritMat";
            }
            catch { }

        }

        public static SqlCeConnection GetConnection()
        {
            if (GeralDAO.Conn == null)
                GeralDAO.OpenDB();

            return GeralDAO.Conn;
        }

        /*public static void CloseConn(Sql)
        {
            if (GeralDAO.Db == "none")
                return;

            if (conn == null) return;
            try
            {
                if (GeralDAO.Conn.State != ConnectionState.Closed)
                    GeralDAO.Conn.Close();
                GeralDAO.Conn = null;
            }
            catch { }

            GeralDAO.Db = "none";
        }*/

        public static void Execute(string command, SqlCeConnection conn)
        {
            if (GeralDAO.Db == "none") return;
            if (conn != null)
                try
                {
                    if (conn.State != ConnectionState.Open) conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        var cmd = new SqlCeCommand(command, conn);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch { }
        }

        public static DataTable Query(string qry, SqlCeConnection conn)
        {
            DataTable dt = null;

            if (Db == "none") return null;
            if (conn == null)
                conn = GeralDAO.GetConnection();
            
            SqlCeCommand command = new SqlCeCommand(qry, conn);
            try
            {
                DataTable s = null;
            
                if (conn.State != ConnectionState.Open) conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    SqlCeDataReader reader = command.ExecuteReader();
                    s = reader.GetSchemaTable();
                    dt = new DataTable();
                    for (int i = 0; i < s.Rows.Count; i++)
                    {
                        dt.Columns.Add(s.Rows[i][0].ToString());
                    }
                    while (reader.Read())
                    {
                        DataRow r = dt.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            r[i] = reader[i];
                        }
                        dt.Rows.Add(r);
                    }
                    reader.Close();
                }
            }
            catch { }

            return dt;
        }


    }
}
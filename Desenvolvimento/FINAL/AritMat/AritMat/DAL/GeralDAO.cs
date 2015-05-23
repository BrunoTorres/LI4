using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Drawing;
using System.IO;
using System.Reflection.Emit;
using AritMat.BOL;

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
            var s = GeralDAO.Path + "\\AritMatDB.sdf";

            if (!File.Exists(s))
                return;

            try
            {
                GeralDAO.Conn = new SqlCeConnection("Data Source=" + s + "; LCID=1033; Case Sensitive = TRUE");
                GeralDAO.Db = "AritMat";
            }
            catch
            {
            }
        }

        public SqlCeConnection GetConnection()
        {
            var s = GeralDAO.Path + "\\AritMatDB.sdf";

            if (!File.Exists(s))
                return null;

            return new SqlCeConnection("Data Source=" + s + "; LCID=1033; Case Sensitive = TRUE");
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

        public static int Execute(string command, SqlCeConnection conn)
        {
            if (conn == null)
            {
                GeralDAO g = new GeralDAO();
                conn = g.GetConnection();
            }
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    var cmd = new SqlCeCommand(command, conn);
                    return cmd.ExecuteNonQuery();
                }

            }
            catch(SqlCeException ex)
            {
            }

            return 0;
        }

        public static DataTable Query(string qry, SqlCeConnection conn)
        {
            DataTable dt = null;

            if (conn == null)
            {
                GeralDAO g = new GeralDAO();
                conn = g.GetConnection();
                conn.Open();
            }

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
            catch(SqlCeException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return dt;
        }

        public static ElementoEstudo GetElementoEstudo(DataRow row)
        {
            if (row[2] != null && row[3] == null)
            {
                return new Texto(row[2].ToString());
            }

            if (row.ItemArray[3] != null && row.ItemArray[2] == null)
            {
                return new Imagem(Image.FromStream(new MemoryStream((byte[]) row[3])));
            }

            return null;
        }
    }
}
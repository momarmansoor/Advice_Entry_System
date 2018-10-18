using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using System.Configuration;
using System.Collections;
using Utility;



namespace Advice_Entry_System.DAL
{
    class BasicDAO
    {
        private static BasicDAO m_Instance = null;
        private string m_ConnString;
        //static private SqlConnection m_Connnection = null;

        private BasicDAO()
        {
            //m_ConnString = "Data Source=.;Initial Catalog=TestApp;Integrated Security=True";

            ConnectionStringSettingsCollection connectionStrings = ConfigurationManager.ConnectionStrings;
            foreach (ConnectionStringSettings connection in connectionStrings)
            {
                if (connection.Name == "Advice_Entry_System.Properties.Settings.ConStr")
                {
                    Encryption.Key = ConfigurationManager.AppSettings["AdviceKey"].ToString();
                    String PlainText = "";
                    PlainText = Encryption.GetDecryptedConnectionString(ConfigurationManager.ConnectionStrings["Advice_Entry_System.Properties.Settings.ConStr"].ConnectionString);
                    m_ConnString = PlainText;
                    //m_ConnString = connection.ConnectionString;
                }
            }
        }

        public static BasicDAO GetInstance()
        {
            if (m_Instance == null)
                m_Instance = new BasicDAO();
            return m_Instance;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(m_ConnString);
            return conn;
        }

        private SqlCommand GetCommand(string statement, SqlConnection conn)
        {
            return new SqlCommand(statement, conn);
        }

        public void CreateObject(string statement)
        {
            SqlConnection conn = GetConnection();
            SqlTransaction trans = null;

            //try
            {
                SqlCommand cmd = GetCommand(statement, conn);
                conn.Open();
                trans = conn.BeginTransaction();
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                trans.Commit();
                cmd.Dispose();
                conn.Close();
            }
            //catch (SqlException ex)
            //{
            //    if (trans != null)
            //    {
            //        trans.Rollback();
            //    }
            //    if (conn != null && conn.State == ConnectionState.Open)
            //    {
            //        conn.Close();
            //    }
            //    throw new BasicDAOException("Some errors occurred while creating object", ex);
            //}

        }

        /// <summary>
        /// Creates an object using sql parameters
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="parameters"></param>
        public void CreateObject(string statement, SqlParameter[] parameters)
        {
            SqlConnection conn = GetConnection();
            SqlTransaction trans = null;

            //try
            {
                SqlCommand cmd = GetCommand(statement, conn);
                foreach (SqlParameter param in parameters)
                    cmd.Parameters.Add(param);

                conn.Open();
                trans = conn.BeginTransaction();
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                trans.Commit();
                cmd.Dispose();
                conn.Close();
            }
            //catch (SqlException ex)
            //{
            //    if (trans != null)
            //    {
            //        trans.Rollback();
            //    }
            //    if (conn != null && conn.State == ConnectionState.Open)
            //    {
            //        conn.Close();
            //    }
            //    throw new BasicDAOException("Some errors occurred while creating object", ex);
            //}

        }

        public DataRow RetreiveObject(string statement)
        {
            DataTable DT = RetreiveObjects(statement);
            if (DT == null || DT.Rows.Count == 0)
                return null;
            else
                return DT.Rows[0];
        }

        public DataTable RetreiveObjects(string statement)
        {
            SqlConnection conn = GetConnection();
            //try
            {
                conn = GetConnection();
                conn.Open();
                SqlCommand cmd = GetCommand(statement, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                cmd.Dispose();
                conn.Close();
                return ds.Tables[0];
            }
            //catch (SqlException ex)
            //{
            //    if (conn != null && conn.State == ConnectionState.Open)
            //    {
            //        conn.Close();
            //    }
            //    throw new BasicDAOException("Some errors occurred while retrieving objects", ex);
            //}
            //catch (Exception ex)
            //{
            //    if (conn != null && conn.State == ConnectionState.Open)
            //    {
            //        conn.Close();
            //    }
            //    throw new Exception("Generic Exception..", ex);
            //}

        }

        public int UpdateObject(string statement)
        {
            SqlConnection conn = GetConnection();
            SqlTransaction trans = null;
            int AffectedRows = 0;
            //try
            {
                SqlCommand cmd = GetCommand(statement, conn);
                conn.Open();
                trans = conn.BeginTransaction();
                cmd.Transaction = trans;
                AffectedRows = cmd.ExecuteNonQuery();
                trans.Commit();
                cmd.Dispose();
                conn.Close();
                return AffectedRows;
            }
            //catch (SqlException ex)
            //{
            //    if (trans != null)
            //    {
            //        trans.Rollback();
            //    }
            //    if (conn != null && conn.State == ConnectionState.Open)
            //    {
            //        conn.Close();
            //    }
            //    throw new BasicDAOException("Some errors occurred while updating objects", ex);
            //}
        }

        public int UpdateObject(string statement, SqlParameter[] parameters)
        {
            SqlConnection conn = GetConnection();
            SqlTransaction trans = null;
            int AffectedRows = 0;
            //try
            {
                SqlCommand cmd = GetCommand(statement, conn);
                foreach (SqlParameter param in parameters)
                    cmd.Parameters.Add(param);

                conn.Open();
                trans = conn.BeginTransaction();
                cmd.Transaction = trans;
                AffectedRows = cmd.ExecuteNonQuery();
                trans.Commit();
                cmd.Dispose();
                conn.Close();
                return AffectedRows;
            }
            //catch (SqlException ex)
            //{
            //    if (trans != null)
            //    {
            //        trans.Rollback();
            //    }
            //    if (conn != null && conn.State == ConnectionState.Open)
            //    {
            //        conn.Close();
            //    }
            //    throw new BasicDAOException("Some errors occurred while updating objects", ex);
            //}
        }

        public int DeleteObject(string statement)
        {
            SqlConnection conn = GetConnection();
            SqlTransaction trans = null;
            int AffectedRows = 0;
            try
            {
                SqlCommand cmd = GetCommand(statement, conn);
                conn.Open();
                trans = conn.BeginTransaction();
                cmd.Transaction = trans;
                AffectedRows = cmd.ExecuteNonQuery();
                trans.Commit();
                cmd.Dispose();
                conn.Close();
                return AffectedRows;
            }
            catch (SqlException ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                throw new BasicDAOException("Some errors occurred while deleting objects", ex);
            }

        }
    }
}

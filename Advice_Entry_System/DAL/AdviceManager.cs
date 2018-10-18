using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Advice_Entry_System.DAL
{
    public class AdviceManager
    {
        
        private static string       m_TableName = "Advice";
        private static AdviceManager  m_Instance = null;
        private static string       m_Statement = null;

        private AdviceManager()
        {}

        /// <summary>
        /// Since it is a singleton class, GetInstance creates an instance of this class and returns its refference.
        /// </summary>
        /// <returns></returns>
        public static AdviceManager GetInstance()
        
        {
            if (m_Instance == null)
                m_Instance = new AdviceManager();
            return m_Instance;
        }

        /// <summary>
        /// Verifies the existance of this object in database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Exists(Entity.Advice cat)
        {
            if (cat == null || cat.ID.ToString() == "-1")
                return false;
            Entity.Advice tcat;

            tcat = Get(cat.ID);
            if (tcat != null)
                    return true;
                else
                    return false;
            
        }

        /// <summary>
        /// Extracts the Item object from a DataRow.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        //private Entity.Item Get(DataRow row)
        public Entity.Advice Get(DataRow row)
        {
            if (row == null)
                return null;
            Entity.Advice cat = new Entity.Advice(Convert.ToInt32(row["ID"].ToString()),
                                          row["Branch_Code"].ToString(),
                                          row["GSL"].ToString(),
                                          Convert.ToChar(row["Dr_Cr"].ToString()),
                                          Convert.ToInt64(row["Amount"]),
                                          (String)row["Advice_No"],
                                          row["Description"].ToString(),
                                          row["user"].ToString(),
                                          row["type"].ToString(),
                                          Convert.ToDateTime(row["transaction_date"]),
                                          //(Entity.Advice.RequestReply)row["Branch_Reply"],
                                          (Entity.Advice.RequestReply)Enum.Parse(typeof(Entity.Advice.RequestReply), (String)row["Branch_Reply"]),
                                          (Entity.Advice.RequestReply)Enum.Parse(typeof(Entity.Advice.RequestReply), (String)row["HO_Reply"]),
                                          (String)row["Message"],
                                          Convert.ToChar(row["O_R"].ToString()),
                                          row["Final_Action"].ToString(),
                                          Convert.ToDateTime(row["Final_Action_Date"].ToString()),
                                          Convert.ToChar(row["Transaction_Status"].ToString()),
                                          Convert.ToInt32(row["Branch_Transaction_Number"].ToString()),
                                          Convert.ToInt32(row["HO_Transaction_Number"].ToString())
                                          );
            return cat;
        }
        
        /// <summary>
        /// Executes the m_Statement and retreives Item class from database.
        /// </summary>
        /// <returns></returns>
        private Entity.Advice Get()
        {
            DataRow row = DAL.BasicDAO.GetInstance().RetreiveObject(m_Statement);
            return Get(row);
        }

        /// <summary>
        /// Extracts Item object from Database having specified ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entity.Advice Get(long id)
        {
            m_Statement = "SELECT * ";
            m_Statement += " FROM " + m_TableName + " WHERE ID =" + id;
            return Get();
        }

        public Entity.Advice GetByAdviceNumber(string Advice_Number)
        {
            m_Statement = "SELECT * ";
            m_Statement += " FROM " + m_TableName + " WHERE Advice_No ='" + Advice_Number + "'";
            return Get();
        }


        /// <summary>
        /// Extracts Item object from Database having specified Name.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entity.Advice Get(string name)
        {
            m_Statement = "SELECT * ";
            m_Statement += " FROM " + m_TableName + " WHERE Branch_Name = \'" + name + "\'";
            return Get();
        }

        //public Entity.Advice GetMax()
        //{
        //    m_Statement = "declare @myid int";
        //    m_Statement += " select  @myid = max(id) from " + m_TableName;
        //    m_Statement += " set @myid = isnull(@myid, 0) ";
        //    m_Statement += " select @myid As Id, m.name from " + m_TableName + " m";
            
        //    return Get();
        //}

        public List<Entity.Advice> GetAllAdvice()
        {
            m_Statement = "SELECT * ";
            m_Statement += " FROM " + m_TableName;
            m_Statement += " WHERE Transaction_Status ='E'"; 
            DataTable rows = DAL.BasicDAO.GetInstance().RetreiveObjects(m_Statement);
            List<Entity.Advice> retArray = new List<Entity.Advice>();
            for (int i = 0; i < rows.Rows.Count; i++)
                retArray.Add(Get(rows.Rows[i]));
            return retArray;
        }

        //public List<Entity.Item> GetAllItemsByName(string name)
        //{
        //    m_Statement = "SELECT * ";
        //    m_Statement += " FROM " + m_TableName + " WHERE Name = \'" + name + "\'";
        //    DataRowCollection rows = DAL.BasicDAO.GetInstance().RetreiveObjects(m_Statement);
        //    List<Entity.Item> retArray = new List<Entity.Item>();
        //    for (int i = 0; i < rows.Count; i++)
        //        retArray.Add(Get(rows[i]));
        //    return retArray;
        //}


        /// <summary>
        /// Synchronize the Item object's ID with the existing database object.
        /// </summary>
        /// <param name="item"></param>
        private void Sync(Entity.Advice cat)
        {
            Entity.Advice Tcat;
            m_Statement = "SELECT * from " + m_TableName + " WHERE Branch_Code='" + cat.Branch_Code + "' and Advice_No='" + cat.Advice_No + "' and GSL='" + cat.GSL + "' and Dr_Cr=" + cat.Dr_Cr + " AND Transaction_Date ='"+cat.transactiondate.ToString("yyyy/MM/dd hh:mm:ss tt") + "'";
            Tcat = Get();
            cat.ID = Tcat.ID;
        }


        /// <summary>
        /// Saves the object, First it verifies the existance of 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Save(Entity.Advice cat)
        {
            try
            {
                if (cat != null)
                {
                    if (Exists(cat))
                    {
                        Update(cat);
                        return true;
                    }
                    else
                    {
                        string statement = "insert into " + m_TableName + "(Branch_Code,GSL,Dr_Cr,Amount,Advice_No,Description,[user],[type],[Transaction_Date],Branch_Reply,HO_Reply, Message,O_R,Final_Action,Final_Action_Date,Transaction_Status,Branch_Transaction_Number,HO_Transaction_Number) ";
                        statement += "values('";
                        
                        statement += cat.Branch_Code+ "','";
                        statement += cat.GSL + "','";
                        statement += cat.Dr_Cr+ "','";
                        statement += cat.Amount + "','";
                        statement += cat.Advice_No + "','";
                        statement += cat.Description + "','";
                        statement += cat.user + "','";
                        statement += cat.type + "','";
                        statement += cat.transactiondate.ToString("yyyy/MM/dd hh:mm:ss tt") + "','";
                        statement += cat.Branch_Reply + "','";
                        statement += cat.HO_Reply + "','";
                        statement += cat.Message + "','";
                        statement += cat.O_R + "','";
                        statement += cat.Final_Action + "','";
                        statement += cat.Final_Action_Date.ToString("yyyy/MM/dd hh:mm:ss tt") + "','";
                        statement += cat.Transaction_Status + "',";
                        statement += cat.Branch_Transaction_Number + ",";
                        statement += cat.HO_Transaction_Number + ")";

                        DAL.BasicDAO.GetInstance().CreateObject(statement);
                        Sync(cat);
                        return true;
                    }
                }
                return false;
            }
            catch (BasicDAOException ex)
            {
                throw ex;
            }
        }
        //
        // Updates and makes changes made in the Item object permanent in DB.
        //
        public bool Update(Entity.Advice cat)
        {
            try
            {
                if (cat != null)
                {
                    string statement = "UPDATE " + m_TableName + " SET ";

                    statement += " Branch_Reply = '" + cat.Branch_Reply.ToString();
                    statement += "', HO_Reply = '" + cat.HO_Reply.ToString();
                    statement += "', Message = '" + cat.Message;
                    statement += "', Final_Action ='" + cat.Final_Action;
                    statement += "', Final_Action_Date ='" + cat.Final_Action_Date.ToString("yyyy/MM/dd hh:mm:ss tt");
                    statement += "', Transaction_Status ='" + cat.Transaction_Status;
                    statement += "', Branch_Transaction_Number = " + cat.Branch_Transaction_Number;
                    statement += ", HO_Transaction_Number = " + cat.HO_Transaction_Number;

                    statement += " WHERE ID=" + cat.ID;

                    //Sync(cat);
                    DAL.BasicDAO.GetInstance().UpdateObject(statement);
                    return true;
                }
                return false;
            }
            catch (BasicDAOException ex)
            {
                throw ex;
            }
        }
        //
        // Deletes that item from database.
        //
        public bool Delete(Entity.Advice cat)
        {
            try
            {
                if (cat != null)
                {
                    string statement = "delete from " + m_TableName + " where ID=" + cat.ID;
                    DAL.BasicDAO.GetInstance().DeleteObject(statement);
                    return true;
                }
            }
            catch (BasicDAOException ex)
            {
                throw ex;
            }
            return false;
        }
    }
}

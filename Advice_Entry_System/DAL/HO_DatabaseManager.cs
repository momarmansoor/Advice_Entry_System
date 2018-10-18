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

namespace Advice_Entry_System.DAL
{
    public class HO_DatabaseManager
    {
        
         private static string       m_TableName = "HO_Database";
        private static HO_DatabaseManager  m_Instance = null;
        private static string       m_Statement = null;

        private HO_DatabaseManager()
        {}

        /// <summary>
        /// Since it is a singleton class, GetInstance creates an instance of this class and returns its refference.
        /// </summary>
        /// <returns></returns>
        public static HO_DatabaseManager GetInstance()
        
        {
            if (m_Instance == null)
                m_Instance = new HO_DatabaseManager();
            return m_Instance;
        }

        /// <summary>
        /// Verifies the existance of this object in database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Exists(Entity.HO_Database cat)
        {
            if (cat == null || cat.ID.ToString() == "-1")
                return false;
            Entity.HO_Database tcat;

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
        public Entity.HO_Database Get(DataRow row)
        {
            if (row == null)
                return null;
            Entity.HO_Database cat = new Entity.HO_Database(Convert.ToInt32(row["ID"].ToString()),
                                          row["Branch_IP_Address"].ToString(),
                                          row["Database_Name"].ToString(),
                                          row["Database_ID"].ToString(),
                                          row["Database_Password"].ToString(),
                                          Convert.ToChar(row["Type"].ToString()),
                                          row["Procedure"].ToString()
                                          );
            return cat;
        }
        
        /// <summary>
        /// Executes the m_Statement and retreives Item class from database.
        /// </summary>
        /// <returns></returns>
        private Entity.HO_Database Get()
        {
            DataRow row = DAL.BasicDAO.GetInstance().RetreiveObject(m_Statement);
            return Get(row);
        }

        /// <summary>
        /// Extracts Item object from Database having specified ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entity.HO_Database Get(long id)
        {
            m_Statement = "SELECT * ";
            m_Statement += " FROM " + m_TableName + " WHERE Branch_Code ='" + id + "'";
            return Get();
        }

        public Entity.HO_Database Get(char Type)
        {
            m_Statement = "SELECT * ";
            m_Statement += " FROM " + m_TableName + " WHERE Type ='" + Type + "'";
            return Get();
        }


        /// <summary>
        /// Extracts Item object from Database having specified Name.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entity.HO_Database Get(string name)
        {
            m_Statement = "SELECT * ";
            m_Statement += " FROM " + m_TableName + " WHERE Branch_Name = \'" + name + "\'";
            return Get();
        }

        //public Entity.HO_Database GetMax()
        //{
        //    m_Statement = "declare @myid int";
        //    m_Statement += " select  @myid = max(id) from " + m_TableName;
        //    m_Statement += " set @myid = isnull(@myid, 0) ";
        //    m_Statement += " select @myid As Id, m.name from " + m_TableName + " m";
            
        //    return Get();
        //}

        //public List<Entity.HO_Database> GetAll()
        //{
        //    m_Statement = "SELECT * ";
        //    m_Statement += " FROM " + m_TableName;
        //    DataTable rows = DAL.BasicDAO.GetInstance().RetreiveObjects(m_Statement);
        //    List<Entity.HO_Database> retArray = new List<Entity.HO_Database>();
        //    for (int i = 0; i < rows.Rows.Count; i++)
        //        retArray.Add(Get(rows.Rows[i]));
        //    return retArray;
        //}

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
        //private void Sync(Entity.Main cat)
        //{
        //    Entity.Heads  Tcat;
        //    m_Statement = "SELECT * from " + m_TableName + " WHERE Name LIKE '" + cat.Name + "'";
        //    Tcat = Get();
        //    cat.Id = Tcat.Id;
        //}


        /// <summary>
        /// Saves the object, First it verifies the existance of 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Save(Entity.HO_Database cat)
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
                        string statement = "insert into " + m_TableName + "(Branch_IP_Address,Database_Name,Database_ID,Database_Password, Type, Procedure) ";
                        statement += "values('";
                        statement += cat.Branch_IP_Address + "','";

                        statement += cat.Database_Name + "','";
                        statement += cat.Database_ID + "','";
                        statement += cat.Database_Password+ "','";
                        statement += cat.Type + "','";
                        statement += cat.Procedure + "')";

                        DAL.BasicDAO.GetInstance().CreateObject(statement);
                        //Sync(cat);
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
        public bool Update(Entity.HO_Database cat)
        {
            try
            {
                if (cat != null)
                {
                    string statement = "UPDATE " + m_TableName + " SET ";
                    statement += "Branch_IP_Address='" + cat.Branch_IP_Address;
                    statement += "', Database_Name='" + cat.Database_Name;
                    statement += "', [Database_ID]='" + cat.Database_ID;
                    statement += "', [Database_Password]='" + cat.Database_Password;
                    statement += "', [Type]='" + cat.Type;
                    statement += "', [Procedure]='" + cat.Procedure;

                    statement += "' WHERE ID=" + cat.ID;
                    //Sync(item);
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
        public bool Delete(Entity.HO_Database cat)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;


namespace Advice_Entry_System.DAL
{
    class Branches_NamesManager
    {
         private static string       m_TableName = "Branches_Names";
        private static Branches_NamesManager  m_Instance = null;
        private static string       m_Statement = null;

        private Branches_NamesManager()
        {}

        /// <summary>
        /// Since it is a singleton class, GetInstance creates an instance of this class and returns its refference.
        /// </summary>
        /// <returns></returns>
        public static Branches_NamesManager GetInstance()
        
        {
            if (m_Instance == null)
                m_Instance = new Branches_NamesManager();
            return m_Instance;
        }

        /// <summary>
        /// Verifies the existance of this object in database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Exists(Entity.Branches_Names cat)
        {
            if (cat == null || cat.Branch_Code == "-1")
                return false;
            Entity.Branches_Names tcat;

            tcat = Get(cat.Branch_Code);
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
        public Entity.Branches_Names Get(DataRow row)
        {
            if (row == null)
                return null;
            Entity.Branches_Names cat = new Entity.Branches_Names(row["Branch_Code"].ToString(),
                                          row["Branch_Name"].ToString(),
                                          Convert.ToChar(row["Branch_Type"].ToString()),
                                          row["Branch_Location"].ToString(),
                                          row["Branch_IP_Address"].ToString(),
                                          row["Database_Name"].ToString(),
                                          row["Database_ID"].ToString(),
                                          row["Database_Password"].ToString(),
                                          row["Branch_RCode"].ToString()
                                          );
            return cat;
        }
        
        /// <summary>
        /// Executes the m_Statement and retreives Item class from database.
        /// </summary>
        /// <returns></returns>
        private Entity.Branches_Names Get()
        {
            DataRow row = DAL.BasicDAO.GetInstance().RetreiveObject(m_Statement);
            return Get(row);
        }

        /// <summary>
        /// Extracts Item object from Database having specified ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entity.Branches_Names GetByCode(string id)
        {
            m_Statement = "SELECT * ";
            m_Statement += " FROM " + m_TableName + " WHERE Branch_Code ='" + id + "'";
            return Get();
        }

        public Entity.Branches_Names GetByNCode(string id)
        {
            m_Statement = "SELECT * ";
            m_Statement += " FROM " + m_TableName + " WHERE Branch_RCode ='" + id + "'";
            return Get();
        }
        

        /// <summary>
        /// Extracts Item object from Database having specified Name.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entity.Branches_Names Get(string name)
        {
            m_Statement = "SELECT * ";
            m_Statement += " FROM " + m_TableName + " WHERE Branch_Name = \'" + name + "\'";
            return Get();
        }

        //public Entity.Branches_Names GetMax()
        //{
        //    m_Statement = "declare @myid int";
        //    m_Statement += " select  @myid = max(id) from " + m_TableName;
        //    m_Statement += " set @myid = isnull(@myid, 0) ";
        //    m_Statement += " select @myid As Id, m.name from " + m_TableName + " m";
            
        //    return Get();
        //}

        public List<Entity.Branches_Names> GetAll()
        {
            m_Statement = "SELECT * ";
            m_Statement += " FROM " + m_TableName;
            DataTable rows = DAL.BasicDAO.GetInstance().RetreiveObjects(m_Statement);
            List<Entity.Branches_Names> retArray = new List<Entity.Branches_Names>();
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
        public bool Save(Entity.Branches_Names cat)
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
                        string statement = "insert into " + m_TableName + "(Branch_Code,Branch_Name,Branch_Type,Branch_Location,Branch_IP_Address,Database_Name,Database_ID,Database_Password, Branch_RCode) ";
                        statement += "values('";
                        statement += cat.Branch_Code + "','";
                        statement += cat.Branch_Name + "','";
                        statement += cat.Branch_Type + "','";
                        statement += cat.Branch_Location + "','";
                        statement += cat.Branch_IP_Address + "','";
                        statement += cat.Database_Name + "','";
                        statement += cat.Database_ID + "','";
                        statement += cat.Database_Password + "','";
                        statement += cat.Branch_RCode + "')";

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
        public bool Update(Entity.Branches_Names cat)
        {
            try
            {
                if (cat != null)
                {
                    string statement = "UPDATE " + m_TableName + " SET ";
                    statement += "Branch_Name='" + cat.Branch_Name;
                    statement += "', Branch_Type='" + cat.Branch_Type;
                    statement += "', [Branch_Location]='" + cat.Branch_Location;
                    statement += "', [Branch_IP_Address]='" + cat.Branch_IP_Address;
                    statement += "', Database_Name='" + cat.Database_Name;
                    statement += "', [Database_ID]='" + cat.Database_ID;
                    statement += "', [Database_Password]='" + cat.Database_Password;
                    statement += "', [Branch_RCode]='" + cat.Branch_RCode;

                    statement += "' WHERE Branch_Code='" + cat.Branch_Code + "'";
                    //Sync(item); 

                    DAL.BasicDAO.GetInstance().UpdateObject(m_Statement);
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
        public bool Delete(Entity.Branches_Names cat)
        {
            try
            {
                if (cat != null)
                {
                    string statement = "delete from " + m_TableName + " where ID='" + cat.Branch_Code +"'";
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

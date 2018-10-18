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

namespace Advice_Entry_System.Entity
{
    public class HO_Database
    {
        private int m_ID;
        private string m_Branch_IP_Address;
        private string m_Database_Name;
        private string m_Database_ID;
        private string m_Database_Password;
        private char m_Type;
        private string m_Procedure;

        public HO_Database()
        {
        }


        public HO_Database(int ID, string Branch_IP_Address, string Database_Name, string Database_ID, string Database_Password, char Type, string Procedure)
        {
            m_ID = ID;
            m_Branch_IP_Address = Branch_IP_Address;
            m_Database_Name = Database_Name;
            m_Database_ID = Database_ID;
            m_Database_Password = Database_Password;
            m_Type = Type;
            m_Procedure = Procedure;
        }

        public HO_Database(string Branch_IP_Address, string Database_Name, string Database_ID, string Database_Password, char Type, string Procedure)
        {
            m_ID = -1;
            m_Branch_IP_Address = Branch_IP_Address;
            m_Database_Name = Database_Name;
            m_Database_ID = Database_ID;
            m_Database_Password = Database_Password;
            m_Type = Type;
            m_Procedure = Procedure;
        }


        public int ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

        public string Branch_IP_Address
        {
            get { return m_Branch_IP_Address; }
            set { m_Branch_IP_Address = value; }
        }

        public string Database_Name
        {
            get { return m_Database_Name; }
            set { m_Database_Name = value; }
        }

        public string Database_ID
        {
            get { return m_Database_ID; }
            set { m_Database_ID = value; }
        }

        public string Database_Password
        {
            get { return m_Database_Password; }
            set { m_Database_Password = value; }
        }

        public char Type
        {
            get { return m_Type; }
            set { m_Type= value; }
        }

        public string Procedure
        {
            get { return m_Procedure; }
            set { m_Procedure = value; }
        }

    }
}

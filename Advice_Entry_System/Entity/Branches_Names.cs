using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advice_Entry_System.Entity
{
    class Branches_Names
    {
        private string  m_Branch_Code;
        private string  m_Branch_Name;
        private char    m_Branch_Type;
        private string  m_Branch_Location;
        private string  m_Branch_IP_Address;
        private string  m_Database_Name;
        private string  m_Database_ID;
        private string  m_Database_Password;
        private string m_Branch_RCode;

        /// <summary>
        /// ////////////////// Class Constructors /////////////////////
        /// </summary>
        public Branches_Names()
        { }

        public Branches_Names(string Branch_Code, string Branch_Name, char Branch_Type, string Branch_Location, string Branch_IP_Address, string Database_Name, string Database_ID, string Database_Password, string Branch_RCode)
        {
            m_Branch_Code = Branch_Code;
            m_Branch_Name = Branch_Name;
            m_Branch_Type = Branch_Type;
            m_Branch_Location = Branch_Location;
            m_Branch_IP_Address = Branch_IP_Address;
            m_Database_Name = Database_Name;
            m_Database_ID = Database_ID;
            m_Database_Password = Database_Password;
            m_Branch_RCode = Branch_RCode;
        }

        //public Branches_Names(string Branch_Name, char Branch_Type, string Branch_Location, string Branch_IP_Address, string Database_Name, string Database_ID, string Database_Password)
        //{
        //    m_Branch_Code = "-1";
        //    m_Branch_Name = Branch_Name;
        //    m_Branch_Type = Branch_Type;
        //    m_Branch_Location = Branch_Location;
        //    m_Branch_IP_Address = Branch_IP_Address;
        //    m_Database_Name = Database_Name;
        //    m_Database_ID = Database_ID;
        //    m_Database_Password = Database_Password;
        //}

        /// <summary>
        /// //////////// Class Properties ////////////////////////////////
        /// </summary>

        public string Branch_Code
        {
            get { return m_Branch_Code; }
            set { m_Branch_Code = value; }
        }

        public string Branch_RCode
        {
            get { return m_Branch_RCode; }
            set { m_Branch_RCode = value; }
        }


        public string Branch_Name
        {
            get { return m_Branch_Name; }
            set { m_Branch_Name = value; }
        }

        public char Branch_Type
        {
            get { return m_Branch_Type; }
            set { m_Branch_Type = value; }
        }

        public string Branch_Location
        {
            get { return m_Branch_Location; }
            set { m_Branch_Location = value; }
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
    }
}

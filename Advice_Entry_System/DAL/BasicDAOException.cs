using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Advice_Entry_System.DAL
{
    class BasicDAOException : Exception
    {
        private string m_Message = "";
        private SqlException m_SQLException = null;
        public BasicDAOException(string message, SqlException ex) : base()
        {
            m_Message = message;
            this.m_SQLException = ex;
        }

        /// <summary>
        /// Overided ToString function member for returning customized error.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return m_Message + "\n" + this.m_SQLException.ToString();
        }
    }
}

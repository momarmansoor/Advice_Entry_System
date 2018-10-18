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
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Advice_Entry_System.Utility
{
    public class Validation
    {

        public static bool IsInteger(string Value)
        {
            try
            {
                int Number = Convert.ToInt32(Value);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsBigInteger(string Value)
        {
            try
            {
                Int64 Number = Convert.ToInt64(Value);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool PasswordLenght(string Value)
        {
            try
            {
                if (Value.Length <= 8)
                    return true;
                else
                    return false;
            }

            catch (Exception ex)
            {
                return false;
            }
        }


        public static bool IsDouble(string Value)
        {
            try
            {
                double Number = Convert.ToDouble(Value);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsNotEmpty(string Value)
        {
            return (Value != "");
        }

        public static bool IsDBNULL(object obj)
        {
            return (obj == DBNull.Value);
        }

    }
}
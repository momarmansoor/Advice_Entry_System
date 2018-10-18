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
using Utility;

namespace Advice_Entry_System.BLL
{
    public class BLL
    {
        public static void ClearAllSessionVariables(System.Web.SessionState.HttpSessionState session)
        {
            // Currently Logged in User.
            session.Remove("user");
            session.Remove("type");
        }

        public static bool AuthenticateUser(string username, string password, char Type, out string value)
        {
            SqlConnection ConName = new SqlConnection();
            SqlTransaction trans = null;
            String msg = String.Empty;
            object code = null;

            // Authenticate the user in database.
            if (password != null)
            {
                Entity.HO_Database ho = DAL.HO_DatabaseManager.GetInstance().Get(Type);
                ConName.ConnectionString = "Data Source=" + ho.Branch_IP_Address + ";Initial Catalog=" + ho.Database_Name + ";User ID=" + ho.Database_ID + ";Password=" + ho.Database_Password + ";";

                //Encryption.Key = ConfigurationManager.AppSettings["AdviceKey"].ToString();
                //String PlainText = "";
                //PlainText = Encryption.GetDecryptedConnectionString(ConfigurationManager.ConnectionStrings["Advice_Entry_System.Properties.Settings.ConStr"].ConnectionString);
                //ConName.ConnectionString = PlainText;
                
                
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@Employee_id", SqlDbType.SmallInt);
                param[0].Direction = ParameterDirection.Input;
                param[0].Value = Convert.ToInt16(username);
                param[1] = new SqlParameter("@Employee_pwd", SqlDbType.VarChar, 8);
                param[1].Direction = ParameterDirection.Input;
                param[1].Value = password;
                param[2] = new SqlParameter("@Err_Msg", SqlDbType.VarChar, 200, "");
                param[2].Direction = ParameterDirection.Output;

                try
                {
                    SqlCommand cmd = new SqlCommand(ho.Procedure, ConName);
                    cmd.CommandTimeout = 60000;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();

                    for (int i = 0; i < param.Length; i++)
                    {
                        cmd.Parameters.Add(param[i]);
                    }

                    var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    ConName.Open();
                    cmd.ExecuteNonQuery();
                    code = returnParameter.Value;
                    msg = cmd.Parameters[2].Value.ToString();

                    cmd.Dispose();
                    ConName.Close();
                    value = msg;
                    if (msg == "user exists" || code.ToString() == "100")
                        return true;
                    else if (code.ToString() == "200")
                        return false;
                }
                catch (SqlException ex)
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }
                    if (ConName != null && ConName.State == ConnectionState.Open)
                    {
                        ConName.Close();
                    }
                    value = msg;
                    return false;
                    //throw ex;
                }
            }
            value = msg;
            return false;
        }

        public static string Make_Transaction_Branch(char Type, string Branch_Code, string GSL, char Dr_Cr, char O_R, long Amount, string Advice_No, string Description, out string value, out string Tr_No)
        {
            value = "";
            Tr_No = "";
            SqlConnection ConName = new SqlConnection();
            SqlTransaction trans = null;
            String msg = String.Empty;
            object code = null;
            String pass = String.Empty;

            // Authenticate the user in database.

            Entity.Branches_Names ho = DAL.Branches_NamesManager.GetInstance().GetByNCode(Branch_Code);
            Entity.HO_Database ho_database = DAL.HO_DatabaseManager.GetInstance().Get(Type);
            pass = Advice_Entry_System.BLL.DataEncryption.Decrypt(ho.Database_Password);
            
            //code to comment when going to live branches starts here
            if (ho_database.Database_Name.Contains("CEO"))
                ho_database.Database_Name ="CEO1";
            else if (ho_database.Database_Name.Contains("IHO"))
                ho_database.Database_Name = "IHO1";
            //code to comment ends here


            ConName.ConnectionString = "Data Source=" + ho.Branch_IP_Address + ";Initial Catalog=" + ho.Database_Name + ";User ID=" + ho.Database_ID + ";Password=" + pass + ";";
            //ConName.ConnectionString = "Data Source=10.20.20.2;Initial Catalog=PEW1June;User ID=sa;Password=@itd@;";

            SqlParameter[] param = new SqlParameter[9];

            param[0] = new SqlParameter("@Instrument_No", SqlDbType.VarChar, 200);
            param[0].Direction = ParameterDirection.Input;
            param[0].Value = Advice_No;

            param[1] = new SqlParameter("@BranchCode", SqlDbType.VarChar, 200);
            param[1].Direction = ParameterDirection.Input;
            param[1].Value = ho_database.Database_Name;

            param[2] = new SqlParameter("@Gsl_Code", SqlDbType.VarChar, 5);
            param[2].Direction = ParameterDirection.Input;
            param[2].Value = GSL;

            param[3] = new SqlParameter("@Amount", SqlDbType.Money);
            param[3].Direction = ParameterDirection.Input;
            param[3].Value = Amount;

            param[4] = new SqlParameter("@Narration", SqlDbType.VarChar, 200);
            param[4].Direction = ParameterDirection.Input;
            param[4].Value = Description;

            param[5] = new SqlParameter("@dr_cr", SqlDbType.Char, 1);
            param[5].Direction = ParameterDirection.Input;
            param[5].Value = Dr_Cr;

            param[6] = new SqlParameter("@Orig_Res", SqlDbType.Char, 1);
            param[6].Direction = ParameterDirection.Input;
            param[6].Value = O_R;

            param[7] = new SqlParameter("@Err_Msg", SqlDbType.VarChar, 200, "");
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Tr_No", SqlDbType.Int);
            param[8].Direction = ParameterDirection.Output;

            try
            {
                SqlCommand cmd = new SqlCommand("ho_InsTransactionAccount", ConName);
                cmd.CommandTimeout = 300;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.Add(param[i]);
                }

                var returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                ConName.Open();
                cmd.ExecuteNonQuery();
                code = returnParameter.Value;
                msg = cmd.Parameters[7].Value.ToString();
                Tr_No = cmd.Parameters[8].Value.ToString();
                string afd = "fdasf";
                cmd.Dispose();
                ConName.Close();

                value = code.ToString();
                return msg;
            }
            catch (SqlException ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                if (ConName != null && ConName.State == ConnectionState.Open)
                {
                    ConName.Close();
                }
                if (ex.Number == 53)
                {
                    return "Branch Is Down!";
                }
                return ex.Message;
            }
        }

        public static string Make_Transaction_HO(char Type, string Branch_Code, string GSL, char Dr_Cr,char O_R, long Amount, string Advice_No, string Description, out string value, out string Tr_No)
        {
            SqlConnection ConName = new SqlConnection();
            SqlTransaction trans = null;
            String msg = String.Empty;
            object code = null;
            String pass = String.Empty;
            value = "";
            Tr_No = "";

            // First Get the right connection string then make transaction in HO.
            Entity.HO_Database ho = DAL.HO_DatabaseManager.GetInstance().Get(Type);
            ConName.ConnectionString = "Data Source=" + ho.Branch_IP_Address + ";Initial Catalog=" + ho.Database_Name + ";User ID=" + ho.Database_ID + ";Password=" + ho.Database_Password + ";";

            SqlParameter[] param = new SqlParameter[10];

            param[0] = new SqlParameter("@Instrument_No", SqlDbType.VarChar, 200);
            param[0].Direction = ParameterDirection.Input;
            param[0].Value = Advice_No;

            param[1] = new SqlParameter("@BranchCode", SqlDbType.VarChar, 200);
            param[1].Direction = ParameterDirection.Input;
            param[1].Value = Branch_Code;

            param[2] = new SqlParameter("@Gsl_Code", SqlDbType.VarChar, 5);
            param[2].Direction = ParameterDirection.Input;
            param[2].Value = GSL;

            param[3] = new SqlParameter("@Amount", SqlDbType.Money);
            param[3].Direction = ParameterDirection.Input;
            param[3].Value = Amount;

            param[4] = new SqlParameter("@Narration", SqlDbType.VarChar, 200);
            param[4].Direction = ParameterDirection.Input;
            param[4].Value = Description;

            param[5] = new SqlParameter("@dr_cr", SqlDbType.Char, 1);
            param[5].Direction = ParameterDirection.Input;
            param[5].Value = Dr_Cr;

            param[6] = new SqlParameter("@Orig_Res", SqlDbType.Char, 1);
            param[6].Direction = ParameterDirection.Input;
            param[6].Value = O_R;

            param[7] = new SqlParameter("@Err_Msg", SqlDbType.VarChar, 200, "");
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("Return", SqlDbType.Int, 4);
            param[8].Direction = ParameterDirection.ReturnValue;

            param[9] = new SqlParameter("@Tr_No", SqlDbType.Int);
            param[9].Direction = ParameterDirection.Output;

            try
            {
                SqlCommand cmd = new SqlCommand("usp_HeadOfficeInsTransactionAccount", ConName);
                cmd.CommandTimeout = 300;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.Add(param[i]);
                }
               
                ConName.Open();
                cmd.ExecuteNonQuery();
                code = param[8].Value;
                msg = cmd.Parameters[7].Value.ToString();
                Tr_No = cmd.Parameters[9].Value.ToString();
                string asdf = "asdf";

                cmd.Dispose();
                ConName.Close();

                value = code.ToString();
                return msg;
            }
            catch (SqlException ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                if (ConName != null && ConName.State == ConnectionState.Open)
                {
                    ConName.Close();
                }
                if (ex.Number == 53)
                {
                    return "Cannot Connect to HO Server!";
                }
                return ex.Message;
            }
        }

        public static bool Make_Advice_Check_Branch(string Branch_Code, string Advice_No)
        {
            SqlConnection ConName = new SqlConnection();
            SqlTransaction trans = null;
            String pass = String.Empty;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            
            // Authenticate the user in database.

            Entity.Branches_Names ho = DAL.Branches_NamesManager.GetInstance().GetByNCode(Branch_Code);

            pass = Advice_Entry_System.BLL.DataEncryption.Decrypt(ho.Database_Password);
            ConName.ConnectionString = "Data Source=" + ho.Branch_IP_Address + ";Initial Catalog=" + ho.Database_Name + ";User ID=" + ho.Database_ID + ";Password=" + pass + ";";
            //ConName.ConnectionString = "Data Source=10.20.20.2;Initial Catalog=PEW1June;User ID=sa;Password=@itd@;";

            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@AdviceNo", SqlDbType.VarChar, 200);
            param[0].Direction = ParameterDirection.Input;
            param[0].Value = Advice_No;

            try
            {
                SqlCommand cmd = new SqlCommand("get_adviceinquiry", ConName);
                cmd.CommandTimeout = 300;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.Add(param[i]);
                }

                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();

                if (dt.Rows.Count == 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException ex)
            {
                if (trans != null)
                {
                    ConName.Close();
                }
                if (ConName != null && ConName.State == ConnectionState.Open)
                {
                    ConName.Close();
                }
                if (ex.Number == 53 || ex.Number == 8145)
                {
                    return false;
                }
                return false;
            }
        }

        public static bool pingcall(string Branch_Code)
        {
            Entity.Branches_Names ho = DAL.Branches_NamesManager.GetInstance().GetByNCode(Branch_Code);
            Advice_Entry_System.BLL.DataEncryption.Decrypt(ho.Database_Password);

            SqlConnection ConName = new SqlConnection();
            ConName.ConnectionString = "Data Source=" + ho.Branch_IP_Address + ";Initial Catalog=" + ho.Database_Name + ";User ID=" + ho.Database_ID + ";Password=" + Advice_Entry_System.BLL.DataEncryption.Decrypt(ho.Database_Password) + ";";
            //ConName.ConnectionString = "Data Source=10.20.20.2;Initial Catalog=PEW1June;User ID=sa;Password=@itd@;Connect Timeout=60;";

            try
            {
                //ConName.ConnectionTimeout = 300;
                ConName.Open();
                return true;
            }
            catch (Exception ex)
            {
                ConName.Close();
                return false;
            }
        }

        public static DataTable Make_Advice_Check_Branch_Table(string Branch_Code, string Advice_No)
        {
            SqlConnection ConName = new SqlConnection();
            SqlTransaction trans = null;
            String pass = String.Empty;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            // Authenticate the user in database.

            Entity.Branches_Names ho = DAL.Branches_NamesManager.GetInstance().GetByNCode(Branch_Code);

            pass = Advice_Entry_System.BLL.DataEncryption.Decrypt(ho.Database_Password);
            ConName.ConnectionString = "Data Source=" + ho.Branch_IP_Address + ";Initial Catalog=" + ho.Database_Name + ";User ID=" + ho.Database_ID + ";Password=" + pass + ";";

            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@AdviceNo", SqlDbType.VarChar, 200);
            param[0].Direction = ParameterDirection.Input;
            param[0].Value = Advice_No;

            try
            {
                SqlCommand cmd = new SqlCommand("get_adviceinquiry", ConName);
                cmd.CommandTimeout = 300;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.Add(param[i]);
                }

                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();

                return dt;
                //if (dt.Rows.Count == 0)
                //    return true;
                //else
                //    return false;

            }
            catch (SqlException ex)
            {
                dt.Dispose();
                if (trans != null)
                {
                    ConName.Close();
                }
                if (ConName != null && ConName.State == ConnectionState.Open)
                {
                    ConName.Close();
                }
                if (ex.Number == 53 || ex.Number == 8145)
                {
                    ConName.Close();
                }
                dt.Clear();
                return dt;
            }
        }

        public static string GetGSLName(string GSL,string Branch_Code)
        {
            SqlConnection ConName = new SqlConnection();
            string ret = String.Empty;
            Entity.Branches_Names ho = DAL.Branches_NamesManager.GetInstance().GetByCode(Branch_Code);
            ConName.ConnectionString = "Data Source=" + ho.Branch_IP_Address + ";Initial Catalog=" + ho.Database_Name + ";User ID=" + ho.Database_ID + ";Password=" + Advice_Entry_System.BLL.DataEncryption.Decrypt(ho.Database_Password) + ";";
            //ConName.ConnectionString = "Data Source=10.20.20.2;Initial Catalog=PEW1June;User ID=sa;Password=@itd@;";

            string query = "  SELECT TOP 1 gc.GSL_Name FROM GSL_Code gc WHERE gc.GSL_Id =" + GSL;

            ConName.Open();
            SqlCommand cmd = new SqlCommand(query, ConName);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ConName.Close();
            if (dt.Rows.Count > 0)
                ret = dt.Rows[0][0].ToString();
            else
                ret = "";
            return ret;
        }
    }
}

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using Utility;

namespace Advice_Entry_System
{
    public partial class GSL_Inquiry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["user"] == null || Session["type"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    lbl_Connected.Text = "Connected To HO : " + Session["type"].ToString() + ", By User : " + Session["user"].ToString();
                }
            }
        }

        protected void Btn_GetData_Click(object sender, EventArgs e)
        {
            //Get Branch Code
            Entity.Branches_Names ho = DAL.Branches_NamesManager.GetInstance().GetByCode(ddBranches.SelectedItem.Value.ToString());

            if (BLL.BLL.pingcall(ho.Branch_RCode)) // Check Weather Branch is Up or Down
            {
                DataTable dt = new DataTable();
                dt = gsl();
                if (dt.Rows.Count > 0)
                {
                    lbl_Balance.Text = String.Format("{0:N}", Convert.ToDouble(dt.Rows[0]["Credit"].ToString()));
                    dt.Rows[0].Delete();
                    dt.AcceptChanges();
                    dt.Columns.Remove("Type");
                    dt.Columns.Remove("AccountNo");
                    dt.Columns.Remove("LoanPM");
                    RadGrid1.DataSource = dt;
                    RadGrid1.DataBind();
                }
                else
                {
                    lbl_Balance.Text = String.Empty;
                    RadGrid1.Dispose();
                    RadGrid1.DataBind();
                }
            }
        }

        public DataTable gsl()
        {
            Entity.Branches_Names ho = DAL.Branches_NamesManager.GetInstance().GetByCode(ddBranches.SelectedItem.Value.ToString());
            SqlConnection ConName = new SqlConnection();
            SqlTransaction trans = null;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            ConName.ConnectionString = "Data Source=" + ho.Branch_IP_Address + ";Initial Catalog=" + ho.Database_Name + ";User ID=" + ho.Database_ID + ";Password=" + Advice_Entry_System.BLL.DataEncryption.Decrypt(ho.Database_Password) + ";";

            SqlParameter[] param = new SqlParameter[7];

            param[0] = new SqlParameter("@GSlCode", SqlDbType.VarChar, 200);
            param[0].Direction = ParameterDirection.Input;
            param[0].Value = txt_GSL.Text;

            param[1] = new SqlParameter("@Customer_id", SqlDbType.Int);
            param[1].Direction = ParameterDirection.Input;
            param[1].Value = 0;

            param[2] = new SqlParameter("@Account_id", SqlDbType.Int);
            param[2].Direction = ParameterDirection.Input;
            param[2].Value = 0;

            param[3] = new SqlParameter("@SDate", SqlDbType.DateTime, 8);
            param[3].Direction = ParameterDirection.Input;
            param[3].Value = System.DateTime.Now.ToString("yyyy/MM/dd");

            param[4] = new SqlParameter("@EDate", SqlDbType.DateTime, 8);
            param[4].Direction = ParameterDirection.Input;
            param[4].Value = System.DateTime.Now.ToString("yyyy/MM/dd");

            param[5] = new SqlParameter("@Deposit_Loan_Ledger", SqlDbType.Char, 3);
            param[5].Direction = ParameterDirection.Input;
            param[5].Value = "GSL";

            param[6] = new SqlParameter("@NoOfRowstoReturn", SqlDbType.Int);
            param[6].Direction = ParameterDirection.Input;
            param[6].Value = 10;

            try
            {
                SqlCommand cmd = new SqlCommand("usp_GslAndCustomerminiStatement", ConName);
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
                //value = code.ToString();
                //return msg;
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
                    ConName.Close();
                }
                return dt;
            }
        }

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Header)
            {
                if (e.Item.Cells[3].Text == "GSLORCustomerName")
                    e.Item.Cells[3].Text = "GSL Name";
            }
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                if (e.Item.Cells[7].Text != "&nbsp;")
                {
                    e.Item.Cells[7].Text = string.Format("{0:N}", Convert.ToDouble(e.Item.Cells[7].Text));
                    e.Item.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                }
                if (e.Item.Cells[8].Text != "&nbsp;")
                {
                    e.Item.Cells[8].Text = string.Format("{0:N}", Convert.ToDouble(e.Item.Cells[8].Text));
                    e.Item.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                }
            }
        }

        protected void SqlDataSource1_Load(object sender, EventArgs e)
        {
            Encryption.Key = ConfigurationManager.AppSettings["AdviceKey"].ToString();
            String PlainText = "";
            PlainText = Encryption.GetDecryptedConnectionString(ConfigurationManager.ConnectionStrings["Advice_Entry_System.Properties.Settings.ConStr"].ConnectionString);
            SqlDataSource1.ConnectionString = PlainText;
        }
    }
}


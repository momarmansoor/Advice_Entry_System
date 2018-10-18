using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Utility;
using System.Configuration;

namespace Advice_Entry_System
{
    public partial class Report_New : System.Web.UI.Page
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
            string Query = String.Empty;
            Query = MakeQuery();
            DataTable dt = new DataTable();
            dt = DAL.BasicDAO.GetInstance().RetreiveObjects(Query);
            if (dt.Rows.Count > 0)
            {
                rgData.DataSource = dt;
                rgData.DataBind();
            }
            else
            {
                rgData.Dispose();
                rgData.DataBind();
            }
        }

        protected void rbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbList.SelectedIndex.ToString() == "0" || rbList.SelectedIndex.ToString() == "6" || rbList.SelectedIndex.ToString() == "7")//For Date
            {
                rdDate.Visible = true;
                ddBranches.Visible = false;
                txt_box.Visible = false;
                ClearAll();
            }
            else if (rbList.SelectedIndex.ToString() == "1" || rbList.SelectedIndex.ToString() == "3" || rbList.SelectedIndex.ToString() == "4" || rbList.SelectedIndex.ToString() == "5")// For text box
            {
                rdDate.Visible = false;
                ddBranches.Visible = false;
                txt_box.Visible = true;
                ClearAll();
            }
            else if (rbList.SelectedIndex.ToString() == "2") // For DropDownList Branches
            {
                rdDate.Visible = false;
                ddBranches.Visible = true;
                txt_box.Visible = false;
                ClearAll();
            }
        }

        private string MakeQuery()
        {
            string QueryToMake = String.Empty;
            QueryToMake = " SELECT a.ID,bn.Branch_Name AS [Branch Name],a.GSL, CASE a.dr_cr WHEN 0 THEN 'Debit' WHEN 1 THEN 'Credit' END AS  [Debit/Credit],";
            QueryToMake += " CASE a.O_R WHEN 'O' THEN 'Originating' WHEN 'R' THEN 'Responding' END AS [HO Originating/Responding],";
            QueryToMake += " CASE a.O_R WHEN 'R' THEN 'Originating' WHEN 'O' THEN 'Responding' END AS [Branch Originating/Responding],";
            QueryToMake += " a.Amount, a.Advice_No AS [Advice No], a.[Description], a.[user], a.[Type], a.Transaction_Date AS [Transaction Date], ";
            QueryToMake += " CASE a.Branch_Reply WHEN 'Yes' THEN 'Sucess' WHEN 'No' THEN 'Fail' END AS [Branch Reply], ";
            QueryToMake += " CASE a.HO_Reply WHEN 'Yes' THEN 'Sucess' WHEN 'No' THEN 'Fail' END AS [HO Reply], a.[Message]";
            QueryToMake += " FROM Advice a";
            QueryToMake += " JOIN Branches_Names bn ON bn.Branch_Code = a.Branch_Code";

            if (rbList.SelectedIndex.ToString() == "0")//For Date
            {
                QueryToMake += " WHERE a.Transaction_Date BETWEEN '" + rdDate.SelectedDate.Value.ToString("yyyy/MM/dd") + " 00:00:00' AND '" + rdDate.SelectedDate.Value.ToString("yyyy/MM/dd") + " 23:59:59'";
            }
            else if (rbList.SelectedIndex.ToString() == "1") // For Advice Number
            {
                QueryToMake += " WHERE a.Advice_No = '" + txt_box.Text + "'";
            }
            else if (rbList.SelectedIndex.ToString() == "2") // For DropDownList Branches
            {
                QueryToMake += " WHERE a.Branch_Code ='" + ddBranches.SelectedItem.Value.ToString() + "'";
            }
            else if (rbList.SelectedIndex.ToString() == "3")// For Amount
            {
                QueryToMake += " WHERE a.Amount = " + txt_box.Text;
            }
            else if (rbList.SelectedIndex.ToString() == "4") // FOr GSL CODE
            {
                QueryToMake += " WHERE a.GSL = '" + txt_box.Text + "'";
            }
            else if (rbList.SelectedIndex.ToString() == "5") // FOr User
            {
                QueryToMake += " WHERE a.[user] = '" + txt_box.Text + "'";
            }
            else if (rbList.SelectedIndex.ToString() == "6") // FOr User
            {
                QueryToMake += " WHERE a.Transaction_Status = 'D' AND a.Transaction_Date BETWEEN '" + rdDate.SelectedDate.Value.ToString("yyyy/MM/dd") + " 00:00:00' AND '" + rdDate.SelectedDate.Value.ToString("yyyy/MM/dd") + " 23:59:59'";
            }
            else if (rbList.SelectedIndex.ToString() == "7") // FOr User
            {
                QueryToMake += " WHERE a.Transaction_Status = 'A' AND a.Transaction_Date BETWEEN '" + rdDate.SelectedDate.Value.ToString("yyyy/MM/dd") + " 00:00:00' AND '" + rdDate.SelectedDate.Value.ToString("yyyy/MM/dd") + " 23:59:59'";
                QueryToMake += " AND a.Branch_Transaction_Number = 0 OR a.HO_Transaction_Number= 0";
            }

            return QueryToMake;
        }

        private void ClearAll()
        {
            txt_box.Text = String.Empty;
            rgData.Dispose();
            rgData.DataBind();
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

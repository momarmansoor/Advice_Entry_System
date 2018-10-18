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
    public partial class Advice_Inquiry : System.Web.UI.Page
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

        protected void Btn_Get_Data_Click(object sender, EventArgs e)
        {
             //Get Branch Code
            Entity.Branches_Names ho = DAL.Branches_NamesManager.GetInstance().GetByCode(Branches_Name.SelectedItem.Value.ToString());

            if (BLL.BLL.pingcall(ho.Branch_RCode)) // Check Weather Branch is Up or Down
            {
                rg_Data.DataSource = BLL.BLL.Make_Advice_Check_Branch_Table(ho.Branch_RCode, txt_Advice.Text);
                rg_Data.DataBind();
                lblerror.Visible = false;
            }
            else
            {
                lblerror.Text = "Branch Is Down!";
                lblerror.Visible = true;
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

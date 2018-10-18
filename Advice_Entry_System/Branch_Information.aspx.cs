using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Configuration;

namespace Advice_Entry_System
{
    public partial class Branch_Information : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn_Get_Data_Click(object sender, EventArgs e)
        {
            Entity.Branches_Names ho = DAL.Branches_NamesManager.GetInstance().GetByCode(Branches_Name.SelectedItem.Value.ToString());
            Make_Visible();
            txt_BranchCode.Text = ho.Branch_Code;
            txt_DataBaseName.Text = ho.Database_Name;
            txt_UserID.Text = ho.Database_ID;
            txt_Password.Text = Advice_Entry_System.BLL.DataEncryption.Decrypt(ho.Database_Password);
        }

        private void Make_Visible()
        {
            lbl_BranchCode.Visible = true;
            lbl_DataBaseName.Visible = true;
            lbl_Password.Visible = true;
            lbl_UserID.Visible = true;

            txt_BranchCode.Visible = true;
            txt_DataBaseName.Visible = true;
            txt_Password.Visible = true;
            txt_UserID.Visible = true;
        }

        private void Make_InVisible()
        {
            lbl_BranchCode.Visible = false;
            lbl_DataBaseName.Visible = false;
            lbl_Password.Visible = false;
            lbl_UserID.Visible = false;

            txt_BranchCode.Visible = false;
            txt_DataBaseName.Visible = false;
            txt_Password.Visible = false;
            txt_UserID.Visible = false;
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

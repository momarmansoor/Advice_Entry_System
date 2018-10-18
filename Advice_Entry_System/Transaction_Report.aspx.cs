using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Advice_Entry_System
{
    public partial class Transaction_Report : System.Web.UI.Page
    {
        int id = 0;
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
                    //lbl_Connected.Text = "Connected To HO : " + Session["type"].ToString() + ", By User : " + Session["user"].ToString();
                    id = Convert.ToInt32(Request.QueryString["ID"].ToString());
                    Entity.Advice ad = DAL.AdviceManager.GetInstance().Get(id);
                    Entity.Branches_Names br = DAL.Branches_NamesManager.GetInstance().GetByCode(ad.Branch_Code);
                    lblID.Text = ad.ID.ToString();
                    lblBranchCode.Text = br.Branch_RCode.ToString();
                    lblGSL.Text = ad.GSL;
                    lblD_C.Text = ad.Dr_Cr.ToString() == "0" ? "Debit" : "Credit";
                    lblO_R.Text = ad.O_R.ToString() == "O" ? "Originating" : "Responding";
                    lblAmount.Text = ad.Amount.ToString();
                    lblAdvice.Text = ad.Advice_No;
                    lblDescription.Text = ad.Description;
                    lbluser.Text = ad.user;
                    lbltype.Text = ad.type;
                    lblTransactionDate.Text = ad.transactiondate.ToString("yyyy/MM/dd hh:mm:ss tt");
                    lblhoreply.Text = ad.HO_Reply.ToString() == "Yes" ? "Success" : "Fail";
                    lblBranchReply.Text = ad.Branch_Reply.ToString() == "Yes" ? "Success" : "Fail";
                    lblMessage.Text = ad.Message;
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            RadGrid1.AllowPaging = false;
            RadGrid1.MasterTableView.AllowFilteringByColumn = false;

            RadGrid1.Rebind();
            RadAjaxPanel1.ResponseScripts.Add("PrintRadGrid('" + RadGrid1.ClientID + "')");
        }

        protected void Btn_Print_Click(object sender, EventArgs e)
        {
         //   Response.Redirect(
        }
    }
}

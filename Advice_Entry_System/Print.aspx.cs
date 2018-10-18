using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Advice_Entry_System
{
    public partial class Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = 0;
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
                    lblStatus.Text = ad.Transaction_Status.ToString() == "E" ? "Entry" : "Authenticated";
                    lbl_HO_Trans.Text = ad.HO_Transaction_Number.ToString();
                    lbl_Branch_Transaction.Text = ad.Branch_Transaction_Number.ToString();
                    lblMessage.Text = ad.Message;

                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "self.print();return false;','','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=30,height=30');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "print", "window.print();", true);

                }
            }
        }

        protected void Btn_Print_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "print", "window.print();", true);
        }
    }
}

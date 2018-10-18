using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Advice_Entry_System
{
    public partial class Authenticate : System.Web.UI.Page
    {
        string value = "0"; // Branch
        string value1 = "0"; // HO

        string Br_Tr_No = "0";
        string HO_Tr_No = "0";

        string msgfordisplay = "";

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
                    PopulateAdviceNumber();
                }
            }
        }

        protected void Btn_GetDetails_Click(object sender, EventArgs e)
        {
            Entity.Advice ad = DAL.AdviceManager.GetInstance().GetByAdviceNumber(ddAdviceNumber.SelectedItem.Value);
            Entity.Branches_Names bn = DAL.Branches_NamesManager.GetInstance().GetByCode(ad.Branch_Code);
            Make_Visible();
            lbl_Error.Text = String.Empty;
            lbl_Advice.Text = ad.Advice_No;
            lbl_Amount.Text = ad.Amount.ToString();
            lbl_Branch_Code.Text = bn.Branch_RCode;
            lbl_Branch_GSL_Code.Text = ad.GSL;
            lbl_C_I.Text = ad.type;
            lbl_D_C.Text = ad.Dr_Cr.ToString() == "0" ? "Debit" : "Credit";
            lbl_Description.Text = ad.Description;
            lbl_ID.Text = ad.ID.ToString();
            lbl_O_R.Text = ad.O_R.ToString() == "R" ? "Responding" : "Originating";
            lbl_Ho_R_O.Text = ad.O_R.ToString() == "O" ? "Responding" : "Originating";
            lbl_Transaction_Maker.Text = ad.user;
        }

        private void Make_Visible()
        {
            Label1.Visible = true;
            Label10.Visible = true;
            Label11.Visible = true;
            Label12.Visible = true;
            Label18.Visible = true;
            Label2.Visible = true;
            Label4.Visible = true;
            Label5.Visible = true;
            Label6.Visible = true;
            Label7.Visible = true;
            Label9.Visible = true;
            Label19.Visible = true;

            lbl_Advice.Visible = true;
            lbl_Amount.Visible = true;
            lbl_Branch_Code.Visible = true;
            lbl_Branch_GSL_Code.Visible = true;
            lbl_C_I.Visible = true;
            lbl_D_C.Visible = true;
            lbl_Description.Visible = true;
            lbl_Error.Visible = true;
            lbl_ID.Visible=true;
            lbl_O_R.Visible = true;
            lbl_Transaction_Maker.Visible = true;
            lbl_Ho_R_O.Visible = true;

            Btn_Make_Transaction.Visible = true;
            Btn_Delete_Transaction.Visible = true;
        }

        private void Make_InVisible()
        {
            Label1.Visible = false;
            Label10.Visible = false;
            Label11.Visible = false;
            Label12.Visible = false;
            Label18.Visible = false;
            Label2.Visible = false;
            Label4.Visible = false;
            Label5.Visible = false;
            Label6.Visible = false;
            Label7.Visible = false;
            Label9.Visible = false;
            Label19.Visible = false;

            lbl_Advice.Visible = false;
            lbl_Amount.Visible = false;
            lbl_Branch_Code.Visible = false;
            lbl_Branch_GSL_Code.Visible = false;
            lbl_C_I.Visible = false;
            lbl_D_C.Visible = false;
            lbl_Description.Visible = false;
            lbl_Error.Visible = false;
            lbl_ID.Visible = false;
            lbl_O_R.Visible = false;
            lbl_Transaction_Maker.Visible = false;
            lbl_Ho_R_O.Visible = false;

            Btn_Make_Transaction.Visible = false;
            Btn_Delete_Transaction.Visible = false;
        }

        private string Generate_Transaction()
        {
            Entity.Advice adv = DAL.AdviceManager.GetInstance().GetByAdviceNumber(lbl_Advice.Text);

            string retvalue = String.Empty;
            string horeply = String.Empty;
            string branchreply = String.Empty;
            string HO_GSL = "5101";
            char debit = '0';
            char credit = '1';

            char type1 = 'a';
            if (Session["type"].ToString() == "Conventional")
            {
                type1 = '0';
            }
            else if (Session["type"].ToString() == "Islamic")
            {
                type1 = '1';
            }

            //Get Branch Code
            Entity.Branches_Names ho = DAL.Branches_NamesManager.GetInstance().GetByCode(adv.Branch_Code);

            if (BLL.BLL.pingcall(ho.Branch_RCode)) // Check Weather Branch is Up or Down
            {
                if (BLL.BLL.Make_Advice_Check_Branch(ho.Branch_RCode, adv.Advice_No)) //Check Branch of the Advice Number
                {
                    //calling branches and HO
                    if (adv.Dr_Cr.ToString() == "0") // Debit Branch First then credit HO
                    {
                        branchreply = BLL.BLL.Make_Transaction_Branch(type1, ho.Branch_RCode, adv.GSL, debit, Convert.ToChar(adv.O_R.ToString() == "R" ? "O" : "R"), Convert.ToInt64(adv.Amount), adv.Advice_No, adv.Description, out value, out Br_Tr_No);
                        if (branchreply == "Transaction Generated Successfully!" || value == "100")
                        {
                            horeply = BLL.BLL.Make_Transaction_HO(type1, ho.Branch_RCode, HO_GSL, credit, Convert.ToChar(adv.O_R.ToString() == "O" ? "O" : "R"), Convert.ToInt64(adv.Amount), adv.Advice_No, adv.Description, out value1, out HO_Tr_No);
                        }
                    }
                    else if (adv.Dr_Cr.ToString() == "1") // Credit HO First then Debit Branch
                    {
                        horeply = BLL.BLL.Make_Transaction_HO(type1, ho.Branch_RCode, HO_GSL, debit, Convert.ToChar(adv.O_R.ToString() == "O" ? "O" : "R"), Convert.ToInt64(adv.Amount), adv.Advice_No, adv.Description, out value1, out HO_Tr_No);
                        if (horeply == "Transaction Generated Successfully!" || value == "100")
                        {
                            branchreply = BLL.BLL.Make_Transaction_Branch(type1, ho.Branch_RCode, adv.GSL, credit, Convert.ToChar(adv.O_R.ToString() == "R" ? "O" : "R"), Convert.ToInt64(adv.Amount), adv.Advice_No, adv.Description, out value, out Br_Tr_No);
                        }
                    }

                    //writing for message to save
                    if (adv.Dr_Cr.ToString() == "0") // Debit Branch Fist then credit HO
                    {
                        retvalue = " Reply from Branch : " + branchreply + ", Reply From HO : " + horeply;
                    }
                    else if (adv.Dr_Cr.ToString() == "1") // Credit HO First then Debit Branch
                    {
                        retvalue = " Reply from HO : " + horeply + ", Reply From Branch : " + branchreply;
                    }

                    //writing label
                    if (adv.Dr_Cr.ToString() == "0") // Debit Branch Fist then credit HO
                    {
                        msgfordisplay = " Reply from Branch : " + branchreply + ", Branch Transaction Number :" + Br_Tr_No + ", Reply From HO : " + horeply + ", HO Transaction Number :" + HO_Tr_No;
                    }
                    else if (adv.Dr_Cr.ToString() == "1") // Credit HO First then Debit Branch
                    {
                        msgfordisplay = " Reply from HO : " + horeply + ", HO Transaction Number :" + HO_Tr_No + ", Reply From Branch : " + branchreply + ", Branch Transaction Number :" + Br_Tr_No;
                    }
                }
                else
                {
                    retvalue = "Advice Number Already Exist in the Branch";
                    msgfordisplay = "Advice Number Already Exist in the Branch";
                }
            }
            else
            {
                retvalue = "Branch Is Down";
                msgfordisplay = "Branch Is Down";
            }
            return retvalue;
        }

        protected void Btn_Make_Transaction_Click(object sender, EventArgs e)
        {
            string msg = String.Empty;
            Entity.Advice ad = DAL.AdviceManager.GetInstance().GetByAdviceNumber(lbl_Advice.Text);
            lbl_Error.Visible = true;
            if (ad.user != Session["user"].ToString())
            {
                try
                {
                    msg = Generate_Transaction();
                    lbl_Error.Text = msgfordisplay;

                    if (msg.Contains("'"))
                    {
                        msg = msg.Replace("'", "''");
                    }

                    Entity.Advice ad1 = new Advice_Entry_System.Entity.Advice(ad.ID, ad.Branch_Code, ad.GSL, ad.Dr_Cr, ad.Amount, ad.Advice_No, ad.Description, ad.user, ad.type, ad.transactiondate, (value == "100" ? Entity.Advice.RequestReply.Yes : Entity.Advice.RequestReply.No), (value1 == "100" ? Entity.Advice.RequestReply.Yes : Entity.Advice.RequestReply.No), msg, ad.O_R, Session["user"].ToString(), System.DateTime.Now, 'A', Convert.ToInt32(Br_Tr_No), Convert.ToInt32(HO_Tr_No));
                    DAL.AdviceManager.GetInstance().Save(ad1);
                    PopulateAdviceNumber();
                    ClearAll();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('Print.aspx?ID=" + ad.ID + "','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=300,height=300');", true);
                }
                catch (Exception ex)
                {
                    lbl_Error.Text = ex.Message;
                }
            }
            else
            {
                lbl_Error.Text = "Cannot Aunthenticate Transaction with the same user, Please login with other user!";
            }
        }

        private void PopulateAdviceNumber()
        {
            ddAdviceNumber.Items.Clear();
            List<Entity.Advice> Advice = DAL.AdviceManager.GetInstance().GetAllAdvice();
            ListItem li;
            foreach (Entity.Advice ad in Advice)
            {
                Entity.Branches_Names bn = DAL.Branches_NamesManager.GetInstance().GetByCode(ad.Branch_Code);
                li = new ListItem(ad.Advice_No + " - " + bn.Branch_RCode, ad.Advice_No);
                ddAdviceNumber.Items.Add(li);
            }
        }

        private void ClearAll()
        {
            lbl_Advice.Text = String.Empty;
            lbl_Amount.Text = String.Empty;
            lbl_Branch_Code.Text = String.Empty;
            lbl_Branch_GSL_Code.Text = String.Empty;
            lbl_C_I.Text = String.Empty;
            lbl_D_C.Text = String.Empty;
            lbl_Description.Text = String.Empty;
            lbl_ID.Text = String.Empty;
            lbl_O_R.Text = String.Empty;
            lbl_Transaction_Maker.Text = String.Empty;
            lbl_Ho_R_O.Text = String.Empty;
            Btn_Make_Transaction.Visible = false;
            Btn_Delete_Transaction.Visible = false;
        }

        protected void Btn_Delete_Transaction_Click(object sender, EventArgs e)
        {
            Entity.Advice ad3 = DAL.AdviceManager.GetInstance().GetByAdviceNumber(lbl_Advice.Text);
            if (ad3.user != Session["user"].ToString())
            {
                try
                {
                    Entity.Advice ad = DAL.AdviceManager.GetInstance().GetByAdviceNumber(ddAdviceNumber.SelectedItem.Value);
                    Entity.Advice ad1 = new Advice_Entry_System.Entity.Advice(ad.ID, ad.Branch_Code, ad.GSL, ad.Dr_Cr, ad.Amount, ad.Advice_No, ad.Description, ad.user, ad.type, ad.transactiondate, ad.Branch_Reply, ad.HO_Reply, ad.Message, ad.O_R, Session["user"].ToString(), System.DateTime.Now, 'D', ad.Branch_Transaction_Number, ad.HO_Transaction_Number);
                    DAL.AdviceManager.GetInstance().Save(ad1);
                    PopulateAdviceNumber();
                    ClearAll();
                }
                catch (Exception ex)
                {
                    lbl_Error.Text = ex.Message;
                }
            }
            else
            {
                lbl_Error.Text = "Cannot Aunthenticate Transaction with the same user, Please login with other user!";
            }
        }
    }
}

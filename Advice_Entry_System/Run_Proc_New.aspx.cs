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
    public partial class Run_Proc_New : System.Web.UI.Page
    {
        string value = ""; // Branch
        string value1 = ""; // HO 

        string Br_Tr_No = "";
        string HO_Tr_No = "";

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
                    ddGSL.Items[0].Enabled = false;
                    ddGSL.Items[3].Enabled = false;

                    ddGSL.Items[2].Enabled = true;
                    ddGSL.Items[1].Enabled = true;
                }
            }
        }

        protected void Btn_Logout_Click(object sender, EventArgs e)
        {
            BLL.BLL.ClearAllSessionVariables(Session);
            Response.Redirect("Login.aspx");
        }

        protected void Btn_Generate_Transaction_Click(object sender, EventArgs e)
        {
            string msg = String.Empty;
            if (Session["user"] != null || Session["type"] != null)
            {
                if (Utility.Validation.IsNotEmpty(txt_GSL.Text) && Utility.Validation.IsNotEmpty(txt_Amount.Text) && Utility.Validation.IsNotEmpty(txt_Advice_No.Text) && Utility.Validation.IsNotEmpty(txt_Description.Text))
                {
                    if (Utility.Validation.IsInteger(txt_GSL.Text) && Utility.Validation.IsInteger(txt_Amount.Text))
                    {
                        if (rbDr_Cr.SelectedIndex.ToString() == "0" || rbDr_Cr.SelectedIndex.ToString() == "1")
                        {
                            if (txt_GSL.Text != "5101")
                            {
                                Entity.Advice ad = new Advice_Entry_System.Entity.Advice(Branches_Name.SelectedItem.Value.ToString(), txt_GSL.Text, Convert.ToChar(rbDr_Cr.SelectedIndex.ToString()), Convert.ToInt64(txt_Amount.Text), txt_Advice_No.Text, txt_Description.Text, Session["user"].ToString(), Session["type"].ToString(), System.DateTime.Now, Convert.ToChar(rb_O_R.SelectedIndex.ToString() == "0" ? "O" : "R"),System.DateTime.Now);
                                DAL.AdviceManager.GetInstance().Save(ad);
                                 
                                Entity.Branches_Names ho = DAL.Branches_NamesManager.GetInstance().GetByCode(Branches_Name.SelectedItem.Value.ToString());

                                if (BLL.BLL.pingcall(ho.Branch_RCode)) // Check Weather Branch is Up or Down
                                {
                                    if (BLL.BLL.Make_Advice_Check_Branch(ho.Branch_RCode, txt_Advice_No.Text)) //Check Branch of the Advice Number
                                    {
                                        //msg = Generate_Transaction();
                                        msg = "Transaction Created, Please Authenticate!";
                                    }
                                    else
                                    {
                                        msg = "Advice Number Already Exist in the Branch";
                                    }
                                }
                                else
                                {
                                    msg = "Branch Is Down";
                                }

                                lbl_error.Visible = true;
                                //lbl_error.Text = " Transaction Generated on GSL : " + txt_GSL.Text + ", at Branch: " + Branches_Name.SelectedItem.Text + ", of Amount: " + txt_Amount.Text + " ," + msg;
                                lbl_error.Text = msg;
                                ClearAll();
                                //if value is 100 branch reply is yes else no
                                //if value1 is 100 HO reply is yes else no

                                if (msg.Contains("'"))
                                {
                                    msg = msg.Replace("'", "''");
                                }

                                Entity.Advice ad1 = new Advice_Entry_System.Entity.Advice(ad.ID, ad.Branch_Code, ad.GSL, ad.Dr_Cr, ad.Amount, ad.Advice_No, ad.Description, ad.user, ad.type, ad.transactiondate, (value == "100" ? Entity.Advice.RequestReply.Yes : Entity.Advice.RequestReply.No), (value1 == "100" ? Entity.Advice.RequestReply.Yes : Entity.Advice.RequestReply.No), msg, ad.O_R, ad.Final_Action, ad.Final_Action_Date, ad.Transaction_Status);
                                DAL.AdviceManager.GetInstance().Save(ad1);
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('Print.aspx?ID=" + ad.ID + "','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=300,height=300');", true);
                                //Response.Redirect("Print.aspx?ID=" + ad.ID);
                                Btn_Change.Visible = false;
                                Btn_GSL_Name.Visible = true;
                                lbl_GSL.Visible = false;
                                Btn_Generate_Transaction.Visible = false;
                                txt_GSL.Enabled = true;
                            }
                            else
                            {
                                lbl_error.Visible = true;
                                lbl_error.Text = "GSL Cannot be 5101, Please Change!";
                            }
                        }
                        else
                        {
                            Entity.Advice.RequestReply.Yes.ToString();

                            lbl_error.Visible = true;
                            lbl_error.Text = "Please Select Any of the Debit or Credit";
                        }
                    }
                    else
                    {
                        lbl_error.Visible = true;
                        lbl_error.Text = "Please Enter Numeric Values of GSL and Amount";
                    }
                }
                else
                {
                    lbl_error.Visible = true;
                    lbl_error.Text = "Please Complete All the Information";
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        private string Generate_Transaction()
        {
            string retvalue = String.Empty;
            string horeply = String.Empty;
            string branchreply = String.Empty;
            string HO_GSL = "5101";
            char debit = '0';
            char credit ='1';
            
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
            Entity.Branches_Names ho = DAL.Branches_NamesManager.GetInstance().GetByCode(Branches_Name.SelectedItem.Value.ToString());

            if (BLL.BLL.pingcall(ho.Branch_RCode)) // Check Weather Branch is Up or Down
            {
                if (BLL.BLL.Make_Advice_Check_Branch(ho.Branch_RCode, txt_Advice_No.Text)) //Check Branch of the Advice Number
                {
                    //calling branches and HO
                    if (rbDr_Cr.SelectedItem.Value.ToString() == "0") // Debit Branch First then credit HO
                    {
                        branchreply = BLL.BLL.Make_Transaction_Branch(type1,ho.Branch_RCode, txt_GSL.Text, debit, Convert.ToChar(rb_O_R.SelectedIndex.ToString() == "1" ? "O" : "R"), Convert.ToInt64(txt_Amount.Text), txt_Advice_No.Text, txt_Description.Text, out value, out Br_Tr_No);
                        if (branchreply == "Transaction Generated Successfully!" || value == "100")
                        {
                            horeply = BLL.BLL.Make_Transaction_HO(type1, ho.Branch_RCode, HO_GSL, credit, Convert.ToChar(rb_O_R.SelectedIndex.ToString() == "0" ? "O" : "R"), Convert.ToInt64(txt_Amount.Text), txt_Advice_No.Text, txt_Description.Text, out value1, out HO_Tr_No);
                        }
                    }
                    else if (rbDr_Cr.SelectedItem.Value.ToString() == "1") // Credit HO First then Debit Branch
                    {
                        horeply = BLL.BLL.Make_Transaction_HO(type1, ho.Branch_RCode, HO_GSL, debit, Convert.ToChar(rb_O_R.SelectedIndex.ToString() == "0" ? "O" : "R"), Convert.ToInt64(txt_Amount.Text), txt_Advice_No.Text, txt_Description.Text, out value1, out HO_Tr_No);
                        if (horeply == "Transaction Generated Successfully!" || value == "100")
                        {
                            branchreply = BLL.BLL.Make_Transaction_Branch(type1, ho.Branch_RCode, txt_GSL.Text, credit, Convert.ToChar(rb_O_R.SelectedIndex.ToString() == "1" ? "O" : "R"), Convert.ToInt64(txt_Amount.Text), txt_Advice_No.Text, txt_Description.Text, out value, out Br_Tr_No);
                        }
                    }

                    //writing label
                    if (rbDr_Cr.SelectedItem.Value.ToString() == "0") // Debit Branch Fist then credit HO
                    {
                        retvalue = " Reply from Branch : " + branchreply + ", Reply From HO : " + horeply;
                    }
                    else if (rbDr_Cr.SelectedItem.Value.ToString() == "1") // Credit HO First then Debit Branch
                    {
                        retvalue = " Reply from HO : " + horeply + ", Reply From Branch : " + branchreply;
                    }
                }
                else
                {
                    retvalue = "Advice Number Already Exist in the Branch";
                }
            }
            else
            {
                retvalue = "Branch Is Down";
            }

            return retvalue;
        }

        private void ClearAll()
        {
            txt_Advice_No.Text = String.Empty;
            txt_Amount.Text = String.Empty;
            txt_Description.Text = String.Empty;
            txt_GSL.Text = String.Empty;
            ddGSL.Visible = true;
            Branches_Name.Enabled = true;
        }

        protected void Btn_GSL_Name_Click(object sender, EventArgs e)
        {
            txt_GSL.Text = ddGSL.SelectedItem.Value.ToString();
            ddGSL.Visible = false;
            Branches_Name.Enabled = false;
            Btn_ChangeOR.Visible = false;
            Btn_ChangeDC.Visible = false;
            rb_O_R.Enabled = false;
            rbDr_Cr.Enabled = false;

            //Get Branch Code
            Entity.Branches_Names ho = DAL.Branches_NamesManager.GetInstance().GetByCode(Branches_Name.SelectedItem.Value.ToString());
            if (Utility.Validation.IsNotEmpty(txt_GSL.Text))
            {
                if (Utility.Validation.IsInteger(txt_GSL.Text))
                {
                    if (BLL.BLL.pingcall(ho.Branch_RCode)) // Check Weather Branch is Up or Down
                    {
                        lbl_GSL.Text = BLL.BLL.GetGSLName(txt_GSL.Text, Branches_Name.SelectedItem.Value.ToString());
                        if (lbl_GSL.Text != "")
                        {
                            lbl_GSL.Visible = true;
                            Btn_Generate_Transaction.Visible = true;
                            Btn_GSL_Name.Visible = false;
                            Btn_Change.Visible = true;
                            txt_GSL.Enabled = false;
                        }
                    }
                    else
                    {
                        lbl_error.Text = "Branch Is Down";
                        lbl_error.Visible = true;
                        lbl_GSL.Visible = false;
                        Btn_Generate_Transaction.Visible = false;
                        Btn_GSL_Name.Visible = true;
                        txt_GSL.Enabled = true;
                        Btn_Change.Visible = false;
                        //to add
                        ddGSL.Visible = true;
                        txt_GSL.Text = "";
                        //to add finsih
                    }
                }
                else
                {
                    lbl_error.Text = "Please Enter Numeric Values of GSL";
                    lbl_error.Visible = true;
                }
            }
            else
            {
                lbl_error.Text = "Please Enter GSL";
                lbl_error.Visible = true;
            }
        }

        protected void Btn_Change_Click(object sender, EventArgs e)
        {
            Branches_Name.Enabled = true;
            ddGSL.Visible = true;
            txt_GSL.Text = "";
            Btn_Generate_Transaction.Visible = false;
            Btn_Change.Visible = false;
            txt_GSL.Enabled = true;
            Btn_GSL_Name.Visible = true;
            lbl_GSL.Visible = false;
            lbl_GSL.Text = "";
            lbl_error.Visible = false;

            Btn_ChangeOR.Visible = true;
            Btn_ChangeDC.Visible = true;
        }

        protected void Branches_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Branches_Name.SelectedItem.Value.ToString()) < 100) // Conventional Side
            {
                ddGSL.Items[0].Enabled = false;
                ddGSL.Items[3].Enabled = false;

                ddGSL.Items[2].Enabled = true;
                ddGSL.Items[1].Enabled = true;
            }
            else // Islamic Side
            {
                ddGSL.Items[2].Enabled = false;
                ddGSL.Items[1].Enabled = false;

                ddGSL.Items[0].Enabled = true;
                ddGSL.Items[3].Enabled = true;
            }
            ChangeSelection();
        }

        protected void ddGSL_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeSelection();
        }

        protected void ChangeSelection()
        {
            if (ddGSL.SelectedItem.Value.ToString() == "5032" || ddGSL.SelectedItem.Value.ToString() == "5039")
            {
                rbDr_Cr.SelectedIndex = 1;
            }
            else
            {
                rbDr_Cr.SelectedIndex = 0;
            }

            rbDr_Cr.Enabled = false;
            Btn_ChangeDC.Visible = true;
            rb_O_R.Enabled = false;
            Btn_ChangeOR.Visible = true;
        }

        protected void Btn_ChangeDC_Click(object sender, EventArgs e)
        {
            rbDr_Cr.Enabled = true;
            Btn_ChangeDC.Visible = false;
        }

        protected void Btn_ChangeOR_Click(object sender, EventArgs e)
        {
            rb_O_R.Enabled = true;
            Btn_ChangeOR.Visible = false;
        }

        protected void SqlDataSourceBranches_Load(object sender, EventArgs e)
        {
            Encryption.Key = ConfigurationManager.AppSettings["AdviceKey"].ToString();
            String PlainText = "";
            PlainText = Encryption.GetDecryptedConnectionString(ConfigurationManager.ConnectionStrings["Advice_Entry_System.Properties.Settings.ConStr"].ConnectionString);
            SqlDataSourceBranches.ConnectionString = PlainText;
        }
    }
}

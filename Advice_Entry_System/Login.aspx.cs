using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Advice_Entry_System
{
    public partial class Login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn_Login_Click(object sender, EventArgs e)
        {
            string value = String.Empty;
            BLL.BLL.ClearAllSessionVariables(Session);
            bool userAuth = false;
            if (rb_HO.SelectedIndex.ToString() == "0" || rb_HO.SelectedIndex.ToString() == "1")
            {
                if (txt_user.Text != String.Empty && txt_password.Text != String.Empty)
                {
                    if (Utility.Validation.IsInteger(txt_user.Text) && Utility.Validation.PasswordLenght(txt_password.Text))
                    {
                        userAuth = BLL.BLL.AuthenticateUser(txt_user.Text, txt_password.Text, Convert.ToChar(rb_HO.SelectedIndex.ToString()),out value);
                        if (userAuth == true)
                        {
                            Session.Add("user", txt_user.Text);

                            if (rb_HO.SelectedIndex.ToString() == "0")
                                Session.Add(("type"), "Conventional");
                            else if (rb_HO.SelectedIndex.ToString() == "1")
                                Session.Add(("type"), "Islamic");

                            Response.Redirect("Run_Proc_New.aspx");
                        }
                        else
                        {
                            lbl_error.Text = value;
                            lbl_error.Visible = true;
                            lbl_HOType.Visible = false;
                        }
                    }
                    else
                    {
                        lbl_error.Text = "Invalid UserName /  Password";
                        lbl_error.Visible = true;
                    }
                }
                else
                {
                    lbl_error.Text = "Invalid UserName /  Password";
                    lbl_error.Visible = true;
                    lbl_HOType.Visible = false;
                }
            }
            //else if (rb_HO.SelectedIndex.ToString() == "1")
            //{
            //    lbl_HOType.Visible = false;
            //}
            else
            {
                lbl_HOType.Visible = true;
                lbl_error.Visible = false;
            }
        }
    }
}

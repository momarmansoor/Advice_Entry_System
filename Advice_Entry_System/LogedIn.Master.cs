using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Advice_Entry_System
{
    public partial class LogedIn : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn_Logout_Click(object sender, EventArgs e)
        {
            BLL.BLL.ClearAllSessionVariables(Session);
            Response.Redirect("Login.aspx");
        }
    }
}

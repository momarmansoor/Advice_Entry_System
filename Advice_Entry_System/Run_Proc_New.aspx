<%@ Page Title="" Language="C#" MasterPageFile="~/LogedIn.Master" AutoEventWireup="true" CodeBehind="Run_Proc_New.aspx.cs" Inherits="Advice_Entry_System.Run_Proc_New" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 54px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <script type="text/javascript">
    function isNumberKey(evt) {
         var charCode = (evt.which) ? evt.which : event.keyCode
         if (charCode > 31 && (charCode < 48 || charCode > 57)) {
             alert("Please Enter Only Numeric Values:");
             return false;
         }
         return true;
     }
   </script>
    <div>
        <table style="width: 100%;">
            <tr align="center">
                <td>
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" 
                        Text="Advice Entry System"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;
                    &nbsp;
                    <asp:Label ID="lbl_Connected" runat="server"></asp:Label>
                </td>
                <td align="right">
                    <asp:Button ID="Btn_Logout" runat="server" onclick="Btn_Logout_Click" 
                        Text="Logout" Visible="False" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lbl_error" runat="server" Visible="False" Font-Bold="True" 
                        ForeColor="Red"></asp:Label>
                </td>
                <td align="right">
                    &nbsp;</td>
            </tr>
            </table>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Branche Name :"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="Branches_Name" runat="server" 
                        DataSourceID="SqlDataSourceBranches" DataTextField="Branch_Name" 
                        DataValueField="Branch_Code" AutoPostBack="True" 
                        onselectedindexchanged="Branches_Name_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Branch GSL :"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddGSL" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddGSL_SelectedIndexChanged">
                        <asp:ListItem Value="2731">2731 (Islamic)</asp:ListItem>
                        <asp:ListItem Value="2732" Selected="True">2732 (Conventional)</asp:ListItem>
                        <asp:ListItem Value="5032">5032 (Conventional)</asp:ListItem>
                        <asp:ListItem Value="5039">5039 (Islamic)</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txt_GSL" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbl_GSL" runat="server" Visible="False"></asp:Label>
                    <asp:Button ID="Btn_GSL_Name" runat="server" onclick="Btn_GSL_Name_Click" 
                        Text="GSL Name" />
                    <asp:Button ID="Btn_Change" runat="server" onclick="Btn_Change_Click" 
                        Text="Change" Visible="False" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Debit / Credit :"></asp:Label>
                </td>
                <td>
                    &nbsp;
                    <asp:RadioButtonList ID="rbDr_Cr" runat="server" Enabled="False">
                        <asp:ListItem Value="0" Selected="True">Debit</asp:ListItem>
                        <asp:ListItem Value="1">Credit</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:Button ID="Btn_ChangeDC" runat="server" Text="Change" 
                        onclick="Btn_ChangeDC_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="HO Originating / Responding :"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rb_O_R" runat="server" Enabled="False">
                        <asp:ListItem Selected="True" Value="0">Originating</asp:ListItem>
                        <asp:ListItem Value="1">Responding</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:Button ID="Btn_ChangeOR" runat="server" Text="Change" 
                        onclick="Btn_ChangeOR_Click" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="Label5" runat="server" Text="Amount"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_Amount" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Advice Number :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_Advice_No" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Description"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_Description" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="DTStamp" runat="server" Text="Transaction Date :" 
                        Visible="False"></asp:Label>
                </td>
                <td>
                    <telerik:RadDatePicker ID="sdate" Runat="server" Visible="False">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"></Calendar>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>

<DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" LabelWidth="40%"></DateInput>
                    </telerik:RadDatePicker>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:SqlDataSource ID="SqlDataSourceBranches" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:Advice_Entry_System.Properties.Settings.ConStr %>" 
                        SelectCommand="SELECT [Branch_Code], [Branch_Name] FROM [Branches_Names]" 
                        onload="SqlDataSourceBranches_Load">
                    </asp:SqlDataSource>
                </td>
                <td>
                    <asp:Button ID="Btn_Generate_Transaction" runat="server" 
                        Text="Generate Transaction" onclick="Btn_Generate_Transaction_Click" 
                        Visible="False" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>

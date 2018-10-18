<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Advice_Entry_System.Login1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div align="center">
    <table>
    <tr>
            <td align="center" colspan="3">
                <asp:Label ID="Label1" runat="server" Font-Bold="True"
                    Text="Online Advice Credit / Debit System"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="margin-left: 40px">
                <asp:Label ID="Label2" runat="server" Text="HO Conventional / HO Islamic :"></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="rb_HO" runat="server">
                    <asp:ListItem Value="0" Selected="True">HO Conventional</asp:ListItem>
                    <asp:ListItem Value="1" Enabled="False">HO Islaimc</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <asp:Label ID="lbl_HOType" runat="server" Text="* Please select HO Type " 
                    Visible="False"></asp:Label>
            </td>
        </tr>
    </table>

    <table>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Login :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_user" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Password :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_password" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="Btn_Login" runat="server" Text="Login" onclick="Btn_Login_Click" 
                    />
            </td>
            <td>
                <asp:Label ID="lbl_error" runat="server" Text="Invalid UserName / Password" 
                    Visible="False"></asp:Label>
                    </td>
        </tr>
    </table>
    </div>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/LogedIn.Master" AutoEventWireup="true" CodeBehind="GSL_Inquiry.aspx.cs" Inherits="Advice_Entry_System.GSL_Inquiry" Title="Untitled Page" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td align="center" colspan="3">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" 
                    ForeColor="Black" Text="GSL Inquiry"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                <asp:Label ID="lbl_Connected" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" ForeColor="Black" Text="Enter GSL :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_GSL" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" ForeColor="Black" Text="Select Branch :"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddBranches" runat="server" DataSourceID="SqlDataSource1" 
                    DataTextField="Branch_Name" DataValueField="Branch_Code">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:Advice_Entry_System.Properties.Settings.ConStr %>" 
                    SelectCommand="SELECT [Branch_Code], [Branch_Name] FROM [Branches_Names]" 
                    onload="SqlDataSource1_Load">
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:Button ID="Btn_GetData" runat="server" onclick="Btn_GetData_Click" 
                    Text="Get Data" />
            </td>
            <td>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" ForeColor="Black" 
                    Text="Current Balance :"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl_Balance" runat="server" ForeColor="Black" Font-Bold="True"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td>
                <telerik:RadGrid ID="RadGrid1" runat="server" 
                    onitemdatabound="RadGrid1_ItemDataBound">
                </telerik:RadGrid>
            </td>
        </tr>
        </table>
</asp:Content>

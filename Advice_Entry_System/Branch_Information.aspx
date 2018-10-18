<%@ Page Title="" Language="C#" MasterPageFile="~/LogedIn.Master" AutoEventWireup="true" CodeBehind="Branch_Information.aspx.cs" Inherits="Advice_Entry_System.Branch_Information" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%;">
        <tr>
            <td align="center" colspan="3">
                <asp:Label ID="Label1" runat="server" Text="Branch Information" 
                    Font-Bold="True" Font-Size="Medium" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Branch Name :" ForeColor="Black"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="Branches_Name" runat="server" 
                    DataSourceID="SqlDataSource1" DataTextField="Branch_Name" 
                    DataValueField="Branch_Code">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="Btn_Get_Data" runat="server" Text="Get Data" 
                    onclick="Btn_Get_Data_Click" />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:Advice_Entry_System.Properties.Settings.ConStr %>" 
                    SelectCommand="SELECT [Branch_Code], [Branch_Name] FROM [Branches_Names]" 
                    onload="SqlDataSource1_Load">
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_BranchCode" runat="server" Text="Branch Code :" 
                    Visible="False" ForeColor="Black"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_BranchCode" runat="server" Visible="False"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_UserID" runat="server" Text="Branch User ID :" 
                    Visible="False" ForeColor="Black"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_UserID" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_Password" runat="server" Text="Branch Password :" 
                    Visible="False" ForeColor="Black"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_Password" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_DataBaseName" runat="server" Text="Data Base Name :" 
                    Visible="False" ForeColor="Black"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_DataBaseName" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

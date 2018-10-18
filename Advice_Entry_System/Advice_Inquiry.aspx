<%@ Page Title="" Language="C#" MasterPageFile="~/LogedIn.Master" AutoEventWireup="true" CodeBehind="Advice_Inquiry.aspx.cs" Inherits="Advice_Entry_System.Advice_Inquiry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td align="center" colspan="3">
                <asp:Label ID="Label1" runat="server" Text="Advice Inquiry" Font-Size="Medium" 
                    Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                <asp:Label ID="lbl_Connected" runat="server" Text="" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAdvice" runat="server" Text="Advice Number :" 
                    ForeColor="Black"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_Advice" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Branch :" ForeColor="Black"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="Branches_Name" runat="server" 
                    DataSourceID="SqlDataSource1" DataTextField="Branch_Name" 
                    DataValueField="Branch_Code">
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
                <asp:Button ID="Btn_Get_Data" runat="server" Text="Get Data" 
                    onclick="Btn_Get_Data_Click" />
            </td>
            <td>
                <asp:Label ID="lblerror" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    
    <table>
        <tr>
            <td>
                <telerik:RadGrid ID="rg_Data" runat="server">
                </telerik:RadGrid>
            </td>
        </tr>
        </table>
</asp:Content>

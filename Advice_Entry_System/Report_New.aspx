<%@ Page Title="" Language="C#" MasterPageFile="~/LogedIn.Master" AutoEventWireup="true" CodeBehind="Report_New.aspx.cs" Inherits="Advice_Entry_System.Report_New" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table>
            <tr>
                <td colspan="3" align="center">
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    <asp:Label ID="Label1" runat="server" Text="Advice Entry System Report" 
                        Font-Bold="True" Font-Size="X-Large" ForeColor="Black"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="lbl_Connected" runat="server" ForeColor="Black"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="rbList" runat="server" AutoPostBack="True" 
                        ForeColor="Black" onselectedindexchanged="rbList_SelectedIndexChanged">
                        <asp:ListItem Value="0">By Date :</asp:ListItem>
                        <asp:ListItem Value="1">By Advice Number :</asp:ListItem>
                        <asp:ListItem Value="2">By Branch:</asp:ListItem>
                        <asp:ListItem Value="3">BY Amount :</asp:ListItem>
                        <asp:ListItem Value="4">By GSL:</asp:ListItem>
                        <asp:ListItem Value="5">By User:</asp:ListItem>
                        <asp:ListItem Value="6">Deleted Transaction</asp:ListItem>
                        <asp:ListItem Value="7">Fail Transaction</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
               
                <td>
                    <telerik:RadDatePicker ID="rdDate" Runat="server" Visible="False">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>

<DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy"></DateInput>
                    </telerik:RadDatePicker>
                    <asp:DropDownList ID="ddBranches" runat="server" DataSourceID="SqlDataSource1" 
                        DataTextField="Branch_Name" DataValueField="Branch_Code" Visible="False">
                    </asp:DropDownList>
                    <asp:TextBox ID="txt_box" runat="server" Visible="False"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" rowspan="2">
                    <asp:Button ID="Btn_GetData" runat="server" onclick="Btn_GetData_Click" 
                        Text="Get Data" />
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:Advice_Entry_System.Properties.Settings.ConStr %>" 
                        SelectCommand="SELECT [Branch_Code], [Branch_Name] FROM [Branches_Names]" 
                        onload="SqlDataSource1_Load">
                    </asp:SqlDataSource>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:RadGrid ID="rgData" runat="server">
                    </telerik:RadGrid>
                </td>
            </tr>
            </table>
    </div>
</asp:Content>

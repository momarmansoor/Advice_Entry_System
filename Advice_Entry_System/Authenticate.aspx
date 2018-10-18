<%@ Page Title="" Language="C#" MasterPageFile="~/LogedIn.Master" AutoEventWireup="true" CodeBehind="Authenticate.aspx.cs" Inherits="Advice_Entry_System.Authenticate" Theme="Label" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr align="center">
            <td colspan="3">
                <asp:Label ID="lbl_Connected" runat="server" Text="Label" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" ForeColor="Black" Text="Advice Number :"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddAdviceNumber" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="Btn_GetDetails" runat="server" Text="Get Advice Details" 
                    onclick="Btn_GetDetails_Click" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                <asp:Label ID="lbl_Error" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    
    <table>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="ID :" Font-Bold="True" 
                    Visible="False"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl_ID" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Branch Code :" Font-Bold="True" 
                    Visible="False"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl_Branch_Code" runat="server" Visible="False"></asp:Label>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Branch GSL  Code :" 
                    Font-Bold="True" Visible="False"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl_Branch_GSL_Code" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Debit / Credit :" Font-Bold="True" 
                    Visible="False"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl_D_C" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Amount :" Font-Bold="True" 
                    Visible="False"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl_Amount" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label18" runat="server" Text="Advice :" Font-Bold="True" 
                    Visible="False"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl_Advice" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Description :" Font-Bold="True" 
                    Visible="False"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl_Description" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Transaction Maker :" 
                    Font-Bold="True" Visible="False"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl_Transaction_Maker" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Islamic / Conventional :" 
                    Font-Bold="True" Visible="False"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl_C_I" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="HO Orignating / Responding :" 
                    Font-Bold="True" Visible="False"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl_O_R" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label19" runat="server" Text="Branch Orignating / Responding :" 
                    Font-Bold="True" Visible="False"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl_Ho_R_O" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="Btn_Delete_Transaction" runat="server" 
                    onclick="Btn_Delete_Transaction_Click" Text="Delete Transaction" 
                    Visible="False" />
            </td>
            <td>
                <asp:Button ID="Btn_Make_Transaction" runat="server" 
                    Text="Generate Transaction" Visible="False" 
                    onclick="Btn_Make_Transaction_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

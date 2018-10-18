<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="Advice_Entry_System.Print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="border-left: 3px solid black; border-right: 3px solid black" border="1" >
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="Btn_Print" runat="server" Text="Print"  
                            OnClientClick="javascript:self.print();return false;" 
                            onclick="Btn_Print_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Size="Medium" 
                            ForeColor="#666699" Text="The Bank Of Khyber"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Size="Medium" 
                            ForeColor="Black" Text="Advice Entry System : Transaction Processed Details"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="ID :" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblID" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Branch Code :" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblBranchCode" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Branch GSL :" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblGSL" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Branch Debit/Credit :" 
                            Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblD_C" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="HO Originating/Responding :" 
                            Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblO_R" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Amount :" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Advice Number :" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAdvice" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Description :" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="User :" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbluser" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label20" runat="server" Text="Type :" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbltype" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label21" runat="server" Text="Transaction Date :" 
                            Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTransactionDate" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label22" runat="server" Text="HO Reply :" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblhoreply" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label23" runat="server" Text="Branch Reply :" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblBranchReply" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label24" runat="server" Text="Message :" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label29" runat="server" Text="Transaction Status :"  Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label27" runat="server" Text="HO Transaction Number :" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_HO_Trans" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label28" runat="server" Text="Branch Transaction Number :" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_Branch_Transaction" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
    </div>
    </form>
</body>
</html>

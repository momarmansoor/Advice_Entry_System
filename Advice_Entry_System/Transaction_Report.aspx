<%@ Page Title="" Language="C#" MasterPageFile="~/LogedIn.Master" AutoEventWireup="true" CodeBehind="Transaction_Report.aspx.cs" Inherits="Advice_Entry_System.Transaction_Report" Theme ="Label"  %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function PrintRadGrid(radGrid)
            {
                setTimeout(function()
                {
                    var previewWnd = window.open('about:blank', '', '', false);
                    var styleStr = "<html><head><link href='Styles.css' rel='stylesheet' type='text/css'></link></head>";
                    var htmlcontent = styleStr + "<body>" + $find(radGrid).get_element().outerHTML + "</body></html>";
                    previewWnd.document.open();
                    previewWnd.document.write(htmlcontent);
                    previewWnd.document.close();
                    previewWnd.print();
                    previewWnd.close();
                }, 100);
            }
        </script>

    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <tr>
            <td align="center">
                <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Size="Larger" 
                    Text="Transaction Detail" Font-Underline="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="Btn_Print" runat="server" Text="Print" 
                    OnClientClick="javascript:self.print();return false;" 
                    onclick="Btn_Print_Click"/>
            </td>
        </tr>
        <tr>
        <td>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="ID :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblID" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Branch Code :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblBranchCode" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="GSL :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblGSL" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Debit/Credit :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblD_C" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="O/R :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblO_R" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Amount :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Advice Number :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAdvice" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Description :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="User :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbluser" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label20" runat="server" Text="Type :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbltype" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label21" runat="server" Text="Transaction Date :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTransactionDate" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label22" runat="server" Text="HO Reply :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblhoreply" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label23" runat="server" Text="Branch Reply :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblBranchReply" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label24" runat="server" Text="Message :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" Visible="false">Print Grid</asp:LinkButton>
 
                <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" >
                    <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" PageSize="9" Skin="Simple"
                Style="margin: 20px;" >
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
            </td>
        </tr>
        </table>
</asp:Content>

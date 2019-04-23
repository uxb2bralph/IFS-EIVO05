<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DonateReportList.ascx.cs"
    Inherits="eIVOGo.Module.Inquiry.DonateReportList" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Data.Linq" %>
<%@ Import Namespace="Model.DataEntity" %>
<%@ Register Src="../Common/CalendarInputDatePicker.ascx" TagName="CalendarInputDatePicker" TagPrefix="uc2" %>
<%@ Register Src="../Common/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc1" %>
<%@ Register Src="../Common/PrintingButton2.ascx" TagName="PrintingButton2" TagPrefix="uc3" %>
<!--路徑名稱-->
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
        <td width="30">
            <img runat="server" enableviewstate="false" id="img6" src="~/images/path_left.gif"
                alt="" width="30" height="29" />
        </td>
        <td bgcolor="#ecedd5">
            首頁 > 捐贈統計表
        </td>
        <td width="18">
            <img runat="server" enableviewstate="false" id="img2" src="~/images/path_right.gif"
                alt="" width="18" height="29" />
        </td>
    </tr>
</table>
<!--交易畫面標題-->
<h1>
    <img runat="server" enableviewstate="false" id="img3" src="~/images/icon_search.gif"
        width="29" height="28" border="0" align="absmiddle" />捐贈統計表</h1>
<!--表格 開始-->
<div  id="border_gray">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
    <tr>
        <th colspan="2" class="Head_style_a">
            查詢條件
        </th>
    </tr>
    <tr>
        <th width="20%">
            <span class="table-title">報表類別</span>
        </th>
        <td class="tdleft">
            &nbsp;<asp:RadioButton ID="RadioButton1" runat="server" Text="捐贈統計表" Checked="True" GroupName="optSelect" />
            &nbsp;<asp:RadioButton ID="RadioButton2" runat="server" Text="捐贈中獎統計表" GroupName="optSelect" />
        </td>
    </tr>
    <tr>
        <th>
            日期區間
        </th>
        <td class="tdleft">
            自&nbsp;<uc2:CalendarInputDatePicker ID="CalendarInputDatePicker1" runat="server" />
            至&nbsp;<uc2:CalendarInputDatePicker ID="CalendarInputDatePicker2" runat="server" />
        </td>
    </tr>
    <tr>
        <th>
            營 業 人
        </th>
        <td class="tdleft">
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <th>
            社福團體
        </th>
        <td class="tdleft">
            <asp:DropDownList ID="DropDownList2" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
</table>
<!--表格 結束-->
</div>
<!--按鈕-->
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td class="Bargain_btn">
            <asp:Button ID="btnQuery" runat="server" CssClass="btn" OnClick="btnQuery_Click"
                Text=" 查詢" />
        </td>
    </tr>
</table>
<div id="Div1" runat="server" clientidmode="Static">
    <h1><img runat="server" enableviewstate="false" id="img4" src="~/images/icon_search.gif" width="29" height="28" border="0" align="absmiddle" />查詢結果</h1>
</div>
<div id="border_gray" runat="server" clientidmode="Static">
    <!--表格 開始-->
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table01">
        <tr>
            <th nowrap="nowrap">
                社福代碼
            </th>
            <th nowrap="nowrap">
                社福名稱
            </th>
            <th nowrap="nowrap" runat="server">
                發票號碼
            </th>
            <th nowrap="nowrap" runat="server" id="trWinYesNo">
                是否中獎
            </th>
        </tr>
        <asp:Repeater ID="rpList" runat="server" EnableViewState="false">
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <%#  ((ToDataInquiry)Container.DataItem).AgencyCode.Substring(((ToDataInquiry)Container.DataItem).AgencyCode.Length - 3, 3) != "tmp" ? Eval("AgencyCode") : ("捐贈張數 : " + Eval("InvoCount"))%>
                    </td>
                    <td>
                        <%# Eval("WAName")%>
                    </td>
                    <td>
                        <%# Eval("No")%>
                    </td>
                    <td align="right" runat="server" visible='<%# trWinYesNo.Visible %>'>
                        <%# Eval("WinningType") != null ? Eval("WinningType") : ""%>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="OldLace">
                    <td align="center">
                        <%#  ((ToDataInquiry)Container.DataItem).AgencyCode.Substring(((ToDataInquiry)Container.DataItem).AgencyCode.Length - 3, 3) != "tmp" ? Eval("AgencyCode") : ("捐贈張數 : " + Eval("InvoCount"))%>
                    </td>
                    <td>
                        <%# Eval("WAName")%>
                    </td>
                    <td>
                        <%# Eval("No")%>
                    </td>
                    <td id="Td1" align="right" runat="server" visible='<%# trWinYesNo.Visible %>'>
                        <%# Eval("WinningType") != null ? Eval("WinningType") : ""%>
                    </td>
                </tr>
            </AlternatingItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="4" align="right" class="total-count">
                總計&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Literal ID="litTotal" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Literal
                    ID="litDonate" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <uc1:PagingControl ID="PagingControl1" runat="server" />
</div>
<!--表格 結束-->
<!--按鈕-->
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td class="Bargain_btn">
            <uc3:PrintingButton2 ID="btnPrint" runat="server" />
            &nbsp;
            <asp:Button ID="btnExcel" runat="server" CssClass="btn" OnClick="btnExcel_Click" Text="下載Excel檔案" />
        </td>
    </tr>
</table>
<cc1:InvoiceDataSource ID="dsInv" runat="server">
</cc1:InvoiceDataSource>

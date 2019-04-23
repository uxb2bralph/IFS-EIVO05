<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DonateReport.ascx.cs"
    Inherits="eIVOGo.Module.Inquiry.DonateReport" %>
<%@ Register Src="../UI/PageAction.ascx" TagName="PageAction" TagPrefix="uc1" %>
<%@ Register Src="../Common/CalendarInputDatePicker.ascx" TagName="CalendarInputDatePicker" TagPrefix="uc2" %>
<%@ Register Src="../Common/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="../Common/PrintingButton2.ascx" TagName="PrintingButton2" TagPrefix="uc3" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<%@ Register Src="DonatedInvoiceList.ascx" TagName="DonatedInvoiceList" TagPrefix="uc4" %>
<!--路徑名稱-->


<uc1:PageAction ID="actionItem" runat="server" ItemName="首頁 > 捐贈統計表" />
<!--交易畫面標題-->
<h1>
    <img runat="server" enableviewstate="false" id="img3" src="~/images/icon_search.gif"
        width="29" height="28" border="0" align="absmiddle" />捐贈統計表
</h1>
<div id="border_gray">
    <!--表格 開始-->
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
                <asp:RadioButtonList ID="rbReport" runat="server" RepeatDirection="Horizontal" CssClass="noborder2" CellPadding="0" CellSpacing="0">
                    <asp:ListItem Selected="True" Value="0">捐贈統計表</asp:ListItem>
                    <asp:ListItem Value="1">捐贈中獎統計表</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <th>
                日期區間
            </th>
            <td class="tdleft">
                自&nbsp;&nbsp; 
                <uc2:CalendarInputDatePicker ID="InvDateFrom" runat="server" />
                至&nbsp; <uc2:CalendarInputDatePicker ID="InvDateTo" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                營 業 人
            </th>
            <td class="tdleft">
                <asp:DropDownList ID="SellerID" runat="server">
                    <asp:ListItem>全部</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>
                社福團體
            </th>
            <td class="tdleft">
                <asp:DropDownList ID="AgencyID" runat="server">
                    <asp:ListItem>全部</asp:ListItem>
                    <asp:ListItem>未指定</asp:ListItem>
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
                Text="查詢" />
        </td>
    </tr>
</table>
<div id="Div2" runat="server" clientidmode="Static">
    <h1><img runat="server" enableviewstate="false" id="img4" src="~/images/icon_search.gif"
        width="29" height="28" border="0" align="absmiddle" />查詢結果</h1>
</div>

<div id="border_gray" runat="server" clientidmode="Static">
    <div id="Div1" runat="server" clientidmode="Static">
        <!--表格 開始-->
        <!--table border="0" cellspacing="0" cellpadding="0" width="100%" class="table01">
        <tbody>
            <tr>
                <td-->
        <asp:PlaceHolder ID="phReport" runat="server"></asp:PlaceHolder>
        <uc2:PagingControl ID="pagingList" runat="server" />
        <!--/td>
            </tr>
        </tbody>
    </table-->
        <!--表格 結束-->
    </div>
    <!--表格 結束-->
    <!--按鈕-->
    <table id="table01" runat="server" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td class="Bargain_btn">
                <uc3:PrintingButton2 ID="btnPrint" runat="server" />
                &nbsp;
                <asp:Button ID="btnExcel" runat="server" CssClass="btn" Text="下載Excel檔案" OnClick="btnExcel_Click" />
            </td>
        </tr>
    </table>
    <center>
        <span id="NoData" runat="server" style="color: Red; font-size: Larger;">查無資料!!</span>
    </center>
</div>
<cc1:InvoiceDataSource ID="dsInv" runat="server">
</cc1:InvoiceDataSource>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BonusReportList.ascx.cs"
    Inherits="eIVOGo.Module.Inquiry.BonusReportList" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Data.Linq" %>
<%@ Import Namespace="Model.DataEntity" %>
<%@ Register Src="../Common/CalendarInput.ascx" TagName="CalendarInput" TagPrefix="uc1" %>
<%@ Register Src="../Common/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="../Common/PrintingButton2.ascx" TagName="PrintingButton2" TagPrefix="uc3" %>
<%@ Register src="../UI/SellerSelector.ascx" tagname="SellerSelector" tagprefix="uc4" %>
<%@ Register src="../UI/TwiceMonthlyPeriod.ascx" tagname="TwiceMonthlyPeriod" tagprefix="uc5" %>
<!--路徑名稱-->
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
        <td width="30">
            <img runat="server" enableviewstate="false" id="img6" src="~/images/path_left.gif"
                alt="" width="30" height="29" />
        </td>
        <td bgcolor="#ecedd5">
            首頁 > 中獎統計表
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
        width="29" height="28" border="0" align="absmiddle" />中獎統計表</h1>
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
                日期區間
            </th>
            <td class="tdleft">
                自&nbsp;<uc5:TwiceMonthlyPeriod 
                    ID="PeriodFrom" runat="server" />
                &nbsp; 至&nbsp;<uc5:TwiceMonthlyPeriod ID="PeriodTo" runat="server" />
            </td>
        </tr>
        <tr>
            <th width="20%">
                營 業 人
            </th>
            <td class="tdleft">
                <uc4:SellerSelector ID="SellerID" runat="server" SelectAll="True" />
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
    <h1>
        <img runat="server" enableviewstate="false" id="img5" src="~/images/icon_search.gif"
            width="29" height="28" border="0" align="absmiddle" />查詢結果</h1>
</div>
<div id="border_gray" runat="server" clientidmode="Static">
    <!--表格 開始-->
    <div id="Div2" runat="server" clientidmode="Static">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table01">
            <tr>
                <th nowrap="nowrap">
                    營業人統編
                </th>
                <th nowrap="nowrap">
                    營業人名稱
                </th>
                <th nowrap="nowrap">
                    營業人地址
                </th>
                <th nowrap="nowrap">
                    中獎張數
                </th>
                <th nowrap="nowrap">
                    捐贈張數
                </th>
            </tr>
            <asp:Repeater ID="rpList" runat="server" EnableViewState="false">
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <%# ((IGrouping<int?,InvoiceWinningNumber>)Container.DataItem).First().InvoiceItem.Organization.ReceiptNo %>
                        </td>
                        <td>
                            <%# ((IGrouping<int?,InvoiceWinningNumber>)Container.DataItem).First().InvoiceItem.Organization.CompanyName %>
                        </td>
                        <td>
                            <%# ((IGrouping<int?,InvoiceWinningNumber>)Container.DataItem).First().InvoiceItem.Organization.Addr %>
                        </td>
                        <td align="right">
                            <%# ((IGrouping<int?,InvoiceWinningNumber>)Container.DataItem).Where(v => v.InvoiceItem.InvoiceCancellation == null).Count() %>
                        </td>
                        <td align="right">
                            <%# ((IGrouping<int?,InvoiceWinningNumber>)Container.DataItem).Where(w=>(w.InvoiceItem.DonateMark=="1") && (w.InvoiceItem.InvoiceCancellation == null)).Count() %>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="OldLace">
                        <td align="center">
                            <%# ((IGrouping<int?,InvoiceWinningNumber>)Container.DataItem).First().InvoiceItem.Organization.ReceiptNo %>
                        </td>
                        <td>
                            <%# ((IGrouping<int?, InvoiceWinningNumber>)Container.DataItem).First().InvoiceItem.Organization.CompanyName%>
                        </td>
                        <td>
                            <%# ((IGrouping<int?,InvoiceWinningNumber>)Container.DataItem).First().InvoiceItem.Organization.Addr %>
                        </td>
                        <td align="right">
                            <%# ((IGrouping<int?,InvoiceWinningNumber>)Container.DataItem).Count() %>
                        </td>
                        <td align="right">
                            <%# ((IGrouping<int?,InvoiceWinningNumber>)Container.DataItem).Where(w=>w.InvoiceItem.DonateMark=="1").Count() %>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="5" align="right" class="total-count">
                    總計&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;中獎總張數：<asp:Literal ID="litTotal" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;捐贈總張數：<asp:Literal
                        ID="litDonate" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <uc2:PagingControl ID="PagingControl1" runat="server" />
    </div>
    <center>
    <span id="NoData" runat="server" style="color:Red;font-size:Larger;">查無資料!!</span>
    </center>
</div>
<!--表格 結束-->
<!--按鈕-->
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td class="Bargain_btn">
            &nbsp;<uc3:PrintingButton2 ID="btnPrint" runat="server" />
        </td>
    </tr>
</table>
<cc1:InvoiceDataSource ID="dsInv" runat="server">
</cc1:InvoiceDataSource>

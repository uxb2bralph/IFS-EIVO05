﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InquireInvoiceItemForBuyer.ascx.cs"
    Inherits="eIVOCenter.Module.Inquiry.InquireInvoiceItemForBuyer" %>
<%@ Register Src="~/Module/UI/PageAction.ascx" TagName="PageAction" TagPrefix="uc1" %>
<%@ Register Src="~/Module/UI/FunctionTitleBar.ascx" TagName="FunctionTitleBar" TagPrefix="uc2" %>
<%@ Register Src="~/Module/Common/CalendarInputDatePicker.ascx" TagName="CalendarInputDatePicker"
    TagPrefix="uc3" %>
<%@ Register Src="~/Module/Inquiry/InvoiceItemList.ascx" TagName="InvoiceItemList"
    TagPrefix="uc4" %>
<%@ Register Src="~/Module/EIVO/Business/MasterBusinessSelector.ascx" TagName="MasterBusinessSelector"
    TagPrefix="uc5" %>
<%@ Register Src="~/Module/Common/EnumSelector.ascx" TagName="EnumSelector" TagPrefix="uc6" %>
<%@ Register Src="~/Module/EIVO/Business/CounterpartBusinessSelector.ascx" TagName="CounterpartBusinessSelector"
    TagPrefix="uc7" %>
<%@ Register src="../Common/PrintingButton2.ascx" tagname="PrintingButton2" tagprefix="uc8" %>


<uc1:PageAction ID="actionItem" runat="server" ItemName="首頁 &gt; 查詢發票" />
<uc2:FunctionTitleBar ID="titleBar" runat="server" ItemName="查詢發票" />
<div class="border_gray">
    <!--表格 開始-->
    <table class="left_title" border="0" cellspacing="0" cellpadding="0" width="100%">
        <tbody>
            <tr>
                <th class="Head_style_a" colspan="2">
                    查詢條件
                </th>
            </tr>
            <tr>
                <th>
                    查詢項目
                </th>
                <td class="tdleft">
                    <asp:RadioButtonList ID="rbChange" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                        AutoPostBack="True" OnSelectedIndexChanged="rbChange_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="~/EIVO/InquireInvoiceItem.aspx">電子發票&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="~/EIVO/InquireInvoiceAllowance.aspx">電子折讓單&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="~/EIVO/InquireInvoiceCancellation.aspx">作廢電子發票&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="~/EIVO/InquireAllowanceCancellation.aspx">作廢電子折讓單</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th>
                    相對營業人
                </th>
                <td class="tdleft">
                    <uc7:CounterpartBusinessSelector ID="CompanyID" runat="server" SelectorIndication="全部" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    日期區間
                </th>
                <td class="tdleft">
                    自&nbsp;
                    <uc3:CalendarInputDatePicker ID="DateFrom" runat="server" />
                    &nbsp;&nbsp; 至&nbsp;<uc3:CalendarInputDatePicker ID="DateTo" runat="server" />
                    &nbsp;&nbsp;
                </td>
            </tr>
        </tbody>
    </table>
    <!--表格 結束-->
</div>
<table border="0" cellspacing="0" cellpadding="0" width="100%">
    <tbody>
        <tr>
            <td class="Bargain_btn">
                <asp:Button ID="btnQuery" runat="server" Text="查詢" class="btn" OnClick="btnQuery_Click" />
            </td>
        </tr>
    </tbody>
</table>
<uc2:FunctionTitleBar ID="resultTitle" runat="server" ItemName="查詢結果" Visible="false" />
<uc4:InvoiceItemList ID="itemList" runat="server" Visible="false" />
<table border="0" cellspacing="0" cellpadding="0" width="100%" runat="server" visible="false"
    enableviewstate="false" id="tblAction">
    <tbody>
        <tr>
            <td class="Bargain_btn">
                <uc8:PrintingButton2 ID="btnPrint" runat="server" />
            </td>
        </tr>
    </tbody>
</table>

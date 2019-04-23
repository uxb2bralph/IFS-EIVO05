﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InquireResendInvoiceItem.ascx.cs"
    Inherits="eIVOCenter.Module.Inquiry.ForOP.InquireResendInvoiceItem" %>
<%@ Register Src="~/Module/UI/PageAction.ascx" TagName="PageAction" TagPrefix="uc1" %>
<%@ Register Src="~/Module/UI/FunctionTitleBar.ascx" TagName="FunctionTitleBar" TagPrefix="uc2" %>
<%@ Register Src="~/Module/Common/CalendarInputDatePicker.ascx" TagName="CalendarInputDatePicker"
    TagPrefix="uc3" %>
<%@ Register Src="~/Module/Inquiry/ForOP/ResendInvoiceItemList.ascx" TagName="InvoiceItemList"
    TagPrefix="uc4" %>
<%@ Register Src="~/Module/EIVO/Business/MasterBusinessSelector.ascx" TagName="MasterBusinessSelector"
    TagPrefix="uc5" %>
<%@ Register Src="~/Module/Common/EnumSelector.ascx" TagName="EnumSelector" TagPrefix="uc6" %>
<%@ Register Src="~/Module/SAM/Business/GroupMemberSelector.ascx" TagName="GroupMemberSelector"
    TagPrefix="uc7" %>

<%@ Register Src="~/Module/SAM/Business/EnterpriseGroupSelector.ascx" TagName="EnterpriseGroupSelector"
    TagPrefix="uc9" %>


<uc1:PageAction ID="actionItem" runat="server" ItemName="首頁 &gt; 重送發票通知" />
<uc2:FunctionTitleBar ID="titleBar" runat="server" ItemName="重送發票通知" />
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
                        <asp:ListItem Selected="True" Value="~/EIVO/OP/ResendInvoiceMail.aspx">電子發票&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
<%--                        <asp:ListItem Value="~/EIVO/OP/InquireInvoiceAllowance.aspx">電子折讓單&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>--%>
                        <asp:ListItem Value="~/EIVO/OP/ResendInvoiceCancellationMail.aspx">作廢電子發票&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
<%--                        <asp:ListItem Value="~/EIVO/OP/InquireAllowanceCancellation.aspx">作廢電子折讓單&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>--%>
                        <asp:ListItem Value="~/EIVO/OP/ResendReceiptItemMail.aspx">收據&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="~/EIVO/OP/ResendReceiptCancellationItemMail.aspx">作廢收據</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th>
                    發票類別
                </th>
                <td class="tdleft">
                    <uc6:EnumSelector ID="BusinessID" runat="server" TypeName="Model.Locale.Naming+InvoiceCenterBusinessQueryType, Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
                        SelectorIndication="全部" />
                </td>
            </tr>
            <tr>
                <th>
                    企業集團
                </th>
                <td class="tdleft">
                        <uc9:EnterpriseGroupSelector ID="EnterpriseID" runat="server" SelectAll="true" />
           
                </td>
            </tr>
            <tr>
                <th>
                    集團成員
                </th>
                <td class="tdleft">
                    <uc7:GroupMemberSelector ID="CompanyID" runat="server" SelectorIndication="全部" />
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
            <tr>
                <th width="20%">
                    營業人統一編號
                </th>
                <td class="tdleft">
                    <asp:TextBox ID="txtReceiptNo" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th width="20%">
                    發票號碼
                </th>
                <td class="tdleft">
                    <asp:TextBox ID="txtInvoiceNO" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    發票狀態
                </th>
                <td class="tdleft">
                    <uc6:EnumSelector ID="LevelID" runat="server" TypeName="Model.Locale.Naming+B2BInvoiceStepDefinition, Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
                        SelectorIndication="全部" />
                </td>
            </tr>
            <tr style="visibility:collapse">
                <th>
                    對帳單
                </th>
                <td class="tdleft">
                    <asp:DropDownList ID="ddlAttach" runat="server">
                        <asp:ListItem Value="">全部</asp:ListItem>
                        <asp:ListItem Value="0">有</asp:ListItem>
                        <asp:ListItem Value="1">無</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="visibility: collapse">
                <th width="20%">
                    列印狀態
                </th>
                <td class="tdleft">
                    <asp:DropDownList ID="ddPrint" runat="server">
                        <asp:ListItem Value="">全部</asp:ListItem>
                        <asp:ListItem Value="1">已列印</asp:ListItem>
                        <asp:ListItem Value="0">未列印</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    主動列印
                </th>
                <td class="tdleft">
                   <asp:DropDownList ID="EntrustToPrint" runat="server">
                       <asp:ListItem Selected="True" Value="">全部</asp:ListItem>
                       <asp:ListItem Value="1">是</asp:ListItem>
                       <asp:ListItem Value="0">否</asp:ListItem>
                    </asp:DropDownList>
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


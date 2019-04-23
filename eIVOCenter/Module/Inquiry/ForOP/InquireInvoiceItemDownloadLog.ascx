﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InquireInvoiceItemDownloadLog.ascx.cs"
    Inherits="eIVOCenter.Module.Inquiry.ForOP.InquireInvoiceItemDownloadLog" %>
<%@ Register Src="~/Module/UI/PageAction.ascx" TagName="PageAction" TagPrefix="uc1" %>
<%@ Register Src="~/Module/UI/FunctionTitleBar.ascx" TagName="FunctionTitleBar" TagPrefix="uc2" %>
<%@ Register Src="~/Module/Common/CalendarInputDatePicker.ascx" TagName="CalendarInputDatePicker"
    TagPrefix="uc3" %>
<%@ Register Src="~/Module/Inquiry/ForOP/DocumentDownloadList.ascx" TagName="DocumentDownloadList"
    TagPrefix="uc4" %>
<%@ Register Src="~/Module/EIVO/Business/MasterBusinessSelector.ascx" TagName="MasterBusinessSelector"
    TagPrefix="uc5" %>
<%@ Register Src="~/Module/Common/EnumSelector.ascx" TagName="EnumSelector" TagPrefix="uc6" %>
<%@ Register Src="~/Module/SAM/Business/GroupMemberSelector.ascx" TagName="GroupMemberSelector"
    TagPrefix="uc7" %>
<%@ Register assembly="Model" namespace="Model.DataEntity" tagprefix="cc1" %>
<%--<%@ Register Src="~/Module/Common/DownloadButton.ascx" TagName="DownloadButton" TagPrefix="uc8" %>--%>


<uc1:PageAction ID="actionItem" runat="server" ItemName="首頁 &gt; 發票下載補送" />
<uc2:FunctionTitleBar ID="titleBar" runat="server" ItemName="發票下載補送" />
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
                    發票類別
                </th>
                <td class="tdleft">
                    <uc6:EnumSelector ID="BusinessID" runat="server" TypeName="Model.Locale.Naming+InvoiceCenterBusinessQueryType, Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
                        SelectorIndication="全部" />
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
                    <asp:TextBox ID="txtInvoiceNO" runat="server"></asp:TextBox>~
                    <asp:TextBox ID="txtInvoiceNOEnd" runat="server"></asp:TextBox>
                </td>
            </tr>
           <%-- <tr>
                <th>
                    發票狀態
                </th>
                <td class="tdleft">
                    <uc6:EnumSelector ID="LevelID" runat="server" TypeName="Model.Locale.Naming+B2BInvoiceStepDefinition, Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
                        SelectorIndication="全部" />
                </td>
            </tr>--%>
            <tr>
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
            <tr>
                <th width="20%">
                    下載狀態
                </th>
                <td class="tdleft">
                    <asp:DropDownList ID="ddDownload" runat="server">
                        <asp:ListItem Value="">全部</asp:ListItem>
                        <asp:ListItem Value="1">已下載</asp:ListItem>
                        <asp:ListItem Value="0">未下載</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
     <%--       <tr>
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
            </tr>--%>
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
<uc4:DocumentDownloadList ID="itemList" runat="server" Visible="false" />
<table border="0" cellspacing="0" cellpadding="0" width="100%" runat="server" visible="false"
    enableviewstate="false" id="tblAction">
    <tbody>
        <tr>
            <td class="Bargain_btn">
                <asp:Button ID="DownloadButton" runat="server" Text="重送發票" 
              
                    OnClick="DownloadButton_Click" />
                <%--<uc8:DownloadButton ID="DownloadButton" runat="server" />--%>
            </td>
        </tr>
    </tbody>
</table>
<cc1:InvoiceDataSource ID="dsEntity" runat="server">
</cc1:InvoiceDataSource>


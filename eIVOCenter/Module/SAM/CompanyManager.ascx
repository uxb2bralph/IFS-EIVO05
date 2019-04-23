﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyManager.ascx.cs"
    Inherits="eIVOCenter.Module.SAM.CompanyManager" %>
<%@ Register Src="~/Module/UI/PageAction.ascx" TagName="PageAction" TagPrefix="uc1" %>
<%@ Register Src="~/Module/UI/FunctionTitleBar.ascx" TagName="FunctionTitleBar" TagPrefix="uc2" %>
<%@ Register Src="~/Module/Common/CalendarInputDatePicker.ascx" TagName="CalendarInputDatePicker"
    TagPrefix="uc3" %>
<%@ Register Src="~/Module/SAM/Business/EnterpriseGroupMemberList.ascx" TagName="EnterpriseGroupMemberList"
    TagPrefix="uc4" %>
<%@ Register src="~/Module/Common/PageAnchor.ascx" tagname="PageAnchor" tagprefix="uc5" %>
<%@ Register src="../Common/DataModelCache.ascx" tagname="DataModelCache" tagprefix="uc6" %>


<uc1:PageAction ID="actionItem" runat="server" ItemName="首頁 &gt; 企業資料維護" />
<uc2:FunctionTitleBar ID="titleBar" runat="server" ItemName="企業資料維護" />
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="top_table">
    <tr>
        <td>
            <asp:Button ID="btnAdd" runat="server" class="btn" Text="新增企業" OnClick="btnAdd_Click" />
        </td>
    </tr>
</table>
<div class="border_gray">
    <!--表格 開始-->
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
        <tr>
            <th colspan="2" class="Head_style_a">
                查詢條件
            </th>
        </tr>
        <tr>
            <th nowrap="nowrap" width="120">
                統編
            </th>
            <td class="tdleft">
                <asp:TextBox ID="ReceiptNo" class="textfield" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th nowrap="nowrap" width="120">
                企業名稱
            </th>
            <td class="tdleft">
                <asp:TextBox ID="CompanyName" class="textfield" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th nowrap="nowrap" width="120">
                企業狀態
            </th>
            <td class="tdleft">
                <asp:DropDownList ID="CompanyStatus" runat="server">
                    <asp:ListItem Value="">全部</asp:ListItem>
                    <asp:ListItem Value="1103">已啟用</asp:ListItem>
                    <asp:ListItem Value="1101">已停用</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
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
<uc4:EnterpriseGroupMemberList ID="itemList" runat="server" Visible="false" />
<uc5:PageAnchor ID="ToEdit" runat="server" TransferTo="~/SAM/EditCompany.aspx" />
<uc6:DataModelCache ID="modelItem" runat="server" KeyName="CompanyID" />


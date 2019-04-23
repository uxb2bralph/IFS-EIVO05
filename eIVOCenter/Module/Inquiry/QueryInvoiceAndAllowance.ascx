﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="queryInvoiceAndAllowance.ascx.cs"
    Inherits="eIVOCenter.Module.Inquiry.queryInvoiceAndAllowance" %>
<%@ Register Src="../Common/CalendarInputDatePicker.ascx" TagName="CalendarInputDatePicker"
    TagPrefix="uc1" %>
<%@ Register Src="../Common/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="../Common/PrintingButton2.ascx" TagName="PrintingButton2" TagPrefix="uc3" %>
<%@ Register Src="../UI/PageAction.ascx" TagName="PageAction" TagPrefix="uc5" %>
<%@ Register Src="../UI/FunctionTitleBar.ascx" TagName="FunctionTitleBar" TagPrefix="uc6" %>
<%@ Register Src="../UI/SellerSelector.ascx" TagName="SellerSelector" TagPrefix="uc7" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc5:PageAction ID="PageAction1" runat="server" ItemName="首頁 > 查詢發票/折讓" />
        <!--交易畫面標題-->
        <uc6:FunctionTitleBar ID="FunctionTitleBar1" runat="server" ItemName="查詢發票/折讓" />
        <div id="border_gray">
            <!--表格 開始-->
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
                <tr>
                    <th colspan="2" class="Head_style_a">
                        查詢條件
                    </th>
                </tr>
                <tr>
                    <th>
                        查詢項目
                    </th>
                    <td class="tdleft">
                        <asp:RadioButtonList ID="rdbSearchItem" RepeatColumns="5" RepeatDirection="Horizontal"
                            runat="server" RepeatLayout="Flow">
                            <asp:ListItem Value="1" Selected="True">電子發票</asp:ListItem>
                            <asp:ListItem Value="2">電子折讓單</asp:ListItem>
                            <asp:ListItem Value="3">作廢電子發票</asp:ListItem>
                            <asp:ListItem Value="4">作廢電子折讓單</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <th>
                        發票／折讓類別
                    </th>
                    <td class="tdleft">
                        <asp:DropDownList ID="ddlType" runat="server">
                            <asp:ListItem>全部</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trGroupMember" runat="server">
                    <th>
                        集團成員
                    </th>
                    <td class="tdleft">
                        <asp:DropDownList ID="ddlGroupMember" runat="server">
                            <asp:ListItem>全部</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th width="20%">
                        日期區間
                    </th>
                    <td class="tdleft">
                        自&nbsp;<uc1:CalendarInputDatePicker ID="DateFrom" runat="server" />
                        &nbsp;至&nbsp;<uc1:CalendarInputDatePicker ID="DateTo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        發票／折讓狀態
                    </th>
                    <td class="tdleft">
                        <asp:DropDownList ID="ddlStep" runat="server">
                            <asp:ListItem>全部</asp:ListItem>
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
                    <asp:Button ID="btnSearch" CssClass="btn" runat="server" Text="查詢" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <div id="divResult" visible="false" runat="server">
            <uc6:FunctionTitleBar ID="FunctionTitleBar2" runat="server" ItemName="查詢結果" />
            <!--表格 開始-->
            <div class="border_gray">
                <div runat="server" id="ResultTitle">
                    <asp:PlaceHolder ID="plResult" runat="server"></asp:PlaceHolder>
                </div>
                <!--表格 結束-->
            </div>
            <!--按鈕-->
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnSearch" />
    </Triggers>
</asp:UpdatePanel>

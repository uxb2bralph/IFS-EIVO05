<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InquireInvoiceAndAllowanceIntoPrint.ascx.cs"
    Inherits="eIVOGo.Module.Inquiry.InquireInvoiceAndAllowanceIntoPrint" %>
<%@ Register Src="../Common/CalendarInputDatePicker.ascx" TagName="CalendarInputDatePicker"
    TagPrefix="uc1" %>
<%@ Register Src="../Common/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="../Common/PrintingButton2.ascx" TagName="PrintingButton2" TagPrefix="uc3" %>
<%@ Register Src="../UI/PageAction.ascx" TagName="PageAction" TagPrefix="uc5" %>
<%@ Register Src="../UI/FunctionTitleBar.ascx" TagName="FunctionTitleBar" TagPrefix="uc6" %>
<%@ Register Src="../UI/SellerSelector.ascx" TagName="SellerSelector" TagPrefix="uc7" %>
<%@ Register src="../UI/PrintInvoiceSellerSelector.ascx" tagname="InvoiceSellerSelector" tagprefix="uc4" %>

<uc5:PageAction ID="PageAction1" runat="server" ItemName="首頁 > 發票列印" />
<!--交易畫面標題-->
<uc6:FunctionTitleBar ID="FunctionTitleBar1" runat="server" ItemName="發票列印" />
<div class="border_gray">
    <!--表格 開始-->
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
        <tr>
            <th colspan="2" class="Head_style_a">
                查詢條件
            </th>
        </tr>
        <tr>
            <th>
                發票類別
            </th>
            <td class="tdleft">
                <asp:RadioButtonList ID="rdbSearchItem" RepeatColumns="5" RepeatDirection="Horizontal"
                    runat="server" RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rdbSearchItem_SelectedIndexChanged">
                    <asp:ListItem Value="1" Selected="True">電子發票</asp:ListItem>
                    <asp:ListItem Value="2">電子折讓單</asp:ListItem>
                    <asp:ListItem Value="3">作廢電子發票</asp:ListItem>
                    <asp:ListItem Value="4">作廢電子折讓單</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr id="trIType" visible="true" runat="server">
            <th>
                查詢項目
            </th>
            <td class="tdleft">
                <asp:RadioButtonList ID="rbInvoiceType" RepeatDirection="Horizontal" runat="server"
                    RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rbInvoiceType_SelectedIndexChanged">
                    <asp:ListItem Value="1" Selected="True">B2B</asp:ListItem>
                    <asp:ListItem Value="2">B2C</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr id="trPType" visible="false" runat="server">
            <th>
                查詢類別
            </th>
            <td class="tdleft">
                <asp:RadioButtonList ID="rdbPriceType" RepeatColumns="5" RepeatDirection="Horizontal"
                    runat="server" RepeatLayout="Flow">
                    <asp:ListItem Value="1" Selected="True">全部</asp:ListItem>
                    <asp:ListItem Value="2">中獎</asp:ListItem>
                    <asp:ListItem Value="3">索取紙本發票</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <th>
                公司名稱
            </th>
            <td class="tdleft">
                <uc4:InvoiceSellerSelector ID="SellerID" runat="server" SelectorIndication="全部" />
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
                發票／折讓單號碼
            </th>
            <td class="tdleft">
                <asp:TextBox ID="invoiceNo" runat="server"></asp:TextBox>
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
        <div runat="server" id="ResultTitle" enableviewstate="false">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
                <tr>
                    <td class="Head_style_a">
                        若為當期發票或為折讓單，因尚未開獎或無法兌獎，故於「是否中獎」欄位呈現「N/A」
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:PlaceHolder ID="plResult" runat="server"></asp:PlaceHolder>
                <center>
                    <asp:Label ID="lblError" Visible="false" ForeColor="Red" Font-Size="Larger" runat="server"
                        Text="查無資料!!" EnableViewState="false"></asp:Label></center>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!--表格 結束-->
    </div>
    <!--按鈕-->
</div>

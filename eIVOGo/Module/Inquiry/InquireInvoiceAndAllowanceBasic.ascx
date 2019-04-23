<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InquireInvoiceAndAllowanceBasic.ascx.cs"
    Inherits="eIVOGo.Module.Inquiry.InquireInvoiceAndAllowanceBasic" %>
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
                            <asp:ListItem Value="5">中獎發票</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <%--<tr id="divReceiptNo" visible="false" runat="server">
                    <th>
                        統編
                    </th>
                    <td class="tdleft">
                        <uc7:SellerSelector ID="SellerID" runat="server" SelectAll="True" />
                    </td>
                </tr>--%>
                <%--<tr id="uxb2b" visible="false" runat="server">
                    <th>
                        查詢類別
                    </th>
                    <td class="tdleft">
                        <asp:RadioButton ID="rdbType1" Checked="true" GroupName="R2" Text="依會員" AutoPostBack="true"
                            runat="server" OnCheckedChanged="rdbType_CheckedChanged" />
                        &nbsp;
                        <asp:RadioButton ID="rdbType2" GroupName="R2" Text="依載具" AutoPostBack="true" runat="server"
                            OnCheckedChanged="rdbType_CheckedChanged" />
                        <asp:Label ID="lblDevice" Visible="false" runat="server" Text="依載具"></asp:Label>
                        <asp:DropDownList ID="ddlDevice" CssClass="textfield" Visible="false" runat="server"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlDevice_SelectedIndexChanged">
                            <asp:ListItem Value="0">-請選擇-</asp:ListItem>
                            <asp:ListItem Value="1">UXB2B條碼卡</asp:ListItem>
                            <asp:ListItem Value="2">悠遊卡</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>--%>
                <%-- <tr id="uxb2b1" visible="false" runat="server">
                    <th>
                        UXB2B條碼卡號
                    </th>
                    <td class="tdleft">
                        <asp:TextBox ID="txtUxb2bBarCode" CssClass="textfield" Width="100" runat="server"></asp:TextBox>
                        （共20碼）
                    </td>
                </tr>--%>
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
                <div runat="server" id="ResultTitle">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
                        <tr>
                            <td class="Head_style_a">
                                若為當期發票或為折讓單，因尚未開獎或無法兌獎，故於「是否中獎」欄位呈現「N/A」
                            </td>
                        </tr>
                        <tr>
                            <td class="Head_style_a">
                                若發票中獎，請至指定地點列印領獎憑證。
                            </td>
                        </tr>
                    </table>
                    <asp:PlaceHolder ID="plResult" runat="server"></asp:PlaceHolder>
                </div>
                <!--表格 結束-->
            </div>
            <!--按鈕-->
        </div>
    </ContentTemplate>
    <Triggers>
        <%--        <asp:AsyncPostBackTrigger ControlID="rdbType1" EventName="CheckedChanged" />
        <asp:AsyncPostBackTrigger ControlID="rdbType2" EventName="CheckedChanged" />
        <asp:AsyncPostBackTrigger ControlID="ddlDevice" EventName="SelectedIndexChanged" />
        --%>
        <asp:PostBackTrigger ControlID="btnSearch" />
    </Triggers>
</asp:UpdatePanel>

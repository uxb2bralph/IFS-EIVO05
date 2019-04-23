<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MaintainInvoiceList.ascx.cs"
    Inherits="eIVOGo.Module.Saler.MaintainInvoiceList" %>
<%@ Register Src="../UI/TwiceMonthlyPeriod.ascx" TagName="TwiceMonthlyPeriod" TagPrefix="uc5" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Data.Linq" %>
<%@ Import Namespace="Model.DataEntity" %>
<%@ Register Src="../Common/CalendarInput.ascx" TagName="CalendarInput" TagPrefix="uc1" %>
<%@ Register Src="../Common/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="../Common/PrintingButton2.ascx" TagName="PrintingButton2" TagPrefix="uc3" %>
<%@ Register Src="../UI/SellerSelector.ascx" TagName="SellerSelector" TagPrefix="uc4" %>
<style type="text/css">
    .SYN_ROW
    {
        background: silver;
    }
    .SYN_TXT
    {
        border-left: 1px solid;
        position: relative;
        left: 4.5em;
        background: white;
        font-family: monospace;
        margin-right: 4.5em;
    }
    .HTML_TXT
    {
        color: #000000;
    }
    .HTML_TAG
    {
        color: #0000ff;
    }
    .HTML_ELM
    {
        color: #800000;
    }
    .HTML_ATR
    {
        color: #ff0000;
    }
    .HTML_VAL
    {
        color: #0000ff;
    }
    .SYN_LNB
    {
        position: absolute;
        left: 0;
    }
    .SYN_LNN
    {
        padding: 0;
        color: black;
        border: 0;
        text-align: right;
        width: 3.5em;
        height: 1em;
        font-family: monospace;
        background: transparent;
        cursor: default;
        font-size: 100%;
    }
    .SYN_BCH
    {
        display: none;
    }
    .HTML_COM
    {
        color: #008000;
    }
    .HTML_CHA
    {
        color: #ff0000;
    }
</style>
<body>
    <!--路徑名稱-->
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="30">
                <img runat="server" enableviewstate="false" id="img6" src="~/images/path_left.gif"
                    alt="" width="30" height="29" />
            </td>
            <td bgcolor="#ecedd5">
                首頁 > 電子發票號碼維護
            </td>
        </tr>
    </table>
    <!--交易畫面標題-->
    <h1>
        <img runat="server" enableviewstate="false" id="img3" src="~/images/icon_search.gif"
            width="29" height="28" border="0" align="Left" />電子發票號碼維護清冊</h1>
    <!--按鈕-->
    <div id="DIV2" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="top_table">
            <tr>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="新增發票號碼" OnClick="btnAdd_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="DIV1" runat="server" class="border_gray">
        <!--表格 開始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
            <tr>
                <th colspan="2" class="Head_style_a">
                    查詢條件
                </th>
            </tr>
            <tr>
                <th width="150" nowrap="nowrap">
                    發票年度（民國年）
                </th>
                <td class="tdleft">
                    <asp:DropDownList ID="SelsectYear" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th width="150" nowrap="nowrap">
                    發票期別
                </th>
                <td class="tdleft">
                    <asp:DropDownList ID="SelsectPeriod" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <!--表格 結束-->
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td id="QTB" runat="server" class="Bargain_btn">
                <asp:Button ID="btnQuery" runat="server" CssClass="btn" OnClick="btnQuery_Click"
                    Text=" 查詢" />
            </td>
        </tr>
    </table>
    <h1 id="H1" runat="server">
        <img id="QImg1" runat="server" enableviewstate="false" src="~/images/icon_search.gif"
            width="29" height="28" border="0" align="left" />查詢結果</h1>
    <div id="DIV3" runat="server" class="border_gray">
        <div id="DIV3_1" runat="server">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table01">
                <tr>
                    <th nowrap="nowrap" class="table-row01">
                        發票年度
                    </th>
                    <th nowrap="nowrap" class="table-row01">
                        發票期別
                    </th>
                    <th nowrap="nowrap" class="table-row01">
                        字軌
                    </th>
                    <th nowrap="nowrap" class="table-row01">
                        發票號碼起
                    </th>
                    <th nowrap="nowrap" class="table-row01">
                        發票號碼迄
                    </th>
                    <th nowrap="nowrap" class="table-row01">
                    </th>
                </tr>
                <asp:Repeater ID="rpList" runat="server" EnableViewState="true" OnItemCommand="rpList_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%# ((ToDataInquiry)Container.DataItem).qYear - 1911%>
                            </td>
                            <td align="center">
                                <%# PeriodNo2String(((ToDataInquiry)Container.DataItem).qPeriodNo)%>
                            </td>
                            <td align="center">
                                <%# ((ToDataInquiry)Container.DataItem).qTrackCode%>
                            </td>
                            <td align="center">
                                <%# ((ToDataInquiry)Container.DataItem).qStartNo.ToString("00000000")%>
                            </td>
                            <td align="center">
                                <%# ((ToDataInquiry)Container.DataItem).qEndNo.ToString("00000000")%>
                            </td>
                            <td align="center">
                                <asp:Button ID="btnUpdate" runat="server" Text="修改" class="btn" CommandName="Update"
                                    Enabled='<%#((ToDataInquiry)Container.DataItem).NumberUsedCount>0?false:true %>'
                                    CommandArgument='<%#((ToDataInquiry)Container.DataItem).qIntervalID %>' />
                                <asp:Button ID="btnDel" runat="server" Text="刪除" OnClientClick='return confirm("確定要刪除該字軌資料?");'
                                    class="btn" CommandName="Delete" Enabled='<%#((ToDataInquiry)Container.DataItem).NumberUsedCount>0?false:true %>'
                                    CommandArgument='<%#((ToDataInquiry)Container.DataItem).qIntervalID %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <table id="TB2" runat="server" width="100%" border="0" align="center" cellpadding="0"
            cellspacing="0" class="table-count">
            <tr>
                <td align="center">
                    <span id="NoData" runat="server" style="color: Red; font-size: Larger;">查無資料!!</span>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <uc2:PagingControl ID="PagingControl1" runat="server" width="100%" />
                </td>
            </tr>
        </table>
    </div>
    <%--<table id="TB2_1" runat="server" width="100%" border="0" align="center" cellpadding="0"
        cellspacing="0" class="table-count">
        <tr>
            <td align="center">
                <asp:Button ID="btnAdd" runat="server" Text="新增發票號碼" OnClick="btnAdd_Click" />
            </td>
        </tr>
    </table>--%>
    <!--表格 結束-->
    </div>
    <!--按鈕-->
    <div id="DIV4" runat="server" class="border_gray">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
            <tr>
                <th id="THTitle" runat="server" colspan="2" class="Head_style_a">
                    修改發票號碼
                </th>
            </tr>
            <tr>
                <th nowrap="nowrap">
                    發票年度（民國年）
                </th>
                <td class="tdleft">
                    &nbsp;<asp:DropDownList ID="SelsectCodeYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelsectCodeYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    發票期別
                </th>
                <td class="tdleft">
                    &nbsp;<asp:DropDownList ID="SelsectCodePeriod" runat="server" OnSelectedIndexChanged="SelsectCodePeriod_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    發票字軌
                </th>
                <td class="tdleft">
                    &nbsp;<asp:DropDownList ID="SelsectCodeTrack" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="lblExCode" runat="server" Text="無字軌資料!" Style="color: Red; font-size: Medium;"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="20%">
                    <span class="red">*</span> 發票號碼起
                </th>
                <td class="tdleft">
                    &nbsp;<asp:TextBox ID="txtInvNoStar" runat="server" Width="63px" MaxLength="8" ValidationGroup="REVSGroup"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="REVStart" runat="server" ControlToValidate="txtInvNoStar"
                        ErrorMessage="失敗!" ForeColor="#FF3300" ValidationExpression="[0-9]{8}" ViewStateMode="Enabled"
                        Display="Dynamic" Font-Size="Medium" ValidationGroup="REVSGroup"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RFVStart" runat="server" ControlToValidate="txtInvNoStar"
                        Display="Dynamic" ErrorMessage="RequiredFieldValidator" Font-Size="Medium" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    <span class="red">*</span> 發票號碼迄
                </th>
                <td class="tdleft">
                    &nbsp;<asp:TextBox ID="txtInvNoEnd" runat="server" Width="63px" MaxLength="8" ValidationGroup="REVSGroup"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="REVEnd" runat="server" ControlToValidate="txtInvNoEnd"
                        ErrorMessage="失敗!" ForeColor="#FF3300" ValidationExpression="[0-9]{8}" ViewStateMode="Enabled"
                        Display="Dynamic" Font-Size="Medium" ValidationGroup="REVSGroup"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RFVEnd" runat="server" ControlToValidate="txtInvNoEnd"
                        Display="Dynamic" ErrorMessage="RequiredFieldValidator" Font-Size="Medium" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td id="TH2" colspan="2" class="tdleft">
                    （發票號碼起、迄均為8位數，迄號必需大於發票起號；且發票號碼起、迄號之差，須為50的倍數）
                </td>
            </tr>
        </table>
    </div>
    <!--表格 結束-->
    <!--按鈕-->
    <table id="TB3" runat="server" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td align="center">
                <asp:Button ID="btnAddCode" runat="server" Text="確定" OnClick="btnAddCode_Click" />
                <asp:Button ID="btnReset" runat="server" Text="重填" OnClick="btnReset_Click" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblEx" runat="server" Text="新增號碼失敗!" Style="color: Red; font-size: Medium;"></asp:Label>
                <asp:Label ID="lblIntervalID" runat="server" Text="lblIntervalID" Style="color: Red;
                    font-size: Medium;" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <cc1:InvoiceDataSource ID="dsInv" runat="server">
    </cc1:InvoiceDataSource>
</body>

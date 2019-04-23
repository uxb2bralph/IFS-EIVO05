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
    <!--���|�W��-->
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="30">
                <img runat="server" enableviewstate="false" id="img6" src="~/images/path_left.gif"
                    alt="" width="30" height="29" />
            </td>
            <td bgcolor="#ecedd5">
                ���� > �q�l�o�����X���@
            </td>
        </tr>
    </table>
    <!--����e�����D-->
    <h1>
        <img runat="server" enableviewstate="false" id="img3" src="~/images/icon_search.gif"
            width="29" height="28" border="0" align="Left" />�q�l�o�����X���@�M�U</h1>
    <!--���s-->
    <div id="DIV2" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="top_table">
            <tr>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="�s�W�o�����X" OnClick="btnAdd_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="DIV1" runat="server" class="border_gray">
        <!--��� �}�l-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
            <tr>
                <th colspan="2" class="Head_style_a">
                    �d�߱���
                </th>
            </tr>
            <tr>
                <th width="150" nowrap="nowrap">
                    �o���~�ס]����~�^
                </th>
                <td class="tdleft">
                    <asp:DropDownList ID="SelsectYear" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th width="150" nowrap="nowrap">
                    �o�����O
                </th>
                <td class="tdleft">
                    <asp:DropDownList ID="SelsectPeriod" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <!--��� ����-->
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td id="QTB" runat="server" class="Bargain_btn">
                <asp:Button ID="btnQuery" runat="server" CssClass="btn" OnClick="btnQuery_Click"
                    Text=" �d��" />
            </td>
        </tr>
    </table>
    <h1 id="H1" runat="server">
        <img id="QImg1" runat="server" enableviewstate="false" src="~/images/icon_search.gif"
            width="29" height="28" border="0" align="left" />�d�ߵ��G</h1>
    <div id="DIV3" runat="server" class="border_gray">
        <div id="DIV3_1" runat="server">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table01">
                <tr>
                    <th nowrap="nowrap" class="table-row01">
                        �o���~��
                    </th>
                    <th nowrap="nowrap" class="table-row01">
                        �o�����O
                    </th>
                    <th nowrap="nowrap" class="table-row01">
                        �r�y
                    </th>
                    <th nowrap="nowrap" class="table-row01">
                        �o�����X�_
                    </th>
                    <th nowrap="nowrap" class="table-row01">
                        �o�����X��
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
                                <asp:Button ID="btnUpdate" runat="server" Text="�ק�" class="btn" CommandName="Update"
                                    Enabled='<%#((ToDataInquiry)Container.DataItem).NumberUsedCount>0?false:true %>'
                                    CommandArgument='<%#((ToDataInquiry)Container.DataItem).qIntervalID %>' />
                                <asp:Button ID="btnDel" runat="server" Text="�R��" OnClientClick='return confirm("�T�w�n�R���Ӧr�y���?");'
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
                    <span id="NoData" runat="server" style="color: Red; font-size: Larger;">�d�L���!!</span>
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
                <asp:Button ID="btnAdd" runat="server" Text="�s�W�o�����X" OnClick="btnAdd_Click" />
            </td>
        </tr>
    </table>--%>
    <!--��� ����-->
    </div>
    <!--���s-->
    <div id="DIV4" runat="server" class="border_gray">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
            <tr>
                <th id="THTitle" runat="server" colspan="2" class="Head_style_a">
                    �ק�o�����X
                </th>
            </tr>
            <tr>
                <th nowrap="nowrap">
                    �o���~�ס]����~�^
                </th>
                <td class="tdleft">
                    &nbsp;<asp:DropDownList ID="SelsectCodeYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelsectCodeYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    �o�����O
                </th>
                <td class="tdleft">
                    &nbsp;<asp:DropDownList ID="SelsectCodePeriod" runat="server" OnSelectedIndexChanged="SelsectCodePeriod_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    �o���r�y
                </th>
                <td class="tdleft">
                    &nbsp;<asp:DropDownList ID="SelsectCodeTrack" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="lblExCode" runat="server" Text="�L�r�y���!" Style="color: Red; font-size: Medium;"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="20%">
                    <span class="red">*</span> �o�����X�_
                </th>
                <td class="tdleft">
                    &nbsp;<asp:TextBox ID="txtInvNoStar" runat="server" Width="63px" MaxLength="8" ValidationGroup="REVSGroup"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="REVStart" runat="server" ControlToValidate="txtInvNoStar"
                        ErrorMessage="����!" ForeColor="#FF3300" ValidationExpression="[0-9]{8}" ViewStateMode="Enabled"
                        Display="Dynamic" Font-Size="Medium" ValidationGroup="REVSGroup"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RFVStart" runat="server" ControlToValidate="txtInvNoStar"
                        Display="Dynamic" ErrorMessage="RequiredFieldValidator" Font-Size="Medium" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    <span class="red">*</span> �o�����X��
                </th>
                <td class="tdleft">
                    &nbsp;<asp:TextBox ID="txtInvNoEnd" runat="server" Width="63px" MaxLength="8" ValidationGroup="REVSGroup"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="REVEnd" runat="server" ControlToValidate="txtInvNoEnd"
                        ErrorMessage="����!" ForeColor="#FF3300" ValidationExpression="[0-9]{8}" ViewStateMode="Enabled"
                        Display="Dynamic" Font-Size="Medium" ValidationGroup="REVSGroup"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RFVEnd" runat="server" ControlToValidate="txtInvNoEnd"
                        Display="Dynamic" ErrorMessage="RequiredFieldValidator" Font-Size="Medium" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td id="TH2" colspan="2" class="tdleft">
                    �]�o�����X�_�B������8��ơA�������ݤj��o���_���F�B�o�����X�_�B�������t�A����50�����ơ^
                </td>
            </tr>
        </table>
    </div>
    <!--��� ����-->
    <!--���s-->
    <table id="TB3" runat="server" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td align="center">
                <asp:Button ID="btnAddCode" runat="server" Text="�T�w" OnClick="btnAddCode_Click" />
                <asp:Button ID="btnReset" runat="server" Text="����" OnClick="btnReset_Click" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblEx" runat="server" Text="�s�W���X����!" Style="color: Red; font-size: Medium;"></asp:Label>
                <asp:Label ID="lblIntervalID" runat="server" Text="lblIntervalID" Style="color: Red;
                    font-size: Medium;" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <cc1:InvoiceDataSource ID="dsInv" runat="server">
    </cc1:InvoiceDataSource>
</body>

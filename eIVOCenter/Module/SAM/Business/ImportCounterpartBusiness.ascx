<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImportCounterpartBusiness.ascx.cs"
    Inherits="eIVOCenter.Module.SAM.Business.ImportCounterpartBusiness" %>
<%@ Register Src="~/Module/UI/PageAction.ascx" TagName="PageAction" TagPrefix="uc1" %>
<%@ Register Src="~/Module/UI/FunctionTitleBar.ascx" TagName="FunctionTitleBar" TagPrefix="uc2" %>
<%@ Register Src="~/Module/Common/EnumSelector.ascx" TagName="EnumSelector" TagPrefix="uc6" %>
<%@ Register Src="ImportCounterpartBusinessList.ascx" TagName="ImportCounterpartBusinessList"
    TagPrefix="uc3" %>
<%@ Register Src="../../UI/EnterpriseGroupMemberSelector.ascx" TagName="EnterpriseGroupMemberSelector"
    TagPrefix="uc4" %>


<uc1:PageAction ID="actionItem" runat="server" ItemName="首頁 &gt; 相對營業人管理-新增或編輯相對營業人資料" />
<uc2:FunctionTitleBar ID="titleBar" runat="server" ItemName="相對營業人管理-新增或編輯相對營業人資料" />
<div class="border_gray">
    <!--表格 開始-->
    <table id="left_title" border="0" cellspacing="0" cellpadding="0" width="100%">
        <tbody>
            <tr class="other">
                <th width="20%" nowrap>
                    匯入格式
                </th>
                <td class="tdleft">
                    <asp:RadioButtonList ID="rbChange" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                        AutoPostBack="True" OnSelectedIndexChanged="rbChange_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="~/SAM/ImportCounterpartBusiness.aspx">CSV&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="~/SAM/ImportCounterpartBusinessXml.aspx">XML&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="other">
                <th width="20%" nowrap>
                    匯入檔案範本
                </th>
                <td class="tdleft">
                    <a id="sample" runat="server" enableviewstate="false" href="~/Published/ImportCompany.csv">
                        <img enableviewstate="false" runat="server" id="img1" border="0" alt="" align="absMiddle"
                            src="~/images/icon_ca.gif" width="27" height="28" /></a> <font color="blue">請依據檔案中各欄位名稱填入相對應內容，每一列代表唯一家相對營業人資料，若匯入資料已存在系統，系統會以編輯方式修改原存在資料</font>
                </td>
            </tr>
            <tr class="other">
                <th width="20%" nowrap>
                    所屬集團成員
                </th>
                <td class="tdleft">
                    <uc4:EnterpriseGroupMemberSelector ID="MasterID" runat="server" />
                </td>
            </tr>
            <tr class="other">
                <th width="20%" nowrap>
                    相對營業人類別
                </th>
                <td class="tdleft">
                    <uc6:EnumSelector ID="BusinessType" runat="server" SelectorIndication="請選擇" TypeName="Model.Locale.Naming+InvoiceCenterBusinessQueryType, Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" />
                </td>
            </tr>
            <tr class="other">
                <th width="20%" nowrap>
                    相對營業人資料匯入
                </th>
                <td class="tdleft">
                    <asp:FileUpload ID="csvFile" runat="server" />
                    &nbsp;<asp:Button ID="btnConfirm" runat="server" Text="確認" OnClick="btnConfirm_Click" />
                </td>
            </tr>
        </tbody>
    </table>
    <!--表格 結束-->
    <uc3:ImportCounterpartBusinessList ID="itemList" runat="server" />
</div>
<table border="0" cellspacing="0" cellpadding="0" width="100%" runat="server" visible="false"
    enableviewstate="false" id="tblAction">
    <tbody>
        <tr>
            <td class="Bargain_btn">
                <asp:Button ID="btnSave" runat="server" Text="確定" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </tbody>
</table>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditOrganization.ascx.cs"
    Inherits="eIVOGo.Module.SAM.EditOrganization" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<%@ Register Src="../Common/DataModelCache.ascx" TagName="DataModelCache" TagPrefix="uc1" %>
<%@ Register Src="../Common/EnumSelector.ascx" TagName="EnumSelector" TagPrefix="uc2" %>
<%@ Register Src="../Common/ActionHandler.ascx" TagName="ActionHandler" TagPrefix="uc3" %>
<div id="border_gray">
    <!--表格 開始-->
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
        <tr>
            <td colspan="4" class="Head_style_a">
                商家基本資料
            </td>
        </tr>
        <tr>
            <th nowrap="nowrap" width="150">
                <span style="color: red">*</span>公司統一編號
            </th>
            <td class="tdleft">
                <asp:TextBox ID="ReceiptNo" runat="server" Text='<%# _entity.ReceiptNo %>' />
            </td>
            <th width="150">
                <span style="color: red">*</span>名稱
            </th>
            <td class="tdleft">
                <asp:TextBox ID="CompanyName" runat="server" Text='<%# _entity.CompanyName %>' />
            </td>
        </tr>
        <tr>
            <th width="150">
                <span style="color: red">*</span>地址
            </th>
            <td colspan="3" class="tdleft">
                <asp:TextBox ID="Addr" Columns="68" runat="server" Text='<%# _entity.Addr %>' />
            </td>
        </tr>
        <tr>
            <th width="150">
                <span style="color: red">*</span>電話
            </th>
            <td class="tdleft">
                <asp:TextBox ID="Phone" runat="server" Text='<%# _entity.Phone %>' />
            </td>
            <th width="150">
                傳真
            </th>
            <td class="tdleft">
                <asp:TextBox ID="Fax" runat="server" Text='<%# _entity.Fax %>' />
            </td>
        </tr>
        <tr>
            <th width="150">
                公司負責人
            </th>
            <td class="tdleft">
                <asp:TextBox ID="UndertakerName" runat="server" Text='<%# _entity.UndertakerName %>' />
            </td>
            <th width="150">
                <span style="color: red">*</span>類別
            </th>
            <td class="tdleft">
                <uc2:EnumSelector ID="CategoryID" runat="server" TypeName="Model.Locale.Naming+B2CCategoryID, Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
                    SelectorIndication="請選擇" SelectedValue="<%# _entity.OrganizationCategory[0].CategoryID %>" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="Head_style_a">
                聯絡方式
            </td>
        </tr>
        <tr>
            <th width="150">
                聯絡人姓名
            </th>
            <td class="tdleft">
                <asp:TextBox ID="ContactName" runat="server" Text='<%# _entity.ContactName %>' />
            </td>
            <th width="150">
                聯絡人職稱
            </th>
            <td class="tdleft">
                <asp:TextBox ID="ContactTitle" runat="server" Text='<%# _entity.ContactTitle %>' />
            </td>
        </tr>
        <tr>
            <th width="150">
                聯絡人電話
            </th>
            <td class="tdleft">
                <asp:TextBox ID="ContactPhone" runat="server" Text='<%# _entity.ContactPhone %>' />
            </td>
            <th width="150">
                聯絡人行動電話
            </th>
            <td class="tdleft">
                <asp:TextBox ID="ContactMobilePhone" runat="server" Text='<%# _entity.ContactMobilePhone %>' />
                &nbsp;
            </td>
        </tr>
        <tr>
            <th width="150">
                <span style="color: red">*</span>聯絡人電子郵件
            </th>
            <td class="tdleft" colspan="3">
                <asp:TextBox ID="ContactEmail" runat="server" Columns="68" Text='<%# _entity.ContactEmail %>' />
            </td>
        </tr>
        <tr>
            <th width="150">
                設定使用發票列印
            </th>
            <td class="tdleft">
                <asp:CheckBox ID="SetToPrintInvoice" runat="server" Checked='<%# _entity.OrganizationStatus.SetToPrintInvoice == true  %>'
                    Text="設定使用發票列印" />
            </td>
            <th width="150">
                發票列印樣式<br />
                折讓單列印樣式
            </th>
            <td class="tdleft">
                <asp:TextBox ID="InvoicePrintView" runat="server" Text='<%# _entity.OrganizationStatus.InvoicePrintView %>'
                    Columns="40" /><br />
                <asp:TextBox ID="AllowancePrintView" runat="server" Text='<%# _entity.OrganizationStatus.AllowancePrintView %>'
                    Columns="40" />
            </td>
        </tr>
        <tr>
            <th width="150">
                <span style="color: red">*</span>電子發票核准函號
            </th>
            <td class="tdleft" colspan="3">
                <asp:TextBox ID="AuthorizationNo" runat="server" Columns="68" Text='<%# _entity.OrganizationStatus.AuthorizationNo %>' />
            </td>
        </tr>
        <tr>
            <th width="150">
                設定使用委外客服
            </th>
            <td class="tdleft" colspan="3">
                <asp:CheckBox ID="SetToOutsourcingCS" runat="server" Checked='<%# _entity.OrganizationStatus.SetToOutsourcingCS == true  %>'
                    Text="使用委外客服" />
            </td>
        </tr>
    </table>
</div>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td class="Bargain_btn" align="center">
            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CssClass="btn"
                CommandName="Update" Text="確定" />
            &nbsp;
            <input name="Reset" type="reset" class="btn" value="重填" />
        </td>
    </tr>
</table>
<cc1:OrganizationDataSource ID="dsEntity" runat="server">
</cc1:OrganizationDataSource>
<uc1:DataModelCache ID="modelItem" runat="server" KeyName="CompanyID" />
<uc3:ActionHandler ID="doConfirm" runat="server" />
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        UpdateButton.OnClientClick = doConfirm.GetPostBackEventReference(null);
    }
</script>

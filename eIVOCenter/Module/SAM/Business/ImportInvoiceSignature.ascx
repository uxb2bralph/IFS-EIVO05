﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImportInvoiceSignature.ascx.cs"
    Inherits="eIVOCenter.Module.SAM.Business.ImportInvoiceSignature" %>
<%@ Register Src="~/Module/UI/PageAction.ascx" TagName="PageAction" TagPrefix="uc1" %>
<%@ Register Src="~/Module/UI/FunctionTitleBar.ascx" TagName="FunctionTitleBar" TagPrefix="uc2" %>
<%@ Register Src="~/Module/Common/EnumSelector.ascx" TagName="EnumSelector" TagPrefix="uc6" %>
<%@ Register Src="~/Module/Common/DataModelCache.ascx" TagName="DataModelCache" TagPrefix="uc4" %>
<%@ Register Src="ImportCounterpartBusinessList.ascx" TagName="ImportCounterpartBusinessList"
    TagPrefix="uc3" %>
<%@ Register Src="~/Module/Common/ActionHandler.ascx" TagName="ActionHandler" TagPrefix="uc5" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<uc1:PageAction ID="actionItem" runat="server" ItemName="首頁 &gt; 統一發票專用章管理" />
<uc2:FunctionTitleBar ID="titleBar" runat="server" ItemName="統一發票專用章管理" />
<div class="border_gray">
    <!--表格 開始-->
    <table class="left_title" border="0" cellspacing="0" cellpadding="0" width="100%">
        <tbody>
            <tr>
                <th width="150">
                    <span style="color: red">*</span>名稱
                </th>
                <td class="tdleft">
                    <%# _entity.CompanyName %>
                </td>
            </tr>
            <tr>
                <th nowrap="nowrap" width="150">
                    <span style="color: red">*</span>公司統一編號
                </th>
                <td class="tdleft">
                    <%# _entity.ReceiptNo %>
                </td>
            </tr>
            <tr>
                <th width="20%" nowrap>
                    目前的印鑑圖檔
                </th>
                <td class="tdleft">
                    <asp:Image ID="imgSignature" runat="server" EnableViewState="false" ImageUrl='<%# VirtualPathUtility.ToAbsolute(String.Format("~/Seal/{0}",_entity.InvoiceSignature)) %>'
                        Visible="<%# !String.IsNullOrEmpty(_entity.InvoiceSignature) %>" />
                </td>
            </tr>
            <tr>
                <th width="20%" nowrap>
                    新的印鑑圖檔(輸入路徑)
                </th>
                <td class="tdleft">
                    <asp:FileUpload ID="imgFile" runat="server" />
                    &nbsp;<asp:Button ID="btnConfirm" runat="server" Text="確認" />
                </td>
            </tr>
        </tbody>
    </table>
</div>
<cc1:OrganizationDataSource ID="dsEntity" runat="server">
</cc1:OrganizationDataSource>
<uc5:ActionHandler ID="doConfirm" runat="server" />
<uc4:DataModelCache ID="modelItem" runat="server" KeyName="CompanyID" />
<script runat="server">

    protected Model.Security.MembershipManagement.UserProfileMember _userProfile;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnConfirm.OnClientClick = doConfirm.GetPostBackEventReference(null);
        this.PreRender += new EventHandler(module_sam_business_importinvoicesignature_ascx_PreRender);

        base.OnInit(e);
        _userProfile = Business.Helper.WebPageUtility.UserProfile;

        if (modelItem.DataItem == null)
        {
            this.QueryExpr = o => o.CompanyID == _userProfile.CurrentUserRole.OrganizationCategory.CompanyID;
        }
        else
        {
            this.QueryExpr = o => o.CompanyID == (int)modelItem.DataItem;
        }

    }

    void module_sam_business_importinvoicesignature_ascx_PreRender(object sender, EventArgs e)
    {
        this.BindData();
    }
</script>

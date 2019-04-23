﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CALogItem.ascx.cs"
    Inherits="eIVOCenter.Module.Entity.CALogItem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Module/UI/FunctionTitleBar.ascx" TagName="FunctionTitleBar" TagPrefix="uc1" %>
<%@ Register Src="~/Module/Common/EnumSelector.ascx" TagName="EnumSelector" TagPrefix="uc4" %>
<%@ Register Src="~/Module/Common/DataModelCache.ascx" TagName="DataModelCache" TagPrefix="uc2" %>
<%@ Register Src="~/Module/Common/ActionHandler.ascx" TagName="ActionHandler" TagPrefix="uc5" %>
<%@ Register assembly="Model" namespace="Model.DataEntity" tagprefix="cc1" %>
<%@ Register src="~/Module/SAM/CAContentDetail.ascx" tagname="CAContentDetail" tagprefix="uc3" %>
<%@ Register src="../SAM/CAContentDetailPKCS7.ascx" tagname="CAContentDetailPKCS7" tagprefix="uc6" %>
<%@ Register src="../SAM/CAContentDetailXmlSig.ascx" tagname="CAContentDetailXmlSig" tagprefix="uc7" %>
<%@ Register src="../SAM/CAContentDetailCA.ascx" tagname="CAContentDetailCA" tagprefix="uc8" %>
<%@ Register src="../SAM/CAContentDetailCommissionedToIssue.ascx" tagname="CAContentDetailCommissionedToIssue" tagprefix="uc9" %>
<%@ Register src="../SAM/CAContentDetailUserAction.ascx" tagname="CAContentDetailUserAction" tagprefix="uc10" %>
<%@ Register src="../SAM/CAContentDetailCommissionedToReceive.ascx" tagname="CAContentDetailCommissionedToReceive" tagprefix="uc11" %>
<%@ Import Namespace="Utility" %>
<%@ Import Namespace="Model.Locale" %>
<%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
<!--路徑名稱-->
<!--交易畫面標題-->
<!--按鈕-->
<asp:Button ID="btnPopup" runat="server" Text="Button" Style="display: none" />
<asp:Panel ID="Panel1" runat="server" Style="display: none; width: 650px; background-color: #ffffdd;
    border-width: 3px; border-style: solid; border-color: Gray; padding: 3px;">
    <asp:Panel ID="Panel3" runat="server" Style="cursor: move; background-color: #DDDDDD;
        border: solid 1px Gray; color: Black">
        <%--        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        --%>
        <!--路徑名稱-->
        <!--交易畫面標題-->
        <!--按鈕-->
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td class="Bargain_btn" align="center">
                    <asp:Button ID="btnCancel" runat="server" Text="關閉" />
                </td>
            </tr>
        </table>
        <%--            </ContentTemplate>
        </asp:UpdatePanel>
        --%>
    </asp:Panel>
</asp:Panel>
<asp:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="btnPopup"
    CancelControlID="btnCancel" PopupControlID="Panel1" BackgroundCssClass="modalBackground"
    DropShadow="true" PopupDragHandleControlID="Panel3" />
<uc5:ActionHandler ID="doCancel" runat="server" />
<uc5:ActionHandler ID="doConfirm" runat="server" />
<cc1:CALogDataSource ID="dsEntity" runat="server">
</cc1:CALogDataSource>
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnCancel.OnClientClick = doCancel.GetPostBackEventReference(null);
    }

    public override void BindData()
    {
        loadEntity();

        UserControl uc;
        switch ((Naming.CACatalogDefinition)_entity.Catalog.Value)
        { 
            case Naming.CACatalogDefinition.簽章測試:
            case Naming.CACatalogDefinition.設定簽章憑證:
            case Naming.CACatalogDefinition.UXGW上傳附件檔:
                uc = (UserControl)this.LoadControl("~/Module/SAM/CAContentDetailCA.ascx");
                ((ASP.module_sam_cacontentdetailca_ascx)uc).DataItem = _entity;
                break;
            case Naming.CACatalogDefinition.開立作廢折讓單:
            case Naming.CACatalogDefinition.開立作廢發票:
            case Naming.CACatalogDefinition.開立折讓單:
            case Naming.CACatalogDefinition.開立發票:
            case Naming.CACatalogDefinition.開立收據:
            case Naming.CACatalogDefinition.開立作廢收據:
            case Naming.CACatalogDefinition.接收作廢折讓單:
            case Naming.CACatalogDefinition.接收作廢發票:
            case Naming.CACatalogDefinition.接收折讓單:
            case Naming.CACatalogDefinition.接收發票:
            case Naming.CACatalogDefinition.接收收據:
            case Naming.CACatalogDefinition.接收作廢收據:
            case Naming.CACatalogDefinition.列印作廢折讓單:
            case Naming.CACatalogDefinition.列印作廢發票:
            case Naming.CACatalogDefinition.列印折讓單:
            case Naming.CACatalogDefinition.列印發票:
                uc = (UserControl)this.LoadControl("~/Module/SAM/CAContentDetailPKCS7.ascx");
                ((ASP.module_sam_cacontentdetailpkcs7_ascx)uc).DataItem = _entity;
                break;
            case Naming.CACatalogDefinition.UXGW上傳資料:
            case Naming.CACatalogDefinition.UXGW自動接收:
                uc = (UserControl)this.LoadControl("~/Module/SAM/CAContentDetail.ascx");
                ((ASP.module_sam_cacontentdetail_ascx)uc).DataItem = _entity;
                break;
            case Naming.CACatalogDefinition.平台自動接收:
                uc = (UserControl)this.LoadControl("~/Module/SAM/CAContentDetailCommissionedToReceive.ascx");
                ((ASP.module_sam_cacontentdetailcommissionedtoreceive_ascx)uc).DataItem = _entity;
                break;
            case Naming.CACatalogDefinition.平台自動開立:
                uc = (UserControl)this.LoadControl("~/Module/SAM/CAContentDetailCommissionedToIssue.ascx");
                ((ASP.module_sam_cacontentdetailcommissionedtoissue_ascx)uc).DataItem = _entity;
                break;
            default:
                uc = (UserControl)this.LoadControl("~/Module/SAM/CAContentDetailPKCS7.ascx");
                ((ASP.module_sam_cacontentdetailpkcs7_ascx)uc).DataItem = _entity;
                break;
        }
        
        uc.InitializeAsUserControl(this.Page);
        Panel3.Controls.AddAt(0, uc);

        this.DataBind();
        this.Visible = true;
        this.ModalPopupExtender.Show();
    }
</script>

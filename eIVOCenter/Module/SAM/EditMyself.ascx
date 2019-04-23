﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditMyself.ascx.cs"
    Inherits="eIVOGo.Module.SAM.EditMyself" %>
<%@ Register Src="../UI/RegisterMessage.ascx" TagName="RegisterMessage" TagPrefix="uc2" %>
<%@ Register Src="../UI/PageAction.ascx" TagName="PageAction" TagPrefix="uc3" %>
<%@ Register Src="EditUserProfile.ascx" TagName="EditUserProfile" TagPrefix="uc4" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<%@ Register src="~/Module/Common/DataModelCache.ascx" tagname="DataModelCache" tagprefix="uc1" %>
<%@ Import Namespace="Uxnet.Web.WebUI" %>
<!--路徑名稱-->


<%--<asp:UpdatePanel ID="Updatepanel1" runat="server">
    <ContentTemplate>--%>
        <div id="mainpage" runat="server">
            <uc3:PageAction ID="actionItem" runat="server" ItemName="會員管理-修改帳號" />
            <!--交易畫面標題-->
            <h1>
                <img id="img4" runat="server" enableviewstate="false" src="~/images/icon_search.gif"
                    width="29" height="28" border="0" align="absmiddle" />會員管理-修改帳號</h1>
            <uc4:EditUserProfile ID="EditItem" runat="server" />
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="Bargain_btn" align="center">
                        <asp:Button ID="btnOK" runat="server" class="btn" Text="確定" OnClick="btnOK_Click" />
                        &nbsp;
                        <input name="Reset" type="reset" class="btn" value="重填" />&nbsp;
                        <asp:Button ID="btnCreateToken" runat="server" Text="設定會員電子簽章數位憑證" 
                            onclick="btnCreateToken_Click" Visible="false" />
                        <asp:Button ID="btnEntrusting" runat="server" Text="設定開立＼接收電子簽章數位憑證" onclick="btnEntrusting_Click" Visible="false" />
                    </td>
                </tr>
            </table>
        </div>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
<cc1:UserProfileDataSource ID="dsUserProfile" runat="server">
</cc1:UserProfileDataSource>
<uc1:DataModelCache ID="modelItem" runat="server" KeyName="CompanyID" />
<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Model.Security.MembershipManagement.UserProfileMember userProfile = Business.Helper.WebPageUtility.UserProfile;
        var orgItem = dsUserProfile.CreateDataManager().GetTable<Organization>().Where(o => o.CompanyID == userProfile.CurrentUserRole.OrganizationCategory.CompanyID).First();
        if (userProfile.CurrentUserRole.RoleID == (int)Model.Locale.Naming.RoleID.集團成員 || userProfile.CurrentUserRole.RoleID == (int)Model.Locale.Naming.RoleID.集團成員_自動開立接收)
        {
            btnEntrusting.Visible = true;
            if (orgItem.OrganizationStatus.Entrusting==true && !orgItem.OrganizationStatus.TokenID.HasValue)
            {
                this.AjaxAlertAndRedirect("您尚未建立自動開立＼接收電子簽章憑證!!請先建立憑證資訊!!", VirtualPathUtility.ToAbsolute("~/SAM/CreateOrganizationEntrustingCertificate.aspx"));
            }            
        }
        else if (orgItem.OrganizationStatus.Entrusting == false&&(orgItem.OrganizationToken == null || String.IsNullOrEmpty(orgItem.OrganizationToken.Thumbprint)))
        {
            btnCreateToken.Visible = true;
            modelItem.DataItem = orgItem.CompanyID;
            this.AjaxAlertAndRedirect("您尚未建立會員電子簽章憑證!!請先建立憑證資訊!!", VirtualPathUtility.ToAbsolute("~/SAM/CreateMemberCertificate.aspx"));
        }
        else
        {
            if (orgItem.OrganizationStatus.Entrusting == false)
                btnCreateToken.Visible = true;
            modelItem.DataItem = orgItem.CompanyID;
        }
    }

    void btnCreateToken_Click(object sender, EventArgs e)
    {
        Model.Security.MembershipManagement.UserProfileMember userProfile = Business.Helper.WebPageUtility.UserProfile;
        modelItem.DataItem = userProfile.CurrentUserRole.OrganizationCategory.CompanyID;
        Response.Redirect("~/SAM/CreateMemberCertificate.aspx");
    }
    
    void btnEntrusting_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/SAM/CreateOrganizationEntrustingCertificate.aspx");
    }
</script>

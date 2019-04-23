<%@ Page Title="" Language="C#" MasterPageFile="~/template/main_page.Master" AutoEventWireup="true" Inherits="eIVOGo.MainPage" StylesheetTheme="Visitor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="30"><img src="images/path_left.gif" alt="" width="30" height="29" /></td>
    <td bgcolor="#ecedd5">首頁 ></td>
    <td width="18"><img src="images/path_right.gif" alt="" width="18" height="29" /></td>
  </tr>
</table>
</asp:Content>
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        Model.Security.MembershipManagement.UserProfileMember userProfile = Business.Helper.WebPageUtility.UserProfile;
        if (userProfile.CurrentUserRole.OrganizationCategory.CategoryID != (int)Model.Locale.Naming.CategoryID.COMP_SYS)
        {
            if (userProfile.Profile.UserProfileStatus != null && userProfile.Profile.UserProfileStatus.CurrentLevel == (int)Model.Locale.Naming.MemberStatusDefinition.Wait_For_Check)
            {
                Server.Transfer("~/SAM/EditMyself.aspx");
            }
            else
            {
                Server.Transfer("~/EIVO/CheckTodoList.aspx");
            }
        }
    }
</script>
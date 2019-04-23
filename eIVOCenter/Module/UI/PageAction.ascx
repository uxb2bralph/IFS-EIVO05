<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageAction.ascx.cs" Inherits="eIVOGo.Module.UI.PageAction" %>
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="30"><img id="img1" runat="server" enableviewstate="false" src="~/images/path_left.gif" alt="" width="30" height="29" /></td>
    <td bgcolor="#ecedd5">
        <asp:Literal ID="litItemName" runat="server" EnableViewState="false"></asp:Literal></td>
    <td width="18"><img id="img3" runat="server" enableviewstate="false" src="~/images/path_right.gif" alt="" width="18" height="29" /></td>
  </tr>
</table>
<script runat="server">
    public bool EnableIcon
    {
        get
        {
            return img1.Visible;
        }
        set
        {
            img1.Visible = value;
            img3.Visible = value;  
        }
    }
</script>
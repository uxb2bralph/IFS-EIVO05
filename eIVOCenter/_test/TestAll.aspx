<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" %>

<%@ Register Src="~/Module/Common/CommonScriptManager.ascx" TagPrefix="uc1" TagName="CommonScriptManager" %>
<%@ Register Src="~/Module/Ajax/PagingControl.ascx" TagPrefix="uc1" TagName="PagingControl" %>
<%@ Register Src="~/Module/Common/PagingControl.ascx" TagPrefix="uc2" TagName="PagingControl" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:CommonScriptManager runat="server" ID="CommonScriptManager" />
    <div>
        <uc1:PagingControl runat="server" ID="PagingControl" CurrentPageIndex="15" RecordCount="555" />
        <uc2:PagingControl runat="server" ID="paging" />
    </div>
    </form>
</body>
</html>
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        paging.RecordCount = 555;
        paging.CurrentPageIndex = 15;
    }
</script>
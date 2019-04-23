<%@ Control Language="C#" AutoEventWireup="true" %>
<asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/Scripts/jquery-1.11.3.js" />
        <asp:ScriptReference Path="~/Scripts/jquery-ui-1.11.4.js" />
        <asp:ScriptReference Path="~/Scripts/bootstrap.js" />
        <asp:ScriptReference Path="~/Scripts/jquery.ui.datepicker-zh-TW.js" />
        <asp:ScriptReference Path="~/Scripts/jquery.form.js" />
    </Scripts>
</asp:ToolkitScriptManager>
<script>
    $(function () {
        $('input[type="button"]').addClass('btn');
    });
</script>
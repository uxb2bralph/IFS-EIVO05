<%@ Control Language="c#" Inherits="Uxnet.Web.Module.Common.MailButton" Codebehind="MailButton.ascx.cs" %>
<asp:button id="_btnDownload" runat="server" Text="�ǰe�ܹq�l�H�c" CssClass="inputText" onclick="_btnDownload_Click"></asp:button>
<input type="hidden" id="mailButtonAddr" name="mailButtonAddr">
<script language="javascript">
<!--
	function sendMail()
	{
		var mailButtonAddr = document.all('mailButtonAddr');
		mailButtonAddr.value = prompt("�п�J����H�q�l�H�c,�p���h�즬���,�Хγr��','���}","");
		if(mailButtonAddr.value!='null' && mailButtonAddr.value.length>0)
			return true;
		
		return false;
	}
//-->
</script>

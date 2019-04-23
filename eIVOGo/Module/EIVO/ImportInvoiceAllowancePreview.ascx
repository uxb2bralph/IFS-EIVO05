<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImportInvoiceAllowancePreview.ascx.cs"
    Inherits="eIVOGo.Module.EIVO.ImportInvoiceAllowancePreview" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<%@ Register Src="../Common/PrintingButton2.ascx" TagName="PrintingButton2" TagPrefix="uc3" %>
<%@ Register Src="../Common/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="../UI/PageAction.ascx" TagName="PageAction" TagPrefix="uc1" %>
<%@ Register Src="../UI/FunctionTitleBar.ascx" TagName="FunctionTitleBar" TagPrefix="uc4" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Data.Linq" %>
<%@ Import Namespace="Model.DataEntity" %>
<%@ Register Src="Item/InvoiceAllowanceUploadList.ascx" TagName="InvoiceAllowanceUploadList" TagPrefix="uc5" %>
<%@ Register src="../Common/PageAnchor.ascx" tagname="PageAnchor" tagprefix="uc6" %>
<!--���|�W��-->

<uc1:PageAction ID="actionItem" runat="server" ItemName="���� &gt; ������פJ" />
<!--����e�����D-->
<uc4:FunctionTitleBar ID="titleBar" runat="server" ItemName="������פJ" />
<!--���s-->
<uc5:InvoiceAllowanceUploadList ID="uploadList" runat="server" />
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td class="Bargain_btn">
            <asp:Button ID="btnAddCode" runat="server" Text="�T�w" OnClick="btnAddCode_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnReset" runat="server" Text="����" OnClick="btnReset_Click" />
        </td>
    </tr>
</table>
<uc6:PageAnchor ID="NextAction" runat="server" RedirectTo="~/EIVO/InvoiceAllowanceUploadList.aspx" />
<uc6:PageAnchor ID="PrevAction" runat="server" RedirectTo="~/EIVO/InvoiceAllowanceImport.aspx" />

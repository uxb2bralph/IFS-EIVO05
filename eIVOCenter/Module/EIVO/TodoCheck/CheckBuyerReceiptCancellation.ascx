﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="eIVOCenter.Module.EIVO.TodoCheck.CheckBuyerInvoice" %>
<%@ Register src="~/Module/Common/ActionHandler.ascx" tagname="ActionHandler" tagprefix="uc1" %>
<%@ Register src="../../Common/PageAnchor.ascx" tagname="PageAnchor" tagprefix="uc2" %>
<%@ Register assembly="Model" namespace="Model.DataEntity" tagprefix="cc1" %>
<tr>
    <td>
        <img runat="server" id="img1" enableviewstate="false" border="0" align="middle" src="~/images/arrow_02.gif"
            width="15" height="15" />
        <a href="#" onclick="<%# doLink.GetPostBackEventReference(null) %>">進項作廢電子收據待接收<%# _count %>筆</a>
    </td>
</tr>
<uc1:ActionHandler ID="doLink" runat="server" />
<uc2:PageAnchor ID="LinkAction" runat="server"  RedirectTo="~/EIVO/ReceiveReceiptCancellation.aspx?query=All" />
<cc1:InvoiceDataSource ID="dsInv" runat="server">
</cc1:InvoiceDataSource>
<script runat="server">
    int _count;
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        var mgr = dsInv.CreateDataManager();

        _count = mgr.GetTable<CDS_Document>().Where(d => d.CurrentStep == (int)Model.Locale.Naming.B2BInvoiceStepDefinition.待接收 && d.DocType == (int)Model.Locale.Naming.DocumentTypeDefinition.E_ReceiptCancellation)
            .Select(d => d.DerivedDocument)
            .Join(mgr.GetTable<ReceiptCancellation>().Where(i => i.ReceiptItem.BuyerID == _userProfile.CurrentUserRole.OrganizationCategory.CompanyID), d => d.SourceID, i => i.ReceiptID, (d, i) => d).Count();
        
        if (_count > 0)
        {
            this.DataBind();
        }
        else
        {
            this.Visible = false;
        }
    }
</script>



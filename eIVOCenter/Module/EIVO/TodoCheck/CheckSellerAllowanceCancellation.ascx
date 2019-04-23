﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="eIVOCenter.Module.EIVO.TodoCheck.CheckBuyerInvoice" %>
<%@ Register src="~/Module/Common/ActionHandler.ascx" tagname="ActionHandler" tagprefix="uc1" %>
<%@ Register src="../../Common/PageAnchor.ascx" tagname="PageAnchor" tagprefix="uc2" %>
<%@ Register assembly="Model" namespace="Model.DataEntity" tagprefix="cc1" %>
<tr>
    <td>
        <img runat="server" id="img1" enableviewstate="false" border="0" align="middle" src="~/images/arrow_02.gif"
            width="15" height="15" />
        <a href="#" onclick="<%# doLink.GetPostBackEventReference(null) %>">銷項作廢電子折讓單待接收<%# _count %>筆</a>
    </td>
</tr>
<uc1:ActionHandler ID="doLink" runat="server" />
<uc2:PageAnchor ID="LinkAction" runat="server" RedirectTo="~/EIVO/ReceiveAllowanceCancellation.aspx?query=All" />
<cc1:InvoiceDataSource ID="dsInv" runat="server">
</cc1:InvoiceDataSource>
<script runat="server">
    int _count;
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        var mgr = dsInv.CreateDataManager();
        
        //var orgItem = mgr.GetTable<Organization>().Where(o => o.CompanyID == _userProfile.CurrentUserRole.OrganizationCategory.CompanyID).First();
        //if (orgItem.EnterpriseGroupMember.Count > 0)
        //{
        //    this.Visible = false;
        //}
        //else
        {
            _count = mgr.GetTable<CDS_Document>().Where(d => d.CurrentStep == (int)Model.Locale.Naming.B2BInvoiceStepDefinition.待接收 && d.DocType == (int)Model.Locale.Naming.DocumentTypeDefinition.E_AllowanceCancellation)
                .Select(d => d.DerivedDocument)
                .Join(mgr.GetTable<InvoiceAllowance>().Where(i => i.InvoiceAllowanceSeller.SellerID == _userProfile.CurrentUserRole.OrganizationCategory.CompanyID), d => d.SourceID, i => i.AllowanceID, (d, i) => d).Count();

            if (_count > 0)
            {
                this.DataBind();
            }
            else
            {
                this.Visible = false;
            }
        }
    }
</script>



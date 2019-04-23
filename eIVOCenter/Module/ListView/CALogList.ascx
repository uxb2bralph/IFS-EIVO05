<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CALogList.ascx.cs" Inherits="eIVOCenter.Module.ListView.CALogList" %>
<%@ Register Src="~/Module/Common/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<%@ Register Src="~/Module/Common/ActionHandler.ascx" TagName="ActionHandler" TagPrefix="uc1" %>
<%@ Import Namespace="Model.DataEntity" %>
<%@ Import Namespace="Utility" %>
<%@ Import Namespace="eIVOCenter.Helper" %>
<%@ Import Namespace="Model.Locale" %>
<%@ Register src="../Entity/CALogItem.ascx" tagname="CALogItem" tagprefix="uc3" %>
<div class="border_gray">
    <asp:GridView ID="gvEntity" runat="server" AutoGenerateColumns="False" Width="100%"
        GridLines="None" CellPadding="0" CssClass="table01" AllowPaging="True" EnableViewState="False"
        DataSourceID="dsEntity" ShowFooter="True" DataKeyNames="LogID">
        <Columns>
            <asp:TemplateField HeaderText="營業人統編 " SortExpression="DocID">
                <ItemTemplate>
                    <%# checkBusiness((CALog)Container.DataItem) %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="營業人名稱 " SortExpression="DocID">
                <ItemTemplate>
                    <%# _business!=null ? _business.CompanyName
                        : ((CALog)Container.DataItem).CDS_Document.DocumentOwner.Organization.CompanyName%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="憑證作業時間" SortExpression="LogDate">
                <ItemTemplate>
                    <%# ValueValidity.ConvertChineseDateTimeString(((CALog)Container.DataItem).LogDate) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="憑證內容" SortExpression="ContentPath">
                <ItemTemplate>
                    <asp:ImageButton ID="imgBtn" runat="server" ImageUrl="~/images/icon_ca.gif" 
                        OnClientClick='<%# doDisplayContent.GetPostBackEventReference(String.Format("{0}",Eval(gvEntity.DataKeyNames[0]))) %>' />
                    <%--<asp:ImageButton ID="imgDownload" runat="server" ImageUrl="~/images/icon_ca.gif" Visible="<%# ((CALog)Container.DataItem).Catalog==(int)Naming.CACatalogDefinition.UXGW上傳附件檔%>"
                        OnClientClick='<%# doDownload.GetPostBackEventReference(String.Format("{0}",Eval(gvEntity.DataKeyNames[0]))) %>' />--%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <FooterStyle CssClass="total-count" />
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle />
        <HeaderStyle />
        <AlternatingRowStyle CssClass="OldLace" />
        <PagerTemplate>
            <uc2:PagingControl ID="pagingList" runat="server" OnPageIndexChanged="PageIndexChanged" />
        </PagerTemplate>
        <RowStyle />
        <EditRowStyle />
    </asp:GridView>
</div>
<uc1:ActionHandler ID="doDisplayContent" runat="server" />
<uc1:ActionHandler ID="doDownload" runat="server" />
<cc1:CALogDataSource ID="dsEntity" runat="server">
</cc1:CALogDataSource>
<uc3:CALogItem ID="logView" runat="server" />
<script runat="server">
    
    DataAccessLayer.basis.GenericManager<EIVOEntityDataContext, CALog> _mgr;
    Organization _business;
        
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        doDisplayContent.DoAction = arg =>
        {
            logView.QueryExpr = o => o.LogID == int.Parse(arg);
            logView.BindData();
        };

        doDownload.DoAction = arg =>
        {
            var item = dsEntity.CreateDataManager().EntityList.Where(o => o.LogID == int.Parse(arg)).FirstOrDefault();
            if (item != null && !String.IsNullOrEmpty(item.ContentPath) && System.IO.File.Exists(item.ContentPath))
            {
                System.Xml.XmlDocument docCA = new System.Xml.XmlDocument();
                docCA.Load(item.ContentPath);
                byte[] certRaw = System.Text.Encoding.Default.GetBytes(docCA.DocumentElement["pkcs7Envelop"]["DataSignature"].InnerXml);

                Response.Clear();
                Response.ContentType = "message/rfc822";
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "Signature.p7b"));
                Response.AddHeader("Content-Length", certRaw.Length.ToString());
                Response.OutputStream.Write(certRaw, 0, certRaw.Length);

                Response.Flush();
                Response.End();
                
                
            }

        };

        _mgr = dsEntity.CreateDataManager();        
        
    }

    String checkBusiness(CALog item)
    {
        _business = null;
        
        if(item.Catalog == (int)Naming.CACatalogDefinition.平台自動接收)
        {
            var doc = _mgr.GetTable<CDS_Document>().Where(d => d.DocID == item.DocID).First();
            switch ((Naming.B2BInvoiceDocumentTypeDefinition?)doc.DocType)
            {
                case Naming.B2BInvoiceDocumentTypeDefinition.電子發票:
                    _business = doc.InvoiceItem.InvoiceBuyer.Organization;
                    break;
                case Naming.B2BInvoiceDocumentTypeDefinition.發票折讓:
                    _business = doc.InvoiceAllowance.InvoiceAllowanceSeller.Organization;
                    break;
                case Naming.B2BInvoiceDocumentTypeDefinition.作廢發票:
                    _business = doc.DerivedDocument.ParentDocument.InvoiceItem.InvoiceBuyer.Organization;
                    break;
                case Naming.B2BInvoiceDocumentTypeDefinition.作廢折讓:
                    _business = doc.DerivedDocument.ParentDocument.InvoiceAllowance.InvoiceAllowanceSeller.Organization;
                    break;
                case Naming.B2BInvoiceDocumentTypeDefinition.收據:
                    _business = doc.ReceiptItem.Buyer;
                    break;
                case Naming.B2BInvoiceDocumentTypeDefinition.作廢收據:
                    _business = doc.DerivedDocument.ParentDocument.ReceiptItem.Buyer;
                    break;
                    
            }
        }
        else if (item.Catalog == (int)Naming.CACatalogDefinition.平台自動開立)
        {
            var doc = _mgr.GetTable<CDS_Document>().Where(d => d.DocID == item.DocID).First();
            switch ((Naming.B2BInvoiceDocumentTypeDefinition?)doc.DocType)
            {
                case Naming.B2BInvoiceDocumentTypeDefinition.電子發票:
                    _business = doc.InvoiceItem.InvoiceSeller.Organization;
                    break;
                case Naming.B2BInvoiceDocumentTypeDefinition.發票折讓:
                    _business = doc.InvoiceAllowance.InvoiceAllowanceBuyer.Organization;
                    break;
                case Naming.B2BInvoiceDocumentTypeDefinition.作廢發票:
                    _business = doc.DerivedDocument.ParentDocument.InvoiceItem.InvoiceSeller.Organization;
                    break;
                case Naming.B2BInvoiceDocumentTypeDefinition.作廢折讓:
                    _business = doc.DerivedDocument.ParentDocument.InvoiceAllowance.InvoiceAllowanceBuyer.Organization;
                    break;
                case Naming.B2BInvoiceDocumentTypeDefinition.收據:
                    _business = doc.ReceiptItem.Seller;
                    break;
                case Naming.B2BInvoiceDocumentTypeDefinition.作廢收據:
                    _business = doc.DerivedDocument.ParentDocument.ReceiptItem.Seller;
                    break;
            }
        }
        
        return _business != null ? _business.ReceiptNo : item.CDS_Document.DocumentOwner.Organization.ReceiptNo;
    }
</script>

﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="eIVOCenter.Module.Base.InvoiceItemList" %>
<%@ Register Src="~/Module/Common/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="~/Module/Common/ActionHandler.ascx" TagName="ActionHandler" TagPrefix="uc1" %>
<%@ Register Src="~/Module/EIVO/Action/InvoiceItemCommonView.ascx" TagName="InvoiceItemCommonView"
    TagPrefix="uc6" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<%@ Register Src="~/Module/Entity/OrganizationItem.ascx" TagName="OrganizationItem" TagPrefix="uc3" %>
<%@ Import Namespace="eIVOCenter.Helper" %>
<%@ Import Namespace="Model.Locale" %>
<%@ Import Namespace="Model.DataEntity" %>
<%@ Import Namespace="Utility" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="Uxnet.Web.WebUI" %>
<div class="border_gray">
    <asp:GridView ID="gvEntity" runat="server" AutoGenerateColumns="False" Width="100%"
        GridLines="None" CellPadding="0" CssClass="table01" AllowPaging="True" EnableViewState="False"
        DataSourceID="dsEntity" ShowFooter="True">
        <Columns>
            <asp:TemplateField HeaderText="日期">
                <ItemTemplate>
                    <%# ValueValidity.ConvertChineseDateString(((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceItem.InvoiceDate)%></ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="集團成員">
                <ItemTemplate>
                    <%# ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceItem.InvoiceSeller.Organization.EnterpriseGroupMember.Count>0 ? ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceItem.InvoiceSeller.CustomerName : null  %> <%# ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceItem.InvoiceBuyer.Organization.EnterpriseGroupMember.Count > 0 ? ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceItem.InvoiceBuyer.CustomerName : null%></ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="開立發票營業人">
                <ItemTemplate>
                    <a href="#" onclick="<%# doDisplayCompany.GetPostBackEventReference(((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceItem.InvoiceSeller.SellerID.ToString()) %>">
                        <%# ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceItem.InvoiceSeller.CustomerName %></a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="接收發票營業人">
                <ItemTemplate>
                    <a href="#" onclick="<%# doDisplayCompany.GetPostBackEventReference(((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceItem.InvoiceBuyer.BuyerID.ToString()) %>">
                        <%# ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceItem.InvoiceBuyer.CustomerName %></a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="發票號碼">
                <ItemTemplate>
                    <a onclick="<%# doDisplayInvoice.GetPostBackEventReference(String.Format("{0}",((CDS_Document)Container.DataItem).DerivedDocument.SourceID)) %>"
                        href="#">
                        <%# ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceItem.TrackCode + ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceItem.No %></a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="未稅金額">
                <ItemTemplate>
                    <%#String.Format("{0:#,0}", ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceItem.InvoiceAmountType.SalesAmount)%></ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="稅 額">
                <ItemTemplate>
                    <%#String.Format("{0:#,0}", ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceItem.InvoiceAmountType.TaxAmount)%></ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
                <FooterTemplate>
                    共<%# _totalRecordCount %>筆</FooterTemplate>
                <FooterStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="含稅金額">
                <ItemTemplate>
                    <%#String.Format("{0:#,0}", ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceItem.InvoiceAmountType.TotalAmount)%></ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="對帳單" FooterText="總計金額">
                <ItemTemplate>
                    <a href="#" onclick="<%# getStatement((CDS_Document)Container.DataItem) %>">
                        <asp:Image ID="Image1" BorderStyle="None" ImageUrl="~/images/icon_ca.gif" runat="server" Visible="<%# ((CDS_Document)Container.DataItem).Attachment.Count<=0 ? false : true %>" />
                    </a>
                </ItemTemplate>                
                <ItemStyle HorizontalAlign="Center" />
                <FooterStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="發票狀態">
                <ItemTemplate>
                    <asp:Label ID="status" runat="server" Text="<%# ((Naming.B2BInvoiceStepDefinition)((CDS_Document)Container.DataItem).CurrentStep).ToString() %>"
                        EnableViewState="false" ForeColor="<%# checkStatus((Naming.B2BInvoiceStepDefinition)((CDS_Document)Container.DataItem).CurrentStep) %>"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <FooterTemplate>
                    <%# String.Format("{0:##,###,###,##0}", this.Select().Sum(d=>d.DerivedDocument.ParentDocument.InvoiceItem.InvoiceAmountType.TotalAmount)) %></FooterTemplate>
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
<cc1:DocumentDataSource ID="dsEntity" runat="server">
</cc1:DocumentDataSource>
<uc1:ActionHandler ID="doDisplayInvoice" runat="server" />
<uc6:InvoiceItemCommonView ID="printInvoice" runat="server" />
<uc1:ActionHandler ID="doDisplayCompany" runat="server" />
<uc1:ActionHandler ID="doDisplayStatement" runat="server" />
<uc3:OrganizationItem ID="orgView" runat="server" Visible="false" />
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        doDisplayInvoice.DoAction = arg =>
        {
            printInvoice.QueryExpr = i => i.InvoiceID == int.Parse(arg);
            printInvoice.BindData();
        };

        doDisplayCompany.DoAction = arg =>
        {
            orgView.QueryExpr = o => o.CompanyID == int.Parse(arg);
            orgView.BindData();
        };

        doDisplayStatement.DoAction = arg =>
        {
            //Response.WriteFileAsDownload(arg, String.Format("{0:yyyy-MM-dd}.pdf", DateTime.Today), false);
            Response.WriteFileAsDownload(arg);
        };
    }

    private String getStatement(CDS_Document dataItem)
    {
        return dataItem.Attachment.Count <= 0 ? "return false;"
            : File.Exists(dataItem.Attachment.FirstOrDefault().StoredPath) ? doDisplayStatement.GetPostBackEventReference(dataItem.Attachment.FirstOrDefault().StoredPath) : "alert('對帳單檔案遺失!!');return false;";
    }
    
    private System.Drawing.Color checkStatus(Naming.B2BInvoiceStepDefinition step)
    {
        switch (step)
        {
            case Naming.B2BInvoiceStepDefinition.待接收:
            case Naming.B2BInvoiceStepDefinition.待開立:
            case Naming.B2BInvoiceStepDefinition.待傳送:
                return System.Drawing.Color.Red;
            default:
                return System.Drawing.Color.Black;
        }
    }
</script>

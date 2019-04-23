﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="eIVOCenter.Module.Base.InvoiceAllowanceList" %>
<%@ Register Src="~/Module/Common/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="~/Module/Common/ActionHandler.ascx" TagName="ActionHandler" TagPrefix="uc1" %>
<%@ Register Src="~/Module/EIVO/Action/AllowanceItemCommonView.ascx" TagName="AllowanceItemCommonView"
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
                    <%# ValueValidity.ConvertChineseDateString(((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceAllowance.AllowanceDate)%></ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="集團成員">
                <ItemTemplate>
                    <%# ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceAllowance.InvoiceAllowanceSeller.Organization.EnterpriseGroupMember.Count > 0 ? ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceAllowance.InvoiceAllowanceSeller.CustomerName : null%> <%# ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceAllowance.InvoiceAllowanceBuyer.Organization.EnterpriseGroupMember.Count > 0 ? ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceAllowance.InvoiceAllowanceBuyer.CustomerName : null%></ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="開立發票折讓營業人">
                <ItemTemplate>
                    <a href="#" onclick="<%# doDisplayCompany.GetPostBackEventReference(((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceAllowance.InvoiceAllowanceBuyer.BuyerID.ToString()) %>">
                        <%# ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceAllowance.InvoiceAllowanceBuyer.CustomerName%></a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="接收發票折讓營業人">
                <ItemTemplate>
                    <a href="#" onclick="<%# doDisplayCompany.GetPostBackEventReference(((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceAllowance.InvoiceAllowanceSeller.SellerID.ToString()) %>">
                        <%# ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceAllowance.InvoiceAllowanceSeller.CustomerName%></a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="折讓單號碼">
                <ItemTemplate>
                    <a onclick="<%# doDisplayInvoice.GetPostBackEventReference(String.Format("{0}",((CDS_Document)Container.DataItem).DerivedDocument.SourceID)) %>"
                        href="#">
                        <%# ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceAllowance.AllowanceNumber%></a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="對帳單">
                <ItemTemplate>
                    <a href="#" onclick="<%# getStatement((CDS_Document)Container.DataItem) %>">
                        <asp:Image ID="Image1" BorderStyle="None" ImageUrl="~/images/icon_ca.gif" runat="server" Visible="<%# ((CDS_Document)Container.DataItem).Attachment.Count<=0 ? false : true %>" />
                    </a>
                </ItemTemplate>                
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="金額" FooterText="總計金額">
                <ItemTemplate>
                    <%#String.Format("{0:#,0}", ((CDS_Document)Container.DataItem).DerivedDocument.ParentDocument.InvoiceAllowance.TotalAmount)%></ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <FooterStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="折讓狀態">
                <ItemTemplate>
                    <asp:Label ID="status" runat="server" Text="<%# checkStatus((Naming.B2BInvoiceStepDefinition)((CDS_Document)Container.DataItem).CurrentStep) %>"                    
                        EnableViewState="false" ></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <FooterTemplate>
                    <%# String.Format("{0:##,###,###,##0}", this.Select().Sum(d=>d.DerivedDocument.ParentDocument.InvoiceAllowance.TotalAmount)) %></FooterTemplate>
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
<uc6:AllowanceItemCommonView ID="printAllowance" runat="server" />
<uc1:ActionHandler ID="doDisplayCompany" runat="server" />
<uc1:ActionHandler ID="doDisplayStatement" runat="server" />
<uc3:OrganizationItem ID="orgView" runat="server" Visible="false" />
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        doDisplayInvoice.DoAction = arg =>
        {
            printAllowance.QueryExpr = i => i.AllowanceID == int.Parse(arg);
            printAllowance.BindData();
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
    
    private string checkStatus(Naming.B2BInvoiceStepDefinition step)
    {
        switch (step)
        {
            case Naming.B2BInvoiceStepDefinition.待接收:
                return (Naming.B2BInvoiceStepDefinition.待接收).ToString();
            case Naming.B2BInvoiceStepDefinition.待開立:
                return (Naming.B2BInvoiceStepDefinition.待開立).ToString();
            case Naming.B2BInvoiceStepDefinition.待傳送:
                return "待接收";
            case Naming.B2BInvoiceStepDefinition.已接收:
                return (Naming.B2BInvoiceStepDefinition.已接收).ToString();
            case Naming.B2BInvoiceStepDefinition.已開立:
                return (Naming.B2BInvoiceStepDefinition.已開立).ToString();
            case Naming.B2BInvoiceStepDefinition.已傳送:
                return "已接收";
            default:
                return "";
        }
    }
</script>

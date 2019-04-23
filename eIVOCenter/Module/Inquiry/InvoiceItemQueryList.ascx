﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoiceItemQueryList.ascx.cs"
    Inherits="eIVOCenter.Module.Inquiry.InvoiceItemQueryList" %>
<%@ Register Src="../Common/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="../EIVO/PNewInvalidInvoicePreview.ascx" TagName="PNewInvalidInvoicePreview"
    TagPrefix="uc4" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<%@ Import Namespace="Model.DataEntity" %>
<%@ Import Namespace="Utility" %>
<%@ Import Namespace="Model.Locale" %>
<%@ Import Namespace="eIVOCenter.Helper" %>
<!--表格 開始-->
<asp:GridView ID="gvEntity" runat="server" AutoGenerateColumns="False" Width="100%"
    GridLines="None" CellPadding="0" CssClass="table01" AllowPaging="True" ClientIDMode="Static"
    EnableViewState="False" DataSourceID="dsEntity" ShowFooter="True">
    <Columns>
        <asp:TemplateField HeaderText="日期">
            <ItemTemplate>
                <%# ValueValidity.ConvertChineseDateString(((CDS_Document)Container.DataItem).InvoiceItem.InvoiceDate)%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="發票/折讓類別">
            <ItemTemplate>
                <%# ((CDS_Document)Container.DataItem).InvoiceItem.CheckBusinessType(this.dsEntity.CreateDataManager(), _userProfile.CurrentUserRole.OrganizationCategory.CompanyID).ToString()%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="開立發票/折讓營業人">
            <ItemTemplate>
                <asp:LinkButton ID="lbtn" runat="server" Text='<%# ((CDS_Document)Container.DataItem).InvoiceItem.InvoiceSeller.Organization.CompanyName%>' 
                     CausesValidation="false" CommandName="Edit" OnClientClick='<%# Page.ClientScript.GetPostBackEventReference(this, String.Format("C:{0}",((CDS_Document)Container.DataItem).InvoiceItem.InvoiceSeller.Organization.ReceiptNo)) + "; return false;" %>' />
                </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="接收發票/折讓營業人">
            <ItemTemplate>
                <asp:LinkButton ID="lbtn" runat="server" Text='<%# ((CDS_Document)Container.DataItem).InvoiceItem.InvoiceBuyer.Organization.CompanyName%>' 
                     CausesValidation="false" CommandName="Edit" OnClientClick='<%# Page.ClientScript.GetPostBackEventReference(this, String.Format("C:{0}",((CDS_Document)Container.DataItem).InvoiceItem.InvoiceBuyer.Organization.ReceiptNo)) + "; return false;" %>' />
                </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="發票/折讓號碼">
            <ItemTemplate>
                <asp:LinkButton ID="lbtn" runat="server" Text='<%# ((CDS_Document)Container.DataItem).InvoiceItem.TrackCode + ((CDS_Document)Container.DataItem).InvoiceItem.No %>'
                    CausesValidation="false" CommandName="Edit" OnClientClick='<%# Page.ClientScript.GetPostBackEventReference(this, String.Format("S:{0}",((CDS_Document)Container.DataItem).InvoiceItem.InvoiceID)) + "; return false;" %>' />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="金額">
            <ItemTemplate>
                <%#String.Format("{0:#,0}", ((CDS_Document)Container.DataItem).InvoiceItem.InvoiceAmountType.TotalAmount)%></ItemTemplate>
            <ItemStyle HorizontalAlign="Right" />
            <FooterTemplate>
                共<%# _totalRecordCount %>筆</FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="發票/折讓狀態">
            <ItemTemplate>
                <%# ((CDS_Document)Container.DataItem).CurrentStep == (int)Naming.B2BInvoiceStepDefinition.已傳送 ? "已傳送" : ((CDS_Document)Container.DataItem).CurrentStep == (int)Naming.B2BInvoiceStepDefinition.待接收 ? "待接收" : ((CDS_Document)Container.DataItem).CurrentStep == (int)Naming.B2BInvoiceStepDefinition.待開立 ? "待開立" : ((CDS_Document)Container.DataItem).CurrentStep == (int)Naming.B2BInvoiceStepDefinition.待傳送 ? "待傳送" : ""%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
            <FooterTemplate>
                總計金額：<%# String.Format("{0:##,###,###,##0}", _subtotal) %></FooterTemplate>
        </asp:TemplateField>
    </Columns>
    <FooterStyle />
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
<center>
    <asp:Label ID="lblError" Visible="false" ForeColor="Red" Font-Size="Larger" runat="server" Text="查無資料!!"
        EnableViewState="false"></asp:Label>
</center>
<cc1:DocumentDataSource ID="dsEntity" runat="server">
</cc1:DocumentDataSource>
<uc4:PNewInvalidInvoicePreview ID="PNewInvalidInvoicePreview1" runat="server" />

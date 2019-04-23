﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoiceItemResetList.ascx.cs"
    Inherits="eIVOGo.Module.EIVO.InvoiceItemResetList" %>
<%@ Register Src="../Common/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="PNewInvalidInvoicePreview.ascx" TagName="PNewInvalidInvoicePreview"
    TagPrefix="uc4" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<%@ Import Namespace="Model.DataEntity" %>
<%@ Import Namespace="Utility" %>
<%@ Register src="ConfirmInvoiceDownload.ascx" tagname="ConfirmInvoiceDownload" tagprefix="uc1" %>
<!--表格 開始-->
<asp:GridView ID="gvEntity" runat="server" AutoGenerateColumns="False" Width="100%"
    GridLines="None" CellPadding="0" CssClass="table01" AllowPaging="True" ClientIDMode="Static"
    EnableViewState="False" DataSourceID="dsInv" ShowFooter="True">
    <Columns>
        <asp:TemplateField HeaderText="日期">
            <ItemTemplate>
                <%# ValueValidity.ConvertChineseDateString(((InvoiceItem)Container.DataItem).InvoiceDate)%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="開立發票營業人">
            <ItemTemplate>
                <%# ((InvoiceItem)Container.DataItem).CDS_Document.DocumentOwner.Organization.CompanyName%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="統編">
            <ItemTemplate>
                <%# ((InvoiceItem)Container.DataItem).CDS_Document.DocumentOwner.Organization.ReceiptNo%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="發票">
            <ItemTemplate>
                <asp:LinkButton ID="lbtn" runat="server" Text='<%# ((InvoiceItem)Container.DataItem).GetMaskInvoiceNo() %>'
                    CausesValidation="false" CommandName="Edit" OnClientClick='<%# Page.ClientScript.GetPostBackEventReference(this, String.Format("S:{0}",((InvoiceItem)Container.DataItem).InvoiceID)) + "; return false;" %>' />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="金額">
            <ItemTemplate>
                <%#String.Format("{0:0,0.00}", ((InvoiceItem)Container.DataItem).InvoiceAmountType.TotalAmount)%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
            <FooterTemplate>
                共<%# _totalRecordCount %>筆</FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="是否中獎" FooterText="總計金額：">
            <ItemTemplate>
                <%# ((InvoiceItem)Container.DataItem).InvoiceWinningNumber != null ? ((InvoiceItem)Container.DataItem).InvoiceWinningNumber.UniformInvoiceWinningNumber.PrizeType : "N/A"%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
            <FooterStyle HorizontalAlign="Right" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="捐贈單位">
            <ItemTemplate>
                <%# ((InvoiceItem)Container.DataItem).DonationID.HasValue?((InvoiceItem)Container.DataItem).Donatory.CompanyName:""%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
            <FooterTemplate>
                <%# String.Format("{0:##,###,###,##0}", _subtotal) %></FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnReset" runat="server" Text='授權下載'
                    CausesValidation="false" CommandName="Reset" OnClientClick='<%# Page.ClientScript.GetPostBackEventReference(this, String.Format("R:{0}",((InvoiceItem)Container.DataItem).InvoiceID)) + "; return false;" %>' />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
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
<cc1:InvoiceDataSource ID="dsInv" runat="server">
</cc1:InvoiceDataSource>
<uc4:PNewInvalidInvoicePreview ID="PNewInvalidInvoicePreview1" runat="server" />
<uc1:ConfirmInvoiceDownload ID="confirmDownload" runat="server" />


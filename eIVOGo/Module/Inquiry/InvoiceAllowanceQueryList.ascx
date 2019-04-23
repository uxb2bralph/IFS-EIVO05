﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoiceAllowanceQueryList.ascx.cs"
    Inherits="eIVOGo.Module.Inquiry.InvoiceAllowanceQueryList" %>
<%@ Register Src="../Common/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="../EIVO/PNewInvalidInvoicePreview.ascx" TagName="PNewInvalidInvoicePreview"
    TagPrefix="uc4" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<%@ Import Namespace="Model.DataEntity" %>
<%@ Import Namespace="Model.Helper" %>
<%@ Import Namespace="Utility" %>
<!--表格 開始-->
<asp:GridView ID="gvEntity" runat="server" AutoGenerateColumns="False" Width="100%"
    GridLines="None" CellPadding="0" CssClass="table01" AllowPaging="True" ClientIDMode="Static"
    EnableViewState="False" ShowFooter="True" DataSourceID="dsInv">
    <Columns>
        <asp:TemplateField HeaderText="日期">
            <ItemTemplate>
                <%# ValueValidity.ConvertChineseDateString(((InvoiceAllowance)Container.DataItem).AllowanceDate)%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="開立發票營業人">
            <ItemTemplate>
                <%# ((InvoiceAllowance)Container.DataItem).CDS_Document.DocumentOwner.Organization.CompanyName%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="統編">
            <ItemTemplate>
                <%# ((InvoiceAllowance)Container.DataItem).CDS_Document.DocumentOwner.Organization.ReceiptNo%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="折讓號碼">
            <ItemTemplate>
                <asp:LinkButton ID="lbtn" runat="server" Text='<%# ((InvoiceAllowance)Container.DataItem).AllowanceNumber %>'
                    CausesValidation="false" CommandName="Edit" OnClientClick='<%# Page.ClientScript.GetPostBackEventReference(this, String.Format("S:{0}",((InvoiceAllowance)Container.DataItem).InvoiceItem.InvoiceID)) + "; return false;" %>' />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
            <FooterTemplate>
                共<%# _totalRecordCount %>筆</FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="金額">
            <ItemTemplate>
                <%#String.Format("{0:0,0.00}", ((InvoiceAllowance)Container.DataItem).TotalAmount)%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
             <FooterTemplate>總計金額：<%# String.Format("{0:##,###,###,##0}", _subtotal) %></FooterTemplate>
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
<cc1:AllowanceDataSource ID="dsInv" runat="server">
</cc1:AllowanceDataSource>
<uc4:PNewInvalidInvoicePreview ID="PNewInvalidInvoicePreview1" runat="server" />

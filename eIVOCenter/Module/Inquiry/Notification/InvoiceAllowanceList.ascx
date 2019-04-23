﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="eIVOCenter.Module.Base.InvoiceAllowanceList" %>
<%@ Register Src="~/Module/Common/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="~/Module/Common/ActionHandler.ascx" TagName="ActionHandler" TagPrefix="uc1" %>
<%@ Register Src="~/Module/EIVO/Action/InvoiceItemCommonView.ascx" TagName="InvoiceItemCommonView"
    TagPrefix="uc6" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<%@ Register Src="~/Module/Entity/OrganizationItem.ascx" TagName="OrganizationItem"
    TagPrefix="uc3" %>
<%@ Import Namespace="eIVOCenter.Helper" %>
<%@ Import Namespace="Model.Locale" %>
<%@ Import Namespace="Model.DataEntity" %>
<%@ Import Namespace="Utility" %>
<div class="border_gray">
    <asp:GridView ID="gvEntity" runat="server" AutoGenerateColumns="False" Width="100%"
        GridLines="None" CellPadding="0" CssClass="table01" AllowPaging="True" EnableViewState="False"
        DataSourceID="dsEntity" ShowFooter="True">
        <Columns>
            <asp:TemplateField HeaderText="日期">
                <ItemTemplate>
                    <%# ValueValidity.ConvertChineseDateString(((CDS_Document)Container.DataItem).InvoiceAllowance.AllowanceDate)%></ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="開立發票折讓營業人">
                <ItemTemplate>
                    <%# ((CDS_Document)Container.DataItem).InvoiceAllowance.InvoiceAllowanceBuyer.CustomerName%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="接收發票折讓營業人">
                <ItemTemplate>
                    <%# ((CDS_Document)Container.DataItem).InvoiceAllowance.InvoiceAllowanceSeller.CustomerName%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="折讓單號碼" FooterText="總計金額">
                <ItemTemplate>
                    <%# ((CDS_Document)Container.DataItem).InvoiceAllowance.AllowanceNumber%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <FooterStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="金額">
                <ItemTemplate>
                    <%#String.Format("{0:#,0}", ((CDS_Document)Container.DataItem).InvoiceAllowance.TotalAmount)%></ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <FooterTemplate>
                    <%# String.Format("{0:##,###,###,##0}", _subtotal) %></FooterTemplate>
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



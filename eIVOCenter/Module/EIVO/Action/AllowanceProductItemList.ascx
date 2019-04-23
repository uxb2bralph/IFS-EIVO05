﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AllowanceProductItemList.ascx.cs"
    Inherits="eIVOCenter.Module.EIVO.Action.AllowanceProductItemList" %>
<%@ Register Src="~/Module/Common/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Import Namespace="Model.DataEntity" %>
<%@ Import Namespace="Utility" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<%@ Register Src="~/Module/Common/ActionHandler.ascx" TagName="ActionHandler" TagPrefix="uc1" %>
<div style="width: 100%; height:auto; max-height: 300px; overflow: scroll">
<asp:GridView ID="gvEntity" runat="server" AutoGenerateColumns="False" Width="100%"
    GridLines="None" CellPadding="0" CssClass="table01" EnableViewState="false" AllowPaging="false"
    DataSourceID="dsEntity" ShowFooter="True">
    <Columns>
        <asp:TemplateField HeaderText="聯式">
            <ItemTemplate>
                <%# loadItem(((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.InvoiceNo).InvoiceBuyer.ReceiptNo.Equals("0000000000") ? "二" : "三"%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="年">
            <ItemTemplate>
                <%# loadItem(((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.InvoiceNo).InvoiceDate.Value.Year%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="月">
            <ItemTemplate>
                <%# loadItem(((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.InvoiceNo).InvoiceDate.Value.Month%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="日">
            <ItemTemplate>
                <%# loadItem(((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.InvoiceNo).InvoiceDate.Value.Day%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="字軌">
            <ItemTemplate>
                <%# loadItem(((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.InvoiceNo).TrackCode%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="號碼">
            <ItemTemplate>
                <%# loadItem(((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.InvoiceNo).No%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="品名">
            <ItemTemplate>
                <per><%# ((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.OriginalDescription%></pre></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="數量">
            <ItemTemplate>
                <%# String.Format("{0:0}", ((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.Piece) %></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="單價">
            <ItemTemplate>
                <%# String.Format("{0:#,0}", ((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.UnitCost) %></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="金額&lt;br/&gt;(不含稅之進貨額)">
            <ItemTemplate>
                <%# String.Format("{0:#,0}", (((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.Amount))%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="營業稅額">
            <ItemTemplate>
                <%# String.Format("{0:#,0}", ((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.Tax) %></ItemTemplate>
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
</div>
<cc1:AllowanceDetailDataSource ID="dsEntity" runat="server">
</cc1:AllowanceDetailDataSource>

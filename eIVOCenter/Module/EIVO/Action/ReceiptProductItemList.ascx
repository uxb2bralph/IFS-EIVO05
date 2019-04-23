﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReceiptProductItemList.ascx.cs"
    Inherits="eIVOCenter.Module.EIVO.Action.ReceiptProductItemList" %>
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
        <asp:TemplateField HeaderText="品名">
            <ItemTemplate>
                <%# ((ReceiptDetail)Container.DataItem).Description%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="數量">
            <ItemTemplate>
                <%# String.Format("{0:0}", ((ReceiptDetail)Container.DataItem).Quantity) %></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="單價">
            <ItemTemplate>
                <%# String.Format("{0:#,0}", ((ReceiptDetail)Container.DataItem).UnitPrice) %></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="金額">
            <ItemTemplate>
                <%# String.Format("{0:#,0}", ((ReceiptDetail)Container.DataItem).Amount) %></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="備註">
            <ItemTemplate>
                <pre><%# ((ReceiptDetail)Container.DataItem).Remark%></pre></ItemTemplate>
            <ItemStyle HorizontalAlign="Left" />
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
<cc1:ReceiptDetailDataSource ID="dsEntity" runat="server">
</cc1:ReceiptDetailDataSource>

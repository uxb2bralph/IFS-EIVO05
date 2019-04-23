<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SellerSelector.ascx.cs" Inherits="eIVOGo.Module.UI.SellerSelector" %>
<%@ Register assembly="Model" namespace="Model.DataEntity" tagprefix="cc1" %>

<asp:DropDownList ID="_Selector" runat="server">
</asp:DropDownList>
<cc1:InvoiceDataSource ID="dsInv" runat="server">
</cc1:InvoiceDataSource>


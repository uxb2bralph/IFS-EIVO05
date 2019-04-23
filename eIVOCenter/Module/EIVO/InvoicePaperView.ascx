﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="eIVOGo.Module.EIVO.InvoicePaperView" %>
<%@ Import Namespace="Model.DataEntity" %>
<%@ Register Src="Item/InvoiceReceiptView.ascx" TagName="InvoiceReceiptView"
    TagPrefix="uc2" %>
<%@ Register Src="Item/InvoiceBalanceView.ascx" TagName="InvoiceBalanceView"
    TagPrefix="uc3" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<div class="Khaki">
    <uc2:InvoiceReceiptView ID="receiptView" runat="server" Item="<%# _item %>" />
</div>
<p style="border-bottom: 1px dotted #000; margin-top: 13px; margin-bottom: 10px;">
</p>
<div class="or">
    <uc3:InvoiceBalanceView ID="balanceView" runat="server" Item="<%# _item %>" />
</div>
<cc1:InvoiceDataSource ID="dsInv" runat="server" Isolated="true">
</cc1:InvoiceDataSource>

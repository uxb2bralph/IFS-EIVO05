﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="eIVOGo.Module.EIVO.InvoicePrintView" %>
<%@ Register Src="Item/DefaultInvoiceReceiptView.ascx" TagName="DefaultInvoiceReceiptView"
    TagPrefix="uc2" %>
<%@ Register Src="Item/DefaultInvoiceBalanceView.ascx" TagName="DefaultInvoiceBalanceView"
    TagPrefix="uc3" %>
<%@ Import Namespace="Model.DataEntity" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<div class="gr" style="page-break-after: always;">
    <uc2:DefaultInvoiceReceiptView ID="receiptView" runat="server" Item="<%# _item %>" />
</div>
<div class="ori">
    <uc3:DefaultInvoiceBalanceView ID="balanceView" runat="server" Item="<%# _item %>" />
</div>
<div id="newPage" runat="server" enableviewstate="false" visible="<%# IsFinal!=true %>" style="page-break-after:always;"></div>
<cc1:InvoiceDataSource ID="dsInv" runat="server">
</cc1:InvoiceDataSource>

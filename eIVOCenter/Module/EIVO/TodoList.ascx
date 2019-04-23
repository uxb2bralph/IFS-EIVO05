﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TodoList.ascx.cs" Inherits="eIVOCenter.Module.EIVO.TodoList" %>
<%@ Register Src="../UI/PageAction.ascx" TagName="PageAction" TagPrefix="uc1" %>
<%@ Register Src="../UI/FunctionTitleBar.ascx" TagName="FunctionTitleBar" TagPrefix="uc2" %>
<%@ Register Src="TodoCheck/CheckBuyerInvoice.ascx" TagName="CheckBuyerInvoice" TagPrefix="uc3" %>
<%@ Register Src="TodoCheck/CheckBuyerInvoiceCancellation.ascx" TagName="CheckBuyerInvoiceCancellation"
    TagPrefix="uc4" %>
<%@ Register Src="TodoCheck/CheckBuyerAllowance.ascx" TagName="CheckBuyerAllowance"
    TagPrefix="uc5" %>
<%@ Register Src="TodoCheck/CheckBuyerAllowanceCancellation.ascx" TagName="CheckBuyerAllowanceCancellation"
    TagPrefix="uc6" %>
<%@ Register Src="TodoCheck/CheckSellerInvoice.ascx" TagName="CheckSellerInvoice"
    TagPrefix="uc7" %>
<%@ Register Src="TodoCheck/CheckSellerInvoiceCancellation.ascx" TagName="CheckSellerInvoiceCancellation"
    TagPrefix="uc8" %>
<%@ Register Src="TodoCheck/CheckSellerAllowance.ascx" TagName="CheckSellerAllowance"
    TagPrefix="uc9" %>
<%@ Register Src="TodoCheck/CheckSellerAllowanceCancellation.ascx" TagName="CheckSellerAllowanceCancellation"
    TagPrefix="uc10" %>
<%@ Register Src="TodoCheck/CheckBuyerReceipt.ascx" TagName="CheckBuyerReceipt"
    TagPrefix="uc12" %>
<%@ Register Src="TodoCheck/CheckBuyerReceiptCancellation.ascx" TagName="CheckBuyerReceiptCancellation"
    TagPrefix="uc13" %>
<%@ Register src="TodoCheck/CheckInvoiceSignature.ascx" tagname="CheckInvoiceSignature" tagprefix="uc11" %>
<%@ Register src="TodoCheck/CheckSellerReceipt.ascx" tagname="CheckSellerReceipt" tagprefix="uc14" %>
<%@ Register src="TodoCheck/CheckSellerReceiptCancellation.ascx" tagname="CheckSellerReceiptCancellation" tagprefix="uc15" %>
<uc1:PageAction ID="actionItem" runat="server" ItemName="首頁 &gt; 待辦事項" />
<uc2:FunctionTitleBar ID="titleBar" runat="server" ItemName="待辦事項" />
<div class="border_gray">
    <!--表格 開始-->
    <table id="todolist" border="0" cellspacing="0" cellpadding="0" width="100%">
        <tbody>
            <uc3:CheckBuyerInvoice ID="buyerInvoice" runat="server" />
            <uc4:CheckBuyerInvoiceCancellation ID="buyerInvoiceCancellation" runat="server" />
            <uc5:CheckBuyerAllowance ID="buyerAllowance" runat="server" />
            <uc6:CheckBuyerAllowanceCancellation ID="buyerAllowanceCancellation" runat="server" />
            <uc7:CheckSellerInvoice ID="sellerInvoice" runat="server" />
            <uc8:CheckSellerInvoiceCancellation ID="sellerInvoiceCancellation" runat="server" />
            <uc9:CheckSellerAllowance ID="sellerAllowance" runat="server" />
            <uc10:CheckSellerAllowanceCancellation ID="sellerAllowanceCancellation" runat="server" />
            <uc14:CheckSellerReceipt ID="sellerReceipt" runat="server" />
            <uc15:CheckSellerReceiptCancellation ID="sellerReceiptCancellation" 
                runat="server" />
            <uc12:CheckBuyerReceipt ID="CheckBuyerReceipt" runat="server" />
            <uc13:CheckBuyerReceiptCancellation ID="CheckBuyerReceiptCancellation" runat="server" />
            <tr id="trNone" runat="server" visible="false" enableviewstate="false">
                <td>
                    <img runat="server" id="img1" enableviewstate="false" border="0" align="middle" src="~/images/arrow_02.gif"
                        width="15" height="15" />尚無待辦事項
                </td>
            </tr>
        </tbody>
    </table>
    <!--表格 結束-->
</div>
<script runat="server">
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        trNone.Visible = !buyerInvoice.Visible && !buyerInvoiceCancellation.Visible && !buyerAllowance.Visible && !buyerAllowanceCancellation.Visible
            && !sellerInvoice.Visible && !sellerInvoiceCancellation.Visible && !sellerAllowance.Visible && !sellerAllowanceCancellation.Visible
            && !CheckBuyerReceipt.Visible && !CheckBuyerReceiptCancellation.Visible
            && !sellerReceipt.Visible && !sellerReceiptCancellation.Visible;
    }
</script>
<script type="text/javascript">
    $(document).ready(function () {
        $("table#todolist tr:odd").attr("class", "OldLace");
    }
    );
</script>

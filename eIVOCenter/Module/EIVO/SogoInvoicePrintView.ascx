﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="eIVOGo.Module.EIVO.InvoicePrintView" %>
<%@ Register Src="Item/SogoInvoiceStubView.ascx" TagName="SogoInvoiceStubView"
    TagPrefix="uc1" %>
<%@ Register Src="Item/SogoInvoiceReceiptView.ascx" TagName="SogoInvoiceReceiptView"
    TagPrefix="uc2" %>
<%@ Register Src="Item/SogoInvoiceBalanceView.ascx" TagName="SogoInvoiceBalanceView"
    TagPrefix="uc3" %>
<%@ Import Namespace="Model.DataEntity" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<style type="text/css">
body{
	margin:0px;
	padding:0px;
	font-family: "Verdana", "Arial", "Helvetica", "sans-serif", "細明體", "新細明體"; 
	font-size: 7pt;
}
.tablenone
 {
	margin:0px auto;
	border-top-width:0px;
	border-left-width:0px;
	border-collapse:collapse;
}
.tablenone td{
	margin:0px;
	padding:5px;
	border-bottom-width:0px;
	border-right-width:0px;
	font-size: 9pt;
	background-color: #FFFFFF;
}
.tablenone th{
	margin:0px;
	padding:5px;
	border-bottom-width:0px;
	border-right-width:0px;
	font-size: 9pt;
	background-color: #FFFFFF;
}

/*橘色*/
.or
{
	/*color: #F26521;*/
	color: #c76c42;
    margin-left:12px
}
.table-or {
	margin:0px;
	border-top:1px solid #c76c42;
	border-left:1px solid #c76c42;
	border-collapse:collapse;
	background-color: #FFFFFF;
}
.table-or td{
	margin:0px;
	padding:0px;
	border-bottom:1px solid #c76c42;
	border-right:1px solid #c76c42;
	font-size: 7pt;
	background-color: #FFFFFF;
}
.table-or th{
	margin:0px;
	padding:0px;
	border-bottom:1px solid #c76c42;
	border-right:1px solid #c76c42;
	font-size: 7pt;
	background-color: #FFFFFF;
}
.table_or_noborder {
	margin:0px;
	border-top:1px solid #c76c42;
	border-collapse:collapse;
	position:absolute;
	bottom:0px;
	left:0px;
	z-index:100;
}
.table_or_noborder td{
	margin:0px;
	padding:0px 2px !important;
	border-bottom:1px solid #c76c42;
	border-right:1px solid #c76c42;
	font-size: 7pt;
	background-color: #FFFFFF;
}
.table_or_noborder th{
	margin:0px;
	padding:0px 2px !important;
	border-bottom:1px solid #c76c42;
	border-right:1px solid #c76c42;
	font-size: 7pt;
	background-color: #FFFFFF;
}
.orcube
{
	border:2px solid #c76c42;
	z-index:100;
}
.orcube_notop
{
	border:2px solid #c76c42;
	border-top-width:0px;
}
.or_boder_top
{
	border-top:1px solid #c76c42;
}
.or_noboder_right
{
	border-right-width:0px !important;
}
.or_nobgcolor
{
	background-color: transparent !important;
}
.item_or {
	margin:0px;
	border-width:0px;
	border-collapse:collapse;
}
.item_or th{
	margin:0px;
	padding:0px;
	border-bottom:1px solid #c76c42;
	border-right:1px solid #c76c42;
	font-size: 7pt;
	background-color: #FFFFFF;
}
.item_or td{
	margin:0px;
	padding:0px 4px 0px 4px;
	border-right:1px solid #c76c42;
	border-bottom-width:0px;
	font-size: 7pt;
	background-color: #FFFFFF;
	color:#000;
}
.item_or_noright {
	margin:0px;
	border-width:0px;
	border-collapse:collapse;
}
.item_or_noright th{
	margin:0px;
	padding:0px;
	border-bottom:1px solid #c76c42;
	font-size: 7pt;
}
.item_or_noright td{
	margin:0px;
	padding:0px 4px 0px 4px;
	border-bottom-width:0px;
	font-size: 7pt;
	color:#000;
}

.or p
{
	font-size: 7pt;
}

/*黃色*/
.Khaki
{
	color: #b79247;
    margin-left:12px
}
.table-Khaki {
	margin:0px;
	border-top:1px solid  #b79247;
	border-left:1px solid  #b79247;
	border-collapse:collapse;
}
.table-Khaki td{
	margin:0px;
	padding:0px;
	border-bottom:1px solid  #b79247;
	border-right:1px solid  #b79247;
	font-size: 7pt;
	background-color: #FFFFFF;
}
.table-Khaki th{
	margin:0px;
	padding:0px;
	border-bottom:1px solid  #b79247;
	border-right:1px solid  #b79247;
	font-size: 7pt;
	background-color: #FFFFFF;
}
.table_Khaki_noborder {
	margin:0px;
	border-top:1px solid  #b79247;
	border-collapse:collapse;
	position:absolute;
	bottom:0px;
	left:0px;
	z-index:100;
}
.table_Khaki_noborder td{
	margin:0px;
	padding:0px 2px !important;
	border-bottom:1px solid  #b79247;
	border-right:1px solid  #b79247;
	font-size: 7pt;
	background-color: #FFFFFF;
}
.table_Khaki_noborder th{
	margin:0px;
	padding:0px 2px !important;
	border-bottom:1px solid  #b79247;
	border-right:1px solid  #b79247;
	font-size: 7pt;
	background-color: #FFFFFF;
}
.Khakicube
{
	border:2px solid #b79247;
}
.Khakicubecube_notop
{
	border:2px solid #b79247;
	border-top-width:0px;
}
.Khaki_boder_top
{
	border-top:1px solid #b79247;
}
.Khaki_noboder_right
{
	border-right-width:0px !important;
}
.Khaki_nobgcolor
{
	background-color: transparent !important;
}

.item_Khaki {
	margin:0px;
	border-width:0px;
	border-collapse:collapse;
}
.item_Khaki th{
	margin:0px;
	padding:0px;
	border-bottom:1px solid  #b79247;
	border-right:1px solid  #b79247;
	font-size: 7pt;
	background-color: #FFFFFF;
}
.item_Khaki td{
	margin:0px;
	padding:0px 4px 0px 4px;
	border-right:1px solid  #b79247;
	border-bottom-width:0px;
	font-size: 7pt;
	background-color: #FFFFFF;
	color:#000;
}
.item_Khaki_noright {
	margin:0px;
	border-width:0px;
	border-collapse:collapse;
}
.item_Khaki_noright th{
	margin:0px;
	padding:0px;
	border-bottom:1px solid  #b79247;
	font-size: 7pt;
}
.item_Khaki_noright td{
	margin:0px;
	padding:0px 4px 0px 4px;
	border-bottom-width:0px;
	font-size: 7pt;
	color:#000;
}

/*黑色*/
.bk
{
	color: #000000;
    margin-left:12px;
}
.table-bk {
	margin:0px;
	border-top:1px solid  #000000;
	border-left:1px solid  #000000;
	border-collapse:collapse;
}
.table-bk td{
	margin:0px;
	padding:0px;
	border-bottom:1px solid  #000000;
	border-right:1px solid  #000000;
	font-size: 7pt;
	background-color: #FFFFFF;
}
.table-bk th{
	margin:0px;
	padding:0px;
	border-bottom:1px solid  #000000;
	border-right:1px solid  #000000;
	font-size: 7pt;
	background-color: #FFFFFF;
}
.table_bk_noborder {
	margin:0px;
	border-top:1px solid  #000000;
	border-collapse:collapse;
	position:absolute;
	bottom:0px;
	left:0px;
	z-index:100;
}
.table_bk_noborder td{
	margin:0px;
	padding:0px 2px !important;
	border-bottom:1px solid  #000000;
	border-right:1px solid  #000000;
	font-size: 7pt;
	background-color: #FFFFFF;
}
.table_bk_noborder th{
	margin:0px;
	padding:0px 2px !important;
	border-bottom:1px solid  #000000;
	border-right:1px solid  #000000;
	font-size: 7pt;
	background-color: #FFFFFF;
}
.bkcube
{
	border:2px solid #000000;
}
.bkcubecube_notop
{
	border:2px solid #000000;
	border-top-width:0px;
}
.bk_boder_top
{
	border-top:1px solid #000000;
}
.bk_noboder_right
{
	border-right-width:0px !important;
}
.bk_nobgcolor
{
	background-color: transparent !important;
}

.item_bk {
	margin:0px;
	border-width:0px;
	border-collapse:collapse;
}
.item_bk th{
	margin:0px;
	padding:0px;
	border-bottom:1px solid  #000000;
	border-right:1px solid  #000000;
	font-size: 7pt;
	background-color: #FFFFFF;
}
.item_bk td{
	margin:0px;
	padding:0px 4px 0px 4px;
	border-right:1px solid  #000000;
	border-bottom-width:0px;
	font-size: 7pt;
	background-color: #FFFFFF;
	color:#000;
}
.item_bk_noright {
	margin:0px;
	border-width:0px;
	border-collapse:collapse;
}
.item_bk_noright th{
	margin:0px;
	padding:0px;
	border-bottom:1px solid  #000000;
	font-size: 7pt;
}
.item_bk_noright td{
	margin:0px;
	padding:0px 4px 0px 4px;
	border-bottom-width:0px;
	font-size: 7pt;
	color:#000;
}
/***共用***/
.title{
	font-size: 11pt;
	font-weight: bold;
	line-height:100%;
}
.title1{
	font-size: 13pt !important;
	font-weight: bold;
	line-height:130%;
}

.title2{
	font-size: 11pt;
	line-height:130%;
}
.title3{
	font-size: 10pt;
	line-height:130%;
}
.title4{
	font-size: 9pt;
	line-height:100%;
}
ol
{
	margin:0px;
	margin-left:20px;
	padding:0px;
}
.noright-border
{
	border-right-width:0px !important;
}
.f_black
{
	color:#000;
}

.company
{
	margin:15px 0px 0px 20px;
	padding:0px;
	color:#000;
}
.company .title
{
	margin:0px;
	padding:0px;
	font-size:15pt;
	font-family:標楷體;
	font-weight:normal;
}
.company p
{
	margin:0px;
	padding:0px;
	font-size:9pt;
	font-family:標楷體;
}

.customer
{
	margin:80px 0px 0px 180px;
	padding:0px;
	color:#000;
}
.customer .title
{
	margin:0px;
	padding:0px;
	font-size:15pt;
	font-family:標楷體;
}
.customer p
{
	margin:0px;
	padding:0px;
	font-size:15pt;
	font-family:標楷體;
}
.bspace h3
{
	 text-align:center;
	 font-size:15px;
	 margin:15px 10px 5px;
}
.bspace p.note
{
	 font-size:12px;
	 margin:10px 20px;
	 line-height:150%;
}
.bspace ul
{
	 font-size:10px;
	 margin:10px 40px;
	 padding:0px;
}
.bspace ul li
{
	 margin:10px 0px;
	 adding:0px;
	 line-height:150%;
}
.eivo_stamp
{
	color: #333379;
	border:1px solid #333379;
	text-align:center;
	padding:5px;
	font-size:9px;
}
.eivo_stamp .notitle
{
	font-size:16px;
	font-weight:bolder;
	line-height:100%;
	margin:0px;
	padding:0px;
	padding-top:4px;
	padding-bottom:4px;
}
</style>
<div id="divFrist" runat="server" class="bk" style="margin-top: 5px;" visible="<%#showFrist%>" enableviewstate="false" >
    <uc1:SogoInvoiceStubView ID="SogoInvoiceStubView" runat="server" Item="<%# _item %>" />
</div>
<div runat="server" enableviewstate="false" visible="<%#showFrist&(showThird&showFourth)==true %>"
    style="page-break-after: always;">
</div>
<div  id="divThird" runat="server" class="or" visible="<%#showThird%>" style="margin-top: 5px;page-break-after: always;" enableviewstate="false" >
    <uc2:SogoInvoiceReceiptView ID="receiptView" runat="server" Item="<%# _item %>" />
</div>
<%--<p style="border-bottom: 1px dotted #000; margin-top: 13px; margin-bottom: 10px;">
</p>--%>
<div  id="divFourth" runat="server"  class="Khaki" style="margin-top: 15px;" visible="<%#showFourth%>" enableviewstate="false" >
    <uc3:SogoInvoiceBalanceView ID="balanceView" runat="server" Item="<%# _item %>" />
</div>
<div id="newPage" runat="server" enableviewstate="false" visible="<%#IsFinal!=true%>"
    style="page-break-after: always;">&nbsp;
</div>
<cc1:InvoiceDataSource ID="dsInv" runat="server">
</cc1:InvoiceDataSource>
<script runat="server">
    bool isPrintSingle;
    bool showThird = true, showFourth = true,showFrist = true;
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!String.IsNullOrEmpty(Request["printAll"]))
        {
            if (!Request["printAll"].ToString().Equals("1"))
            {
                showFrist = false;
            }
            else
            {
                showThird = false;
                showFourth = false;
            }

        }
        else
        {
            showFrist = false;
        }
        
        //if (!String.IsNullOrEmpty(Request["isPrintSingle"]))
        //{
        //    bool.TryParse(Request["isPrintSingle"], out isPrintSingle);
        //}
    }
</script>

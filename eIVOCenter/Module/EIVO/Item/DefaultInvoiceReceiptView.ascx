﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="eIVOGo.Module.EIVO.Item.InvoiceReceiptView" %>
<%@ Register Src="DefaultInvoiceProductPrintView.ascx" TagName="DefaultInvoiceProductPrintView"
    TagPrefix="uc1" %>
<%@ Import Namespace="Model.DataEntity" %>
<table width="100%" border="0" cellspacing="0" cellpadding="4" class="tablenone">
    <tr>
        <td colspan="4" align="center">
            <span style="font-size: 18pt">
                <%# _item.InvoiceSeller.CustomerName  %></span>
        </td>
    </tr>
    <tr>
        <td width="40%" rowspan="2">
            統一發票號碼：<span class="f_black"><%# _item.TrackCode%><%# _item.No %></span>
        </td>
        <td class="title1">
            電子發票
        </td>
        <td>
            <%# _item.CDS_Document.DocumentPrintLogs.Any(l=>l.TypeID==(int)Model.Locale.Naming.DocumentTypeDefinition.E_Invoice)?"副　本":"正　本" %>
<%--            <%# _item.CDS_Document.DocumentPrintLogs.Any(l=>l.TypeID==(int)Model.Locale.Naming.DocumentTypeDefinition.E_Invoice)?"<span style=\"text-decoration:line-through;\">正　本</span>":"正　本" %>
--%>        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            (收執聯)
        </td>
        <td nowrap>
            <%--<%# _item.CDS_Document.DocumentPrintLogs.Any(l=>l.TypeID==(int)Model.Locale.Naming.DocumentTypeDefinition.E_Invoice)?"複印本":"<span style=\"text-decoration:line-through;\">複印本</span>" %>--%>
        </td>
        <td>
            中華民國 <span class="f_black">
                <%# _item.InvoiceDate.Value.Year-1911 %></span> 年 <span class="f_black">
                    <%# String.Format("{0:00}",_item.InvoiceDate.Value.Month) %></span> 月 <span class="f_black">
                        <%# String.Format("{0:00}",_item.InvoiceDate.Value.Day)%></span> 日
        </td>
    </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td width="180" align="center">
            <table width="160" border="0" cellpadding="2" cellspacing="0" class="table-gr">
                <tr>
                    <td width="15" rowspan="3" align="center">
                        買受人註記欄
                    </td>
                    <td width="30" align="center">
                        區分
                    </td>
                    <td width="47" align="center">
                        進貨及<br />
                        費用
                    </td>
                    <td width="47" align="center">
                        固定<br />
                        資產
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        得扣<br />
                        抵
                    </td>
                    <td align="center" class="contant">
                        <%# _item.BuyerRemark=="1"?"V":"&nbsp;" %>
                    </td>
                    <td align="center" class="contant">
                        <%# _item.BuyerRemark=="2"?"V":"&nbsp;" %>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        不得<br />
                        扣抵
                    </td>
                    <td align="center" class="contant">
                        <%# _item.BuyerRemark=="3"?"V":"&nbsp;" %>
                    </td>
                    <td align="center" class="contant">
                        <%# _item.BuyerRemark=="4"?"V":"&nbsp;" %>
                    </td>
                </tr>
            </table>
        </td>
        <td>
            <table width="100%" border="0" cellpadding="6" cellspacing="0" class="grcube">
                <tr>
                    <td width="75" align="right">
                        郵遞區號：
                    </td>
                    <td class="f_black">
                        <%# _buyerOrg != null ? null : _buyer != null ? _buyer.IsB2C() ? null : _buyer.PostCode : null%>
                    </td>
                </tr>
                <tr>
                    <td width="75" align="right">
                        地 址：
                    </td>
                    <td class="f_black">
                        <%# _buyerOrg != null ? _buyerOrg.Addr : _buyer != null ? _buyer.IsB2C() ? null : _buyer.Address : null%>
                    </td>
                </tr>
                <tr>
                    <td width="75" align="right">
                        買受人名稱&nbsp;&nbsp;&nbsp;
                    </td>
                    <td class="f_black">
                        <%# _buyer.IsB2C() ? null : _buyer.CustomerName%>
                    </td>
                </tr>
                <tr>
                    <td width="75" align="right">
                        及統一編號：
                    </td>
                    <td class="f_black">
                        <%# _buyer.IsB2C()?null:_buyer.ReceiptNo %>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table width="100%" border="0" cellpadding="5" cellspacing="0" class="item_gr">
    <tr>
        <th width="45" rowspan="2" align="center">
            項目
        </th>
        <th width="172" rowspan="2" align="center">
            摘要
        </th>
        <th colspan="2" align="center">
            數量
        </th>
        <th colspan="2" align="center">
            單價
        </th>
        <th colspan="2" align="center">
            總價
        </th>
    </tr>
    <tr>
        <th align="center">
            件 數
        </th>
        <th align="center">
            重 量
        </th>
        <th align="center">
            代收付<br />
            運 費
        </th>
        <th align="center">
            貨 款
        </th>
        <th align="center">
            代收付<br />
            運費
        </th>
        <th align="center">
            貨 款
        </th>
    </tr>
    <asp:Repeater ID="rpList" runat="server" EnableViewState="false">
        <ItemTemplate>
            <uc1:DefaultInvoiceProductPrintView ID="productView" runat="server" />
        </ItemTemplate>
    </asp:Repeater>
    <tr valign="top">
        <td align="center">
            &nbsp;
        </td>
        <td valign="top">
            &nbsp;
        </td>
        <td align="right" valign="top">
            &nbsp;
        </td>
        <td align="right" valign="top">
            &nbsp;
        </td>
        <td align="right" valign="top">
            &nbsp;
        </td>
        <td align="right" valign="top">
            &nbsp;
        </td>
        <td align="right" valign="top">
            &nbsp;
        </td>
        <td align="right" valign="top">
            &nbsp;
        </td>
    </tr>
</table>
<table width="100%" border="0" cellpadding="2" cellspacing="0" class="table-gr">
    <tr>
        <td colspan="7" align="center">
            代 收 付 運 費 及 貨 款 總 額
        </td>
        <td width="15%" align="right" class="f_black">
            <strong>
                <%# String.Format("{0:##,###,###,###.00}",_item.InvoiceAmountType.SalesAmount) %></strong>
        </td>
    </tr>
    <tr>
        <td width="21%" align="center">
            營業稅
        </td>
        <td width="15%" align="center">
            應稅
        </td>
        <td width="6%" align="center" class="f_black">
            <%# _item.InvoiceAmountType.TaxType == (byte)1 ? "V" : "&nbsp;"%>
        </td>
        <td width="16%" align="center">
            零稅率
        </td>
        <td width="6%" align="center" class="f_black">
            <%# _item.InvoiceAmountType.TaxType == (byte)2 ? "V" : "&nbsp;"%>
        </td>
        <td width="16%" align="center">
            免稅
        </td>
        <td width="5%" align="center" class="f_black">
            <%# _item.InvoiceAmountType.TaxType == (byte)3 ? "V" : "&nbsp;"%>
        </td>
        <td width="15%" align="right" class="f_black">
            <strong>
                <%# String.Format("{0:##,###,###,###.00}",_item.InvoiceAmountType.TaxAmount) %></strong>
        </td>
    </tr>
    <tr>
        <td align="center">
            總計新台幣
        </td>
        <td colspan="6">
            <span class="f_black">
                <%# _totalAmtChar[7] %></span> 仟 <span class="f_black">
                    <%# _totalAmtChar[6] %></span> 佰 <span class="f_black">
                        <%# _totalAmtChar[5] %></span> 拾 <span class="f_black">
                            <%# _totalAmtChar[4] %></span> 萬 <span class="f_black">
                                <%# _totalAmtChar[3] %></span> 仟 <span class="f_black">
                                    <%# _totalAmtChar[2] %></span> 佰 <span class="f_black">
                                        <%# _totalAmtChar[1] %></span> 拾 <span class="f_black">
                                            <%# _totalAmtChar[0] %></span> 元整
        </td>
        <td width="15%" align="right" class="f_black">
            <strong>
                <%# String.Format("{0:##,###,###,###.00}",_item.InvoiceAmountType.TotalAmount) %></strong>
        </td>
    </tr>
</table>
<table width="100%" border="0" cellpadding="4" cellspacing="0" class="table-gr">
    <tr>
        <td height="60" class="title">
            備註：<%# _item.Remark %><br />
            <span class="f_black" style="border-right-width: 0px;">
                <%# _buyer.IsB2C() ? String.Format("個人識別碼:{0}", _buyer.Name) : null%></span>
        </td>
        <td rowspan="2" align="center" width="155">
            <div class="eivo_stamp">
                <%# _item.InvoiceSeller.CustomerName %><br />
                統一編號<br />
                <div class="notitle">
                    <%# _item.InvoiceSeller.ReceiptNo %></div>
                電話:<%# _item.InvoiceSeller.Phone%><br />
                <%# _item.InvoiceSeller.Address %><br />
            </div>
        </td>
    </tr>
    <tr>
        <td height="60" class="title2">
            1.本銷貨發票奉台北市國稅局中正分局99年12月28日財北國稅中正營業字第0990031712號函核准使用。<br />
            2.無統一編號者視同二聯式發票，第二聯扣抵聯作廢，不得憑以扣抵稅款。<br />
            3.本銷貨發票凡經塗改即屬無效。
        </td>
    </tr>
</table>

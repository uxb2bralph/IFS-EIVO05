﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SOGOReceiptView.ascx.cs"
    Inherits="eIVOCenter.Module.EIVO.Item.SOGOReceiptView" %>
<%@ Register Src="ReceiptProductPrintView.ascx" TagName="ReceiptProductPrintView"
    TagPrefix="uc1" %>
<%@ Import Namespace="Model.DataEntity" %>
<%@ Register assembly="Model" namespace="Model.DataEntity" tagprefix="cc1" %>
<div class="red" style="margin-top: 20px;">
    <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="10">
                &nbsp;
            </td>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="center">
                            <table border="0" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td align="center" nowrap="nowrap" class="title">
                                        太平洋崇光百貨股份有限公司
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" nowrap="nowrap" class="title">
                                        職工福利委員會收據 <%--(
                                                <%# IsUnPrint ? "正本" : "副本"%>
                                                )--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="title4">
                                        中華民國&nbsp;<span class="f_black"><%# _item.ReceiptDate.Year-1911 %></span>&nbsp;年&nbsp;
                                        <span class="f_black">
                                            <%# String.Format("{0:00}", _item.ReceiptDate.Month)%></span>&nbsp;月&nbsp; <span
                                                class="f_black">
                                                <%# String.Format("{0:00}", _item.ReceiptDate.Day)%></span>&nbsp;日
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="63%">
                            <table width="100%" border="0" cellpadding="1" cellspacing="0" class="title4">
                                <tr>
                                    <td width="8%" align="right" nowrap="nowrap">
                                        收據編號：
                                    </td>
                                    <td class="f_black">
                                        <%# _item.No %>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="8%" align="right" nowrap="nowrap">
                                        買 受 人：
                                    </td>
                                    <td class="f_black">
                                        <%# _item.Buyer.CompanyName %>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="8%" align="right" nowrap="nowrap">
                                        統一編號：
                                    </td>
                                    <td class="f_black">
                                        <%# _item.Buyer.ReceiptNo %>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="8%" align="right" nowrap="nowrap">
                                        地&nbsp;&nbsp;&nbsp;&nbsp;址：
                                    </td>
                                    <td class="f_black">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="15" align="center" valign="top">
                第<br />
                一<br />
                聯<br />
                ：<br />
                繳<br />
                款<br />
                聯
            </td>
            <td valign="top" style="position:relative;overflow:hidden;">
                <div style="position:absolute;top:0px;left:38%;z-index:-1;border-right:1px solid #C54C67;width:1px;height:100%;">&nbsp;</div>
                <div style="position:absolute;top:0px;left:48%;z-index:-1;border-right:1px solid #C54C67;width:1px;height:100%;">&nbsp;</div>
                <div style="position:absolute;top:0px;left:64%;z-index:-1;border-right:1px solid #C54C67;width:1px;height:100%;">&nbsp;</div>     
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="redcube">
                    <tr>
                        <td width="80%" valign="top" style="position:relative;border-right:1px solid #C54C67;">               
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="item_red_noright" style="margin-bottom:41px;">
                                <tr>
                                    <th height="20" width="50%">
                                        項目 / 商品名稱
                                    </th>
                                    <th width="10%" height="20">
                                        數 量
                                    </th>
                                    <th height="20" width="20%">
                                        單 價
                                    </th>
                                    <th height="20">
                                        金 額
                                    </th>
                                </tr>
                                <tr>
                                    <asp:Repeater ID="rpList" runat="server" EnableViewState="false">
                                        <ItemTemplate>
                                            <tr>  
                                                <td height="20" valign="top" width="50%">                                                    
                                                    <%#((ReceiptDetail)Container.DataItem).Description %>                                                    
                                                </td>
                                                <td width="10%" height="20" align="right" valign="top">                                                    
                                                    <%# String.Format("{0:0}",((ReceiptDetail)Container.DataItem).Quantity) %>
                                                </td>
                                                <td height="20" align="right" valign="top" width="20%">                                                    
                                                    <%# String.Format("{0:#,0}",((ReceiptDetail)Container.DataItem).UnitPrice) %>
                                                </td>
                                                <td height="20" align="right" valign="top">
                                                    <%# String.Format("{0:#,0}",((ReceiptDetail)Container.DataItem).Amount) %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_red_noright" style="position:absolute;bottom:0;">
                                <tr>
                                    <td height="20" align="center" width="78%" style="background-color:#FFFFFF;">
                                        總&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;計
                                    </td>
                                    <td height="20" align="right" class="f_black">
                                        <%# String.Format("{0:##,###,###,###}",_item.TotalAmount) %>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" colspan="2" style="border-bottom-width: 0px;background-color:#FFFFFF;">
                                        總計新台幣： <span class="f_black">
                                            <%# _totalAmtChar[7] %></span> 仟 <span class="f_black">
                                                <%# _totalAmtChar[6] %></span> 佰 <span class="f_black">
                                                    <%# _totalAmtChar[5] %></span> 拾 <span class="f_black">
                                                        <%# _totalAmtChar[4] %></span> 萬 <span class="f_black">
                                                            <%# _totalAmtChar[3] %></span> 仟 <span class="f_black">
                                                                <%# _totalAmtChar[2] %></span> 佰 <span class="f_black">
                                                                    <%# _totalAmtChar[1] %></span> 拾 <span class="f_black">
                                                                        <%# _totalAmtChar[0] %></span> 元整
                                    </td>
                                </tr>
                            </table>                            
                        </td>
                        <td height="265" valign="top" style="position:relative;">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table-red" style="border-width: 0px;">
                                <tr>
                                    <th height="20" style="border-right-width: 0px;">
                                        備 註
                                    </th>
                                </tr>
                                <tr>
                                    <td valign="top" class="f_black" style="border-width: 0px;">
                                        <pre><%# String.Format("{0}", (_item.ReceiptDetail.Count > 0 ? _item.ReceiptDetail[0].Remark : null))%></pre>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table-red" style="border-left-width: 0px;position:absolute;bottom:0px;left:0px;">
                                <tr>
                                    <td height="105" align="center" style="border-bottom-width: 0px; border-right-width: 0px;">
                                        <img id="sealImg" runat="server" src="~/Seal/SOGO.jpg" width="130" height="91" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<div id="newPage" runat="server" enableviewstate="false" visible="<%# IsFinal!=true %>"
    style="page-break-after: always;">
</div>
<cc1:ReceiptDataSource ID="dsInv" runat="server">
</cc1:ReceiptDataSource>

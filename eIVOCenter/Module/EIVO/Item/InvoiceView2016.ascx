<%@ Control Language="C#" AutoEventWireup="true" %>
<%@ Register Src="InvoiceProductPrintView80279131.ascx" TagName="InvoiceProductPrintView"
    TagPrefix="uc1" %>
<%@ Import Namespace="Model.DataEntity" %>
<%@ Import Namespace="Utility" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<% if (Item != null)
   { %>
<div class="Invo_page" style="page-break-after: always; width: 23cm;">
    <!-- 列印頁面 -->
    <div class="pageOne">
        <%--<div class="title">
            <img src="images/chc_logo.gif" width="403" height="51"></div>--%>
        <h2>電子發票證明聯</h2>
        <p class="text-center"><%= String.Format("{0:yyyy-MM-dd}",Item.InvoiceDate) %></p>
        <div class="invoice_data">
            <p>發票號碼：<%= Item.TrackCode %><%= Item.No %><span class="fright">格式：25</span></p>
            <p>買　　方：<%= Item.InvoiceBuyer.IsB2C() ? null : Item.InvoiceBuyer.CustomerName %></p>
            <p>統一編號：<%= Item.InvoiceBuyer.IsB2C() ? null : Item.InvoiceBuyer.ReceiptNo %></p>
            <p>地　　址：<%--<%= Item.InvoiceBuyer.IsB2C() ? null : Item.InvoiceBuyer.Address %>--%></p>
            <p class="text-right">第 1 頁/共 <%= _pageCount %> 頁</p>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tableMain" style="height: 26cm; width: 23cm;">
            <tbody>
                <tr>
                    <td valign="top" height="100%">
                        <table height="100%" border="0" cellpadding="0" cellspacing="0" class="item_list_left" width="100%">
                            <tr>
                                <th align="center">品名</th>
                                <th width="40" align="center">數量</th>
                                <th width="100" align="center">單價</th>
                                <th width="140" align="center">金額</th>
                                <th width="200" align="center">備註</th>
                            </tr>
                            <% for (_itemIdx = 0; _itemIdx < __PAGE_ITEM_COUNT && _itemIdx < _products.Length; _itemIdx++)
                               {
                                   var prodItem = _products[_itemIdx];
                                   var product = prodItem.InvoiceProduct; %>
                            <tr>
                                <td><%= prodItem.No + 1%>.<%= product.Brief%></td>
                                <td align="right"><%= String.Format("{0:##,###,###,###.}", prodItem.Piece)%></td>
                                <td align="right"><%= String.Format("{0:##,###,###,###.}", prodItem.UnitCost)%></td>
                                <td align="right"><%= String.Format("{0:##,###,###,###.}", prodItem.CostAmount)%></td>
                                <td>
                                    <% if(_itemIdx==0 && Item.Organization.ReceiptNo=="80279131") { %>
                                    <span style="font-weight: bolder; font-size:large;">更正發票請於收到發票後5日內辦理逾期恕不受理</span>
                                    <% } %>
                                    <%= prodItem.Remark%>
                                </td>
                            </tr>
                            <% } %>
                            <!-- 最後一列不能刪-撐高table用 -->
                            <tr>
                                <td height="100%">&nbsp;</td>
                                <td height="100%">&nbsp;</td>
                                <td height="100%">&nbsp;</td>
                                <td height="100%">&nbsp;</td>
                                <td height="100%">&nbsp;</td>
                            </tr>
                            <!-- 最後一列不能刪-撐高table用 -->
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="total" style="width: 23cm;">
            <tr>
                <td colspan="7">銷售額合計</td>
                <td width="130" align="right"><strong><%= String.Format("{0:##,###,###,###.}",Item.InvoiceAmountType.SalesAmount) %></strong></td>
                <td width="155" align="center">營業人蓋統一發票專用章</td>
            </tr>
            <tr>
                <td>營業稅</td>
                <td align="center">應稅</td>
                <td align="center"><%= Item.InvoiceAmountType.TaxType == (byte)1 ? "V" : "&nbsp;"%></td>
                <td align="center">零稅率</td>
                <td align="center"><%= Item.InvoiceAmountType.TaxType == (byte)2 ? "V" : "&nbsp;"%></td>
                <td align="center">免稅</td>
                <td align="center"><%= Item.InvoiceAmountType.TaxType == (byte)3 ? "V" : "&nbsp;"%></td>
                <td width="130" align="right"><strong><%= String.Format("{0:##,###,###,###.}",Item.InvoiceAmountType.TaxAmount) %></strong></td>
                <td width="155" rowspan="3" align="center">
                    <div class="eivo_stamp">
                        <%= Item.InvoiceSeller.CustomerName %><br/>
                        統一編號<br/>
                        <div class="notitle"><%= Item.InvoiceSeller.ReceiptNo %></div>
                        <%= Item.InvoiceSeller.Address %><br/>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="7">總計</td>
                <td width="130" align="right"><strong><%= String.Format("{0:##,###,###,###.}",Item.InvoiceAmountType.TotalAmount) %></strong></td>
            </tr>
            <tr>
                <td colspan="8">總計新台幣
                    <br/>
                    (中文大寫) <span class="totalno"><%= _totalAmtChar[7] %> 仟 
                        <%= _totalAmtChar[6] %> 佰 
                        <%= _totalAmtChar[5] %> 拾 
                        <%= _totalAmtChar[4] %> 萬 
                        <%= _totalAmtChar[3] %> 仟 
                        <%= _totalAmtChar[2] %> 佰 
                        <%= _totalAmtChar[1] %> 拾 
                        <%= _totalAmtChar[0] %> 元 整</span></td>
            </tr>
        </table>
    </div>
    <% 
                               int pageNum = 1;
                               while (_itemIdx < _products.Length)
                               {
                                   pageNum++;
    %>
    <div class="pageTwo" style="page-break-after: always">
<%--        <div class="title">
            <img src="images/chc_logo.gif" width="403" height="51">
        </div>--%>
        <h2 class="text-left">交易明細 (續)</h2>
        <p class="text-center"><%= String.Format("{0:yyyy-MM-dd}",Item.InvoiceDate) %></p>
        <div class="invoice_data">
            <p>發票號碼：<%= Item.TrackCode %><%= Item.No %></p>
            <p class="text-right">第 <%= pageNum %> 頁/共 <%= _pageCount %> 頁</p>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tableMain" style="height: 32cm; width: 23cm;">
            <tbody>
                <tr>
                    <td valign="top" height="100%">
                        <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" class="item_list_left">
                            <tr>
                                <th align="center">品名</th>
                                <th width="80" align="center">數量</th>
                                <th width="100" align="center">單價</th>
                                <th width="140" align="center">金額</th>
                                <th width="165" align="center">備註</th>
                            </tr>
                            <%
            for (int idx = 0; idx < __PAGE_ITEM_COUNT && _itemIdx < _products.Length; idx++, _itemIdx++)
            {
                var prodItem = _products[_itemIdx];
                var product = prodItem.InvoiceProduct; 
                            %>
                            <tr>
                                <td><%= prodItem.No + 1%>.<%= product.Brief%></td>
                                <td align="right"><%= String.Format("{0:##,###,###,###.}", prodItem.Piece)%></td>
                                <td align="right"><%= String.Format("{0:##,###,###,###.}", prodItem.UnitCost)%></td>
                                <td align="right"><%= String.Format("{0:##,###,###,###.}", prodItem.CostAmount)%></td>
                                <td><%= prodItem.Remark%></td>
                            </tr>
                            <% } %>
                            <!-- 最後一列不能刪-撐高table用 -->
                            <tr>
                                <td height="100%">&nbsp;</td>
                                <td height="100%">&nbsp;</td>
                                <td height="100%">&nbsp;</td>
                                <td height="100%">&nbsp;</td>
                                <td height="100%">&nbsp;</td>
                            </tr>
                            <!-- 最後一列不能刪-撐高table用 -->
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <%  } %>
    <!-- 列印頁面 -->
</div>
<% } %>
<cc1:InvoiceDataSource ID="dsEntity" runat="server">
</cc1:InvoiceDataSource>
<script runat="server">

    protected char[] _totalAmtChar;

    protected InvoiceItem _item;
    protected InvoiceProductItem[] _products;
    protected int _itemIdx;
    protected int _pageCount;

    protected const int __PAGE_ITEM_COUNT = 20;

    [System.ComponentModel.Bindable(true)]
    public InvoiceItem Item
    {
        get
        {
            return _item;
        }
        set
        {
            _item = value;
            if (_item != null)
            {
                var mgr = dsEntity.CreateDataManager();
                _products = mgr.GetTable<InvoiceDetail>().Where(d => d.InvoiceID == _item.InvoiceID)
                    .Join(mgr.GetTable<InvoiceProduct>(), d => d.ProductID, p => p.ProductID, (d, p) => p)
                    .Join(mgr.GetTable<InvoiceProductItem>(), d => d.ProductID, i => i.ProductID, (p, i) => i)
                    .OrderBy(i => i.ItemID).ToArray();
                _pageCount = (_products.Length + __PAGE_ITEM_COUNT - 1) / __PAGE_ITEM_COUNT;

                _totalAmtChar = ((int)_item.InvoiceAmountType.TotalAmount.Value).GetChineseNumberSeries(8);
            }

        }
    }

</script>

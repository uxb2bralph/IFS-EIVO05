<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DonatedInvoiceList.ascx.cs" Inherits="eIVOGo.Module.Inquiry.DonatedInvoiceList" %>
<%@ Import Namespace="eIVOGo.Module.Inquiry" %>    
<table border="0" cellspacing="0" cellpadding="0" width="100%" class="table01">
            <tr>
                <th nowrap>
                    社福機構統編
                </th>
                <th nowrap>
                    社福名稱
                </th>
                <th nowrap>
                    發票號碼
                </th>
            </tr>
            <asp:Repeater ID="rpList" runat="server" EnableViewState="false">
                <AlternatingItemTemplate>
                    <tr id="tr1" class="OldLace" runat="server" visible="<%# ((_QueryItem)Container.DataItem).InvoiceID.HasValue %>">
                        <td align="center">
                            <%#  ((_QueryItem)Container.DataItem).Agency!=null ? ((_QueryItem)Container.DataItem).Agency.ReceiptNo : ""%>
                        </td>
                        <td>
                            <%# ((_QueryItem)Container.DataItem).Agency != null ? ((_QueryItem)Container.DataItem).Agency.CompanyName : ""%>
                        </td>
                        <td align="center">
                            <%# ((_QueryItem)Container.DataItem).InvoiceNo %>
                        </td>
                    </tr>
                    <tr id="tr2" runat="server" visible="<%# !((_QueryItem)Container.DataItem).InvoiceID.HasValue %>">
                        <td class="total-count" colspan="3" align="right">
                            總計&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;發票張數：<%# String.Format("{0:##,###,###,###}",((_QueryItem)Container.DataItem).InvoiceCount) %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    </tr>
                </AlternatingItemTemplate>
                <ItemTemplate>
                    <tr id="tr1" runat="server" visible="<%# ((_QueryItem)Container.DataItem).InvoiceID.HasValue %>">
                        <td align="center">
                            <%# ((_QueryItem)Container.DataItem).Agency!=null ?((_QueryItem)Container.DataItem).Agency.ReceiptNo : "" %>
                        </td>
                        <td>
                            <%# ((_QueryItem)Container.DataItem).Agency!=null ? ((_QueryItem)Container.DataItem).Agency.CompanyName : "" %>
                        </td>
                        <td align="center">
                            <%# ((_QueryItem)Container.DataItem).InvoiceNo %>
                        </td>
                    </tr>
                    <tr id="tr2" runat="server" visible="<%# !((_QueryItem)Container.DataItem).InvoiceID.HasValue %>">
                        <td class="total-count" colspan="3" align="right">
                            總計&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;發票張數：<%# String.Format("{0:##,###,###,###}",((_QueryItem)Container.DataItem).InvoiceCount) %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
    </table>

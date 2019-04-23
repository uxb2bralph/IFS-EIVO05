﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="eIVOGo.Module.EIVO.Item.InvoiceProductPrintView" %>
<%@ Import Namespace="Model.DataEntity" %>

<style type="text/css">
    .protuct td{
        width: 280px;
        word-break:break-all;
        word-wrap: break-word; /*internetexplorer5.5+*/        
        font-size: 10px;
        font-weight:bold;
    }

</style>

<asp:Repeater ID="rpList" runat="server" EnableViewState="false">
    <ItemTemplate>
        <tr class="protuct">
            <td height="20" valign="top">
                <pre class="pre"><%# Item.Brief %><%# ((InvoiceProductItem)Container.DataItem).Spec %></pre>
            </td>
            <td width="12%" height="20" align="right" valign="top">
                <%# String.Format("{0:0}", ((InvoiceProductItem)Container.DataItem).Piece) %>
            </td>
            <td height="20" align="right" valign="top">
                <%# String.Format("{0:#,0}", ((InvoiceProductItem)Container.DataItem).UnitCost) %>
            </td>
            <td width="20%" height="20" align="right" valign="top" >
                <%# String.Format("{0:#,0}", ((InvoiceProductItem)Container.DataItem).CostAmount) %>
            </td>
        </tr>
    </ItemTemplate>
</asp:Repeater>

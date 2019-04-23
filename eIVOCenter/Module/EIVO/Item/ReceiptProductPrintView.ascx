﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReceiptProductPrintView.ascx.cs" Inherits="eIVOCenter.Module.EIVO.Item.ReceiptProductPrintView" %>
<%@ Import Namespace="Model.DataEntity" %>
<asp:Repeater ID="rpList" runat="server" EnableViewState="false">
    <ItemTemplate>
        <tr>
            <td height="20" valign="top">
                <%#((ReceiptDetail)Container.DataItem).Description %>
            </td>
            <td width="10%" height="20" align="right" valign="top">
                <%#((ReceiptDetail)Container.DataItem).Quantity %>
            </td>
            <td height="20" align="right" valign="top">
                <%#((ReceiptDetail)Container.DataItem).UnitPrice %>
            </td>
            <td width="20%" height="20" align="right" valign="top">
                <%#((ReceiptDetail)Container.DataItem).Amount %>
            </td>
        </tr>
    </ItemTemplate>
</asp:Repeater>
﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReceiptItemCommonView.ascx.cs"
    Inherits="eIVOCenter.Module.EIVO.Action.ReceiptItemCommonView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Module/UI/FunctionTitleBar.ascx" TagName="FunctionTitleBar" TagPrefix="uc1" %>
<%@ Register Src="~/Module/Common/EnumSelector.ascx" TagName="EnumSelector" TagPrefix="uc4" %>
<%@ Register Src="~/Module/Common/DataModelCache.ascx" TagName="DataModelCache" TagPrefix="uc2" %>
<%@ Register Src="~/Module/Common/ActionHandler.ascx" TagName="ActionHandler" TagPrefix="uc5" %>
<%@ Register assembly="Model" namespace="Model.DataEntity" tagprefix="cc1" %>
<%@ Import Namespace="Utility" %>
<%@ Register Src="~/Module/EIVO/Action/ReceiptProductItemList.ascx" TagName="ReceiptProductItemList" TagPrefix="uc3" %>
<%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
<!--路徑名稱-->
<!--交易畫面標題-->
<!--按鈕-->
<asp:Button ID="btnPopup" runat="server" Text="Button" Style="display: none" />
<asp:Panel ID="Panel1" runat="server" Style="display: none; width: 650px; background-color: #ffffdd;
    border-width: 3px; border-style: solid; border-color: Gray; padding: 3px;">
    <asp:Panel ID="Panel3" runat="server" Style="cursor: move; background-color: #DDDDDD;
        border: solid 1px Gray; color: Black">
        <%--        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        --%>
        <!--路徑名稱-->
        <!--交易畫面標題-->
        <uc1:FunctionTitleBar ID="titleBar" runat="server" ItemName="收據" />
        <!--按鈕-->
        <div class="border_gray" id="holder" runat="server">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
                    <tr>
                        <th width="100">
                            收據號碼
                        </th>
                        <td class="tdleft">
                          <%# _entity.No  %>
                        </td>
                         <th width="100">
                            收據日期
                        </th>
                        <td class="tdleft">
                           <%# ValueValidity.ConvertChineseDateString(_entity.ReceiptDate) %>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            買受人
                        </th>
                        <td  class="tdleft">
                            <%#_entity.Buyer.CompanyName   %>
                        </td>
                        <th width="100">
                            買受人統編
                        </th>
                        <td  class="tdleft">
                            <%#_entity.Buyer.ReceiptNo  %>
                        </td>

                    </tr>
                    <tr>
                        <th width="100">
                            地　址
                        </th>
                        <td  class="tdleft">
                           <%#_entity.Buyer.Addr%>
                        </td>
                        <th width="100">
                           總金額
                        </th>
                        <td class="tdleft">
                            <%# String.Format("{0:0,0}", _entity.TotalAmount) %>
                        </td>
                    </tr>
                </table>
                <br />
                <uc3:ReceiptProductItemList ID="ReceiptProductItems" runat="server" />
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td class="Bargain_btn" align="center">
                    <asp:Button ID="btnCancel" runat="server" Text="關閉" />
                    
                </td>
            </tr>
        </table>
        <%--            </ContentTemplate>
        </asp:UpdatePanel>
        --%>
    </asp:Panel>
</asp:Panel>
<asp:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="btnPopup"
    CancelControlID="btnCancel" PopupControlID="Panel1" BackgroundCssClass="modalBackground"
    DropShadow="true" PopupDragHandleControlID="Panel3" />
<uc5:ActionHandler ID="doCancel" runat="server" />
<uc5:ActionHandler ID="doConfirm" runat="server" />
<cc1:ReceiptDataSource ID="dsEntity" runat="server">
</cc1:ReceiptDataSource>
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnCancel.OnClientClick = doCancel.GetPostBackEventReference(null);
    }
</script>

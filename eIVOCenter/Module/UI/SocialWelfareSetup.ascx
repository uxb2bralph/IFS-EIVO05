<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SocialWelfareSetup.ascx.cs" Inherits="eIVOGo.Module.UI.SocialWelfareSetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:UpdatePanel ID="Updatepanel1" runat="server">
    <ContentTemplate>
        <asp:Button ID="btnHidden" runat="Server" Style="display: none" /> 
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:Panel ID="Panel1" runat="server" Style="display: none; width:650px; background-color:#ffffdd; border-width:3px; border-style:solid; border-color:Gray; padding:3px;">
            <asp:Panel ID="Panel3" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black">
                <!--路徑名稱--><!--交易畫面標題-->
                <h1><img runat="server" enableviewstate="false" id="img2" src="~/images/icon_search.gif" width="29" height="28" border="0" align="absmiddle" />社福機構設定</h1>
                <div id="Div1">
                        <asp:RadioButtonList ID="rdbSocialWelfare" runat="server" RepeatColumns="3" Width="100%" RepeatDirection="Horizontal">
                        </asp:RadioButtonList>
                </div>
                    <p><asp:Label ID="lblError" Visible="false" ForeColor="Red" Font-Size="Larger" runat="server"></asp:Label></p>
                <!--按鈕-->
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                    <td class="Bargain_btn"><span class="table-title">
                        <asp:Button ID="OkButton" runat="server" Text="OK" OnClick="OkButton_Click" />
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" />
                    </span></td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" 
            TargetControlID="btnHidden"
            PopupControlID="Panel1" 
            BackgroundCssClass="modalBackground" 
            CancelControlID="CancelButton" 
            DropShadow="true"
            PopupDragHandleControlID="Panel3" />
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="OkButton" />
    </Triggers>
</asp:UpdatePanel>
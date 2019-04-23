﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentFlowControlItem.ascx.cs"
    Inherits="eIVOCenter.Module.Flow.DocumentFlowControlItem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Module/UI/FunctionTitleBar.ascx" TagName="FunctionTitleBar" TagPrefix="uc1" %>
<%@ Register Src="../Common/EnumSelector.ascx" TagName="EnumSelector" TagPrefix="uc4" %>
<%@ Register Src="DocumentFlowControlSelector.ascx" TagName="DocumentFlowControlSelector"
    TagPrefix="uc5" %>
<%@ Register Assembly="Model" Namespace="Model.DocumentFlowManagement" TagPrefix="cc1" %>
<%@ Import Namespace="eIVOCenter.Module.Flow" %>
<%@ Register src="../Common/DataModelCache.ascx" tagname="DataModelCache" tagprefix="uc2" %>
<asp:Button ID="btnPopup" runat="server" Text="Button" Style="display: none" />
<asp:Panel ID="Panel1" runat="server" Style="display: none; width: 650px; background-color: #ffffdd;
    border-width: 3px; border-style: solid; border-color: Gray; padding: 3px;">
    <asp:Panel ID="Panel3" runat="server" Style="cursor: move; background-color: #DDDDDD;
        border: solid 1px Gray; color: Black">
        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <!--路徑名稱-->
        <!--交易畫面標題-->
        <uc1:FunctionTitleBar ID="titleBar" runat="server" ItemName="資料流程維護" />
        <!--按鈕-->
        <div class="border_gray">
            <asp:DetailsView ID="dvEntity" runat="server" AutoGenerateRows="False" DataKeyNames="StepID" 
                DataSourceID="dsEntity" DefaultMode="Insert" OnItemCommand="dvEntity_ItemCommand"
                CssClass="left_title" GridLines="None" OnItemInserted="dvEntity_ItemInserted"
                OnItemInserting="dvEntity_ItemInserting" OnItemUpdated="dvEntity_ItemUpdated"
                OnItemUpdating="dvEntity_ItemUpdating" 
                onitemcreated="dvEntity_ItemCreated">
                <FieldHeaderStyle CssClass="th" />
                <Fields>
                    <asp:TemplateField HeaderText="步驟狀態" SortExpression="LevelID">
                        <EditItemTemplate>
                            <uc4:EnumSelector ID="LevelID" runat="server" TypeName="Model.Locale.Naming+B2BInvoiceStepDefinition, Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
                                SelectedValue='<%# Eval("LevelID") %>' />
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <uc4:EnumSelector ID="LevelID" runat="server" TypeName="Model.Locale.Naming+B2BInvoiceStepDefinition, Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="上一步" SortExpression="PrevStep">
                        <EditItemTemplate>
                            <uc5:DocumentFlowControlSelector ID="PrevStep" runat="server" SelectorIndication="未設定" SelectedValue="<%# ((DocumentFlowControl)Container.DataItem).PrevStep %>"/>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <uc5:DocumentFlowControlSelector ID="PrevStep" runat="server" SelectorIndication="未設定" />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="下一步" SortExpression="NextStep">
                        <EditItemTemplate>
                            <uc5:DocumentFlowControlSelector ID="NextStep" runat="server" SelectorIndication="未設定" SelectedValue="<%# ((DocumentFlowControl)Container.DataItem).NextStep %>"/>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <uc5:DocumentFlowControlSelector ID="NextStep" runat="server" SelectorIndication="未設定" />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <EditItemTemplate>
                            <input type="checkbox" name="InitStep" value="1" />設為流程進入點
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <input type="checkbox" name="InitStep" value="1" />設為流程進入點
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:Button ID="btnUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                Text="確定" />
                            &nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="取消" />
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:Button ID="btnInsert" runat="server" CausesValidation="True" CommandName="Insert"
                                Text="確定" />
                            &nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="取消" />
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="修改" />
                            &nbsp;<asp:Button ID="btnNew" runat="server" CausesValidation="False" CommandName="New"
                                Text="新增" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Fields>
            </asp:DetailsView>
        </div>
        <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
    </asp:Panel>
</asp:Panel>
<asp:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="btnPopup"
    PopupControlID="Panel1" BackgroundCssClass="modalBackground" DropShadow="true"
    PopupDragHandleControlID="Panel3" />
<cc1:DocumentFlowControlDataSource ID="dsEntity" runat="server">
</cc1:DocumentFlowControlDataSource>
<uc2:DataModelCache ID="modelItem" runat="server" KeyName="FlowID" />


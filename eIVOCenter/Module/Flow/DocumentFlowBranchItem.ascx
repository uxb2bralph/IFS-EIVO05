﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentFlowBranchItem.ascx.cs"
    Inherits="eIVOCenter.Module.Flow.DocumentFlowBranchItem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Module/UI/FunctionTitleBar.ascx" TagName="FunctionTitleBar" TagPrefix="uc1" %>
<%@ Register Src="~/Module/Common/EnumSelector.ascx" TagName="EnumSelector" TagPrefix="uc4" %>
<%@ Register Src="~/Module/Common/DataModelCache.ascx" TagName="DataModelCache" TagPrefix="uc2" %>
<%@ Register Src="~/Module/Common/ActionHandler.ascx" TagName="ActionHandler" TagPrefix="uc5" %>
<%@ Register Assembly="Model" Namespace="Model.DocumentFlowManagement" TagPrefix="cc1" %>
<%@ Register Src="DocumentTypeFlowCandidateSelector.ascx" TagName="DocumentTypeFlowCandidateSelector"
    TagPrefix="uc3" %>
<%@ Register Src="DocumentFlowSelector.ascx" TagName="DocumentFlowSelector" TagPrefix="uc6" %>
<%@ Register Src="DocumentFlowControlSelector.ascx" TagName="DocumentFlowControlSelector"
    TagPrefix="uc7" %>
<%@ Import Namespace="Uxnet.Web.WebUI" %>
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
        <!--路徑名稱-->
        <!--交易畫面標題-->
        <uc1:FunctionTitleBar ID="titleBar" runat="server" ItemName="修改發票號碼" />
        <!--按鈕-->
        <div class="border_gray" id="holder" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="left_title" border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tbody>
                            <tr>
                                <th nowrap>
                                    選取流程
                                </th>
                                <td class="tdleft">
                                    <uc6:DocumentFlowSelector ID="flow" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th nowrap>
                                    選取流程步驟
                                </th>
                                <td class="tdleft">
                                    <uc7:DocumentFlowControlSelector ID="flowStep" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td class="Bargain_btn" align="center">
                    <asp:Button ID="btnUpdate" runat="server" CausesValidation="True" CommandName="Update"
                        Text="確定" />
                    &nbsp;
                    <input type="reset" value="重填" class="btn" />
                    &nbsp;<asp:Button ID="btnCancel" runat="server" Text="關閉" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
<asp:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="btnPopup"
    CancelControlID="btnCancel" PopupControlID="Panel1" BackgroundCssClass="modalBackground"
    DropShadow="true" PopupDragHandleControlID="Panel3" />
<cc1:DocumentFlowBranchDataSource ID="dsEntity" runat="server">
</cc1:DocumentFlowBranchDataSource>
<uc5:ActionHandler ID="doConfirm" runat="server" />
<uc2:DataModelCache ID="modelItem" runat="server" KeyName="StepID" />
<uc5:ActionHandler ID="doCancel" runat="server" />
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        btnUpdate.OnClientClick = doConfirm.GetPostBackEventReference(null);
        btnCancel.OnClientClick = doCancel.GetPostBackEventReference(null);
        flow.Selector.AutoPostBack = true;
        this.PreRender += new EventHandler(module_flow_documentflowbranchitem_ascx_PreRender);
    }

    void module_flow_documentflowbranchitem_ascx_PreRender(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(flow.SelectedValue))
        {
            flowStep.QueryExpr = s => s.FlowID == int.Parse(flow.SelectedValue);
            flowStep.Selector.DataSource = flowStep.Select()
                .OrderBy(o => o.StepID).Select(o => new
                        {
                            o.LevelExpression.Expression,
                            o.StepID
                        }
                    );
            flowStep.BindData();
        }
    }

    protected override bool saveEntity()
    {
        var mgr = dsEntity.CreateDataManager();
        var step = mgr.GetTable<DocumentFlowControl>().Where(s=>s.StepID==(int?)modelItem.DataItem).FirstOrDefault();

        if (step==null)
        {
            this.AjaxAlert("流程步驟不存在!!");
            return false;
        }

        if (String.IsNullOrEmpty(flowStep.SelectedValue))
        {
            this.AjaxAlert("請選擇分歧步驟!!");
            return false;
        }

        int branchID = int.Parse(flowStep.SelectedValue);

        if (step.BranchFlow.Any(b=>b.BranchStep==branchID))
        {
            this.AjaxAlert("流程分歧步驟已存在!!");
            return false;
        }

        step.BranchFlow.Add(new DocumentFlowBranch
        {
            BranchStep = branchID
        });

        mgr.SubmitChanges();

        return true;
    }
    
</script>

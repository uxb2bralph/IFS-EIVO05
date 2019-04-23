﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentFlowBranchList.ascx.cs"
    Inherits="eIVOCenter.Module.Flow.DocumentFlowBranchList" %>
<%@ Register Src="~/Module/Common/ActionHandler.ascx" TagName="ActionHandler" TagPrefix="uc1" %>
<%@ Register Src="~/Module/Common/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Assembly="Model" Namespace="Model.DocumentFlowManagement" TagPrefix="cc1" %>
<%@ Register src="../Common/DataModelCache.ascx" tagname="DataModelCache" tagprefix="uc6" %>
<%@ Register src="DocumentFlowBranchItem.ascx" tagname="DocumentFlowBranchItem" tagprefix="uc3" %>
<%@ Import Namespace="Model.Locale" %>
<%@ Import Namespace="Utility" %>
<%@ Import Namespace="Uxnet.Web.WebUI" %>
<div>流程名稱:<%# String.Format("{0},{1}",_item.DocumentFlow.FlowName,((Naming.B2BInvoiceStepDefinition)_item.LevelID).ToString()) %></div>
<asp:GridView ID="gvEntity" runat="server" AutoGenerateColumns="False" Width="100%"
    GridLines="None" CellPadding="0" CssClass="table01" AllowPaging="True" EnableViewState="False"
    DataSourceID="dsEntity" ShowFooter="True" DataKeyNames="StepID,BranchStep">
    <Columns>
        <asp:TemplateField HeaderText="流程名稱" SortExpression="PrevStep">
            <ItemTemplate>
                <%# ((DocumentFlowBranch)Container.DataItem).BranchStepItem.DocumentFlow.FlowName %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="流程狀態" SortExpression="LevelID">
            <ItemTemplate>
                <%# ((Naming.B2BInvoiceStepDefinition)((DocumentFlowBranch)Container.DataItem).BranchStepItem.LevelID).ToString() %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="">
            <ItemTemplate>
                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                    Text="刪除" CommandArgument='<%# String.Format("{0}",Eval(gvEntity.DataKeyNames[1])) %>'
                    OnClientClick='<%# doDelete.GetConfirmedPostBackEventReference("確認刪除此筆資料?", String.Format("{0}",Eval(gvEntity.DataKeyNames[1]))) %>' />
            </ItemTemplate>
            <FooterTemplate>
                <asp:Button ID="btnCreate" runat="server" CausesValidation="False" CommandName="Create"
                    Text="新增分歧流程" OnClientClick='<%# doCreate.GetPostBackEventReference(null) %>' /></FooterTemplate>
        </asp:TemplateField>
    </Columns>
    <FooterStyle CssClass="total-count" />
    <EmptyDataTemplate>
        <asp:Button ID="btnCreate" runat="server" CausesValidation="False" Text="新增分歧流程"
            OnClientClick='<%# doCreate.GetPostBackEventReference(null) %>' />
    </EmptyDataTemplate>
    <EmptyDataRowStyle CssClass="noborder" />
    <PagerStyle HorizontalAlign="Center" />
    <SelectedRowStyle />
    <HeaderStyle />
    <AlternatingRowStyle CssClass="OldLace" />
    <PagerTemplate>
        <uc2:PagingControl ID="pagingList" runat="server" OnPageIndexChanged="PageIndexChanged" />
    </PagerTemplate>
    <RowStyle />
    <EditRowStyle />
</asp:GridView>
<cc1:DocumentFlowBranchDataSource ID="dsEntity" runat="server">
</cc1:DocumentFlowBranchDataSource>
<uc1:ActionHandler ID="doCreate" runat="server" />
<uc1:ActionHandler ID="doDelete" runat="server" />
<uc6:DataModelCache ID="modelItem" runat="server" KeyName="StepID" />
<uc3:DocumentFlowBranchItem ID="editItem" runat="server" />
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.PreRender += new EventHandler(module_flow_documentflowcontrollist_ascx_PreRender);
            this.QueryExpr = f => f.StepID == (int?)modelItem.DataItem;
            _item = dsEntity.CreateDataManager().GetTable<DocumentFlowControl>().Where(f => f.StepID == (int?)modelItem.DataItem).FirstOrDefault();

            doCreate.DoAction = arg => 
            {
                editItem.Show();
            };

            doDelete.DoAction = arg =>
                {
                    delete(arg);
                };
        
            editItem.Done += new EventHandler(editItem_Done);
    }

    void editItem_Done(object sender, EventArgs e)
    {
        editItem.Close();
    }

    protected void delete(string keyValue)
    {
        try
        {
            dsEntity.CreateDataManager().DeleteAny(r => r.BranchStep == int.Parse(keyValue) && r.StepID==(int?)modelItem.DataItem );
        }
        catch (Exception ex)
        {
            Logger.Error(ex);
            this.AjaxAlert("刪除資料失敗,原因:" + ex.Message);
        }
    }

    void module_flow_documentflowcontrollist_ascx_PreRender(object sender, EventArgs e)
    {
        if (_item != null)
        {
            this.DataBind();
        }
    }
</script>

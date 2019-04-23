﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="eIVOGo.template.ContentControlTemplate" %>
<%@ Register src="~/Module/UI/PageAction.ascx" tagname="PageAction" tagprefix="uc1" %>
<%@ Register src="~/Module/UI/FunctionTitleBar.ascx" tagname="FunctionTitleBar" tagprefix="uc2" %>
<%@ Register src="Business/EnterpriseGroupMemberItem.ascx" tagname="EnterpriseGroupMemberItem" tagprefix="uc3" %>
<%@ Import Namespace="Uxnet.Web.WebUI" %>


<uc1:PageAction ID="actionItem" runat="server" ItemName="首頁 &gt; 企業管理-公司資料" />
<uc2:FunctionTitleBar ID="titleBar" runat="server" ItemName="企業管理-公司資料" />

<uc3:EnterpriseGroupMemberItem ID="editItem" runat="server" />
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        editItem.BindData();
        editItem.Done += new EventHandler(editItem_Done);
    }

    void editItem_Done(object sender, EventArgs e)
    {
        this.AjaxAlertAndRedirect("作業完成!!", VirtualPathUtility.ToAbsolute("~/SAM/CompanyManager.aspx"));
    }
</script>



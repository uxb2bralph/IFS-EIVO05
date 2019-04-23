﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="eIVOGo.template.ContentControlTemplate" %>
<%@ Register src="~/Module/UI/PageAction.ascx" tagname="PageAction" tagprefix="uc1" %>
<%@ Register src="~/Module/UI/FunctionTitleBar.ascx" tagname="FunctionTitleBar" tagprefix="uc2" %>


<%@ Register src="DocumentFlowBranchList.ascx" tagname="DocumentFlowBranchList" tagprefix="uc3" %>




<uc1:PageAction ID="actionItem" runat="server" ItemName="首頁 &gt; 設定流程分歧" />
<uc2:FunctionTitleBar ID="titleBar" runat="server" ItemName="設定流程分歧" />



<uc3:DocumentFlowBranchList ID="DocumentFlowBranchList1" runat="server" />





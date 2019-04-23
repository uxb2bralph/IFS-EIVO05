﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="eIVOGo.template.ContentControlTemplate" %>
<%@ Register src="~/Module/UI/PageAction.ascx" tagname="PageAction" tagprefix="uc1" %>
<%@ Register src="~/Module/UI/FunctionTitleBar.ascx" tagname="FunctionTitleBar" tagprefix="uc2" %>

<%@ Register src="AnnouncementList.ascx" tagname="AnnouncementList" tagprefix="uc3" %>

<uc1:PageAction ID="actionItem" runat="server" ItemName="首頁 &gt; 跑馬燈訊息維護" />
<uc2:FunctionTitleBar ID="titleBar" runat="server" ItemName="跑馬燈訊息維護" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
       <uc3:AnnouncementList ID="AnnouncementList" runat="server" />
  </ContentTemplate>
</asp:UpdatePanel>


﻿<%@ Page Language="C#" MasterPageFile="~/template/main_page.Master" AutoEventWireup="true"  Inherits="eIVOGo.template.base_page" StylesheetTheme="Visitor"  %>
<%@ Register src="../Module/SCM/MarketResourceMaintainAdd.ascx" tagname="MarketResourceMaintainAdd" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <uc1:MarketResourceMaintainAdd ID="MarketResourceMaintainAdd1" runat="server" />
</asp:Content>

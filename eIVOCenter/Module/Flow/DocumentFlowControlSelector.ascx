﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentFlowControlSelector.ascx.cs" Inherits="eIVOCenter.Module.Flow.DocumentFlowControlSelector" %>
<%@ Register assembly="Model" namespace="Model.DocumentFlowManagement" tagprefix="cc1" %>
<%@ Register src="../Common/DataModelCache.ascx" tagname="DataModelCache" tagprefix="uc1" %>
<asp:DropDownList ID="selector" runat="server" EnableViewState="False" DataTextField="Expression" DataValueField="StepID"
    ondatabound="selector_DataBound">
</asp:DropDownList>
<cc1:DocumentFlowControlDataSource ID="dsEntity" 
    runat="server">
</cc1:DocumentFlowControlDataSource>
<uc1:DataModelCache ID="modelItem" runat="server" KeyName="FlowID" />


<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Announcement.ascx.cs" Inherits="eIVOCenter.Module.UI.Announcement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>

   <%-- <asp:Label ID="AnnMessage" runat="server"></asp:Label>--%>
                <asp:Repeater ID="rpList" runat="server" EnableViewState="false">
                <ItemTemplate>
                    ‧系統公告：<font color="#434343"><%# ((Announcement_REC)Container.DataItem).AnnMessage%></font>
                </ItemTemplate>
            </asp:Repeater>
<cc1:AnnouncementRECDataSource ID="dsEntity" runat="server" Isolated="true">
</cc1:AnnouncementRECDataSource>

﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImportCounterpartBusinessList.ascx.cs"
    Inherits="eIVOCenter.Module.SAM.Business.ImportCounterpartBusinessList" %>
<%@ Register Src="~/Module/Common/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc2" %>
<%@ Register Src="~/Module/Common/DataModelCache.ascx" TagName="DataModelCache" TagPrefix="uc1" %>
<%@ Import Namespace="Model.DataEntity" %>
<%@ Import Namespace="Utility" %>
<%@ Import Namespace="Model.UploadManagement" %>
<asp:GridView ID="gvEntity" runat="server" AutoGenerateColumns="False" Width="100%"
    GridLines="None" CellPadding="0" CssClass="table01" AllowPaging="False" EnableViewState="False"
    ShowFooter="True">
    <Columns>
        <asp:TemplateField HeaderText="類別">
            <ItemTemplate>
                <%# ((eIVOCenter.Helper.BusinessCounterpartXmlUploadManager)_mgr).BusinessType.ToString() %></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="營業人名稱">
            <ItemTemplate>
                <%# (((ItemUpload<Organization>)Container.DataItem)).Entity.CompanyName %></ItemTemplate>
            <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="統一編號">
            <ItemTemplate>
                <%# (((ItemUpload<Organization>)Container.DataItem)).Entity.ReceiptNo%></ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="聯絡人電子郵件">
            <ItemTemplate>
                <%# (((ItemUpload<Organization>)Container.DataItem)).Entity.ContactEmail %>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="地址" FooterText="總計">
            <ItemTemplate>
                <%# (((ItemUpload<Organization>)Container.DataItem)).Entity.Addr %>
            </ItemTemplate>
            <FooterStyle HorizontalAlign="Right" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="電話">
            <ItemTemplate>
                <%# (((ItemUpload<Organization>)Container.DataItem)).Entity.Phone %>
            </ItemTemplate>
            <FooterTemplate>
                匯入總筆數：<%# _mgr.ItemCount %>筆</FooterTemplate>
            <FooterStyle HorizontalAlign="Right" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="匯入狀態">
            <ItemTemplate>
                <font color="red"><%# String.IsNullOrEmpty((((ItemUpload<Organization>)Container.DataItem)).Status) ? (((ItemUpload<Organization>)Container.DataItem)).UploadStatus.ToString() : (((ItemUpload<Organization>)Container.DataItem)).Status %></font>
            </ItemTemplate>
            <FooterTemplate>
                成功：<%# _mgr.IsValid?_mgr.ItemCount:_mgr.ItemCount-_mgr.ErrorList.Count %>失敗：<%# _mgr.IsValid?0:_mgr.ErrorList.Count %>
            </FooterTemplate>
        </asp:TemplateField>
    </Columns>
    <FooterStyle CssClass="total-count" />
    <EmptyDataTemplate>
        <center>
            <font color="red">匯入資料格式錯誤!!</font></center>
    </EmptyDataTemplate>
    <PagerStyle HorizontalAlign="Center" />
    <SelectedRowStyle />
    <HeaderStyle />
    <AlternatingRowStyle CssClass="OldLace" />
</asp:GridView>
<uc2:PagingControl ID="pagingList" runat="server" />
<uc1:DataModelCache ID="modelItem" runat="server" KeyName="uploadBusiness" />

﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnnouncementList.ascx.cs"
    Inherits="eIVOCenter.Module.SYS.AnnouncementList" %>
<%@ Register Src="../Common/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc1" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<%@ Register Src="Item/AnnouncementItem.ascx" TagName="AnnouncementItem" TagPrefix="uc2" %>
<div class="border_gray">
    <asp:GridView ID="gvEntity" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataKeyNames="AnnID" DataSourceID="dsEntity" EnableViewState="False" OnRowCommand="gvEntity_RowCommand"
        ShowFooter="True">
        <Columns>
            <asp:BoundField DataField="AnnID" HeaderText="AnnID" ReadOnly="True" SortExpression="AnnID"
                InsertVisible="False"  Visible="false"/>
                <asp:BoundField DataField="AnnMessage" HeaderText="訊息內容" ReadOnly="True"  />
             <asp:BoundField DataField="StartDate" HeaderText="起始時間" ReadOnly="True"  ItemStyle-Width="100" 
             DataFormatString="{0:yyyy/MM/dd}"   ItemStyle-HorizontalAlign="Center"/>
               <asp:BoundField DataField="EndDate" HeaderText="結束時間" ReadOnly="True"   ItemStyle-Width="100"
               DataFormatString="{0:yyyy/MM/dd}"  ItemStyle-HorizontalAlign="Center"/>
              <%--   <asp:BoundField DataField="AlwaysShow" HeaderText="永久顯示" ReadOnly="True"  ItemStyle-Width="50" />--%>
              <asp:TemplateField HeaderText="永久顯示">
                <ItemTemplate>
                    <%# ((Announcement_REC)Container.DataItem).AlwaysShow ? "是" : "否"%></ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="100">
                <FooterTemplate>
                    <center><asp:Button ID="btnCreate" runat="server" CausesValidation="False" CommandName="Create"
                        Text="新增" /></center>
               
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Button ID="btnModify" runat="server" CausesValidation="False" CommandName="Modify"
                        Text="修改" CommandArgument='<%# Eval("AnnID") %>' />
                    &nbsp;<asp:Button ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="刪除" CommandArgument='<%# Eval("AnnID") %>' OnClientClick='return confirm("確認刪除此筆資料?");' />
                    
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
               <center><asp:Button ID="btnCreate" runat="server" CausesValidation="False" CommandName="Create"
                        Text="新增" /></center>
        </EmptyDataTemplate>
        <PagerTemplate>
            <uc1:PagingControl ID="pagingList" runat="server" OnPageIndexChanged="PageIndexChanged" />
        </PagerTemplate>
    </asp:GridView>
       <asp:Button ID="btnCreate" runat="server" CausesValidation="False" CommandName="Create"  Visible="false"
       OnClick="btnCreate_Click"
                        Text="新增" />
</div>
<cc1:AnnouncementRECDataSource ID="dsEntity" runat="server">
</cc1:AnnouncementRECDataSource>
<uc2:AnnouncementItem ID="AnnouncementItem" runat="server" />

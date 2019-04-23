<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadDataExceptionNotification.ascx.cs"
    Inherits="eIVOCenter.Module.UI.UploadDataExceptionNotification" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
    <tr>
        <th class="Head_style_a">
            發票
        </th>
    </tr>
</table>
<asp:GridView ID="gvInvoice" runat="server" Width="100%" AutoGenerateColumns="False"
    EnableViewState="False" EmptyDataText="沒有新資料!!" CssClass="table01" DataKeyNames="LogID">
    <Columns>
        <asp:TemplateField HeaderText="發票號碼" SortExpression="DataContent">
            <ItemTemplate>
                <a href='<%# String.Format("{0}{1}?logID={2}",Uxnet.Web.Properties.Settings.Default.HostUrl,VirtualPathUtility.ToAbsolute("~/Published/DumpExceptionLog.ashx"),Eval("LogID"))%>' target="_blank"><%# getInvoiceContent((String)Eval("DataContent"))%></a> 
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="LogTime" HeaderText="時間" SortExpression="LogTime" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" />
        <asp:BoundField DataField="Message" HeaderText="錯誤訊息" SortExpression="Message" />
    </Columns>
    <AlternatingRowStyle CssClass="OldLace" />
</asp:GridView>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
    <tr>
        <th class="Head_style_a">
            作廢發票
        </th>
    </tr>
</table>
<asp:GridView ID="gvCancel" runat="server" Width="100%" AutoGenerateColumns="False"
    EnableViewState="False" EmptyDataText="沒有新資料!!" CssClass="table01" DataKeyNames="LogID">
    <Columns>
        <asp:TemplateField HeaderText="作廢發票號碼" SortExpression="DataContent">
            <ItemTemplate>
                <a href='<%# String.Format("{0}{1}?logID={2}",Uxnet.Web.Properties.Settings.Default.HostUrl,VirtualPathUtility.ToAbsolute("~/Published/DumpExceptionLog.ashx"),Eval("LogID"))%>' target="_blank"><%# getCancellationContent((String)Eval("DataContent"))%></a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="LogTime" HeaderText="時間" SortExpression="LogTime" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" />
        <asp:BoundField DataField="Message" HeaderText="錯誤訊息" SortExpression="Message" />
    </Columns>
    <AlternatingRowStyle CssClass="OldLace" />
</asp:GridView>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
    <tr>
        <th class="Head_style_a">
            發票折讓
        </th>
    </tr>
</table>
<asp:GridView ID="gvAllowance" runat="server" Width="100%" AutoGenerateColumns="False"
    EnableViewState="False" EmptyDataText="沒有新資料!!" CssClass="table01" DataKeyNames="LogID">
    <Columns>
        <asp:TemplateField HeaderText="折讓單號碼" SortExpression="DataContent">
            <ItemTemplate>
                <a href='<%# String.Format("{0}{1}?logID={2}",Uxnet.Web.Properties.Settings.Default.HostUrl,VirtualPathUtility.ToAbsolute("~/Published/DumpExceptionLog.ashx"),Eval("LogID"))%>' target="_blank"><%# getAllowanceContent((String)Eval("DataContent"))%></a> 
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="LogTime" HeaderText="時間" SortExpression="LogTime" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" />
        <asp:BoundField DataField="Message" HeaderText="錯誤訊息" SortExpression="Message" />
    </Columns>
    <AlternatingRowStyle CssClass="OldLace" />
</asp:GridView>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
    <tr>
        <th class="Head_style_a">
            作廢折讓單
        </th>
    </tr>
</table>
<asp:GridView ID="gvCancelAllowance" runat="server" Width="100%" AutoGenerateColumns="False"
    EnableViewState="False" EmptyDataText="沒有新資料!!" CssClass="table01" DataKeyNames="LogID">
    <Columns>
        <asp:TemplateField HeaderText="作廢折讓單號碼" SortExpression="DataContent">
            <ItemTemplate>
                <a href='<%# String.Format("{0}{1}?logID={2}",Uxnet.Web.Properties.Settings.Default.HostUrl,VirtualPathUtility.ToAbsolute("~/Published/DumpExceptionLog.ashx"),Eval("LogID"))%>' target="_blank"><%# getCancelAllowanceContent((String)Eval("DataContent"))%></a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="LogTime" HeaderText="時間" SortExpression="LogTime" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" />
        <asp:BoundField DataField="Message" HeaderText="錯誤訊息" SortExpression="Message" />
    </Columns>
    <AlternatingRowStyle CssClass="OldLace" />
</asp:GridView>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
    <tr>
        <th class="Head_style_a">
            收據
        </th>
    </tr>
</table>
<asp:GridView ID="gvReceipt" runat="server" Width="100%" AutoGenerateColumns="False"
    EnableViewState="False" EmptyDataText="沒有新資料!!" CssClass="table01" DataKeyNames="LogID">
    <Columns>
        <asp:TemplateField HeaderText="收據號碼" SortExpression="DataContent">
            <ItemTemplate>
                <a href='<%# String.Format("{0}{1}?logID={2}",Uxnet.Web.Properties.Settings.Default.HostUrl,VirtualPathUtility.ToAbsolute("~/Published/DumpExceptionLog.ashx"),Eval("LogID"))%>' target="_blank"><%# getReceiptContent((String)Eval("DataContent"))%></a> 
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="LogTime" HeaderText="時間" SortExpression="LogTime" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" />
        <asp:BoundField DataField="Message" HeaderText="錯誤訊息" SortExpression="Message" />
    </Columns>
    <AlternatingRowStyle CssClass="OldLace" />
</asp:GridView>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
    <tr>
        <th class="Head_style_a">
            作廢收據
        </th>
    </tr>
</table>
<asp:GridView ID="gvCancelReceipt" runat="server" Width="100%" AutoGenerateColumns="False"
    EnableViewState="False" EmptyDataText="沒有新資料!!" CssClass="table01" DataKeyNames="LogID">
    <Columns>
        <asp:TemplateField HeaderText="作廢收據號碼" SortExpression="DataContent">
            <ItemTemplate>
                <a href='<%# String.Format("{0}{1}?logID={2}",Uxnet.Web.Properties.Settings.Default.HostUrl,VirtualPathUtility.ToAbsolute("~/Published/DumpExceptionLog.ashx"),Eval("LogID"))%>' target="_blank"><%# getCancelReceiptContent((String)Eval("DataContent"))%></a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="LogTime" HeaderText="時間" SortExpression="LogTime" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" />
        <asp:BoundField DataField="Message" HeaderText="錯誤訊息" SortExpression="Message" />
    </Columns>
    <AlternatingRowStyle CssClass="OldLace" />
</asp:GridView>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
    <tr>
        <th class="Head_style_a">
            空白未使用字軌檔
        </th>
    </tr>
</table>
<asp:GridView ID="gvBranchTrackBlank" runat="server" Width="100%" AutoGenerateColumns="False"
    EnableViewState="False" EmptyDataText="沒有新資料!!" CssClass="table01" DataKeyNames="LogID">
    <Columns>
        <asp:TemplateField HeaderText="公司統編" SortExpression="DataContent">
            <ItemTemplate>
                <a href='<%# String.Format("{0}{1}?logID={2}",Uxnet.Web.Properties.Settings.Default.HostUrl,VirtualPathUtility.ToAbsolute("~/Published/DumpExceptionLog.ashx"),Eval("LogID"))%>' target="_blank"><%# getBranchTrackBlankContent((String)Eval("DataContent"))%></a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="LogTime" HeaderText="時間" SortExpression="LogTime" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" />
        <asp:BoundField DataField="Message" HeaderText="錯誤訊息" SortExpression="Message" />
    </Columns>
    <AlternatingRowStyle CssClass="OldLace" />
</asp:GridView>
<cc1:InvoiceDataSource ID="dsInv" runat="server">
</cc1:InvoiceDataSource>

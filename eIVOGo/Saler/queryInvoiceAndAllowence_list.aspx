<%@ Page Title="" Language="C#" MasterPageFile="~/template/main_page.Master" AutoEventWireup="true" CodeBehind="base_page.aspx.cs" Inherits="eIVOGo.template.base_page" StylesheetTheme="Visitor" %>
<%@ Register src="../Module/Inquiry/QueryInvoiceAndAllowenceListForSaler.ascx" tagname="QueryInvoiceAndAllowenceListForSaler" tagprefix="uc1" %>
<asp:Content ID="header" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">
    <uc1:QueryInvoiceAndAllowenceListForSaler ID="QueryInvoiceAndAllowenceListForSaler1" 
        runat="server" />
</asp:Content>

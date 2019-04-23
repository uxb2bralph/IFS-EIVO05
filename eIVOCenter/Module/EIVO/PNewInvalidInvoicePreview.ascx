<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PNewInvalidInvoicePreview.ascx.cs" Inherits="eIVOCenter.Module.EIVO.PNewInvalidInvoicePreview" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly ="Uxnet.Com.Net4" Namespace ="Utility" TagPrefix="cc2"  %>
<%@ Register Src="../UI/FunctionTitleBar.ascx" TagName="FunctionTitleBar" TagPrefix="uc1" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<asp:Button ID="btnHidden" runat="Server" Style="display: none" />
<asp:HiddenField ID="HiddenField1" runat="server" />
<asp:Panel ID="Panel1" runat="server" Style="display: none; width: 650px; background-color: #ffffdd;
    border-width: 3px; border-style: solid; border-color: Gray; padding: 3px;">
    <asp:Panel ID="Panel3" runat="server" Style="cursor: move; background-color: #DDDDDD;
        border: solid 1px Gray; color: Black">
        <!--路徑名稱-->
        <div id="divInvoice" runat="server">
            <uc1:FunctionTitleBar ID="FunctionTitleBar1" runat="server" ItemName="發票" />
            <div id="border_gray">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
                    <tr>
                        <th width="100">
                            發票號碼
                        </th>
                        <td class="tdleft">
                          <%# invoice != null ? invoice.No : ""  %>
                        </td>
                         <th width="100">
                            發票日期
                        </th>
                        <td class="tdleft">
                           <%# invoice != null ? ValueValidity.ConvertChineseDateString(invoice.InvoiceDate) : "" %>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            檢查號碼
                        </th>
                        <td class="tdleft">
                           
                        </td>
                        <th width="100">
                           總金額
                        </th>
                        <td class="tdleft">
                            <%#invoice != null ?  String.Format("{0:#,0}", invoice.InvoiceAmountType.TotalAmount) : ""%>
                        </td>
                    </tr>
                   
                   
                   
                    <tr>
                        <th width="100">
                            買受人
                        </th>
                        <td  class="tdleft">
                            <%#invoice != null ? invoice.InvoiceBuyer.Name : ""  %>
                        </td>
                        <th width="100">
                            買受人統編
                        </th>
                        <td  class="tdleft">
                            <%#invoice != null ? invoice.InvoiceBuyer.ReceiptNo : ""  %>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            地　址
                        </th>
                        <td  colspan="3" class="tdleft">
                           <%#invoice != null ? invoice.InvoiceBuyer.Address : ""%>
                        </td>
                      
                    </tr>
                </table>
                <br />
                <asp:GridView ID="gvEntity" runat="server" AutoGenerateColumns="False" Width="100%"
                    GridLines="None" CellPadding="0" CssClass="table01" ClientIDMode="Static" EnableViewState="false">
                    <Columns>
                        <asp:TemplateField HeaderText="序號">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1%></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="品名">
                            <ItemTemplate>
                                <%# Eval("Brief")%></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="數量">
                            <ItemTemplate>
                                <%# Eval("Piece")%></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="單位">
                            <ItemTemplate>
                                <%# Eval("PieceUnit")%></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="單價">
                            <ItemTemplate>
                                <%# String.Format("{0:#,0}",Eval("UnitCost"))%></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="金額">
                            <ItemTemplate>
                                <%# String.Format("{0:#,0}",Eval("CostAmount"))%></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="備註">
                            <ItemTemplate>
                                <%# Eval("Memo")%></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle />
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle />
                    <HeaderStyle />
                    <AlternatingRowStyle CssClass="OldLace" />
                    <RowStyle />
                    <EditRowStyle />
                </asp:GridView>
              
            </div>
        </div>
        <div id="divAllowance" runat="server">
            <uc1:FunctionTitleBar ID="FunctionTitleBar2" runat="server" ItemName="折讓" />
            <div id="border_gray">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title" >
                <tr>
                    <th width="100" nowrap="nowrap">折讓日期</th>
                    <td class="tdleft"><asp:Label ID="lblAllowanceDate" runat="server"></asp:Label></td>
                </tr>
            </table>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
                <tr>
                    <th width="100" colspan="4" class="Head_style_a">原開立銷貨發票單位</th>
                </tr>
                <tr>
                    <th width="100" nowrap="nowrap">統一編號</th>
                    <td width="30%" class="tdleft"><asp:Label ID="lblReceiptNo" runat="server"></asp:Label></td>
                    <th width="100" nowrap="nowrap">名　　稱</th>
                    <td class="tdleft"><asp:Label ID="lblCompanyName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th width="100">營業所在地址</th>
                    <td colspan="3" class="tdleft"><asp:Label ID="lblCompanyAddr" runat="server"></asp:Label></td>
                </tr>
            </table>
                <br />
                <asp:GridView ID="gvAllowance" runat="server" AutoGenerateColumns="False" Width="100%"
                    GridLines="None" CellPadding="0" CssClass="table01" ClientIDMode="Static" EnableViewState="false"
                    OnRowCreated="gvAllowance_RowCreated">
                    <Columns>
                        <asp:TemplateField HeaderText="聯式">
                            <ItemTemplate>
                                <%# ((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowance.InvoiceAllowanceSeller.ReceiptNo.Equals("0000000000") ? "二" : "三"%></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="年">
                            <ItemTemplate>
                                <%# ((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.InvoiceDate.Value.Year%></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="月">
                            <ItemTemplate>
                                <%# ((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.InvoiceDate.Value.Month%></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="日">
                            <ItemTemplate>
                                <%# ((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.InvoiceDate.Value.Day%></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="字軌">
                            <ItemTemplate>
                                <%# ((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.InvoiceNo .Substring (0,2)%></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="號碼">
                            <ItemTemplate>
                                <%# ((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.InvoiceNo.Substring(2)%></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="品名">
                            <ItemTemplate>
                                <%# ((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.OriginalDescription%></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="數量">
                            <ItemTemplate>
                                <%# ((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.Piece%></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="單價">
                            <ItemTemplate>
                                <%# String.Format("{0:#,0}", ((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.UnitCost) %></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="金額&lt;br/&gt;(不含稅之進貨額)">
                            <ItemTemplate>
                                <%# String.Format("{0:#,0}", (((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.Amount - ((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.Tax))%></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="營業稅額">
                            <ItemTemplate>
                                <%# String.Format("{0:#,0}", ((InvoiceAllowanceDetail)Container.DataItem).InvoiceAllowanceItem.Tax) %></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle />
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle />
                    <HeaderStyle />
                    <AlternatingRowStyle CssClass="OldLace" />
                    <RowStyle />
                    <EditRowStyle />
                </asp:GridView>
            <!--表格 結束-->
            </div>
        </div>
          <div id="divCompany" runat="server">
            <uc1:FunctionTitleBar ID="FunctionTitleBar3" runat="server" ItemName="公司資料" />
            <div id="border_gray">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
                    <tr>
                        <th width="100">
                            營業人名稱
                        </th>
                        <td class="tdleft">
                          <%# company != null ? company.CompanyName : ""  %>
                        </td>
                         <th width="100">
                            統一編號
                        </th>
                        <td class="tdleft">
                           <%# company != null ? company.ReceiptNo : ""  %>
                        </td>
                    </tr>
                    <tr>
                        <th width="100">
                            電話
                        </th>
                        <td class="tdleft">
                            <%# company != null ? company.Phone : ""   %>
                        </td>
                        <th width="100">
                           聯絡人電子郵件
                        </th>
                        <td class="tdleft">
                            <%# company != null ? company.ContactEmail : "" %>
                        </td>
                    </tr>
                   
                  
                    <tr>
                        <th width="100">
                            地　址
                        </th>
                        <td  colspan="3" class="tdleft">
                           <%# company != null ? company.Addr : ""%>
                        </td>
                      
                    </tr>
                </table>
               
            </div>
        </div>
          <p><asp:Label ID="lblError" Visible="false" ForeColor="Red" Font-Size="Larger" runat="server"></asp:Label></p>
        <!--按鈕-->
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td class="Bargain_btn">
                    <span class="table-title">
                        <asp:Button ID="CancelButton" CssClass="btn" runat="server" Text="關閉視窗" />
                    </span>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="btnHidden"
    PopupControlID="Panel1" BackgroundCssClass="modalBackground" CancelControlID="CancelButton"
    DropShadow="true" PopupDragHandleControlID="Panel3" />

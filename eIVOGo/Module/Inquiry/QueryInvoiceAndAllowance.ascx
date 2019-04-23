<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QueryInvoiceAndAllowance.ascx.cs" Inherits="eIVOGo.Module.Inquiry.QueryInvoiceAndAllowance" %>
<%@ Register src="../Common/CalendarInputDatePicker.ascx" tagname="ROCCalendarInput" tagprefix="uc1" %>
<%@ Register src="../Common/PagingControl.ascx" tagname="PagingControl" tagprefix="uc2" %>
<%@ Register src="../Common/PrintingButton2.ascx" tagname="PrintingButton2" tagprefix="uc3" %>
<%@ Register src="../EIVO/PNewInvalidInvoicePreview.ascx" tagname="PNewInvalidInvoicePreview" tagprefix="uc4" %>

<%@ Register src="../UI/PageAction.ascx" tagname="PageAction" tagprefix="uc5" %>
<%@ Register src="../UI/FunctionTitleBar.ascx" tagname="FunctionTitleBar" tagprefix="uc6" %>
<%@ Register src="../Common/EnumSelector.ascx" tagname="EnumSelector" tagprefix="uc7" %>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc5:PageAction ID="PageAction1" runat="server" ItemName="首頁 > 查詢發票/折讓" />        
        <!--交易畫面標題-->
        <uc6:FunctionTitleBar ID="FunctionTitleBar1" runat="server" ItemName="查詢發票/折讓" />
        <div id="border_gray">
        <!--表格 開始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
          <tr>
            <th colspan="2" class="Head_style_a">查詢條件</th>
            </tr>
          <tr>
            <th>查詢項目</th>
            <td class="tdleft">
                <asp:RadioButtonList ID="rdbSearchItem" RepeatColumns="5" 
                    RepeatDirection="Horizontal" runat="server" RepeatLayout="Flow">
                </asp:RadioButtonList>
            </td>
          </tr>
          <tr id="divReceiptNo" visible="false" runat="server">
            <th>統編</th>
            <td class="tdleft">
                <asp:DropDownList ID="ddlReceiptNo" CssClass="textfield" runat="server" 
                    AutoPostBack="True" onselectedindexchanged="ddlReceiptNo_SelectedIndexChanged" >
                    <asp:ListItem>全部</asp:ListItem>
                </asp:DropDownList>
            </td>
          </tr>
          <tr id="divGoogleID" visible="false" runat="server">
            <th><asp:Label ID="lblName" runat="server" /></th>
            <td class="tdleft"><asp:TextBox ID="txtGoogleID" runat="server"></asp:TextBox></td>
          </tr>
          <tr id="uxb2b" visible="false" runat="server">
            <th>查詢類別</th>
            <td class="tdleft">
                <asp:RadioButton ID="rdbType1" Checked="true" GroupName="R2" Text="依會員" AutoPostBack="true"
                    runat="server" oncheckedchanged="rdbType_CheckedChanged" />
                &nbsp;
                <asp:RadioButton ID="rdbType2" GroupName="R2" Text="依載具" AutoPostBack="true"
                    runat="server" oncheckedchanged="rdbType_CheckedChanged" />
                <asp:Label ID="lblDevice" Visible="false" runat="server" Text="依載具"></asp:Label>
                <asp:DropDownList ID="ddlDevice" CssClass="textfield" Visible="false" runat="server" 
                    AutoPostBack="True" onselectedindexchanged="ddlDevice_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
          </tr>
          <tr id="uxb2b1" visible="false" runat="server">
            <th>UXB2B條碼卡號</th>
            <td class="tdleft">
                <asp:TextBox ID="txtUxb2bBarCode" CssClass="textfield" Width="100" runat="server"></asp:TextBox>
              （共20碼）</td>
          </tr>
          <tr>
            <th width="20%">日期區間</th>
            <td class="tdleft">
            自&nbsp;<uc1:ROCCalendarInput ID="DateFrom" runat="server" />
        &nbsp;至&nbsp;<uc1:ROCCalendarInput ID="DateTo" runat="server" /></td>
          </tr>
          <tr>
              <th>
                  發票／折讓單號碼
              </th>
              <td class="tdleft">
                  <asp:TextBox ID="invoiceNo" runat="server"></asp:TextBox>
              </td>
          </tr>
        </table>
        <!--表格 結束-->
        </div>
        <!--按鈕-->
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td class="Bargain_btn">
                <asp:Button ID="btnSearch" CssClass="btn" runat="server" Text="查詢" onclick="btnSearch_Click" />
            </td>
          </tr>
        </table>

        <div id="divResult" visible="false" runat="server">
            <uc6:FunctionTitleBar ID="FunctionTitleBar2" runat="server" ItemName="查詢結果" />
            <!--表格 開始-->
            <div id="border_gray">
            <div runat="server" id="ResultTitle">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
            <tr>
                <td class="Head_style_a">若為當期發票或為折讓單，因尚未開獎或無法兌獎，故於「是否中獎」欄位呈現「N/A」</td>
            </tr>
            </table>
            <asp:GridView ID="gvEntity" runat="server" AutoGenerateColumns="False"
            Width="100%" GridLines="None" CellPadding="0" CssClass="table01"
                AllowPaging="True" ClientIDMode="Static" EnableViewState="false" >
            <Columns>
                <asp:TemplateField HeaderText="日期" > <ItemTemplate><%# ((dataType)Container.DataItem).ChineseInvoiceDate%></ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GoogleID" Visible="false" > <ItemTemplate><%# ((dataType)Container.DataItem).googleID%></ItemTemplate>  
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="序號"> <ItemTemplate><%# ((dataType)Container.DataItem).SID%></ItemTemplate>  
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="開立發票營業人" > <ItemTemplate><%# ((dataType)Container.DataItem).CompanyName%></ItemTemplate>  
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="統編" > <ItemTemplate><%# ((dataType)Container.DataItem).ReceiptNo%></ItemTemplate>  
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="發票/折讓號碼" > <ItemTemplate>                    
                    <asp:LinkButton ID="lbtn" runat="server" Text='<%# String.Format("{0}{1}",((dataType)Container.DataItem).TrackCode,((dataType)Container.DataItem).DonateMark.Equals("0") ? ((dataType)Container.DataItem).No : ((dataType)Container.DataItem).No.Substring(0,5)+"***")%>' 
                     CausesValidation="false" CommandName="Edit" OnClientClick='<%# Page.ClientScript.GetPostBackEventReference(this, String.Format("S:{0}",((dataType)Container.DataItem).InvoiceID)) + "; return false;" %>' />
                    </ItemTemplate>  
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="未稅金額" > <ItemTemplate><%#String.Format("{0:0,0.00}", ((dataType)Container.DataItem).SalesAmount)%></ItemTemplate>  
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="稅額" > <ItemTemplate><%#String.Format("{0:0,0.00}", ((dataType)Container.DataItem).TaxAmount)%></ItemTemplate>  
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="含稅金額" > <ItemTemplate><%#String.Format("{0:0,0.00}", ((dataType)Container.DataItem).TotalAmount)%></ItemTemplate>  
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="是否中獎" > <ItemTemplate><%# ((dataType)Container.DataItem).check??"N/A"%></ItemTemplate>  
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="捐贈單位" Visible="false" > <ItemTemplate><%# ((dataType)Container.DataItem).DonationName%></ItemTemplate>  
                    <ItemStyle HorizontalAlign="Center"  />
                </asp:TemplateField>     
                <asp:TemplateField HeaderText="買受人統編" > <ItemTemplate><%# ((dataType)Container.DataItem).BuyerReceiptNo%></ItemTemplate>  
                    <ItemStyle HorizontalAlign="Center"  />
                </asp:TemplateField>               
                <asp:TemplateField HeaderText="備註" > <ItemTemplate><%# getRemark(((dataType)Container.DataItem).memo)%></ItemTemplate>  
                    <ItemStyle HorizontalAlign="Center"  />
                </asp:TemplateField>
            </Columns>         
            <FooterStyle />
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle />
            <HeaderStyle />
            <AlternatingRowStyle CssClass="OldLace" />
                <PagerTemplate>
                    <span>
                    <uc2:PagingControl ID="pagingIndex" runat="server" />
                    </span>
                </PagerTemplate>
            <RowStyle />
            <EditRowStyle />
            </asp:GridView>
            
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table01">
            <tr>
                <td class="total-count" align="right">共 <asp:Label ID="lblRowCount" Text="0" runat="server"></asp:Label> 筆&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;總計金額：<asp:Label ID="lblTotalSum" Text="0" runat="server"></asp:Label></td>
            </tr>
            </table>
            </div>
            <!--表格 結束-->
            <center>
            <asp:Label ID="lblError" Visible="false" ForeColor="Red" Font-Size="Larger" runat="server"></asp:Label>
            </center>
            </div>            
            <!--按鈕-->
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="Bargain_btn">
                        <uc3:PrintingButton2 ID="PrintingButton21" runat="server" Visible="false" />
                    </td>
                </tr>
            </table>
            </div>
            <uc4:PNewInvalidInvoicePreview ID="PNewInvalidInvoicePreview1" runat="server" />
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="rdbType1" EventName="CheckedChanged" />
        <asp:AsyncPostBackTrigger ControlID="rdbType2" EventName="CheckedChanged" />
        <asp:AsyncPostBackTrigger ControlID="ddlDevice" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="ddlReceiptNo" EventName="SelectedIndexChanged" />
        <asp:PostBackTrigger ControlID="btnSearch" />
        <asp:PostBackTrigger ControlID="gvEntity" />
        <asp:PostBackTrigger ControlID="PrintingButton21" />
    </Triggers>
</asp:UpdatePanel>

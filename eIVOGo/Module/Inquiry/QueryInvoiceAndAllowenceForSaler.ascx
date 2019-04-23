<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QueryInvoiceAndAllowenceForSaler.ascx.cs" Inherits="eIVOGo.Module.Inquiry.QueryInvoiceAndAllowenceForSaler" %>

<!--路徑名稱-->
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="30"><img runat="server" enableviewstate="false" id="img5" 
            src="~/images/path_left.gif" alt="" width="30" height="29" /></td>
    <td bgcolor="#ecedd5">首頁 > 查詢/列印發票/折讓</td>
    <td width="18"><img runat="server" enableviewstate="false" id="img3" 
            src="~/images/path_right.gif" alt="" width="18" height="29" /></td>
  </tr>
</table>
<!--交易畫面標題-->
<h1><img runat="server" enableviewstate="false" id="img4" 
        src="~/images/icon_search.gif" width="29" height="28" border="0" 
        align="absmiddle" />查詢/列印發票/折讓</h1>
<div id="border_gray">
<!--表格 開始-->

<table width="100%" border="0" cellpadding="0" cellspacing="0" class="left_title">
  <tr>
    <th colspan="2" class="Head_style_a">查詢條件</th>
    </tr>
  <tr>
    <th>查詢項目</th>
    <td class="tdleft"><input type="radio" value="1" checked="checked" name="R1" />
      電子發票
        <input type="radio" value="2" name="R1" />
        電子折讓單&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="radio" value="3" name="R1" />
        作廢電子發票&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="radio" value="4" name="R1" />
        作廢電子折讓單&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="radio" value="5" name="R1" />
      全部&nbsp;&nbsp;&nbsp;&nbsp; </td>
  </tr>
  <tr>
    <th>查詢類別</th>
    <td class="tdleft"><input type="radio" value="V1" checked="checked" name="R2" id="member" />
      依會員&nbsp;&nbsp;&nbsp;
      <input type="radio" value="V1" name="R2" id="r_dev" />
      依載具
      <select name="D1" size="1" class="textfield" id="device">
        <option>-請選擇-</option>
        <option>UXB2B條碼卡</option>
        <option>悠遊卡</option>
      </select></td>
  </tr>
  <tr id="uxb2b">
    <th>UXB2B條碼卡號</th>
    <td class="tdleft"><input name="T" type="text" class="textfield" size="20" />
      （共20碼）</td>
  </tr>
  <tr>
    <th width="20%">日期區間</th>
    <td class="tdleft">
    自&nbsp;<input readonly name="appstartday" type="text" class="textfield" size="10" />
    &nbsp;&nbsp;<img runat="server" enableviewstate="false" id="img2" 
            src="~/images/date.gif" width="16" height="15" border="0" align="absmiddle" 
            style="cursor:hand;" onClick=calender(addForm.appstartday) />
    至&nbsp;<input readonly name="appendday" type="text" class="textfield" size="10" />
    &nbsp;<img runat="server" enableviewstate="false" id="img1" src="~/images/date.gif" width="16" height="15" border="0" align="absmiddle" style="cursor:hand;" onClick=calender(addForm.appendday) /></td>
  </tr>
</table>

<!--表格 結束-->
</div>
<!--按鈕-->
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td class="Bargain_btn"><input type="button" name="Submit" class="btn" value="查詢" onClick="window.location='queryInvoiceAndAllowence_list.htm'" /></td>
  </tr>
</table>

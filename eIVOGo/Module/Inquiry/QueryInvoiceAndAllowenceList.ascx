<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QueryInvoiceAndAllowenceList.ascx.cs" Inherits="eIVOGo.Module.Inquiry.QueryInvoiceAndAllowenceList" %>

<!--路徑名稱-->
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="30"><img runat="server" enableviewstate="false" id="img6" src="~/images/path_left.gif" alt="" width="30" height="29" /></td>
    <td bgcolor="#ecedd5">首頁 > 查詢/列印發票/折讓</td>
    <td width="18"><img runat="server" enableviewstate="false" id="img5" src="~/images/path_right.gif" alt="" width="18" height="29" /></td>
  </tr>
</table>
<!--交易畫面標題-->
<h1><img runat="server" enableviewstate="false" id="img4" src="~/images/icon_search.gif" width="29" height="28" border="0" align="absmiddle" />查詢/列印發票/折讓</h1>
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
        電子折讓單
        <input type="radio" value="3" name="R1" />
        作廢電子發票
        <input type="radio" value="4" name="R1" />
        作廢電子折讓單
        <input type="radio" value="5" name="R1" />
      全部</td>
  </tr>
  <tr>
    <th>查詢類別</th>
    <td class="tdleft">
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
    &nbsp;&nbsp;<img runat="server" enableviewstate="false" id="img3" src="~/images/date.gif" width="16" height="15" border="0" align="absmiddle" style="cursor:hand;" onClick=calender(addForm.appstartday) />
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
<h1><img runat="server" enableviewstate="false" id="img2" src="~/images/icon_search.gif" width="29" height="28" border="0" align="absmiddle" />查詢結果</h1>
<div id="border_gray">
  <!--表格 開始-->
  <table width="100%" border="0" cellpadding="0" cellspacing="0" id="table01">
    <tr>
      <td colspan="8" class="Head_style_a">若為當期發票或為折讓單，因尚未開獎或無法兌獎，故於「是否中獎」欄位呈現「N/A」</td>
    </tr>
    <tr>
      <th nowrap="nowrap">日期</th>
      <th nowrap="nowrap">開立發票營業人</th>
      <th nowrap="nowrap">統編</th>
      <th nowrap="nowrap">發票/折讓號碼</th>
      <th nowrap="nowrap">金額</th>
      <th nowrap="nowrap">是否中獎</th>
      <th width="120" nowrap="nowrap">捐贈單位</th>
    </tr>
    <tr>
      <td align="center">100年02月10日</td>
      <td>網際優勢</td>
      <td align="center">70762419</td>
      <td align="center"><a href="newInvalidInvoicePreview.htm" target="_blank">AY74423555</a></td>
      <td align="right">1,000</td>
      <td align="center" class="red">是</td>
      <td width="120" align="center">
      <div class="tdbonus">
        <a class="bonus" href="#">UB0001</a>
        <em>伊甸基金會</em>
      </div>
      </td>
    </tr>
    <tr class="OldLace">
      <td align="center">100年02月10日</td>
      <td>網際優勢</td>
      <td align="center">70762419</td>
      <td align="center"><a href="newInvalidInvoicePreview.htm" target="_blank">AY74423556</a></td>
      <td align="right">1,000</td>
      <td align="center">否</td>
      <td width="120" align="center">
      <div class="tdbonus">
        <a class="bonus" href="#">UB0001</a>
        <em>伊甸基金會</em>
      </div>
      </td>
    </tr>
    <tr>
      <td align="center">100年02月10日</td>
      <td>網際優勢</td>
      <td align="center">70762419</td>
      <td align="center"><a href="newInvalidInvoicePreview.htm" target="_blank">AY74423557</a></td>
      <td align="right">1,000</td>
      <td align="center">N/A</td>
      <td width="120" align="center">
      <div class="tdbonus">
        <a class="bonus" href="#">UB0001</a>
        <em>伊甸基金會</em>
      </div>
      </td>
    </tr>
    <tr class="OldLace">
      <td align="center">100年02月10日</td>
      <td>網際優勢</td>
      <td align="center">70762419</td>
      <td align="center"><a href="newInvalidInvoicePreview.htm" target="_blank">AY74423558</a></td>
      <td align="right">1,000</td>
      <td align="center" class="red">是</td>
      <td width="120" align="center">
      <div class="tdbonus">
        <a class="bonus" href="#">UB0002</a>
        <em>兒福聯盟</em>
      </div>
      </td>
    </tr>
    <tr>
      <td align="center">100年02月10日</td>
      <td>網際優勢</td>
      <td align="center">70762419</td>
      <td align="center"><a href="newInvalidInvoicePreview.htm" target="_blank">AY74423559</a></td>
      <td align="right">-1,000</td>
      <td align="center">N/A</td>
      <td width="120" align="center">
      <div class="tdbonus">
        <a class="bonus" href="#">UB0001</a>
        <em>伊甸基金會</em>
      </div>
      </td>
    </tr>
    <tr>
      <td colspan="8" align="right" class="total-count">共 100 筆&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;總計金額：1,000,000</td>
    </tr>
  </table>
  <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" id="table-count">
    <tr>
      <td>| 1 | <a href="#">2</a> |&nbsp; <a href="#">後10頁</a> | <a href="#">最後1頁</a>
        <input name="textfield" type="text" class="textfield" size="3" />
        <input name="cancel22" type="reset" class="btn" value="頁數" /></td>
      <td align="right" nowrap="nowrap"><span>總筆數：100&nbsp;&nbsp;&nbsp;總頁數：10</span></td>
    </tr>
  </table>
<!--表格 結束-->
</div>
<!--按鈕-->
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td class="Bargain_btn"><input name="B3" type="button" class="btn" value="資料列印" /></td>
  </tr>
</table>

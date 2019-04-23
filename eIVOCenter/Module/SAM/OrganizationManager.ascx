﻿<%@ Control Language="C#" AutoEventWireup="true" %>
<%@ Register Src="~/Module/UI/PageAction.ascx" TagName="PageAction" TagPrefix="uc1" %>
<%@ Register Src="~/Module/UI/FunctionTitleBar.ascx" TagName="FunctionTitleBar" TagPrefix="uc2" %>
<%@ Register Src="~/Module/Common/CalendarInputDatePicker.ascx" TagName="CalendarInputDatePicker"
    TagPrefix="uc3" %>
<%@ Register Src="~/Module/SAM/Business/OrganizationList.ascx" TagName="OrganizationList"
    TagPrefix="uc4" %>
<%@ Register src="~/Module/Common/PageAnchor.ascx" tagname="PageAnchor" tagprefix="uc5" %>
<uc1:PageAction ID="actionItem" runat="server" ItemName="首頁 &gt; 集團成員資料維護" />
<uc2:FunctionTitleBar ID="titleBar" runat="server" ItemName="集團成員資料維護" />
<%@ Import Namespace="Model.DataEntity" %>
<%@ Import Namespace="Utility" %>
<div class="border_gray">
    <!--表格 開始-->
    <table width="100%" class="left_title" border="0" cellspacing="0" cellpadding="0">
        <tbody>
            <tr>
                <th class="Head_style_a" colspan="2">查詢條件
            </th>
            </tr>
            <tr>
                <th width="120" nowrap="nowrap">統編
            </th>
                <td class="tdleft">
                    <input name="receiptNo" class="textfield" id="receiptNo" type="text">
                </td>
            </tr>
            <tr>
                <th width="120" nowrap="nowrap">企業名稱
            </th>
                <td class="tdleft">
                    <input name="companyName" class="textfield" id="companyName" type="text">
                </td>
            </tr>
            <tr>
                <th width="120" nowrap="nowrap">發票開立集團狀態
            </th>
                <td class="tdleft">
                    <select name="groupStatus" id="groupStatus">
                        <option selected="selected" value="A">全部</option>
                        <option value="Y">已啟用</option>
                        <option value="N">已停用</option>
                    </select>
                </td>
            </tr>
        </tbody>
    </table>
    <!--表格 結束-->
</div>
<table border="0" cellspacing="0" cellpadding="0" width="100%" id="tblQuery">
    <tbody>
        <tr>
            <td class="Bargain_btn">
                <input type="button" name="btnQuery" value="查詢" id="btnQuery" />
            </td>
        </tr>
    </tbody>
</table>
<script>
    submitPagingIndex = undefined;

    $(function () {
        $("#btnQuery").on("click", function (ev, data) {
            var $this = $(this);
            $("form").ajaxForm({
                url: 'ListOrganization.aspx',
                success: function (data) {
                    $('#result').remove();
                    $('#tblQuery').after($(data).find('#result'));
                }
            }).submit();
        });

        submitPagingIndex = function (pageNum) {
            $("#btnQuery").click();
        }
    });

</script>
<script runat="server">

    public static void BuildQuery(eIVOCenter.Module.SAM.Business.OrganizationList itemList)
    {
        var Request = HttpContext.Current.Request;
        
        System.Linq.Expressions.Expression<Func<Organization, bool>> queryExpr = f => true;
        if(!String.IsNullOrEmpty(Request["receiptNo"]))
        {
            queryExpr = queryExpr.And(c => c.ReceiptNo == Request["receiptNo"].Trim());
        }
        if (!String.IsNullOrEmpty(Request["companyName"]))
        {
            queryExpr = queryExpr.And(c => c.CompanyName.Contains(Request["companyName"].Trim()));
        }
        
        if(Request["groupStatus"]=="Y")
        {
            queryExpr = queryExpr.And(c => c.EnterpriseGroupMember.Count > 0);
        }
        else if (Request["groupStatus"] == "N")
        {
            queryExpr = queryExpr.And(c => c.EnterpriseGroupMember.Count == 0);
        }

        itemList.BuildQuery = table =>
            {
                return table.Where(queryExpr)
                    .OrderBy(c => c.ReceiptNo);
            };
    }
</script>
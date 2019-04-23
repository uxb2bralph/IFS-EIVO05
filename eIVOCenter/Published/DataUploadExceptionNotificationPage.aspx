<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Register Src="~/Module/UI/UploadDataExceptionNotification.ascx" TagName="UploadDataExceptionNotification"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        下列資料由商家上傳時,發生錯誤,請處理!!</div>
    <br />
    <uc2:UploadDataExceptionNotification ID="notification" runat="server" />
    </form>
</body>
</html>
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        
        int companyID;
        if (!String.IsNullOrEmpty(Request["companyID"]) && int.TryParse(Request["companyID"], out companyID))
        {
            notification.QueryExpr = d => d.ExceptionReplication != null && d.CompanyID == companyID;
        }
        else
        {
            notification.QueryExpr = d => d.ExceptionReplication != null;
        }
    }
</script>
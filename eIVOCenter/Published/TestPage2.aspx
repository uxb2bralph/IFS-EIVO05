<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="Visitor" %>

<%@ Import Namespace="Utility" %>
<%@ Import Namespace="Uxnet.Web.WebUI" %>
<%@ Register Src="../Module/Flow/DocumentFlowItem.ascx" TagName="DocumentFlowItem"
    TagPrefix="uc1" %>
<%@ Register Src="../Module/Flow/DocumentFlowControlItem.ascx" TagName="DocumentFlowControlItem"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.js"></script>
    <title></title>
    <style type="text/css">
        .test
        {
            overflow: scroll;
            height: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" /><uc2:DocumentFlowControlItem
        ID="modalItem" runat="server" />
    </form>
</body>
</html>
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //        Page.Error += new EventHandler(Page_Error);
        if (!Business.Helper.WebPageUtility.Logon)
        {
            Business.Workflow.LoginController login = new Business.Workflow.LoginController();
            login.ProcessLogin("ifsadmin");
        }
        if (Request["id"] != null)
        {
        }
        else if (Page.PreviousPage != null)
        {
        }
        this.Load += new EventHandler(published_testpage_aspx_Load);
        modalItem.FlowID = 9;
    }

    void Selector_DataBound(object sender, EventArgs e)
    {
    }

    void published_testpage_aspx_Load(object sender, EventArgs e)
    {
        //if (this.IsPostBack)
        //{
        //    PRODUCTS_DATA item = ((String)ViewState["item"]).DeserializeDataContract<PRODUCTS_DATA>();
        //    dsEntity.CreateDataManager().EntityList.Attach(item);
        //}
        //else
        //{
        //    ViewState["item"] = dsEntity.CreateDataManager().EntityList.First().SerializeDataContract();
        //}
    }

    //void Page_Error(object sender, EventArgs e)
    //{
    //    Exception ex = Server.GetLastError();
    //    if (ex != null)
    //    {
    //        Page.Items["error"] = ex;
    //        Server.ClearError();
    //        Server.Execute("~/Published/ErrorPage.aspx", Response.Output);
    //    }
    //}

    protected void Button1_Click(object sender, EventArgs e)
    {
        modalItem.Show();
    }
</script>

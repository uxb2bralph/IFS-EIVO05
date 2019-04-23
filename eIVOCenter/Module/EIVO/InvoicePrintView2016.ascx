<%@ Control Language="C#" AutoEventWireup="true" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<%@ Register Src="~/Module/EIVO/Item/InvoiceView2016.ascx" TagPrefix="uc1" TagName="InvoiceView2016" %>

<%@ Import Namespace="Utility" %>
<%@ Import Namespace="Model.DataEntity" %>
<uc1:InvoiceView2016 runat="server" ID="itemView" />
<cc1:InvoiceDataSource ID="dsEntity" runat="server">
</cc1:InvoiceDataSource>
<script runat="server">

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        initializeData();
    }

    protected virtual void initializeData()
    {
        int invoiceID;
        if (Request.GetRequestValue("id", out invoiceID))
        {
            var mgr = dsEntity.CreateDataManager();
            itemView.Item = mgr.GetTable<InvoiceItem>().Where(i => i.InvoiceID == invoiceID).FirstOrDefault();
        }
        else if (Request["no"] != null && Request["no"].Length == 10)
        {
            var mgr = dsEntity.CreateDataManager();
            itemView.Item = mgr.GetTable<InvoiceItem>().Where(i => i.TrackCode == Request["no"].Substring(0, 2)
                && i.No == Request["no"].Substring(2)).FirstOrDefault();
        }
    }    
    
</script>

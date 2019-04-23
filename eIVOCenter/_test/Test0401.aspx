<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="Uxnet.Web.WebUI" %>
<%@ Import Namespace="Utility" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="Model.Helper" %>
<%@ Import Namespace="eIVOCenter.Helper" %>
<%@ Import Namespace="Model.InvoiceManagement" %>
<%@ Import Namespace="Model.Locale" %>

<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>Invoice No:
        <textarea cols="80" rows="10" name="invoiceNo"><%= Request["invoiceNo"] %></textarea><br />
            Track Code:<input type="text" name="trackCode" value="<%= Request["trackCode"] %>" />
            No: <input type="text" name="startNo" value="<%= Request["startNo"] %>" /> ~ 
            <input type="text" name="endNo" value="<%= Request["endNo"] %>" />            
            <asp:Button ID="btnCreate" runat="server" Text="OK!!" />
            <br />
            Invoice Date:<input type="text" name="dateFrom" value="<%= Request["dateFrom"] %>" />~<input type="text" name="dateTo" value="<%= Request["dateTo"] %>" /> 
            <asp:Button ID="btnCreatePDF" runat="server" Text="Create PDF" OnClick="btnCreatePDF_Click" />
        </div>
        <cc1:InvoiceDataSource ID="dsEntity" runat="server">
        </cc1:InvoiceDataSource>
    </form>
</body>
</html>
<script runat="server">

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        doTask();
    }

    void doTask()
    {
        String invoiceNo = Request["invoiceNo"];
        if (invoiceNo != null)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(t =>
            {
                String[] items = invoiceNo.Split(new String[] { "\r\n", ",", ";", "、" }, StringSplitOptions.RemoveEmptyEntries);
                using (InvoiceManager mgr = new InvoiceManager())
                {
                    foreach (var invNo in items)
                    {
                        if (invNo.Length != 10)
                            break;
                        var item = mgr.EntityList.Where(i => i.TrackCode == invNo.Substring(0, 2)
                            && i.No == invNo.Substring(2)).FirstOrDefault();
                        if (item != null)
                        {
                            storedMIG(item);
                        }
                    }
                }
            });

        }
        else if (Request["trackCode"] != null)
        {
            System.Linq.Expressions.Expression<Func<InvoiceItem, bool>> queryExpr = i => i.TrackCode == Request["trackCode"];
            bool hasNo = false;
            if (Request["startNo"] != null)
            {
                queryExpr = queryExpr.And(i => String.Compare(i.No, Request["startNo"]) >= 0);
                hasNo = true;
            }
            if (Request["endNo"] != null)
            {
                queryExpr = queryExpr.And(i => String.Compare(i.No, Request["endNo"]) <= 0);
                hasNo = true;
            }
            if (hasNo)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(t =>
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        foreach (var item in mgr.EntityList.Where(queryExpr))
                        {
                            storedMIG(item);
                        }
                    }
                });
            }
        }

    }

    void storedMIG(InvoiceItem item)
    {
        String fileName;
        if (item.InvoiceSeller.Organization.OrganizationStatus != null && item.InvoiceSeller.Organization.OrganizationStatus.IronSteelIndustry == true)
        {
            fileName = Path.Combine(@"D:\EINVTurnkey\UpCast\B2BSTORAGE\A1401\SRC", String.Format("A1401-{0}{1}.xml", item.TrackCode, item.No));
            item.CreateA1401().ConvertToXml().Save(fileName);
        }
        else
        {
            fileName = Path.Combine(@"D:\EINVTurnkey\UpCast\B2BSTORAGE\A0401\SRC", String.Format("A0401-{0}{1}.xml", item.TrackCode, item.No));
            item.CreateA0401().ConvertToXml().Save(fileName);
        }
    }


    protected void btnCreatePDF_Click(object sender, EventArgs e)
    {
        DateTime dateFrom, dateTo;
        if (DateTime.TryParse(Request["dateFrom"], out dateFrom) && DateTime.TryParse(Request["dateTo"], out dateTo))
        {
            System.Threading.ThreadPool.QueueUserWorkItem(info =>
            {
                try
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        var items = mgr.GetTable<InvoiceItem>()
                            .Where(i => i.InvoiceDate >= dateFrom && i.InvoiceDate < dateTo.AddDays(1))
                            .Where(i => i.Organization.EnterpriseGroupMember.Any(g => g.EnterpriseID == (int)Naming.EnterpriseGroup.SOGO百貨)
                                && i.InvoiceBuyer.Organization.OrganizationStatus.EntrustToPrint == true);

                        foreach (var item in items)
                        {
                            Logger.Debug("PDF => " + mgr.PrepareToDownload(item, false));
                        }
                    }
                }
                catch(Exception ex)
                {
                    Logger.Error(ex);
                }
            });
        }
    }
</script>

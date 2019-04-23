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
        <%= DateTime.Now %><br />
        <div>Invoice No:
        <textarea cols="80" rows="10" name="invoiceNo"><%= invoiceNo ?? msg %></textarea><br />
            Track Code:<input type="text" name="trackCode" value="<%= Request["trackCode"] %>" />
            No: <input type="text" name="startNo" value="<%= Request["startNo"] %>" /> ~ 
            <input type="text" name="endNo" value="<%= Request["endNo"] %>" />            
            <asp:Button ID="btnCreate" runat="server" Text="OK!!" />
            <br />
        </div>
        <cc1:InvoiceDataSource ID="dsEntity" runat="server">
        </cc1:InvoiceDataSource>
    </form>
</body>
</html>
<script runat="server">

    String msg;
    String invoiceNo;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        doTask();
    }

    void doTask()
    {
        invoiceNo = Request["invoiceNo"].GetEfficientString();
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
                            storedMIG(mgr,item);
                        }
                    }
                }
            });

        }
        else if (Request["trackCode"] != null)
        {
            String trackCode = Request["trackCode"].GetEfficientString();
            String startNo = Request["startNo"].GetEfficientString();
            String endNo = Request["endNo"].GetEfficientString();

            //if (startNo!=null || endNo!=null)
            //{
            //System.Threading.ThreadPool.QueueUserWorkItem(t =>
            //{
            using (InvoiceManager mgr = new InvoiceManager())
            {
                IQueryable<InvoiceItem> items = mgr.GetTable<InvoiceItem>()
                    .Where(i => i.InvoiceCancellation == null)
                    .Where(i => i.TrackCode == trackCode);
                if (startNo != null)
                    items = items.Where(i => String.Compare(i.No, startNo) >= 0);
                if (endNo != null)
                    items = items.Where(i => String.Compare(i.No, endNo) <= 0);

                //msg = items.ToString() + ":" + items.Count();
                msg = "";
                foreach (var item in items)
                {
                    storedMIG(mgr, item);
                }
            }
            //});
            //}
        }

    }

    void storedMIG(InvoiceManager mgr, InvoiceItem item)
    {
        var cancelItem = item.InvoiceCancellation = new InvoiceCancellation
        {
            InvoiceID = item.InvoiceID,
            CancellationNo = item.TrackCode + item.No,
            CancelReason = "發票作廢",
            CancelDate = DateTime.Now,
            Remark = "發票作廢",
        };

        int ownerID = item.CDS_Document.DocumentOwner.OwnerID;

        var doc = new DerivedDocument
        {
            CDS_Document = new CDS_Document
            {
                DocType = (int)Naming.DocumentTypeDefinition.E_InvoiceCancellation,
                DocDate = DateTime.Now,
                CurrentStep = (int)Naming.B2BInvoiceStepDefinition.待傳送,
                DocumentOwner = new DocumentOwner
                {
                    OwnerID = ownerID
                }
            },
            SourceID = item.InvoiceID
        };

        var flow = mgr.GetTable<DocumentTypeFlow>().Where(f => f.TypeID == (int)Naming.B2BInvoiceDocumentTypeDefinition.作廢發票
            && f.CompanyID == ownerID && f.BusinessID == (int)Naming.InvoiceCenterBusinessType.銷項).FirstOrDefault();

        if (flow != null && flow.DocumentFlow.InitialStep.HasValue)
        {
            var initialStep = flow.DocumentFlow.DocumentFlowControl;
            doc.CDS_Document.CurrentStep = initialStep.LevelID;

            doc.CDS_Document.DocumentFlowStep = new DocumentFlowStep
            {
                CurrentFlowStep = initialStep.StepID
            };
        }

        mgr.GetTable<InvoiceCancellation>().InsertOnSubmit(cancelItem);
        mgr.GetTable<DerivedDocument>().InsertOnSubmit(doc);

        mgr.SubmitChanges();
        //msg += (cancelItem.CancellationNo + "\r\n");
    }

</script>

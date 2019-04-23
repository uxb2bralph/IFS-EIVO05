<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Register Assembly="Model" Namespace="Model.DataEntity" TagPrefix="cc1" %>
<%@ Import Namespace="Model.Locale" %>
親愛的客戶您好, 您在<%# _item.DocType==(int)Naming.DocumentTypeDefinition.E_Invoice ? _item.InvoiceItem.InvoiceSeller.CustomerName
           : _item.DocType==(int)Naming.DocumentTypeDefinition.E_InvoiceCancellation ? _item.DerivedDocument.ParentDocument.InvoiceItem.InvoiceSeller.CustomerName 
           : _item.DocType==(int)Naming.DocumentTypeDefinition.E_Allowance ? _item.InvoiceAllowance.InvoiceAllowanceBuyer.CustomerName
           : _item.DocType==(int)Naming.DocumentTypeDefinition.E_AllowanceCancellation ? _item.DerivedDocument.ParentDocument.InvoiceAllowance.InvoiceAllowanceBuyer.CustomerName
           : "" %>採購之<%# ((Naming.B2BInvoiceDocumentTypeDefinition)_item.DocType).ToString() %>已開立
發票號碼: 
<%# _item.DocType==(int)Naming.DocumentTypeDefinition.E_Invoice ? String.Format("{0}{1}",_item.InvoiceItem.TrackCode,_item.InvoiceItem.No)
               : _item.DocType == (int)Naming.DocumentTypeDefinition.E_InvoiceCancellation ? String.Format("{0}{1}", _item.DerivedDocument.ParentDocument.InvoiceItem.TrackCode, _item.DerivedDocument.ParentDocument.InvoiceItem.No)
           : _item.DocType==(int)Naming.DocumentTypeDefinition.E_Allowance ? _item.InvoiceAllowance.AllowanceNumber
           : _item.DocType==(int)Naming.DocumentTypeDefinition.E_AllowanceCancellation ? _item.DerivedDocument.ParentDocument.InvoiceAllowance.AllowanceNumber
           : "" %>
開立日期: 
<%# _item.DocType==(int)Naming.DocumentTypeDefinition.E_Invoice ? String.Format("{0:yyyy/MM/dd}",_item.InvoiceItem.InvoiceDate)
                   : _item.DocType == (int)Naming.DocumentTypeDefinition.E_InvoiceCancellation ? String.Format("{0:yyyy/MM/dd}", _item.DerivedDocument.ParentDocument.InvoiceItem.InvoiceCancellation.CancelDate)
               : _item.DocType == (int)Naming.DocumentTypeDefinition.E_Allowance ? String.Format("{0:yyyy/MM/dd}", _item.InvoiceAllowance.AllowanceDate)
               : _item.DocType == (int)Naming.DocumentTypeDefinition.E_AllowanceCancellation ? String.Format("{0:yyyy/MM/dd}", _item.DerivedDocument.ParentDocument.InvoiceAllowance.InvoiceAllowanceCancellation.CancelDate)
           : "" %>
消費金額: $<%# _item.DocType==(int)Naming.DocumentTypeDefinition.E_Invoice ? String.Format("{0}",_item.InvoiceItem.InvoiceAmountType.TotalAmount)
                   : _item.DocType == (int)Naming.DocumentTypeDefinition.E_InvoiceCancellation ? String.Format("{0}", _item.DerivedDocument.ParentDocument.InvoiceItem.InvoiceAmountType.TotalAmount)
               : _item.DocType == (int)Naming.DocumentTypeDefinition.E_Allowance ? String.Format("{0}", _item.InvoiceAllowance.TotalAmount)
               : _item.DocType == (int)Naming.DocumentTypeDefinition.E_AllowanceCancellation ? String.Format("{0}", _item.DerivedDocument.ParentDocument.InvoiceAllowance.TotalAmount)
           : "" %>
<cc1:DocumentDataSource ID="dsEntity" runat="server">
</cc1:DocumentDataSource>
<script runat="server">

    CDS_Document _item;
    
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.PreRender += new EventHandler(published_smsinvoicenotification_aspx_PreRender);
    }

    void published_smsinvoicenotification_aspx_PreRender(object sender, EventArgs e)
    {
        int docID;
        if (!String.IsNullOrEmpty(Request["id"]) && int.TryParse(Request["id"],out docID))
        {
            _item = dsEntity.CreateDataManager().EntityList.Where(d => d.DocID == docID).FirstOrDefault();
            if (_item != null)
            {
                this.DataBind();
            }
            else
            {
                this.Visible = false;
            }
        }
    }
</script>

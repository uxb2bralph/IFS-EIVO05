using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

using Model.DataEntity;
using Model.Locale;
using Model.Schema.EIVO.B2B;
using Model.Schema.EIVO.E0402;
using Utility;

namespace eIVOCenter.Module.UI
{
    public partial class UploadDataExceptionNotification : System.Web.UI.UserControl
    {
        protected SellerInvoiceRootInvoice _invoiceItem;
        protected BuyerInvoiceRootInvoice _buyerInvoice;
        protected CancelInvoiceRootCancelInvoice _cancelItem;
        protected AllowanceRootAllowance _allowance;
        protected CancelAllowanceRootCancelAllowance _cancelAllowance;
        protected ReceiptRootReceipt _receipt;
        protected CancelReceiptRootCancelReceipt _cancelReceipt;
        protected BranchTrackBlank _BranchTrackBlank;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public Expression<Func<ExceptionLog, bool>> QueryExpr
        {
            get;
            set;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.PreRender += new EventHandler(GovPlatformNotification_PreRender);
        }

        protected String getInvoiceContent(String data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            _invoiceItem = null;
            _buyerInvoice = null;
            if (doc["SellerInvoiceRootInvoice"] != null)
            {
                _invoiceItem = doc.ConvertTo<SellerInvoiceRootInvoice>();
                return _invoiceItem.InvoiceNumber;
            }
            else if (doc["BuyerInvoiceRootInvoice"] != null)
            {
                _buyerInvoice = doc.ConvertTo<BuyerInvoiceRootInvoice>();
                return _buyerInvoice.DataNumber + "(進項發票單據號碼)";
            }
            return null;
        }

        protected String getCancellationContent(String data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            _cancelItem = doc.ConvertTo<CancelInvoiceRootCancelInvoice>();
            return _cancelItem.CancelInvoiceNumber;
        }

        protected String getAllowanceContent(String data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            _allowance = doc.ConvertTo<AllowanceRootAllowance>();
            return _allowance.AllowanceNumber;
        }

        protected String getCancelAllowanceContent(String data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            _cancelAllowance = doc.ConvertTo<CancelAllowanceRootCancelAllowance>();
            return _cancelAllowance.CancelAllowanceNumber;
        }

        protected String getReceiptContent(String data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            _receipt = doc.ConvertTo<ReceiptRootReceipt>();
            return _receipt.ReceiptNumber;
        }

        protected String getCancelReceiptContent(String data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            _cancelReceipt = doc.ConvertTo<CancelReceiptRootCancelReceipt>();
            return _cancelReceipt.CancelReceiptNumber;
        }
        protected String getBranchTrackBlankContent(String data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            _BranchTrackBlank = doc.ConvertTo<BranchTrackBlank>();
            return _BranchTrackBlank.Main.BranchBan;
        }


        void GovPlatformNotification_PreRender(object sender, EventArgs e)
        {
            var mgr = dsInv.CreateDataManager();
            IQueryable<ExceptionLog> logItems = QueryExpr != null ? mgr.GetTable<ExceptionLog>().Where(QueryExpr) : mgr.GetTable<ExceptionLog>();
            var invItems = logItems.Where(r => r.TypeID == (int)Naming.DocumentTypeDefinition.E_Invoice);
            gvInvoice.DataSource = invItems;
            gvInvoice.DataBind();

            var cancelItems = logItems.Where(r => r.TypeID == (int)Naming.DocumentTypeDefinition.E_InvoiceCancellation);
            gvCancel.DataSource = cancelItems;
            gvCancel.DataBind();

            var allowanceItems = logItems.Where(r => r.TypeID == (int)Naming.DocumentTypeDefinition.E_Allowance);
            gvAllowance.DataSource = allowanceItems;
            gvAllowance.DataBind();

            var cancelAllowanceItems = logItems.Where(r => r.TypeID == (int)Naming.DocumentTypeDefinition.E_AllowanceCancellation);
            gvCancelAllowance.DataSource = cancelAllowanceItems;
            gvCancelAllowance.DataBind();

            var receiptItems = logItems.Where(r => r.TypeID == (int)Naming.DocumentTypeDefinition.E_Receipt);
            gvReceipt.DataSource = receiptItems;
            gvReceipt.DataBind();

            var cancelReceiptItems = logItems.Where(r => r.TypeID == (int)Naming.DocumentTypeDefinition.E_ReceiptCancellation);
            gvCancelReceipt.DataSource = cancelReceiptItems;
            gvCancelReceipt.DataBind();

            var branchtrackblankItems = logItems.Where(r => r.TypeID == (int)Naming.DocumentTypeDefinition.BranchTrackBlank);
            gvBranchTrackBlank.DataSource = branchtrackblankItems;
            gvBranchTrackBlank.DataBind();
        }
    }
}
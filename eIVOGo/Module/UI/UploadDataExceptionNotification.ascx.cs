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
using Model.Schema.EIVO;
using Utility;

namespace eIVOGo.Module.UI
{
    public partial class UploadDataExceptionNotification : System.Web.UI.UserControl
    {
        protected InvoiceRootInvoice _invoiceItem;
        protected CancelInvoiceRootCancelInvoice _cancelItem;

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
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                _invoiceItem = doc.ConvertTo<InvoiceRootInvoice>();
                return _invoiceItem.InvoiceNumber;
            }
            catch (Exception ex)
            {
                return data;
            }
        }

        protected String getCancellationContent(String data)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                _cancelItem = doc.ConvertTo<CancelInvoiceRootCancelInvoice>();
                return _cancelItem.CancelInvoiceNumber;
            }
            catch (Exception ex)
            {
                return data;
            }
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
        }
    }
}
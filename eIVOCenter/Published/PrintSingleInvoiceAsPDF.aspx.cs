using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eIVOCenter.Helper;
using eIVOCenter.Properties;
using Utility;


namespace eIVOCenter.Published
{
    public partial class PrintSingleInvoiceAsPDF : System.Web.UI.Page
    {
        protected String _singleInvoiceUrl = "~/Published/PrintSingleInvoicePage.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            createInvoicePDF();
        }

        private void createInvoicePDF()
        {
            String pdfFile = Server.CreateContentAsPDF(_singleInvoiceUrl, Session.Timeout);
            if (pdfFile != null)
            {
                if (Request["nameOnly"] != null)
                {
                    String outputFile = Path.Combine(Logger.LogDailyPath, Path.GetFileName(pdfFile));
                    File.Move(pdfFile, outputFile);
                    Response.Write(outputFile);
                    Response.End();
                }
                else
                {
                    Response.WriteFileAsDownload(pdfFile, String.Format("{0:yyyy-MM-dd}.pdf", DateTime.Today), true);
                }
            }
            else
            {
                Response.Output.WriteLine("系統忙錄中，請稍後再試...");
                Response.End();
            }
        }

        [Bindable(true)]
        public String PrintSingleInvoiceUrl
        {
            get
            {
                return _singleInvoiceUrl;
            }
            set
            {
                _singleInvoiceUrl = value;
            }
        }
    }
}
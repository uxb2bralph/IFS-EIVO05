using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.InvoiceManagement;
using Model.DataEntity;
using System.Xml;
using Model.Locale;

namespace eIVOGo.Published
{
    /// <summary>
    /// Summary description for DumpExceptionLog
    /// </summary>
    public class DumpExceptionLog : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/xml";
            HttpResponse Response = context.Response;
            HttpRequest Request = context.Request;

            int logID;

            if (!String.IsNullOrEmpty(Request["logID"]) && int.TryParse(Request["logID"], out logID))
            {
                using (InvoiceManager mgr = new InvoiceManager())
                {
                    var item = mgr.GetTable<ExceptionLog>().Where(e => e.LogID == logID).FirstOrDefault();
                    if (item != null)
                    {
                        if (item.IsCSV == true)
                        {
                            context.Response.ContentType = "text/plain";
                            Response.Write(item.DataContent);
                            Response.Flush();
                        }
                        else
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(item.DataContent);
                            XmlWriter xtw = XmlWriter.Create(Response.OutputStream);
                            //xtw.Settings.Indent = true;
                            xtw.WriteStartDocument(true);
                            switch ((Naming.DocumentTypeDefinition)item.TypeID)
                            {
                                case Naming.DocumentTypeDefinition.E_Invoice:
                                    xtw.WriteStartElement("Invoice");
                                    break;
                                case Naming.DocumentTypeDefinition.E_InvoiceCancellation:
                                    xtw.WriteStartElement("CancelInvoice");
                                    break;
                                case Naming.DocumentTypeDefinition.E_Allowance:
                                    xtw.WriteStartElement("Allowance");
                                    break;
                                case Naming.DocumentTypeDefinition.E_AllowanceCancellation:
                                    xtw.WriteStartElement("CancelAllowance");
                                    break;
                                default:
                                    xtw.WriteStartElement("Undefined");
                                    break;
                            }
                            doc.DocumentElement.WriteContentTo(xtw);
                            xtw.WriteEndElement();
                            xtw.Flush();
                            xtw.Close();
                        }
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
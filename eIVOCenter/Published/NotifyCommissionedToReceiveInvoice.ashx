<%@ WebHandler Language="C#" Class="eIVOCenter.Published.NotifyCommissionedToReceiveInvoice" %>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Xml;

using eIVOCenter.Helper;
using eIVOGo.Properties;
using Model.DataEntity;
using Model.DocumentManagement;
using Model.Helper;
using Model.InvoiceManagement;
using Model.Locale;
using Model.Schema.EIVO.B2B;
using Model.Schema.MIG3_1;
using Model.Schema.TurnKey;
using Model.Schema.TXN;
using Utility;
using Uxnet.Com.Security.UseCrypto;
using Uxnet.Web.Helper;

namespace eIVOCenter.Published
{
    /// <summary>
    /// Summary description for NotifyCommissionedToReceiveInvoice
    /// </summary>
    public class NotifyCommissionedToReceiveInvoice : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var Request = context.Request;
            var Response = context.Response;
            var Context = HttpContext.Current;

            int invoiceID;
            if (!String.IsNullOrEmpty(Request["invoiceID"]) && int.TryParse(Request["invoiceID"],out invoiceID))
            {
                var cacheKey = String.Format("INV:{0}", Thread.CurrentThread.ManagedThreadId);

                try
                {

                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        var item = mgr.EntityList.Where(i => i.InvoiceID == invoiceID).FirstOrDefault();

                        if (item == null)
                            return;

                        String pdfFile = string.Empty;

                        pdfFile = mgr.PrepareToDownload(item, Request["isMail"] == "true");

                        if (item.Organization.EnterpriseGroupMember.Any(g => g.EnterpriseID == (int)Naming.EnterpriseGroup.SOGO百貨)
                            && item.InvoiceBuyer.Organization.OrganizationStatus.EntrustToPrint == true)
                        {
                            ///不用寄送Email通知
                            ///
                        }
                        else
                        {

                            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(pdfFile, MediaTypeNames.Application.Octet);
                            ///修改附件檔名為發票號碼
                            ///
                            attachment.Name = String.Format("{0}{1}.pdf", item.TrackCode, item.No);

                            String mailTo = String.Join(",",
                                mgr.GetUserListByCompanyID(item.InvoiceBuyer.BuyerID.Value)
                                .Select(u => u.EMail)
                                .Where(m => m != null));

                            if (!String.IsNullOrEmpty(mailTo))
                            {

                                Context.Cache[cacheKey] = item;

                                "~/Published/NotifyAll.aspx".MailServerPage(
                                    String.Format("{0}{1}-{2}{3}", item.TrackCode, item.No, item.Organization.EnterpriseGroupMember.FirstOrDefault().EnterpriseGroup.EnterpriseName, " 自動接收通知"),
                                    new System.Net.Mail.Attachment[] { attachment }, mailTo);

                                Logger.Info(string.Format("系統寄送已自動接收發票資料:{0}{1} 至相對營業人 {2}(Source:{3}) ", item.TrackCode, item.No, item.InvoiceBuyer.ReceiptNo, pdfFile));

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
                finally
                {
                    Context.Cache.Remove(cacheKey);
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

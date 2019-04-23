using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Schema.EIVO;
using System.Xml;
using InvoiceClient.Properties;
using Utility;
using System.Threading;
using System.IO;

namespace InvoiceClient.Agent
{

    public class InvoiceWelfareInspector
    {
        private bool _isRunning;

        public InvoiceWelfareInspector() { }

        public SocialWelfareAgenciesRoot GetUpdatedData()
        {
            WS_Invoice.eInvoiceService invSvc = InvoiceWatcher.CreateInvoiceService();

            try
            {
                XmlNode doc = invSvc.GetUpdatedWelfareAgenciesInfo(Settings.Default.SellerReceiptNo);
                if (doc != null)
                {
                    String storedPath = Path.Combine(Settings.Default.InvoiceTxnPath, Settings.Default.WelfareInfoFolder);
                    storedPath.CheckStoredPath();
                    String path = Path.Combine(storedPath, String.Format("{0:yyyyMMddHHmmssf}_SWA.xml", DateTime.Now));
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.Encoding = Settings.Default.OutputEncodingWithoutBOM ? new UTF8Encoding() : Encoding.GetEncoding(Settings.Default.OutputEncoding);
                    doc.Save(path, settings);
                    return doc.ConvertTo<SocialWelfareAgenciesRoot>();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        public SocialWelfareAgenciesRoot GetAll()
        {
            WS_Invoice.eInvoiceService invSvc = InvoiceWatcher.CreateInvoiceService();

            try
            {
                XmlNode doc = invSvc.GetWelfareAgenciesInfo(Settings.Default.SellerReceiptNo);
                if (doc != null)
                {
                    String storedPath = Path.Combine(Settings.Default.InvoiceTxnPath, Settings.Default.WelfareInfoFolder);
                    storedPath.CheckStoredPath();
                    String path = Path.Combine(storedPath, String.Format("{0:yyyyMMddHHmmssf}_SWA.xml", DateTime.Now));
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.Encoding = Settings.Default.OutputEncodingWithoutBOM ? new UTF8Encoding() : Encoding.GetEncoding(Settings.Default.OutputEncoding);
                    doc.Save(path, settings);
                    return doc.ConvertTo<SocialWelfareAgenciesRoot>();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }


        public void StartUp()
        {
            if (Settings.Default.IsAutoWelfare && !_isRunning)
            {
                ThreadPool.QueueUserWorkItem(p =>
                {
                    while (Settings.Default.IsAutoWelfare)
                    {
                        _isRunning = true;
                        GetUpdatedData();
                        Thread.Sleep(Settings.Default.AutoWelfareInterval > 0 ? Settings.Default.AutoWelfareInterval * 60 * 1000 : 180000);
                    }
                    _isRunning = false;
                });
            }
        }
    }
}

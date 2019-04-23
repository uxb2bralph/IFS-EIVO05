using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

using InvoiceClient.Helper;
using InvoiceClient.Properties;
using Model.Schema.EIVO;
using Model.Schema.EIVO.B2B;
using Model.Schema.TXN;
using Utility;
using System.Text.RegularExpressions;
namespace InvoiceClient.Agent
{
    public class BranchTrackBlankWatcher : InvoiceWatcher
    {
        public BranchTrackBlankWatcher(String fullPath)
            : base(fullPath)
        {

        }
        protected override void _watcher_Created(object sender, FileSystemEventArgs e)
        {
            Thread.Sleep(5000);
            var files = Directory.GetFiles(_watcher.Path);
            if (files != null && files.Count() > 0)
            {
                foreach (String fullPath in files)
                {
                    processFile(fullPath);
                }
              
            }
        }
        protected override Root processUpload(WS_Invoice.eInvoiceService invSvc, XmlDocument docInv)
        {
          
            var result = invSvc.UploadBranchTrackBlank(docInv);


            //return result.ConvertTo<Root>();
            return result.ConvertTo<RootBranchTrackBlank>();
        }


        //protected override void processError(IEnumerable<RootResponseInvoiceNo> rootInvoiceNo, XmlDocument docInv, string fileName)
        //{
        //    if (rootInvoiceNo != null && rootInvoiceNo.Count() > 0)
        //    {
        //        IEnumerable<String> message = rootInvoiceNo.Select(i => String.Format("發票號碼:{0}=>{1}", i.Value, i.Description));
        //        Logger.Warn(String.Format("在上傳發票檔({0})時,傳送失敗!!原因如下:\r\n{1}", fileName, String.Join("\r\n", message.ToArray())));

        //        SellerInvoiceRoot invoice = docInv.ConvertTo<SellerInvoiceRoot>();
        //        SellerInvoiceRoot stored = new SellerInvoiceRoot();
        //        stored.Invoice = rootInvoiceNo.Where(i=>i.ItemIndexSpecified).Select(i => invoice.Invoice[i.ItemIndex]).ToArray();
        //        stored.ConvertToXml().Save(Path.Combine(_failedTxnPath, String.Format("{0}-{1:yyyyMMddHHmmssfff}.xml", Path.GetFileName(fileName), DateTime.Now)));
        //    }
        //}
        protected override void processError(string message, XmlDocument docInv, string fileName)
        {
            Logger.Warn(String.Format("在上傳空白未使用字軌檔資料({0})時,傳送失敗!!原因如下:\r\n{1}", fileName, message));
        }

        public override string ReportError()
        {
            int count = Directory.GetFiles(_failedTxnPath).Length;
            return count > 0 ? String.Format("{0}筆空白未使用字軌檔資料資料傳送失敗!!\r\n", count) : null;
        }
    }
}

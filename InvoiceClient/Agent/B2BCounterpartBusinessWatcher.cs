﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

using InvoiceClient.Helper;
using InvoiceClient.Properties;
using Model.Schema.EIVO.B2B;
using Model.Schema.TXN;
using Utility;

namespace InvoiceClient.Agent
{
    public class B2BCounterpartBusinessWatcher : InvoiceWatcher
    {
        public B2BCounterpartBusinessWatcher(String fullPath)
            : base(fullPath)
        {

        }

        protected override Root processUpload(WS_Invoice.eInvoiceService invSvc, XmlDocument docInv)
        {
            var result = invSvc.UploadCounterpartBusiness(docInv).ConvertTo<Root>();
            return result;
        }


        protected override void processError(IEnumerable<RootResponseInvoiceNo> rootInvoiceNo, XmlDocument docInv, string fileName)
        {
            if (rootInvoiceNo != null && rootInvoiceNo.Count() > 0)
            {
                IEnumerable<String> message = rootInvoiceNo.Select(i => i.Description);
                Logger.Warn(String.Format("在上傳相對營業人資料檔({0})時,傳送失敗!!原因如下:\r\n{1}", fileName, String.Join("\r\n", message.ToArray())));

                docInv.Save(Path.Combine(_failedTxnPath, fileName));
            }
        }

    }
}

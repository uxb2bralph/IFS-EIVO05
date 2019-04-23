using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;

using InvoiceClient.Properties;
using Model.Schema.EIVO;

namespace InvoiceClient.Agent
{
    public static class InvoiceClientTransferManager
    {
        private static InvoiceWelfareInspector _WelfareInspector = new InvoiceWelfareInspector();
        private static InvoiceServerInspector _ServerInspector = new InvoiceServerInspector();
        private static ServiceController _ServiceController;

        public static void StartUp(String fullPath)
        {
            ResetServiceController();
            B2CInvoiceTransferManager.PauseAll();
            B2BInvoiceTransferManager.PauseAll();
            InvoiceCenterTransferManager.PauseAll();
            PhysicalChannelInvoiceTransferManager.PauseAll();
            ReceiptTransferManager.PauseAll();
            CsvInvoiceTransferManager.PauseAll();
            InvoiceAttachmentTransferManager.PauseAll();

            if (_ServiceController == null || _ServiceController.Status != ServiceControllerStatus.Running)
            {

                if (!Settings.Default.DisableB2CInvoiceCenterTab)
                {
                    B2CInvoiceTransferManager.EnableAll(fullPath);
                }
                if (!Settings.Default.DisableInvoiceCenterTab)
                {
                    B2BInvoiceTransferManager.EnableAll(fullPath);
                }
                if (!Settings.Default.DisableEIVOPlatformTab)
                {
                    InvoiceCenterTransferManager.EnableAll(fullPath);
                }
                if (!Settings.Default.DisablePhysicalChannelTab)
                {
                    PhysicalChannelInvoiceTransferManager.EnableAll(fullPath);
                }
                if (!Settings.Default.DisableReceiptUploadTab)
                {
                    ReceiptTransferManager.EnableAll(fullPath);
                }
                if (!Settings.Default.DisableCsvInvoiceCenterTab)
                {
                    CsvInvoiceTransferManager.EnableAll(fullPath);
                }
                if (!Settings.Default.DisableCheckedStatementTab)
                {
                    InvoiceAttachmentTransferManager.EnableAll(fullPath);
                }

            }
        }

        public static void ResetServiceController()
        {
            if (Environment.UserInteractive)
            {
                _ServiceController = ServiceController.GetServices().Where(s => s.ServiceName == Settings.Default.ServiceName).FirstOrDefault();
            }
        }

        public static void ClearServiceController()
        {
            _ServiceController = null;
        }

        public static ServiceController ServiceInstance
        {
            get
            {
                return _ServiceController;
            }
        }


        public static SocialWelfareAgenciesRoot UpdateWelfareAgency()
        {
            return _WelfareInspector.GetUpdatedData();
        }

        public static SocialWelfareAgenciesRoot GetWelfareAgency()
        {
            return _WelfareInspector.GetAll();
        }


        public static void SetAutoUpdateWelfareAgency()
        {
            _WelfareInspector.StartUp();
        }

        public static void SetAutoInvoiceService()
        {
            _ServerInspector.StartUp();
        }

        public static List<String> ExceuteInvoiceService()
        {
            List<String> pathInfo = new List<string>();
            var items = _ServerInspector.GetIncomingInvoices();
            if (items != null && items.Count() > 0)
                pathInfo.Add(Path.Combine(Settings.Default.InvoiceTxnPath, Settings.Default.DownloadInvoiceFolder));
            items = _ServerInspector.GetIncomingInvoiceCancellations();
            if (items != null && items.Count() > 0)
                pathInfo.Add(Path.Combine(Settings.Default.InvoiceTxnPath, Settings.Default.DownloadInvoiceCancellationFolder));
            items = _ServerInspector.GetIncomingAllowances();
            if (items != null && items.Count() > 0)
                pathInfo.Add(Path.Combine(Settings.Default.InvoiceTxnPath, Settings.Default.DownloadAllowanceFolder));
            items = _ServerInspector.GetIncomingAllowanceCancellations();
            if (items != null && items.Count() > 0)
                pathInfo.Add(Path.Combine(Settings.Default.InvoiceTxnPath, Settings.Default.DownloadAllowanceCancellationFolder));
            if (_ServerInspector.GetIncomingWinningInvoices() != null)
                pathInfo.Add(Path.Combine(Settings.Default.InvoiceTxnPath, Settings.Default.DownloadWinningFolder));

            String path = _ServerInspector.GetSaleInvoices();
            if (path != null)
                pathInfo.Add(path);

            if (!Settings.Default.DisableB2BAutoReceiving)
            {
                path = _ServerInspector.ReceiveA1401();
                if (path != null)
                    pathInfo.Add(path);
                path = _ServerInspector.ReceiveB1401();
                if (path != null)
                    pathInfo.Add(path);
                path = _ServerInspector.ReceiveA0501();
                if (path != null)
                    pathInfo.Add(path);
                path = _ServerInspector.ReceiveB0501();
                if (path != null)
                    pathInfo.Add(path);
            }

            return pathInfo;
        }

    }
}

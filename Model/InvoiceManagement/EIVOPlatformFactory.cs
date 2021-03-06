﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading;
using System.Xml;

using Model.DataEntity;
using Model.Properties;
using Utility;
using System.Security.Cryptography.X509Certificates;
using Uxnet.Com.Security.UseCrypto;
using Model.Helper;
using Model.Locale;

namespace Model.InvoiceManagement
{
    public static class EIVOPlatformFactory
    {
        private static bool _IsActive = false;
        private static Queue<DateTime?> _EventQ = new Queue<DateTime?>();

        public static Func<XmlDocument, bool> Sign
        {
            get;
            set;
        }

        public static Func<String, SignedCms> SignCms
        {
            get;
            set;
        }

        public static EventHandler SendNotification
        {
            get;
            set;
        }

        public static EventHandler<EventArgs<NotifyToProcessID>> NotifyToIssueItem
        {
            get;
            set;
        }

        public static EventHandler<EventArgs<NotifyToProcessID>> NotifyToReceiveItem
        {
            get;
            set;
        }

        public static EventHandler<EventArgs<NotifyToProcessID>> NotifyCommissionedToReceive
        {
            get;
            set;
        }

        public static EventHandler<EventArgs<NotifyMailInfo>> NotifyCommissionedToReceiveInvoice
        {
            get;
            set;
        }

        public static EventHandler<EventArgs<NotifyToProcessID>> NotifyCommissionedToReceiveInvoiceCancellation
        {
            get;
            set;
        }


        public static EventHandler<EventArgs<InvoiceItem>> NotifyReceivedInvoice
        {
            get;
            set;
        }




        public static void Notify()
        {
            _EventQ.Enqueue(DateTime.Now);
            if (!_IsActive)
            {
                ThreadPool.QueueUserWorkItem(notifyToProcess);
            }
        }

        private static void notifyToProcess(object stateInfo)
        {
            bool bRun = false;
            lock (typeof(EIVOPlatformFactory))
            {
                if (!_IsActive)
                {
                    bRun = true;
                    _IsActive = true;
                }
            }

            if (!bRun)
                return;

            try
            {
                processEventQueue();
                Logger.Info("傳送至IFS資料處理完成!!");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            _IsActive = false;
        }

        private static void processEventQueue()
        {
            while (_EventQ.Count > 0)
            {
                DateTime? ev = (DateTime?)_EventQ.Dequeue();
                EIVOPlatformManager mgr = new EIVOPlatformManager();

                //傳送待傳送資料
                mgr.TransmitInvoice();
                //自動接收
                mgr.CommissionedToReceive();
                //自動開立
                mgr.CommissionedToIssue();
                mgr.MatchDocumentAttachment();
                mgr.NotifyToProcess();
            }
        }
    }
}

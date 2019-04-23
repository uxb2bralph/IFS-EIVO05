using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using Model.InvoiceManagement;
using eIVOGo.Properties;
using eIVOGo.Module.SAM;

namespace eIVOCenter.services
{
    public static class ServiceWorkItem
    {
        private static bool __IsNotifyingGovPlatform;
        private static bool __IsNotifyingClientAlert;

        public static void NotifyGovPlatform()
        {
            if (!__IsNotifyingGovPlatform)
            {
                if (ThreadSafeCheckEnable(ref __IsNotifyingGovPlatform))
                {
                    ThreadPool.QueueUserWorkItem(info =>
                        {
                            EIVOPlatformFactory.Notify();
                            Thread.Sleep(Settings.Default.GovPlatformAutoTransferInterval);
                            SystemMonitorControl.BackgroundService.Interrupt();
                            __IsNotifyingGovPlatform = false;
                        });
                }

            }
        }


        public static void Reset()
        {
            __IsNotifyingGovPlatform = false;
            __IsNotifyingClientAlert = false;
        }

        public static bool ThreadSafeCheckEnable(ref bool token)
        {
            var bRun = false;
            lock (typeof(ServiceWorkItem))
            {
                if (!token)
                {
                    bRun = true;
                    token = true;
                }
            }
            return bRun;
        }

        public static void NotifyClientResponseTimeoutAlert()
        {
            if (!__IsNotifyingClientAlert)
            {
                if (ThreadSafeCheckEnable(ref __IsNotifyingClientAlert))
                {
                    ThreadPool.QueueUserWorkItem(info =>
                    {

                        Thread.Sleep(Settings.Default.ClientResponseTimeoutAlertInterval);
                        SystemMonitorControl.BackgroundService.Interrupt();
                        __IsNotifyingClientAlert = false;
                    });
                }
            }
        }

    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.InvoiceManagement;
using Model.DataEntity;
using Utility;
using Model.Locale;
using Model.Schema.EIVO.B2B;
using Model.UploadManagement;

namespace Model.Helper
{
    public class B2BExceptionInfo
    {
        public OrganizationToken Token { get; set; }
        public Dictionary<int, Exception> ExceptionItems { get; set; }
        public SellerInvoiceRoot InvoiceData { get; set; }
        public CancelInvoiceRoot CancelInvoiceData { get; set; }
        public AllowanceRoot AllowanceData { get; set; }
        public CancelAllowanceRoot CancelAllowanceData { get; set; }
        public BuyerInvoiceRoot BuyerInvoiceData { get; set; }
        public ReceiptRoot ReceiptData { get; set; }
        public CancelReceiptRoot CancelReceiptData { get; set; }
        public IEnumerable<ItemUpload<Organization>> CounterpartBusinessError { get; set; }

    }

    public static class B2BExceptionNotification
    {
        public static EventHandler<ExceptionEventArgs> SendExceptionNotification;

        public static void SendNotification(object stateInfo)
        {
            B2BExceptionInfo info = stateInfo as B2BExceptionInfo;
            if (info == null)
                return;

            try
            {
                if (info.InvoiceData != null)
                {
                    notifyExceptionWhenUploadInvoice(info);
                }
                else if (info.CancelInvoiceData != null)
                {
                    notifyExceptionWhenUploadCancellation(info);
                }
                else if (info.AllowanceData != null)
                {
                    notifyExceptionWhenUploadAllowance(info);
                }
                else if (info.CancelAllowanceData != null)
                {
                    notifyExceptionWhenUploadAllowanceCancellation(info);
                }
                else if (info.BuyerInvoiceData != null)
                {
                    notifyExceptionWhenUploadBuyerInvoice(info);
                }
                else if (info.ReceiptData != null)
                {
                    notifyExceptionWhenUploadReceipt(info);
                }
                else if (info.CancelReceiptData != null)
                {
                    notifyExceptionWhenUploadReceiptCancellation(info);
                }
                else if(info.CounterpartBusinessError!=null)
                {
                    notifyExceptionWhenUploadCounterpartBusiness(info);
                }

                processNotification();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }

        private static void processNotification()
        {
            using (InvoiceManager mgr = new InvoiceManager())
            {
                var items = mgr.GetTable<ExceptionLog>().Where(e => e.ExceptionReplication != null);
                foreach (var item in items.GroupBy(i => i.CompanyID))
                {
                    SendExceptionNotification(mgr, new ExceptionEventArgs
                    {
                        Enterprise = item.ElementAt(0).Organization.EnterpriseGroupMember.FirstOrDefault().EnterpriseGroup,
                        CompanyID = item.Key,
                        EMail = item.ElementAt(0).Organization.ContactEmail
                    });
                }

                SendExceptionNotification(mgr, new ExceptionEventArgs
                {
                    ///送給系統管理員接收全部異常資料
                    ///
                });

                mgr.GetTable<ExceptionReplication>().DeleteAllOnSubmit(items.Select(i => i.ExceptionReplication));
                mgr.SubmitChanges();
            }
        }

        private static void notifyExceptionWhenUploadInvoice(B2BExceptionInfo info)
        {
            int? companyID = info.Token != null ? info.Token.CompanyID : (int?)null;
            using (InvoiceManager mgr = new InvoiceManager())
            {
                var table = mgr.GetTable<ExceptionLog>();

                table.InsertAllOnSubmit(info.ExceptionItems.Select(e => new ExceptionLog
                {
                    CompanyID = companyID,
                    LogTime = DateTime.Now,
                    TypeID = (int)Naming.DocumentTypeDefinition.E_Invoice,
                    Message = e.Value.Message,
                    DataContent = info.InvoiceData.Invoice[e.Key].GetXml()
                }));
                mgr.SubmitChanges();
            }
        }

        private static void notifyExceptionWhenUploadBuyerInvoice(B2BExceptionInfo info)
        {
            int? companyID = info.Token != null ? info.Token.CompanyID : (int?)null;
            using (InvoiceManager mgr = new InvoiceManager())
            {
                var table = mgr.GetTable<ExceptionLog>();

                table.InsertAllOnSubmit(info.ExceptionItems.Select(e => new ExceptionLog
                {
                    CompanyID = companyID,
                    LogTime = DateTime.Now,
                    TypeID = (int)Naming.DocumentTypeDefinition.E_Invoice,
                    Message = e.Value.Message,
                    DataContent = info.BuyerInvoiceData.Invoice[e.Key].GetXml()
                }));
                mgr.SubmitChanges();
            }
        }


        private static void notifyExceptionWhenUploadAllowance(B2BExceptionInfo info)
        {
            int? companyID = info.Token != null ? info.Token.CompanyID : (int?)null;
            using (InvoiceManager mgr = new InvoiceManager())
            {
                var table = mgr.GetTable<ExceptionLog>();

                table.InsertAllOnSubmit(info.ExceptionItems.Select(e => new ExceptionLog
                {
                    CompanyID = companyID,
                    LogTime = DateTime.Now,
                    TypeID = (int)Naming.DocumentTypeDefinition.E_Allowance,
                    Message = e.Value.Message,
                    DataContent = info.AllowanceData.Allowance[e.Key].GetXml()
                }));
                mgr.SubmitChanges();
            }
        }


        private static void notifyExceptionWhenUploadCancellation(B2BExceptionInfo info)
        {
            int? companyID = info.Token != null ? info.Token.CompanyID : (int?)null;
            using (InvoiceManager mgr = new InvoiceManager())
            {
                var table = mgr.GetTable<ExceptionLog>();

                table.InsertAllOnSubmit(info.ExceptionItems.Select(e => new ExceptionLog
                {
                    CompanyID = companyID,
                    LogTime = DateTime.Now,
                    TypeID = (int)Naming.DocumentTypeDefinition.E_InvoiceCancellation,
                    Message = e.Value.Message,
                    DataContent = info.CancelInvoiceData.CancelInvoice[e.Key].GetXml()
                }));
                mgr.SubmitChanges();
            }
        }

        private static void notifyExceptionWhenUploadAllowanceCancellation(B2BExceptionInfo info)
        {
            int? companyID = info.Token != null ? info.Token.CompanyID : (int?)null;
            using (InvoiceManager mgr = new InvoiceManager())
            {
                var table = mgr.GetTable<ExceptionLog>();

                table.InsertAllOnSubmit(info.ExceptionItems.Select(e => new ExceptionLog
                {
                    CompanyID = companyID,
                    LogTime = DateTime.Now,
                    TypeID = (int)Naming.DocumentTypeDefinition.E_AllowanceCancellation,
                    Message = e.Value.Message,
                    DataContent = info.CancelAllowanceData.CancelAllowance[e.Key].GetXml()
                }));
                mgr.SubmitChanges();
            }
        }

        private static void notifyExceptionWhenUploadReceipt(B2BExceptionInfo info)
        {
            int? companyID = info.Token != null ? info.Token.CompanyID : (int?)null;
            using (InvoiceManager mgr = new InvoiceManager())
            {
                var table = mgr.GetTable<ExceptionLog>();

                table.InsertAllOnSubmit(info.ExceptionItems.Select(e => new ExceptionLog
                {
                    CompanyID = companyID,
                    LogTime = DateTime.Now,
                    TypeID = (int)Naming.B2BInvoiceDocumentTypeDefinition.收據,
                    Message = e.Value.Message,
                    DataContent = info.ReceiptData.Receipt[e.Key].GetXml()
                }));
                mgr.SubmitChanges();
            }
        }

        private static void notifyExceptionWhenUploadReceiptCancellation(B2BExceptionInfo info)
        {
            int? companyID = info.Token != null ? info.Token.CompanyID : (int?)null;
            using (InvoiceManager mgr = new InvoiceManager())
            {
                var table = mgr.GetTable<ExceptionLog>();

                table.InsertAllOnSubmit(info.ExceptionItems.Select(e => new ExceptionLog
                {
                    CompanyID = companyID,
                    LogTime = DateTime.Now,
                    TypeID = (int)Naming.B2BInvoiceDocumentTypeDefinition.作廢收據,
                    Message = e.Value.Message,
                    DataContent = info.CancelReceiptData.CancelReceipt[e.Key].GetXml()
                }));
                mgr.SubmitChanges();
            }
        }

        private static void notifyExceptionWhenUploadCounterpartBusiness(B2BExceptionInfo info)
        {
            int? companyID = info.Token != null ? info.Token.CompanyID : (int?)null;
            using (InvoiceManager mgr = new InvoiceManager())
            {
                var table = mgr.GetTable<ExceptionLog>();

                table.InsertAllOnSubmit(info.CounterpartBusinessError.Select(e => new ExceptionLog
                {
                    CompanyID = companyID,
                    LogTime = DateTime.Now,
                    TypeID = (int)Naming.DocumentTypeDefinition.E_InvoiceCancellation,
                    Message = e.Status,
                    DataContent = e.DataContent
                }));
                mgr.SubmitChanges();
            }
        }



    }


}

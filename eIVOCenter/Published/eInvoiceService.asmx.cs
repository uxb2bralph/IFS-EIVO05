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
using eIVOGo.Module.Common;
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

namespace eIVOCenter.Published
{
    /// <summary>
    /// eInvoiceService 的摘要描述
    /// </summary>
    [WebService(Namespace = "http://www.uxb2b.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
    // [System.Web.Script.Services.ScriptService]
    public class eInvoiceService : eIVOGo.Published.eInvoiceService
    {
        public static new void StartUp()
        {
            EIVOPlatformFactory.SendNotification =
                (o, e) =>
                {
                    try
                    {

                        (Uxnet.Web.Properties.Settings.Default.HostUrl + VirtualPathUtility.ToAbsolute(Settings.Default.GovPlatformNotificationUrl))
                            .MailWebPage(Uxnet.Web.Properties.Settings.Default.WebMaster, "eIvo05電子發票服務平台集團加值中心 IFS-EIVO傳送異常通知");

                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                };

            B2BExceptionNotification.SendExceptionNotification =
                (o, e) =>
                {
                    try
                    {
                        String subject = string.Format("{0} ERP/Gateway資料傳送異常通知",
                             e.Enterprise.EnterpriseName);

                        String.Format("{0}{1}?companyID={2}",
                            Uxnet.Web.Properties.Settings.Default.HostUrl,
                            VirtualPathUtility.ToAbsolute(Settings.Default.ExceptionNotificationUrl),
                            e.CompanyID)
                            .MailWebPage(String.IsNullOrEmpty(e.EMail) ? Uxnet.Web.Properties.Settings.Default.WebMaster : e.EMail, subject);

                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                };

            EIVOPlatformFactory.NotifyToIssueItem =
                (o, e) =>
                {
                    try
                    {
                        using (InvoiceManager mgr = new InvoiceManager())
                        {
                            var seller = mgr.GetTable<Organization>().Where(c => c.CompanyID == e.Argument.Seller.CompanyID)
                                .First().EnterpriseGroupMember.FirstOrDefault().EnterpriseGroup;

                            var mailto = String.Join(",",
                                mgr.GetUserListByCompanyID(e.Argument.MailToID)
                            .Select(u => u.EMail)
                            .Where(m => m != null));


                            if (!String.IsNullOrEmpty(mailto))
                            {
                                String subject = string.Format("{0} 傳送開立通知", seller != null ? seller.EnterpriseName : null);

                                String.Format("{0}{1}?businessID={2}", Uxnet.Web.Properties.Settings.Default.HostUrl,
                                    VirtualPathUtility.ToAbsolute(eIVOCenter.Properties.Settings.Default.NotifyToIssue), e.Argument.MailToID)
                                .MailWebPage(mailto, subject);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                };

            EIVOPlatformFactory.NotifyToReceiveItem =
                (o, e) =>
                {
                    try
                    {
                        using (InvoiceManager mgr = new InvoiceManager())
                        {
                            var seller = mgr.GetTable<Organization>().Where(c => c.CompanyID == e.Argument.Seller.CompanyID)
                                .First().EnterpriseGroupMember.FirstOrDefault().EnterpriseGroup;

                            var mailto = String.Join(",",
                                mgr.GetUserListByCompanyID(e.Argument.MailToID)
                            .Select(u => u.EMail)
                            .Where(m => m != null));


                            if(!String.IsNullOrEmpty(mailto))
                            {
                                String subject = string.Format("{0} 傳送接收通知", seller != null ? seller.EnterpriseName : null);
                                String.Format("{0}{1}?businessID={2}", Uxnet.Web.Properties.Settings.Default.HostUrl,
                                    VirtualPathUtility.ToAbsolute(eIVOCenter.Properties.Settings.Default.NotifyToReceive), e.Argument.MailToID)
                                    .MailWebPage(mailto, subject);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                };

            EIVOPlatformFactory.NotifyCommissionedToReceive =
            (o, e) =>
            {
                try
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        var seller = mgr.GetTable<Organization>().Where(c => c.CompanyID == e.Argument.Seller.CompanyID)
                            .First().EnterpriseGroupMember.FirstOrDefault().EnterpriseGroup;

                        var mailto = String.Join(",",
                            mgr.GetUserListByCompanyID(e.Argument.MailToID)
                        .Select(u => u.EMail)
                        .Where(m => m != null));

                        if (!String.IsNullOrEmpty(mailto))
                        {
                            string subject = String.Format("{0}{1}", e.Argument.Subject!=null ? e.Argument.Subject : seller != null ? seller.EnterpriseName : "電子發票加值中心", " 自動接收通知");

                            ((seller != null ? seller.EnterpriseName : "") 
                                + "已開出您本期發票/收據/折讓,系統已自動幫您接受發票/收據/折讓資料," + System.Environment.NewLine 
                                + "煩請上電子發票服務平台進行後續作業 https://eivo.uxifs.com")
                                 .SendMailMessage(mailto, subject);

                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            };

            EIVOPlatformFactory.NotifyCommissionedToReceiveInvoiceCancellation =
            (o, e) =>
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    try
                    {
                        using (InvoiceManager mgr = new InvoiceManager())
                        {
                            var item = mgr.GetTable<CDS_Document>().Where(d => d.DocID == e.Argument.DocID).FirstOrDefault();
                            if (item == null)
                                return;

                            var mailto = String.Join(",",
                                mgr.GetUserListByCompanyID(item.DerivedDocument.ParentDocument.InvoiceItem.InvoiceBuyer.BuyerID)
                            .Select(u => u.EMail)
                            .Where(m => m != null));

                            if (!String.IsNullOrEmpty(mailto))
                            {
                                string subject = item.DerivedDocument.ParentDocument.InvoiceItem.InvoiceSeller.CustomerName
                                    + "開立作廢電子發票(" + item.DerivedDocument.ParentDocument.InvoiceItem.TrackCode
                                                    + item.DerivedDocument.ParentDocument.InvoiceItem.No + ") 自動接收通知";
                                
                                (item.DerivedDocument.ParentDocument.InvoiceItem.InvoiceSeller.CustomerName
                                    + "已開出您本期作廢發票(" 
                                    + item.DerivedDocument.ParentDocument.InvoiceItem.TrackCode
                                    + item.DerivedDocument.ParentDocument.InvoiceItem.No + "),系統已自動幫您接受作廢發票資料," + System.Environment.NewLine
                                    + "煩請上電子發票服務平台進行後續作業 https://eivo.uxifs.com")
                                     .SendMailMessage(mailto, subject);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                });

            };


            EIVOPlatformFactory.NotifyCommissionedToReceiveInvoice =
                        (o, e) =>
                        {
                            try
                            {
                                using (WebClient client = new WebClient())
                                {
                                    client.DownloadString(String.Format("{0}{1}?invoiceID={2}&isMail={3}",
                                        Uxnet.Web.Properties.Settings.Default.HostUrl,
                                        VirtualPathUtility.ToAbsolute("~/Published/NotifyCommissionedToReceiveInvoice.ashx"),
                                        e.Argument.InvoiceItem.InvoiceID,
                                        e.Argument.isMail));
                                }

                            }
                            catch (Exception ex)
                            {
                                Logger.Error(ex);
                            }
                        };

            EIVOPlatformFactory.NotifyReceivedInvoice =
                        (o, e) =>
                        {
                            try
                            {
                                using (InvoiceManager mgr = new InvoiceManager())
                                {
                                    var invItem = mgr.EntityList.Where(i => i.InvoiceID == e.Argument.InvoiceID).First();

                                    String pdfFile = mgr.PrepareToDownload(invItem,false);

                                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(pdfFile, MediaTypeNames.Application.Octet);
                                    ///修改附件檔名為發票號碼
                                    ///
                                    attachment.Name = String.Format("{0}{1}.pdf", e.Argument.TrackCode, e.Argument.No);

                                    var mailTo = String.Join(",",
                                        mgr.GetUserListByCompanyID(invItem.InvoiceBuyer.BuyerID)
                                    .Select(u => u.EMail)
                                    .Where(m => m != null));

                                    if(!String.IsNullOrEmpty(mailTo))
                                    {
                                        var enterprise = invItem.InvoiceSeller.Organization.EnterpriseGroupMember.FirstOrDefault();

                                        String Subject = String.Format("{0} 發票已接收通知(發票號碼:{1}{2})"
                                            ,enterprise!=null ? enterprise.EnterpriseGroup.EnterpriseName:null , e.Argument.TrackCode, e.Argument.No);
                                        String Body = " 您已接收本期由" + invItem.InvoiceSeller.CustomerName + "開出之發票資料,請參考附件發票證明聯,"
                                            + System.Environment.NewLine
                                            + "亦可登入電子發票平台查詢電子發票相關資訊," + System.Environment.NewLine
                                            + "關於電子發票服務請至 https://eivo.uxifs.com";

                                        Body.SendMailMessage(mailTo, Subject, new System.Net.Mail.Attachment[] { attachment });
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                Logger.Error(ex);
                            }
                        };

        }


        [WebMethod()]
        public XmlDocument B2BUploadReceipt(System.Xml.XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    ReceiptRoot receiptAll = uploadData.ConvertTo<ReceiptRoot>();
                    using (ReceiptManager mgr = new ReceiptManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            mgr.PrepareSignerCertificate(token.Organization);

                            var items = mgr.SaveUploadReceipt(receiptAll, token);
                            if (items.Count > 0)
                            {
                                result.Response = new RootResponse
                                {
                                    InvoiceNo =
                                    items.Select(d => new RootResponseInvoiceNo
                                    {
                                        Value = receiptAll.Receipt[d.Key].ReceiptNumber,
                                        Description = d.Value.Message,
                                        ItemIndexSpecified = true,
                                        ItemIndex = d.Key
                                    }).ToArray()
                                };

                                ThreadPool.QueueUserWorkItem(B2BExceptionNotification.SendNotification,
                                    new B2BExceptionInfo
                                    {
                                        Token = token,
                                        ExceptionItems = items,
                                        ReceiptData = receiptAll
                                    });
                            }
                            else
                            {
                                result.Result.value = 1;
                            }
                        }
                        else
                        {
                            result.Result.message = "會員憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "資料簽章不符!!";
                }

                //GovPlatformFactory.Notify();
                //EIVOPlatformFactory.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod()]
        public XmlDocument B2BUploadReceiptCancellation(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    CancelReceiptRoot item = uploadData.ConvertTo<CancelReceiptRoot>();
                    using (ReceiptManager mgr = new ReceiptManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {

                            var items = mgr.SaveUploadReceiptCancellation(item, token);
                            if (items.Count > 0)
                            {
                                result.Response = new RootResponse
                                {
                                    InvoiceNo =
                                    items.Select(d => new RootResponseInvoiceNo
                                    {
                                        Value = item.CancelReceipt[d.Key].CancelReceiptNumber,
                                        Description = d.Value.Message,
                                        ItemIndexSpecified = true,
                                        ItemIndex = d.Key
                                    }).ToArray()
                                };

                                ThreadPool.QueueUserWorkItem(B2BExceptionNotification.SendNotification,
                                                                    new B2BExceptionInfo
                                                                    {
                                                                        Token = token,
                                                                        ExceptionItems = items,
                                                                        CancelReceiptData = item
                                                                    });

                            }
                            else
                            {
                                result.Result.value = 1;
                            }
                        }
                        else
                        {
                            result.Result.message = "會員憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "收據資料簽章不符!!";
                }

                //GovPlatformFactory.Notify();
                //EIVOPlatformFactory.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod()]
        public XmlDocument B2BUploadInvoice(System.Xml.XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    SellerInvoiceRoot invoice = uploadData.ConvertTo<SellerInvoiceRoot>();
                    using (B2BInvoiceManager mgr = new B2BInvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            mgr.PrepareSignerCertificate(token.Organization);

                            var items = mgr.SaveUploadInvoice(invoice, token);
                            if (items.Count > 0)
                            {
                                result.Response = new RootResponse
                                {
                                    InvoiceNo =
                                    items.Select(d => new RootResponseInvoiceNo
                                    {
                                        Value = invoice.Invoice[d.Key].InvoiceNumber,
                                        Description = d.Value.Message,
                                        ItemIndexSpecified = true,
                                        ItemIndex = d.Key
                                    }).ToArray()
                                };

                                ThreadPool.QueueUserWorkItem(B2BExceptionNotification.SendNotification,
                                    new B2BExceptionInfo
                                    {
                                        Token = token,
                                        ExceptionItems = items,
                                        InvoiceData = invoice
                                    });
                            }
                            else
                            {
                                result.Result.value = 1;
                            }
                        }
                        else
                        {
                            result.Result.message = "會員憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                //GovPlatformFactory.Notify();
                //EIVOPlatformFactory.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod()]
        public XmlDocument B2BUploadAllowance(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    AllowanceRoot allowance = uploadData.ConvertTo<AllowanceRoot>();
                    using (B2BInvoiceManager mgr = new B2BInvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            var items = mgr.SaveUploadAllowance(allowance, token);
                            if (items.Count > 0)
                            {
                                result.Response = new RootResponse
                                {
                                    InvoiceNo =
                                    items.Select(d => new RootResponseInvoiceNo
                                    {
                                        Value = allowance.Allowance[d.Key].AllowanceNumber,
                                        ItemIndexSpecified=true,
                                        ItemIndex = d.Key,
                                        Description = d.Value.Message
                                    }).ToArray()
                                };

                                ThreadPool.QueueUserWorkItem(B2BExceptionNotification.SendNotification,
                                    new B2BExceptionInfo
                                    {
                                        Token = token,
                                        ExceptionItems = items,
                                        AllowanceData = allowance
                                    });
                            }
                            else
                            {
                                result.Result.value = 1;
                            }
                        }
                        else
                        {
                            result.Result.message = "會員憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                //EIVOPlatformFactory.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod]
        public XmlDocument B2BUploadAllowanceCancellation(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    CancelAllowanceRoot item = uploadData.ConvertTo<CancelAllowanceRoot>();
                    using (B2BInvoiceManager mgr = new B2BInvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {

                            var items = mgr.SaveUploadAllowanceCancellation(item,token);
                            if (items.Count > 0)
                            {
                                result.Response = new RootResponse
                                {
                                    InvoiceNo =
                                    items.Select(d => new RootResponseInvoiceNo
                                    {
                                        Value = item.CancelAllowance[d.Key].CancelAllowanceNumber,
                                        ItemIndexSpecified = true,
                                        ItemIndex = d.Key,
                                        Description = d.Value.Message
                                    }).ToArray()
                                };

                                ThreadPool.QueueUserWorkItem(B2BExceptionNotification.SendNotification,
                                                                    new B2BExceptionInfo
                                                                    {
                                                                        Token = token,
                                                                        ExceptionItems = items,
                                                                        CancelAllowanceData = item
                                                                    });

                            }
                            else
                            {
                                result.Result.value = 1;
                            }
                        }
                        else
                        {
                            result.Result.message = "會員憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                //EIVOPlatformFactory.Notify();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod()]
        public XmlDocument B2BUploadInvoiceCancellation(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    CancelInvoiceRoot item = uploadData.ConvertTo<CancelInvoiceRoot>();
                    using (B2BInvoiceManager mgr = new B2BInvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            mgr.PrepareSignerCertificate(token.Organization);

                            var items = mgr.SaveUploadInvoiceCancellation(item, token);
                            if (items.Count > 0)
                            {
                                result.Response = new RootResponse
                                {
                                    InvoiceNo =
                                    items.Select(d => new RootResponseInvoiceNo
                                    {
                                        Value = item.CancelInvoice[d.Key].CancelInvoiceNumber,
                                        Description = d.Value.Message,
                                        ItemIndexSpecified = true,
                                        ItemIndex = d.Key
                                    }).ToArray()
                                };

                                ThreadPool.QueueUserWorkItem(B2BExceptionNotification.SendNotification,
                                                                    new B2BExceptionInfo
                                                                    {
                                                                        Token = token,
                                                                        ExceptionItems = items,
                                                                        CancelInvoiceData = item
                                                                    });

                            }
                            else
                            {
                                result.Result.value = 1;
                            }
                        }
                        else
                        {
                            result.Result.message = "會員憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                //GovPlatformFactory.Notify();
                //EIVOPlatformFactory.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod()]
        public XmlDocument B2BUploadBuyerInvoice(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    BuyerInvoiceRoot invoice = uploadData.ConvertTo<BuyerInvoiceRoot>();
                    using (B2BInvoiceManager mgr = new B2BInvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            var items = mgr.SaveUploadInvoice(invoice, token);
                            if (items.Count > 0)
                            {
                                result.Response = new RootResponse
                                {
                                    InvoiceNo =
                                    items.Select(d => new RootResponseInvoiceNo
                                    {
                                        Value = invoice.Invoice[d.Key].DataNumber,
                                        Description = d.Value.Message,
                                        ItemIndexSpecified = true,
                                        ItemIndex = d.Key
                                    }).ToArray()
                                };

                                ThreadPool.QueueUserWorkItem(B2BExceptionNotification.SendNotification,
                                    new B2BExceptionInfo
                                    {
                                        Token = token,
                                        ExceptionItems = items,
                                        BuyerInvoiceData = invoice
                                    });
                            }
                            else
                            {
                                result.Result.value = 1;
                            }
                        }
                        else
                        {
                            result.Result.message = "會員憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                //EIVOPlatformFactory.Notify();

                //GovPlatformFactory.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }
        [WebMethod]
        public XmlDocument UploadBranchTrackBlank(XmlDocument uploadData)
        {
            Model.Schema.EIVO.RootBranchTrackBlank result = new Model.Schema.EIVO.RootBranchTrackBlank
            {
                UXB2B = "電子發票系統",
                Result = new RootResult
                {
                    timeStamp = DateTime.Now,
                    value = 0
                }
            };
            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    //InvoiceNoRequestRoot invoiceNoRequest = uploadData.ConvertTo<InvoiceNoRequestRoot>();
                    Model.Schema.EIVO.E0402.BranchTrackBlank item = uploadData.ConvertTo<Model.Schema.EIVO.E0402.BranchTrackBlank>();
                    using (B2BInvoiceManager mgr = new B2BInvoiceManager())
                    {
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            var ex = mgr.CheckBranchTrackBlank(item);
                            if (ex != null)
                            {
                                result.Result.message = ex.Message;

                                Dictionary<int, Exception> errorResult = new Dictionary<int, Exception>();
                                errorResult.Add(0, ex);
                                ThreadPool.QueueUserWorkItem(ExceptionNotification.SendNotification,
                                new ExceptionInfo
                                {
                                    Token = token,
                                    BranchTrackBlankError=item,
                                    ExceptionItems = errorResult,
                                });
                            }
                            else
                            {
                                //var response = new Model.Schema.EIVO.RootResponseForBranchTrackBlank();

                                var TurnkeyResult = new Model.Schema.MIG3_1.E0402.BranchTrackBlank
                                {
                                    Main = new Model.Schema.MIG3_1.E0402.Main
                                    {
                                        HeadBan = item.Main.HeadBan,
                                        BranchBan = item.Main.BranchBan,
                                        InvoiceType = (Model.Schema.MIG3_1.E0402.InvoiceTypeEnum) item.Main.InvoiceType,
                                        YearMonth = item.Main.YearMonth,
                                        InvoiceTrack = item.Main.InvoiceTrack
                                    },
                                    Details = buildE0402Details(item)
                                };
                                result.Response = new Model.Schema.EIVO.RootResponseForBranchTrackBlank()
                                {
                                    TrackBlank = item
                                };

                                Model.InvoiceManagement.EIVOPlatformManager Platform = new Model.InvoiceManagement.EIVOPlatformManager();
                                Platform.saveBranchTrackBlankToPlatform(TurnkeyResult.ConvertToXml());
                                result.Result.value = 1;
                            }
                        }
                        else
                        {
                            result.Result.message = "終端設備使用者憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "簽章不符!!";
                }

                //GovPlatformFactoryForB2C.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }
        private static Model.Schema.MIG3_1.E0402.DetailsBranchTrackBlankItem[] buildE0402Details(Model.Schema.EIVO.E0402.BranchTrackBlank item)
        {
            List<Model.Schema.MIG3_1.E0402.DetailsBranchTrackBlankItem> items = new List<Model.Schema.MIG3_1.E0402.DetailsBranchTrackBlankItem>();

            foreach (var detail in item.Details.ToList())
            {
                items.Add(new Model.Schema.MIG3_1.E0402.DetailsBranchTrackBlankItem
                {
                    InvoiceBeginNo = String.Format("{0:00000000}", detail.InvoiceBeginNo),
                    InvoiceEndNo = String.Format("{0:00000000}", detail.InvoiceEndNo)
                }
                );
            }
            return items.ToArray();
        }
        //[WebMethod]
        //public override XmlDocument GetIncomingInvoices(XmlDocument sellerInfo)
        //{
        //    RootA1401 result = new RootA1401
        //    {
        //        UXB2B = "電子發票系統－集團加值中心",
        //        Result = new RootResult
        //        {
        //            timeStamp = DateTime.Now,
        //            value = 0
        //        }
        //    };

        //    try
        //    {
        //        CryptoUtility crypto = new CryptoUtility();
        //        if (crypto.VerifyXmlSignature(sellerInfo))
        //        {
        //            using (InvoiceManager mgr = new InvoiceManager())
        //            {
        //                ///憑證資料檢查
        //                ///
        //                var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
        //                if (token != null)
        //                {
        //                    buildIncomingInvoices(result, mgr, token.CompanyID);
        //                    Root root = sellerInfo.ConvertTo<Root>();
        //                    acknowledgeReport(mgr, token, root.Request.periodicalIntervalSpecified ? root.Request.periodicalInterval : (int?)null);
        //                }
        //                else
        //                {
        //                    result.Result.message = "會員憑證資料驗證不符!!";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            result.Result.message = "發票資料簽章不符!!";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        result.Result.message = ex.Message;
        //    }
        //    return result.ConvertToXml();
        //}

        //protected override void buildIncomingInvoices(Root result, InvoiceManager mgr, int companyID)
        //{
        //    var table = mgr.GetTable<DocumentDispatch>();
        //    var items = mgr.GetTable<CDS_Document>().Where(d => d.CurrentStep == (int)Naming.B2BInvoiceStepDefinition.待接收 && d.DocType == (int)Naming.DocumentTypeDefinition.E_Invoice)
        //            .Join(mgr.GetTable<InvoiceItem>().Where(i => i.InvoiceBuyer.BuyerID == companyID)
        //                , d => d.DocID, i => i.InvoiceID, (d, i) => d);


        //    if (items.Count() > 0)
        //    {
        //        result.Response = new RootResponseForA1401
        //        {
        //            Invoice =
        //                items.Select(d => d.InvoiceItem.CreateA1401()).ToArray(),
        //            DataNumber = items.Select(d => d.InvoiceItem.B2BBuyerInvoiceTag.DataNumber).ToArray()
        //        };

        //        result.Result.value = 1;
        //    }
        //}


        [WebMethod]
        public XmlDocument B2BReceiveA1401(XmlDocument sellerInfo)
        {
            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(sellerInfo))
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            Root root = sellerInfo.ConvertTo<Root>();
                            acknowledgeReport(mgr, token, root.Request.periodicalIntervalSpecified ? root.Request.periodicalInterval : (int?)null);

                            var table = mgr.GetTable<DocumentDispatch>();
                            var items = mgr.GetTable<CDS_Document>().Where(d => d.CurrentStep == (int)Naming.B2BInvoiceStepDefinition.待接收 && d.DocType == (int)Naming.DocumentTypeDefinition.E_Invoice)
                                    .Join(mgr.GetTable<InvoiceItem>().Where(i => i.InvoiceBuyer.BuyerID == token.CompanyID)
                                        , d => d.DocID, i => i.InvoiceID, (d, i) => d);

                            if (items.Count() > 0)
                            {
                                var item = items.First();
                                var result = item.InvoiceItem.CreateA1401();
                                result.DataNumber = item.InvoiceItem.B2BBuyerInvoiceTag != null ? item.InvoiceItem.B2BBuyerInvoiceTag.DataNumber : null;
                                result.InvoiceID = item.DocID.ToString();

                                item.MoveToNextStep(mgr);

                                return result.ConvertToXml();
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        [WebMethod()]
        public void AcknowledgeReceiving(XmlDocument uploadData)
        {
            try
            {
                CryptoUtility crypto = new CryptoUtility();
                PKCS7Log log = crypto.CA_Log.Table.DataSet as PKCS7Log;
                if (log!=null)
                {
                    log.Crypto = crypto;
                    log.Catalog = Naming.CACatalogDefinition.UXGW自動接收;
                }
                crypto.VerifyXmlSignature(uploadData);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        [WebMethod()]
        public override void NotifyCounterpartBusiness(XmlDocument uploadData)
        {
            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    //EIVOPlatformManager mgr = new EIVOPlatformManager();
                    //mgr.NotifyToProcess();
                    EIVOPlatformFactory.Notify();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }


        [WebMethod]
        public XmlDocument B2BReceiveB1401(XmlDocument sellerInfo)
        {
            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(sellerInfo))
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            Root root = sellerInfo.ConvertTo<Root>();
                            acknowledgeReport(mgr, token, root.Request.periodicalIntervalSpecified ? root.Request.periodicalInterval : (int?)null);

                            var table = mgr.GetTable<DocumentDispatch>();
                            var items = mgr.GetTable<CDS_Document>().Where(d => d.CurrentStep == (int)Naming.B2BInvoiceStepDefinition.待接收 && d.DocType == (int)Naming.DocumentTypeDefinition.E_Allowance)
                                    .Join(mgr.GetTable<InvoiceAllowance>().Where(i => i.InvoiceAllowanceSeller.SellerID == token.CompanyID)
                                        , d => d.DocID, i => i.AllowanceID, (d, i) => d);

                            if (items.Count() > 0)
                            {
                                var item = items.First();
                                var result = item.InvoiceAllowance.CreateB1401();

                                item.MoveToNextStep(mgr);

                                return result.ConvertToXml();
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        [WebMethod]
        public XmlDocument B2BReceiveA0501(XmlDocument sellerInfo)
        {
            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(sellerInfo))
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            Root root = sellerInfo.ConvertTo<Root>();
                            acknowledgeReport(mgr, token, root.Request.periodicalIntervalSpecified ? root.Request.periodicalInterval : (int?)null);

                            var table = mgr.GetTable<DocumentDispatch>();
                            var items = mgr.GetTable<CDS_Document>().Where(d => d.CurrentStep == (int)Naming.B2BInvoiceStepDefinition.待接收 && d.DocType == (int)Naming.DocumentTypeDefinition.E_InvoiceCancellation)
                                    .Join(mgr.GetTable<DerivedDocument>()
                                        .Join(mgr.GetTable<InvoiceItem>().Where(i => i.InvoiceBuyer.BuyerID == token.CompanyID), d => d.SourceID, i => i.InvoiceID, (d, i) => d)
                                    , d => d.DocID, i => i.DocID, (d, i) => d);

                            if (items.Count() > 0)
                            {
                                var item = items.First();
                                var result = item.DerivedDocument.ParentDocument.InvoiceItem.CreateA0501();

                                item.MoveToNextStep(mgr);

                                return result.ConvertToXml();
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        [WebMethod]
        public XmlDocument B2BReceiveB0501(XmlDocument sellerInfo)
        {
            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(sellerInfo))
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            Root root = sellerInfo.ConvertTo<Root>();
                            acknowledgeReport(mgr, token, root.Request.periodicalIntervalSpecified ? root.Request.periodicalInterval : (int?)null);

                            var table = mgr.GetTable<DocumentDispatch>();
                            var items = mgr.GetTable<CDS_Document>().Where(d => d.CurrentStep == (int)Naming.B2BInvoiceStepDefinition.待接收 && d.DocType == (int)Naming.DocumentTypeDefinition.E_AllowanceCancellation)
                                    .Join(mgr.GetTable<DerivedDocument>()
                                        .Join(mgr.GetTable<InvoiceAllowance>().Where(i => i.InvoiceAllowanceSeller.SellerID == token.CompanyID)
                                        , d => d.SourceID, i => i.AllowanceID, (d, i) => d)
                                    , d => d.DocID, i => i.DocID, (d, i) => d);

                            if (items.Count() > 0)
                            {
                                var item = items.First();
                                var result = item.DerivedDocument.ParentDocument.InvoiceAllowance.CreateB0501();

                                item.MoveToNextStep(mgr);

                                return result.ConvertToXml();
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        [WebMethod]
        public String[] ReceiveContentAsPDF(XmlDocument sellerInfo)
        {
            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(sellerInfo))
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)//&& token.Organization.OrganizationStatus.EntrustToPrint == true
                        {
                            Root root = sellerInfo.ConvertTo<Root>();
                            acknowledgeReport(mgr, token, root.Request.periodicalIntervalSpecified ? root.Request.periodicalInterval : (int?)null);

                            List<String> pdfFiles = new List<String>();
                            pdfFiles.Add(String.Format("{0}{1}",
                                    Uxnet.Web.Properties.Settings.Default.HostUrl,
                                    VirtualPathUtility.ToAbsolute("~/Published/GetInvoicePDF.ashx")));
                            CipherDecipherSrv cipher = new CipherDecipherSrv();

                            var items = mgr.GetTable<DocumentSubscriptionQueue>()
                                .Join(mgr.GetTable<CDS_Document>().Where(d => d.DocumentOwner.OwnerID == token.CompanyID),
                                    q => q.DocID, d => d.DocID, (q, d) => d)
                                .Select(d => d.InvoiceItem);

                            foreach (var item in items)
                            {
                                pdfFiles.Add(String.Join(",",
                                    item.InvoiceSeller.ReceiptNo,
                                    item.InvoiceDate.Value.Year.ToString(),
                                   string.Format("{0:MM}", item.InvoiceDate.Value),
                                    string.Format("{0:dd}", item.InvoiceDate.Value),
                                    item.TrackCode + item.No,
                                    item.InvoiceID.ToString(),
                                    item.CDS_Document.CustomerDefined != null ? item.CDS_Document.CustomerDefined.IsolationFolder : null
                                    ));

                            }
                            return pdfFiles.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        [WebMethod]
        public bool DeleteTempForReceivePDF(XmlDocument sellerInfo,int docID)
        {
            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(sellerInfo))
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)//&& token.Organization.OrganizationStatus.EntrustToPrint == true
                        {
                            Root root = sellerInfo.ConvertTo<Root>();
                            acknowledgeReport(mgr, token, root.Request.periodicalIntervalSpecified ? root.Request.periodicalInterval : (int?)null);

                            var tableQ = mgr.GetTable<DocumentSubscriptionQueue>();
                            var item = tableQ.Where(d => d.DocID == docID && d.CDS_Document.DocumentOwner.OwnerID == token.CompanyID).FirstOrDefault();

                            if (item != null)
                            {

                                mgr.GetTable<DocumentDownloadLog>().InsertOnSubmit(new DocumentDownloadLog
                                {
                                    DocID = item.DocID,
                                    TypeID = (int)item.CDS_Document.DocType,
                                    DownloadDate = DateTime.Now,
                                    UID = token.Organization.OrganizationCategory.First().UserRole.First().UID
                                });

                                tableQ.DeleteOnSubmit(item);

                                mgr.SubmitChanges();

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return true;
        }

        public static String CreatePdfFile(int InvoiceID)
        {
            String File = string.Empty;
            using (WebClient client = new WebClient())
            {
                File = client.DownloadString(String.Format("{0}{1}?id={2}&nameOnly=true&printAll='0'",
                                        Uxnet.Web.Properties.Settings.Default.HostUrl,
                                        VirtualPathUtility.ToAbsolute("~/Published/PrintSingleInvoiceAsPDF.aspx"),
                                        InvoiceID));
            }
            return File;
        }

        public static void DocumentDownload(string pdfFile, InvoiceItem item)
        {

            string TempForReceivePDF = Path.Combine(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory), "TempForReceivePDF");// Server.MapPath("~/TempForReceivePDF");
            if (!Directory.Exists(TempForReceivePDF))
                Directory.CreateDirectory(TempForReceivePDF);
            String dest = Path.Combine(TempForReceivePDF, string.Format("{0}_{1:yyyy_MM}_{2}{3}.pdf", item.InvoiceSeller.Organization.ReceiptNo, item.InvoiceDate, item.TrackCode, item.No));
            if (item.InvoiceBuyer.Organization.OrganizationStatus.EntrustToPrint == true
                && !File.Exists(dest))
            {
                File.Copy(pdfFile, dest);
            }
            Logger.Info(string.Format("系統寄送已自動接收發票資料:{0}{1} 至相對營業人 {2}(Source:{3}) ", item.TrackCode, item.No, item.InvoiceBuyer.ReceiptNo, pdfFile));

        }

        [WebMethod()]
        public XmlDocument UploadCounterpartBusiness(System.Xml.XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    using (B2BInvoiceManager mgr = new B2BInvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            BusinessCounterpartXmlUploadManager csvMgr = new BusinessCounterpartXmlUploadManager(mgr);
                            csvMgr.BusinessType = Naming.InvoiceCenterBusinessType.銷項;
                            csvMgr.MasterID = token.CompanyID;
                            if (token.Organization.IsEnterpriseGroupMember())
                            {
                                csvMgr.MasterGroup = token.Organization.EnterpriseGroupMember.First().EnterpriseGroup.EnterpriseGroupMember.Select(m => m.CompanyID).ToArray();
                            }
                            csvMgr.SaveData(uploadData);

                            if (csvMgr.ErrorList.Count>0)
                            {
                                var items = csvMgr.ErrorList;
                                result.Response = new RootResponse
                                {
                                    InvoiceNo =
                                    items.Select(d => new RootResponseInvoiceNo
                                    {
                                        Value = d.DataContent,
                                        Description = d.Status,
                                        ItemIndexSpecified = true,
                                        ItemIndex = csvMgr.ItemList.IndexOf(d)
                                    }).ToArray()
                                };

                                ThreadPool.QueueUserWorkItem(B2BExceptionNotification.SendNotification,
                                    new B2BExceptionInfo
                                    {
                                        Token = token,
                                        CounterpartBusinessError = items
                                    });
                            }
                            else
                            {
                                result.Result.value = 1;
                            }
                        }
                        else
                        {
                            result.Result.message = "會員憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                //GovPlatformFactory.Notify();
                //EIVOPlatformFactory.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }


    }
}

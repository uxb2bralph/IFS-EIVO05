using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Xml;

using eIVOGo.Module.Common;
using eIVOGo.Properties;
using Model.DataEntity;
using Model.Helper;
using Model.InvoiceManagement;
using Model.Locale;
using Model.Schema.EIVO;
using Model.Schema.TurnKey;
using Model.Schema.TXN;
using Utility;
using Uxnet.Com.Security.UseCrypto;
using System.IO;

namespace eIVOGo.Published
{
    /// <summary>
    /// Summary description for eInvoiceService
    /// </summary>
    [WebService(Namespace = "http://www.uxb2b.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class eInvoiceService : System.Web.Services.WebService
    {
        static eInvoiceService()
        {
            GovPlatformFactoryForB2C.SendNotification =
                            (o, e) =>
                            {
                                try
                                {
                                    Settings.Default.GovPlatformNotificationUrl
                                        .MailServerPage(Uxnet.Web.Properties.Settings.Default.WebMaster, "網際優勢電子發票獨立第三方平台 大平台傳送異常通知");
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex);
                                }
                            };

            GovPlatformFactory.SendNotification =
                (o, e) =>
                {
                    try
                    {
                        Settings.Default.GovPlatformNotificationUrl
                            .MailServerPage(Uxnet.Web.Properties.Settings.Default.WebMaster, "電子發票系統 大平台傳送異常通知");
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                };

            ExceptionNotification.SendExceptionNotification =
                (o, e) =>
                {
                    try
                    {
                        MailMessage message = new MailMessage();
                        message.From = new MailAddress(Uxnet.Web.Properties.Settings.Default.WebMaster);
                        if (String.IsNullOrEmpty(e.EMail))
                        {
                            message.To.Add(Uxnet.Web.Properties.Settings.Default.WebMaster);
                        }
                        else
                        {
                            message.To.Add(e.EMail);
                        }
                        message.Subject = "電子發票系統 商家資料傳送異常通知";
                        message.IsBodyHtml = true;

                        using (WebClient wc = new WebClient())
                        {
                            wc.Encoding = Encoding.UTF8;
                            message.Body = wc.DownloadString(String.Format("{0}{1}?companyID={2}",
                                Uxnet.Web.Properties.Settings.Default.HostUrl,
                                VirtualPathUtility.ToAbsolute(Settings.Default.ExceptionNotificationUrl),
                                e.CompanyID));
                        }

                        SmtpClient smtpclient = new SmtpClient(Uxnet.Web.Properties.Settings.Default.MailServer);
                        smtpclient.Credentials = CredentialCache.DefaultNetworkCredentials;
                        smtpclient.Send(message);

                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                };
        }

        public static void StartUp() { }

        [WebMethod]
        public virtual XmlDocument UploadInvoice(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    InvoiceRoot invoice = uploadData.ConvertTo<InvoiceRoot>();
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            var items = mgr.SaveUploadInvoice(invoice,token);
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

                                ThreadPool.QueueUserWorkItem(ExceptionNotification.SendNotification,
                                    new ExceptionInfo
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
                            result.Result.message = "商家憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                GovPlatformFactory.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod]
        public virtual XmlDocument UploadAllowance(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    AllowanceRoot allowance = uploadData.ConvertTo<AllowanceRoot>();
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            var items = mgr.SaveUploadAllowance(allowance,token);
                            if (items.Count > 0)
                            {
                                result.Response = new RootResponse
                                {
                                    InvoiceNo =
                                    items.Select(d => new RootResponseInvoiceNo
                                    {
                                        Value = allowance.Allowance[d.Key].AllowanceNumber,
                                        Description = d.Value.Message,
                                        ItemIndexSpecified = true,
                                        ItemIndex = d.Key
                                    }).ToArray()
                                };

                                ThreadPool.QueueUserWorkItem(ExceptionNotification.SendNotification,
                                    new ExceptionInfo
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
                            result.Result.message = "商家憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                GovPlatformFactory.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod]
        public virtual XmlDocument UploadB1401(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    Model.Schema.TurnKey.B1401.Allowance allowance = uploadData.ConvertTo<Model.Schema.TurnKey.B1401.Allowance>();
                    using (B2BInvoiceManager mgr = new B2BInvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            mgr.SaveB1401(allowance, token);
                            result.Result.value = 1;
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

                EIVOPlatformFactory.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod]
        public virtual XmlDocument UploadB0401(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    Model.Schema.TurnKey.B0401.Allowance allowance = uploadData.ConvertTo<Model.Schema.TurnKey.B0401.Allowance>();
                    using (B2BInvoiceManager mgr = new B2BInvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            mgr.SaveB0401(allowance, token);
                            result.Result.value = 1;
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

                EIVOPlatformFactory.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod]
        public virtual XmlDocument UploadInvoiceAutoTrackNo(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    InvoiceRoot invoice = uploadData.ConvertTo<InvoiceRoot>();
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            var items = mgr.SaveUploadInvoiceAutoTrackNo(invoice, token);
                            if (items.Count > 0)
                            {
                                result.Response = new RootResponse
                                {
                                    InvoiceNo =
                                    items.Select(d => new RootResponseInvoiceNo
                                    {
                                        Value =  invoice.Invoice[d.Key].InvoiceNumber,
                                        Description = d.Value.Message,
                                        ItemIndexSpecified = true,
                                        ItemIndex = d.Key
                                    }).ToArray()
                                };

                                ThreadPool.QueueUserWorkItem(ExceptionNotification.SendNotification,
                                    new ExceptionInfo
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
                            result.Result.message = "商家憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                GovPlatformFactory.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod]
        public virtual XmlDocument UploadInvoiceCmsCSVAutoTrackNo(byte[] uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                byte[] dataToSign;
                if (crypto.VerifyEnvelopedPKCS7(uploadData,out dataToSign))
                {
                    String fileName = Path.Combine(Logger.LogDailyPath,String.Format("Invoice_{0}.csv",Guid.NewGuid()));
                    using (FileStream fs = new FileStream(fileName, FileMode.Create))
                    {
                        fs.Write(dataToSign, 0, dataToSign.Length);
                        fs.Flush();
                        fs.Close();
                    }

                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            CsvInvoiceUploadManager csvMgr = new CsvInvoiceUploadManager(mgr, token.CompanyID);
                            csvMgr.ParseData(null, fileName, Encoding.GetEncoding(Settings.Default.CsvUploadEncoding));
                            if(!csvMgr.Save())
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

                                ThreadPool.QueueUserWorkItem(ExceptionNotification.SendNotification,
                                    new ExceptionInfo
                                    {
                                        Token = token,
                                        InvoiceError =  items
                                    });
                            }
                            else
                            {
                                result.Result.value = 1;
                                csvMgr.SendInvoiceMail();
                            }
                        }
                        else
                        {
                            result.Result.message = "商家憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                GovPlatformFactory.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod]
        public virtual XmlDocument UploadInvoiceCancellationCmsCSV(byte[] uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                byte[] dataToSign;
                if (crypto.VerifyEnvelopedPKCS7(uploadData, out dataToSign))
                {
                    String fileName = Path.Combine(Logger.LogDailyPath, String.Format("CancelInvoie_{0}.csv", Guid.NewGuid()));
                    using (FileStream fs = new FileStream(fileName, FileMode.Create))
                    {
                        fs.Write(dataToSign, 0, dataToSign.Length);
                        fs.Flush();
                        fs.Close();
                    }

                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            CsvInvoiceCancellationUploadManager csvMgr = new CsvInvoiceCancellationUploadManager(mgr, token.CompanyID);
                            csvMgr.ParseData(null, fileName, Encoding.GetEncoding(Settings.Default.CsvUploadEncoding));
                            if (!csvMgr.Save())
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

                                ThreadPool.QueueUserWorkItem(ExceptionNotification.SendNotification,
                                    new ExceptionInfo
                                    {
                                        Token = token,
                                        InvoiceCancellationError = items
                                    });
                            }
                            else
                            {
                                result.Result.value = 1;
                                csvMgr.ItemList.Select(i => i.Entity.InvoiceID).SendB2CInvoiceCancellationMail();
                            }
                        }
                        else
                        {
                            result.Result.message = "商家憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                GovPlatformFactory.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }



        protected Root createMessageToken()
        {
            Root result = new Root
            {
                UXB2B = "電子發票系統",
                Result = new RootResult
                {
                    timeStamp = DateTime.Now,
                    value = 0
                }
            };
            return result;
        }

        [WebMethod]
        public virtual XmlDocument UploadInvoiceCancellation(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    CancelInvoiceRoot item = uploadData.ConvertTo<CancelInvoiceRoot>();
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {

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

                                ThreadPool.QueueUserWorkItem(ExceptionNotification.SendNotification,
                                                                    new ExceptionInfo
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
                            result.Result.message = "商家憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                GovPlatformFactory.Notify();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod]
        public virtual XmlDocument UploadAllowanceCancellation(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    CancelAllowanceRoot item = uploadData.ConvertTo<CancelAllowanceRoot>();
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {

                            var items = mgr.SaveUploadAllowanceCancellation(item);
                            if (items.Count > 0)
                            {
                                result.Response = new RootResponse
                                {
                                    InvoiceNo =
                                    items.Select(d => new RootResponseInvoiceNo
                                    {
                                        Value = item.CancelAllowance[d.Key].CancelAllowanceNumber,
                                        Description = d.Value.Message,
                                        ItemIndexSpecified = true,
                                        ItemIndex = d.Key
                                    }).ToArray()
                                };

                                ThreadPool.QueueUserWorkItem(ExceptionNotification.SendNotification,
                                                                    new ExceptionInfo
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
                            result.Result.message = "商家憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                GovPlatformFactory.Notify();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod]
        public virtual XmlDocument UploadB0501(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    Model.Schema.TurnKey.B0501.CancelAllowance item = uploadData.ConvertTo<Model.Schema.TurnKey.B0501.CancelAllowance>();
                    using (B2BInvoiceManager mgr = new B2BInvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {

                            mgr.SaveB0501(item, token);
                            result.Result.value = 1;
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

//                EIVOPlatformFactory.Notify();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod]
        public XmlDocument GetUpdatedWelfareAgenciesInfo(String sellerReceiptNo)
        {
            try
            {
                using (InvoiceManager mgr = new InvoiceManager())
                {
                    var items = mgr.GetUpdatedWelfareAgenciesForSeller(sellerReceiptNo);
                    if (items != null && items.Count() > 0)
                    {
                        SocialWelfareAgenciesRoot welfare = new SocialWelfareAgenciesRoot
                        {
                            CompanyBan = sellerReceiptNo,
                            SocialWelfareAgencies = items.Select(i => new SocialWelfareAgenciesRootSocialWelfareAgencies
                            {
                                Address = i.InvoiceWelfareAgency.WelfareAgency.Organization.Addr,
                                Ban = i.InvoiceWelfareAgency.WelfareAgency.Organization.ReceiptNo,
                                Email = String.IsNullOrEmpty(i.InvoiceWelfareAgency.WelfareAgency.Organization.ContactEmail) ? "N/A" : i.InvoiceWelfareAgency.WelfareAgency.Organization.ContactEmail,
                                Name = i.InvoiceWelfareAgency.WelfareAgency.Organization.CompanyName,
                                TEL = i.InvoiceWelfareAgency.WelfareAgency.Organization.Phone,
                                Code = String.IsNullOrEmpty(i.InvoiceWelfareAgency.WelfareAgency.AgencyCode) ? "待登錄" : i.InvoiceWelfareAgency.WelfareAgency.AgencyCode
                            }).ToArray()
                        };

                        mgr.GetTable<WelfareReplication>().DeleteAllOnSubmit(items);
                        mgr.SubmitChanges();

                        return welfare.ConvertToXml();
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
        public XmlDocument GetWelfareAgenciesInfo(String sellerReceiptNo)
        {
            try
            {
                using (InvoiceManager mgr = new InvoiceManager())
                {
                    var items = mgr.GetWelfareAgenciesForSeller(sellerReceiptNo);
                    if (items != null && items.Count() > 0)
                    {
                        SocialWelfareAgenciesRoot welfare = new SocialWelfareAgenciesRoot
                        {
                            CompanyBan = sellerReceiptNo,
                            SocialWelfareAgencies = items.Select(i => new SocialWelfareAgenciesRootSocialWelfareAgencies
                            {
                                Address = i.WelfareAgency.Organization.Addr,
                                Ban = i.WelfareAgency.Organization.ReceiptNo,
                                Email = String.IsNullOrEmpty(i.WelfareAgency.Organization.ContactEmail) ? "N/A" : i.WelfareAgency.Organization.ContactEmail,
                                Name = i.WelfareAgency.Organization.CompanyName,
                                TEL = i.WelfareAgency.Organization.Phone,
                                Code = String.IsNullOrEmpty(i.WelfareAgency.AgencyCode) ? "待登錄" : i.WelfareAgency.AgencyCode
                            }).ToArray()
                        };
                        return welfare.ConvertToXml();
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
        public virtual XmlDocument GetIncomingInvoices(XmlDocument sellerInfo)
        {
            RootA0101 result = new RootA0101
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
                if (crypto.VerifyXmlSignature(sellerInfo))
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            buildIncomingInvoices(result, mgr, token.CompanyID);
                            Root root = sellerInfo.ConvertTo<Root>();
                            acknowledgeReport(mgr, token, root.Request.periodicalIntervalSpecified ? root.Request.periodicalInterval : (int?)null);
                        }
                        else
                        {
                            result.Result.message = "商家憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                //GovPlatformFactory.Notify();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        protected virtual void buildIncomingInvoices(Root result, InvoiceManager mgr, int companyID)
        {
            var table = mgr.GetTable<DocumentDispatch>();
            var items = table.Where(d => d.CDS_Document.DocumentOwner.OwnerID == companyID && d.TypeID == (int)Naming.DocumentTypeDefinition.E_Invoice);

            if (items.Count() > 0)
            {
                result.Response = new RootResponseForA0101
                {
                    Invoice =
                    items.Select(d => d.CDS_Document.InvoiceItem.BuildA0101()).ToArray()
                };

                table.DeleteAllOnSubmit(items);
                mgr.SubmitChanges();

                result.Result.value = 1;
            }
        }

        [WebMethod]
        public XmlDocument GetIncomingInvoiceCancellations(XmlDocument sellerInfo)
        {
            RootA0201 result = new RootA0201
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
                if (crypto.VerifyXmlSignature(sellerInfo))
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            buildIncomingInvoiceCancellations(result, mgr, token.CompanyID);
                        }
                        else
                        {
                            result.Result.message = "商家憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                //GovPlatformFactory.Notify();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        protected void buildIncomingInvoiceCancellations(Root result, InvoiceManager mgr, int companyID)
        {
            var table = mgr.GetTable<DocumentDispatch>();
            var items = table.Where(d => d.CDS_Document.DocumentOwner.OwnerID == companyID && d.TypeID == (int)Naming.DocumentTypeDefinition.E_InvoiceCancellation);

            if (items.Count() > 0)
            {
                result.Response = new RootResponseForA0201
                {
                    CancelInvoice =
                    items.Select(d => d.CDS_Document.InvoiceItem.BuildA0201()).ToArray()
                };

                table.DeleteAllOnSubmit(items);
                mgr.SubmitChanges();

                result.Result.value = 1;
            }
        }

        [WebMethod]
        public XmlDocument GetIncomingAllowances(XmlDocument sellerInfo)
        {
            RootB0101 result = new RootB0101
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
                if (crypto.VerifyXmlSignature(sellerInfo))
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            buildIncomingAllowances(result, mgr, token.CompanyID);
                        }
                        else
                        {
                            result.Result.message = "商家憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                //GovPlatformFactory.Notify();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        protected void buildIncomingAllowances(Root result, InvoiceManager mgr, int companyID)
        {
            var table = mgr.GetTable<DocumentDispatch>();
            var items = table.Where(d => d.CDS_Document.DocumentOwner.OwnerID == companyID && d.TypeID == (int)Naming.DocumentTypeDefinition.E_Allowance);

            if (items.Count() > 0)
            {
                result.Response = new RootResponseForB0101
                {
                    Allowance =
                    items.Select(d => d.CDS_Document.InvoiceAllowance.BuildB0101()).ToArray()
                };

                table.DeleteAllOnSubmit(items);
                mgr.SubmitChanges();

                result.Result.value = 1;
            }
        }

        [WebMethod]
        public XmlDocument GetIncomingAllowanceCancellations(XmlDocument sellerInfo)
        {
            RootB0201 result = new RootB0201
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
                if (crypto.VerifyXmlSignature(sellerInfo))
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            buildIncomingAllowanceCancellations(result, mgr, token.CompanyID);
                        }
                        else
                        {
                            result.Result.message = "商家憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                //GovPlatformFactory.Notify();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        protected void buildIncomingAllowanceCancellations(Root result, InvoiceManager mgr, int companyID)
        {
            var table = mgr.GetTable<DocumentDispatch>();
            var items = table.Where(d => d.CDS_Document.DocumentOwner.OwnerID == companyID && d.TypeID == (int)Naming.DocumentTypeDefinition.E_AllowanceCancellation);

            if (items.Count() > 0)
            {
                result.Response = new RootResponseForB0201
                {
                    CancelAllowance =
                    items.Select(d => d.CDS_Document.InvoiceAllowance.BuildB0201()).ToArray()
                };

                table.DeleteAllOnSubmit(items);
                mgr.SubmitChanges();

                result.Result.value = 1;
            }
        }

        [WebMethod]
        public void AcknowledgeLivingReport(XmlDocument sellerInfo)
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
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        protected void acknowledgeReport(InvoiceManager mgr, OrganizationToken token,int? periodicalInterval)
        {
            if (token.Organization.OrganizationStatus == null)
            {
                token.Organization.OrganizationStatus = new OrganizationStatus
                {
                    CurrentLevel = (int)Naming.MemberStatusDefinition.Checked
                };
            }
            token.Organization.OrganizationStatus.LastTimeToAcknowledge = DateTime.Now;
            token.Organization.OrganizationStatus.RequestPeriodicalInterval = periodicalInterval;
            mgr.SubmitChanges();
        }

        [WebMethod]
        public XmlDocument GetIncomingWinningInvoices(XmlDocument sellerInfo)
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
                            var table = mgr.GetTable<InvoiceWinningNumber>();
                            var items = table.Where(w => w.InvoiceItem.CDS_Document.DocumentOwner.OwnerID == token.CompanyID && !w.DownloadDate.HasValue);
                            if (items.Count() > 0)
                            {
                                var welfare = token.Organization.InvoiceWelfareAgencies.Select(w => w.WelfareAgency.Organization).FirstOrDefault();
                                String welfareReceiptNo = welfare != null ? welfare.ReceiptNo : null;
                                BonusInvoiceRoot root = new BonusInvoiceRoot
                                {
                                    BonusInvoice = items.Select(w => new BonusInvoiceRootBonusInvoice
                                    {
                                        InvoiceNumber = String.Concat(w.InvoiceItem.TrackCode, w.InvoiceItem.No),
                                        SWABan = welfareReceiptNo
                                    }).ToArray()
                                };
                                foreach (var item in items)
                                {
                                    item.DownloadDate = DateTime.Now;
                                }
                                mgr.SubmitChanges();

                                return root.ConvertToXml();
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
        public String GetSignerCertificateContent(String activationKey)
        {
            using (InvoiceManager mgr = new InvoiceManager())
            {
                Guid keyID;
                if (Guid.TryParse(activationKey, out keyID))
                {

                    var item = mgr.GetTable<OrganizationToken>().Where(t => t.KeyID == keyID).FirstOrDefault();
                    if (item != null)
                    {
                        return item.PKCS12;
                    }
                }
            }
            return null;
        }

        [WebMethod]
        public XmlDocument GetRegisteredMember(XmlDocument sellerInfo)
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
                            return token.Organization.SerializeDataContractToXml();
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
        public virtual XmlDocument UploadA1101(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    Model.Schema.TurnKey.A1101.Invoice invoice = uploadData.ConvertTo<Model.Schema.TurnKey.A1101.Invoice>();
                    using (B2BInvoiceManager mgr = new B2BInvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            mgr.SaveA1101(invoice, token);
                            result.Result.value = 1;
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

                EIVOPlatformFactory.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod]
        public virtual XmlDocument UploadA1401(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    Model.Schema.TurnKey.A1401.Invoice invoice = uploadData.ConvertTo<Model.Schema.TurnKey.A1401.Invoice>();
                    using (B2BInvoiceManager mgr = new B2BInvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            mgr.SaveA1401(invoice, token);
                            result.Result.value = 1;
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

                EIVOPlatformFactory.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod]
        public virtual XmlDocument UploadA0401(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    Model.Schema.TurnKey.A0401.Invoice invoice = uploadData.ConvertTo<Model.Schema.TurnKey.A0401.Invoice>();
                    using (B2BInvoiceManager mgr = new B2BInvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            mgr.SaveA0401(invoice, token);
                            result.Result.value = 1;
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

                EIVOPlatformFactory.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod]
        public virtual XmlDocument UploadB1101(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    Model.Schema.TurnKey.B1101.Allowance allowance = uploadData.ConvertTo<Model.Schema.TurnKey.B1101.Allowance>();
                    using (B2BInvoiceManager mgr = new B2BInvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            mgr.SaveB1101(allowance, token);
                            result.Result.value = 1;
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

                EIVOPlatformFactory.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod]
        public virtual XmlDocument UploadA0201(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    Model.Schema.TurnKey.A0201.CancelInvoice item = uploadData.ConvertTo<Model.Schema.TurnKey.A0201.CancelInvoice>();
                    using (B2BInvoiceManager mgr = new B2BInvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {

                            mgr.SaveA0201(item, token);
                            result.Result.value = 1;
                        }
                        else
                        {
                            result.Result.message = "商家憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                EIVOPlatformFactory.Notify();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod]
        public virtual XmlDocument UploadA0501(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    Model.Schema.TurnKey.A0501.CancelInvoice item = uploadData.ConvertTo<Model.Schema.TurnKey.A0501.CancelInvoice>();
                    using (B2BInvoiceManager mgr = new B2BInvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {

                            mgr.SaveA0501(item, token);
                            result.Result.value = 1;
                        }
                        else
                        {
                            result.Result.message = "商家憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                EIVOPlatformFactory.Notify();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }


        [WebMethod]
        public virtual XmlDocument UploadB0201(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    Model.Schema.TurnKey.B0201.CancelAllowance item = uploadData.ConvertTo<Model.Schema.TurnKey.B0201.CancelAllowance>();
                    using (B2BInvoiceManager mgr = new B2BInvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {

                            mgr.SaveB0201(item, token);
                            result.Result.value = 1;
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

                EIVOPlatformFactory.Notify();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }


        [WebMethod]
        public XmlDocument B2CUploadInvoice(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    InvoiceRoot invoice = uploadData.ConvertTo<InvoiceRoot>();
                    using (B2CInvoiceManager mgr = new B2CInvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {
                            var items = mgr.SaveUploadInvoice(invoice,token);
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

                                ThreadPool.QueueUserWorkItem(ExceptionNotification.SendNotification,
                                    new ExceptionInfo
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
                            result.Result.message = "商家憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                GovPlatformFactoryForB2C.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod]
        public XmlDocument B2CUploadInvoiceCancellation(XmlDocument uploadData)
        {
            Root result = createMessageToken();

            try
            {
                CryptoUtility crypto = new CryptoUtility();
                if (crypto.VerifyXmlSignature(uploadData))
                {
                    CancelInvoiceRoot item = uploadData.ConvertTo<CancelInvoiceRoot>();
                    using (B2CInvoiceManager mgr = new B2CInvoiceManager())
                    {
                        ///憑證資料檢查
                        ///
                        var token = mgr.GetTable<OrganizationToken>().Where(t => t.Thumbprint == crypto.SignerCertificate.Thumbprint).FirstOrDefault();
                        if (token != null)
                        {

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

                                ThreadPool.QueueUserWorkItem(ExceptionNotification.SendNotification,
                                                                    new ExceptionInfo
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
                            result.Result.message = "商家憑證資料驗證不符!!";
                        }
                    }
                }
                else
                {
                    result.Result.message = "發票資料簽章不符!!";
                }

                GovPlatformFactoryForB2C.Notify();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Result.message = ex.Message;
            }
            return result.ConvertToXml();
        }

        [WebMethod()]
        public virtual void NotifyCounterpartBusiness(XmlDocument uploadData)
        {

        }



    }
}

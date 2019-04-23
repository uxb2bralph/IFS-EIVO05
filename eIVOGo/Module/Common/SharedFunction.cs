using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net.Mail;
using eIVOGo.Properties;
using System.Net;
using Utility;
using Model.Security.MembershipManagement;
using Model.DataEntity;
using Model.InvoiceManagement;
using System.Threading;

using System.IO;
using Model.Locale;
using System.Net.Mime;

namespace eIVOGo.Module.Common
{
    public static class SharedFunction
    {
        #region "Using Thread to Send Notify Mail"
        /// <summary>
        /// 定義類別參數
        /// </summary>
        public class _MailQueryState
        {
            public int setYear { get; set; }
            public int setPeriod { get; set; }
            public string allInvoiceID { get; set; }
        }

        /// <summary>
        /// 外部呼叫執行Mail通知
        /// </summary>
        /// <param name="mailState"></param>
        public static void doSendMaild(_MailQueryState mailState)
        {
            ThreadPool.QueueUserWorkItem(mailWorkItem, mailState);
        }

        /// <summary>
        /// Mail內容的建立
        /// </summary>
        /// <param name="stateInfo"></param>
        private static void mailWorkItem(object stateInfo)
        {
            _MailQueryState state = (_MailQueryState)stateInfo;
            if (string.IsNullOrEmpty(state.allInvoiceID))
            {
                int year = state.setYear;
                int period = state.setPeriod;
                int smonth = (period * 2) - 1;
                int emonth = period * 2;
                try
                {
                    using (InvoiceManager im = new InvoiceManager())
                    {
                        var winningInvoices = im.GetTable<InvoiceWinningNumber>().Where(n => n.Year == year & n.MonthFrom == (byte)smonth & n.MonthTo == (byte)emonth);
                        foreach (var d in winningInvoices)
                        {
                            if (im.GetTable<OrganizationCategory>().Where(og => og.CategoryID == (int)Naming.CategoryID.COMP_VIRTUAL_CHANNEL && og.CompanyID == d.InvoiceItem.SellerID).Count() > 0)
                            {
                                string url = String.Format("{0}{1}?{2}", Uxnet.Web.Properties.Settings.Default.HostUrl, VirtualPathUtility.ToAbsolute("~/Published/WinningInvoiceMailPageForVIRTUAL.aspx"), (new CipherDecipherSrv(16)).cipher(d.InvoiceID.ToString()));
                                sendInvoiceNotifyMail(d.InvoiceItem.InvoiceBuyer.EMail, url, "中獎電子發票郵件通知");
                            }
                            else
                            {
                                string url = String.Format("{0}{1}?{2}", Uxnet.Web.Properties.Settings.Default.HostUrl, VirtualPathUtility.ToAbsolute("~/Published/WinningInvoiceMailPage.aspx"), (new CipherDecipherSrv(16)).cipher(d.InvoiceID.ToString()));
                                sendInvoiceNotifyMail(d.InvoiceItem.InvoiceBuyer.EMail, url, "Google中獎電子發票郵件通知");
                                //sendInvoiceNotifyMail("howard@uxb2b.com", url, "Google中獎電子發票郵件通知");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }
            else
            {
                string[] allID = state.allInvoiceID.Split(',');
                try
                {
                    using (InvoiceManager im = new InvoiceManager())
                    {
                        foreach (var d in allID)
                        {
                            string mailTo = im.EntityList.Where(i => i.InvoiceID == int.Parse(d)).FirstOrDefault().InvoiceBuyer.EMail;
                            string url = String.Format("{0}{1}?{2}", Uxnet.Web.Properties.Settings.Default.HostUrl, VirtualPathUtility.ToAbsolute("~/Published/InvoiceCancelMailPage.aspx"), (new CipherDecipherSrv(16)).cipher(d.Trim()));
                            sendInvoiceNotifyMail(mailTo, url, "Google作廢電子發票郵件通知");
                            //sendInvoiceNotifyMail("howard@uxb2b.com", url, "Google作廢電子發票郵件通知");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }
        }
        #endregion


        public static void sendMail(string mailto, string pid, string tempPassword, string type)
        {
            try
            {
                StringBuilder body = new StringBuilder();
                MailMessage message = new MailMessage();
                message.From = new MailAddress(Uxnet.Web.Properties.Settings.Default.WebMaster);
                message.To.Add(mailto);
                if (type.Equals("member"))
                {
                    message.Subject = "網際優勢電子發票獨立第三方平台 會員啟用認證信";
                }
                else
                {
                    message.Subject = "網際優勢電子發票獨立第三方平台 商家帳號啟用認證信";
                }
                message.IsBodyHtml = true;

                if (type.Equals("member"))
                    body.Append("本信件由 網際優勢電子發票獨立第三方平台 寄出，為本站之會員註冊確認信。<br><br>");
                else
                    body.Append("本信件由 網際優勢電子發票獨立第三方平台 寄出，為本站商家帳號啟用認證信。<br><br>");

                body.Append("(本信件為系統自動發出，請勿回覆本信件。)<br>");
                body.Append("-------------------------------------------------<br>");
                body.Append("會員帳號：").Append(pid).Append("<br>");
                body.Append("會員密碼：").Append(tempPassword).Append("<br>");
                body.Append("-------------------------------------------------<br>");
                body.Append("請立即透過下面帳號啟用連結登入 網際優勢電子發票獨立第三方平台 變更密碼 。<br><br>");
                body.Append("帳號啟用連結： ");
                body.Append("<a href=").Append(Uxnet.Web.Properties.Settings.Default.HostUrl).Append(VirtualPathUtility.ToAbsolute("~/SAM/EditMyself.aspx")).Append("?active=aEfs45WE>會員帳號啟用</a>");
                body.Append("<br><br>電子發票獨立第三方平台 感謝您的加入");

                message.Body = body.ToString();

                SmtpClient smtpclient = new SmtpClient(Uxnet.Web.Properties.Settings.Default.MailServer);
                smtpclient.Credentials = CredentialCache.DefaultNetworkCredentials;
                smtpclient.Send(message);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public static Boolean CreatePWDSendMail(int uid)
        {
            Boolean result = false;
            try
            {
                using (UserProfileManager mgr = new UserProfileManager())
                {   
                    IQueryable<UserProfile> items = mgr.EntityList.Where(u => u.UID == uid);
                    string _tempPassword = Utility.ExtensionMethods.CreateRandomPassword(6);
                    items.FirstOrDefault().Password2 = Utility.ValueValidity.MakePassword(_tempPassword);
                    var BusinessRelationship=mgr.GetTable<BusinessRelationship>().Where(b=>b.RelativeID==items.FirstOrDefault().UserRole.FirstOrDefault().OrganizationCategory.CompanyID);
                    StringBuilder body = new StringBuilder();
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(Uxnet.Web.Properties.Settings.Default.WebMaster);
                    message.To.Add(items.FirstOrDefault().EMail);
                    message.Subject = "網際優勢電子發票獨立第三方平台 會員啟用認證信";
                    message.IsBodyHtml = true;

                    body.Append("本信件由 網際優勢電子發票獨立第三方平台 寄出，為本站之會員啟用認證信。<br><br>");
                    body.Append("(本信件為系統自動發出，請勿回覆本信件。)<br>");
                    body.Append("-------------------------------------------------<br>");
                    body.Append("會員帳號：").Append(items.FirstOrDefault().PID).Append("<br>");
                    body.Append("會員密碼：").Append(_tempPassword).Append("<br>");
                    body.Append("-------------------------------------------------<br>");
                    body.Append("請立即透過下方帳號啟用連結 登入網際優勢電子發票獨立第三方平台 變更密碼 。<br><br>");
                    body.Append("帳號啟用連結： ");
                    body.Append("<a href=").Append(Uxnet.Web.Properties.Settings.Default.HostUrl).Append(VirtualPathUtility.ToAbsolute("~/SAM/EditMyself.aspx")).Append("?active=aEfs45WE>會員帳號啟用</a><br><br>");
                    var count=BusinessRelationship.ToList().Where(b=>b.BusinessMaster.EnterpriseGroupMember.FirstOrDefault().EnterpriseID == 2).Count();
                    if (count > 0)
                    {
                        body.Append("為提昇 貴公司與本公司發票作業效率，依財政部「電子發票實施作業要點」將全面實施使用<br> ");
                        body.Append("網際網路傳輸統一發票（簡稱「電子發票」），系統會主動寄發電子發票檔至貴單位e_Mail信箱，<br> ");
                        body.Append("不再郵寄傳統紙本發票。<br> ");
                        body.Append("若需要紙本發票可登入電子發票系統 https://eivo.uxifs.com/login.aspx ，進行查詢、列印。<br> ");
                        body.Append("報稅方面，若 貴公司是人工申報，請列印電子發票申報；若是媒體申報，則沒有任何改變。<br> ");
                    }
                    body.Append("<br><br>電子發票獨立第三方平台 感謝您的加入");

                    message.Body = body.ToString();
                    
                    SmtpClient smtpclient = new SmtpClient(Uxnet.Web.Properties.Settings.Default.MailServer);
                    smtpclient.Credentials = CredentialCache.DefaultNetworkCredentials;
                    smtpclient.Send(message);

                    mgr.SubmitChanges();
                }
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return result;
        }

        public static Boolean sendInvoiceNotifyMail(string mailto, string Url, string Title)
        {
            Boolean isSuccess = true;
            try
            {
                Url.Trim().MailWebPage(mailto, Title);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                isSuccess = false;
            }
            return isSuccess;
        }

        public static Boolean sendWinningAlertMail(string mailto, string Url)
        {
            Boolean isSuccess = true;
            try
            {
                Url.Trim().MailWebPage(mailto, "網際優勢電子發票獨立第三方平台 發票中獎通知");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                isSuccess = false;
            }
            return isSuccess;
        }

        public static void MailWebPage(this String url, String mailTo, String subject)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(Uxnet.Web.Properties.Settings.Default.WebMaster);
            message.To.Add(mailTo.Replace(';', ',').Replace('、', ','));
            message.Subject = subject;
            message.IsBodyHtml = true;

            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                message.Body = wc.DownloadString(url);
            }

            SmtpClient smtpclient = new SmtpClient(Uxnet.Web.Properties.Settings.Default.MailServer);
            smtpclient.Credentials = CredentialCache.DefaultNetworkCredentials;
            smtpclient.Send(message);

        }

        public static void MailServerPage(this String relativeUrl, String mailTo, String subject)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(Uxnet.Web.Properties.Settings.Default.WebMaster);

            message.To.Add(mailTo.Replace(';', ',').Replace('、', ','));
            message.Subject = subject;
            message.IsBodyHtml = true;

            message.Body = relativeUrl.GetPageContent();

            SmtpClient smtpclient = new SmtpClient(Uxnet.Web.Properties.Settings.Default.MailServer);
            smtpclient.Credentials = CredentialCache.DefaultNetworkCredentials;
            smtpclient.Send(message);

        }


        public static void SendMailMessage(this String body, String mailTo, String subject,params String[] attachment)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(Uxnet.Web.Properties.Settings.Default.WebMaster);

            mailTo = mailTo.Replace(';', ',').Replace('、',',');
            message.To.Add(mailTo);
            message.Subject = subject;
            message.IsBodyHtml = false;
            message.Body = body;

            if (attachment != null && attachment.Length > 0)
            {
                foreach (var item in attachment)
                {
                    if (File.Exists(item))
                    {
                        message.Attachments.Add(new System.Net.Mail.Attachment(item, MediaTypeNames.Application.Octet));
                    }
                }
            }

            SmtpClient smtpclient = new SmtpClient(Uxnet.Web.Properties.Settings.Default.MailServer);
            smtpclient.Credentials = CredentialCache.DefaultNetworkCredentials;
            smtpclient.Send(message);

        }


        public static void SendMailMessage(this String body, String mailTo, String subject, System.Net.Mail.Attachment[] attachment)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(Uxnet.Web.Properties.Settings.Default.WebMaster);
            message.To.Add(mailTo);
            message.Subject = subject;
            message.IsBodyHtml = false;
            message.Body = body;

            if (attachment != null && attachment.Length > 0)
            {
                foreach (var item in attachment)
                {
                    message.Attachments.Add(item);
                }
            }

            SmtpClient smtpclient = new SmtpClient(Uxnet.Web.Properties.Settings.Default.MailServer);
            smtpclient.Credentials = CredentialCache.DefaultNetworkCredentials;
            smtpclient.Send(message);

        }

        public static void SendMailMessage(this String body, String mailTo, String subject, System.Net.Mail.Attachment[] attachment,System.Net.Mail.Attachment[] Ad)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(Uxnet.Web.Properties.Settings.Default.WebMaster);
            message.To.Add(mailTo);
            message.Subject = subject;
            message.IsBodyHtml = true;
            //message.Body = string.Format("<img src=\"cid:attach.gif\" />{0}", body);
            message.BodyEncoding = Encoding.GetEncoding("utf-8");
            if (attachment != null && attachment.Length > 0)
            {
                foreach (var item in attachment)
                {
                    message.Attachments.Add(item);
                }
            }
            if (Ad != null && Ad.Length > 0)
            {
                foreach (var item in Ad)
                {
                    if (item != null )
                    {
                   // message.Body += string.Format("<img src=\" {0}\"/>", item.Name);
                    item.ContentDisposition.Inline = true;
                    item.ContentDisposition.DispositionType =
                       System.Net.Mime.DispositionTypeNames.Inline;
                   string cid = item.ContentId;
                    message.Attachments.Add(item);
                   message.Body += String.Format("<img src=\" cid:{0}\"/ alt='三個月免費試用' name='三個月免費試用'><br>", cid);
                      //message.Body += String.Format("<img src=\" {0}\"/ alt='三個月免費試用' name='三個月免費試用'><br>", item.ContentStream);  
                
                    }
                }
            }
            message.Body += body;
            SmtpClient smtpclient = new SmtpClient(Uxnet.Web.Properties.Settings.Default.MailServer);
            smtpclient.Credentials = CredentialCache.DefaultNetworkCredentials;
            smtpclient.Send(message);

        }


        public static void SendInvoiceMail(this GoogleInvoiceUploadManager manager)
        {
            if (manager.IsValid)
            {
                manager.ItemList.Where(i => i.Invoice.InvoiceBuyer.IsB2C()).Select(i => i.Invoice.InvoiceID).SendGoogleInvoiceMail();

                SendMailMessage("Google電子發票已匯入,請執行發票列印作業!!", Uxnet.Web.Properties.Settings.Default.WebMaster, "Google電子發票開立郵件通知");

                //ThreadPool.QueueUserWorkItem(stateInfo =>
                //{
                //    GoogleInvoiceUploadManager mgr = (GoogleInvoiceUploadManager)stateInfo;
                //    var cipher = new CipherDecipherSrv(16);
                //    String url = String.Format("{0}{1}", Uxnet.Web.Properties.Settings.Default.HostUrl, VirtualPathUtility.ToAbsolute(Settings.Default.InvoiceMailUrl));
                //    foreach (var item in mgr.ItemList)
                //    {
                //        try
                //        {
                //            String.Format("{0}?{1}", url, cipher.cipher(item.Invoice.InvoiceID.ToString()))
                //                .MailWebPage(String.Format("{0} <{1}>",
                //                        item.Invoice.InvoiceBuyer.ContactName, item.Invoice.InvoiceBuyer.EMail),
                //                    "Google電子發票郵件通知");

                //        }
                //        catch (Exception ex)
                //        {
                //            Logger.Warn(String.Format("Google電子發票郵件通知客戶傳送失敗,發票號碼:{0}{1}", item.Invoice.TrackCode, item.Invoice.No));
                //            Logger.Error(ex);
                //        }
                //    }
                //}, manager);
            }
        }

        public static void SendInvoiceMail(this CsvInvoiceUploadManager manager)
        {
            if (manager.IsValid)
            {
                String subject = String.Format("{0}電子發票開立郵件通知", manager.Seller.CompanyName);
                manager.ItemList.Where(i => i.Entity != null && i.Entity.InvoiceBuyer.IsB2C()).Select(i => i.Entity.InvoiceID)
                    .SendB2CInvoiceMail(subject);
                SendMailMessage(String.Format("{0}電子發票已匯入,請執行發票列印作業!!", manager.Seller.CompanyName), Uxnet.Web.Properties.Settings.Default.WebMaster, subject);
            }
        }

        //Min-Yu 2011-11-21 Add
        public static void SendGoogleInvAllowanceMail(this IEnumerable<int> allowanceID)
        {
            if (allowanceID != null)
            {
                ThreadPool.QueueUserWorkItem(stateInfo =>
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        //IEnumerable<int> items = (IEnumerable<int>)stateInfo;
                        var cipher = new CipherDecipherSrv(16);
                        String url = String.Format("{0}{1}", Uxnet.Web.Properties.Settings.Default.HostUrl, VirtualPathUtility.ToAbsolute(Settings.Default.AllowanceMailUrl));

                        try
                        {
                            foreach (var id in allowanceID)
                            {
                                var item = mgr.EntityList.Where(i => i.InvoiceAllowances.FirstOrDefault().AllowanceID == id).First();
                                String.Format("{0}?{1}", url, cipher.cipher(id.ToString()))
                                    .MailWebPage(item.InvoiceBuyer.EMail, "Google折讓開立郵件通知");
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Warn(String.Format("Google折讓郵件通知客戶傳送失敗,ID:{0}", allowanceID));
                            Logger.Error(ex);
                        }

                    }
                }, allowanceID);
            }
        }

        public static void SendGoogleInvAllowanceCancelMail(this IEnumerable<int> allowanceID)
        {
            if (allowanceID != null)
            {
                ThreadPool.QueueUserWorkItem(stateInfo =>
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        //IEnumerable<int> items = (IEnumerable<int>)stateInfo;
                        var cipher = new CipherDecipherSrv(16);
                        String url = String.Format("{0}{1}", Uxnet.Web.Properties.Settings.Default.HostUrl, VirtualPathUtility.ToAbsolute(Settings.Default.AllowanceMailUrl));

                        try
                        {
                            foreach (var id in allowanceID)
                            {
                                var item = mgr.EntityList.Where(i => i.InvoiceAllowances.FirstOrDefault().AllowanceID == id).First();
                                String.Format("{0}?{1}", url, cipher.cipher(id.ToString()))
                                    .MailWebPage(item.InvoiceBuyer.EMail, "Google作廢折讓開立郵件通知");
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Warn(String.Format("Google作廢折讓郵件通知客戶傳送失敗,ID:{0}", allowanceID));
                            Logger.Error(ex);
                        }

                    }
                }, allowanceID);
            }
        }

        public static void SendGoogleInvoiceMail(this IEnumerable<int> invoiceID)
        {
            if (invoiceID != null && invoiceID.Count() > 0)
            {
                ThreadPool.QueueUserWorkItem(stateInfo =>
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        IEnumerable<int> items = (IEnumerable<int>)stateInfo;
                        var cipher = new CipherDecipherSrv(16);
                        String url = String.Format("{0}{1}", Uxnet.Web.Properties.Settings.Default.HostUrl, VirtualPathUtility.ToAbsolute(Settings.Default.InvoiceMailUrl));
                        foreach (var id in items)
                        {
                            try
                            {
                                var item = mgr.EntityList.Where(i => i.InvoiceID == id).First();
                                //String.Format("{0}?{1}", url, cipher.cipher(id.ToString()))
                                //    .MailWebPage(String.Format("{0} <{1}>",
                                //            item.InvoiceBuyer.ContactName, item.InvoiceBuyer.EMail),
                                //        "Google電子發票郵件通知");
                                String.Format("{0}?{1}", url, cipher.cipher(id.ToString()))
                                    .MailWebPage(item.InvoiceBuyer.EMail, "Google電子發票開立郵件通知");
                            }
                            catch (Exception ex)
                            {
                                Logger.Warn(String.Format("Google電子發票郵件通知客戶傳送失敗,ID:{0}", id));
                                Logger.Error(ex);
                            }
                        }
                    }
                    
                }, invoiceID);
            }
        }

        public static void SendB2CInvoiceMail(this IEnumerable<int> invoiceID,String subject)
        {
            if (invoiceID != null && invoiceID.Count() > 0)
            {
                ThreadPool.QueueUserWorkItem(stateInfo =>
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        IEnumerable<int> items = (IEnumerable<int>)stateInfo;
                        var cipher = new CipherDecipherSrv(16);
                        String url = String.Format("{0}{1}", Uxnet.Web.Properties.Settings.Default.HostUrl, VirtualPathUtility.ToAbsolute("~/Published/B2CInvoiceNotification.aspx"));
                        foreach (var id in items)
                        {
                            try
                            {
                                var item = mgr.EntityList.Where(i => i.InvoiceID == id).First();
                                String.Format("{0}?{1}", url, cipher.cipher(id.ToString()))
                                    .MailWebPage(item.InvoiceBuyer.EMail, subject);
                            }
                            catch (Exception ex)
                            {
                                Logger.Warn(String.Format("{0}客戶傳送失敗,ID:{1}", subject, id));
                                Logger.Error(ex);
                            }
                        }
                    }

                }, invoiceID);
            }
        }


        public static void SendGoogleInvoiceCancellationMail(this IEnumerable<int> invoiceID)
        {
            if (invoiceID != null && invoiceID.Count() > 0)
            {
                ThreadPool.QueueUserWorkItem(stateInfo =>
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        IEnumerable<int> items = (IEnumerable<int>)stateInfo;
                        var cipher = new CipherDecipherSrv(16);
                        String url = String.Format("{0}{1}", Uxnet.Web.Properties.Settings.Default.HostUrl, VirtualPathUtility.ToAbsolute(Settings.Default.InvoiceCancellationMailUrl));
                        foreach (var id in items)
                        {
                            try
                            {
                                var item = mgr.EntityList.Where(i => i.InvoiceID == id).First();
                                if (item.InvoiceCancellation != null)
                                {
                                    String.Format("{0}?{1}", url, cipher.cipher(id.ToString()))
                                        .MailWebPage(item.InvoiceBuyer.EMail, "Google作廢電子發票開立郵件通知");
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.Warn(String.Format("Google作廢電子發票郵件通知客戶傳送失敗,ID:{0}", id));
                                Logger.Error(ex);
                            }
                        }
                    }
                }, invoiceID);
            }
        }

        public static void SendB2CInvoiceCancellationMail(this IEnumerable<int> invoiceID)
        {
            if (invoiceID != null && invoiceID.Count() > 0)
            {
                ThreadPool.QueueUserWorkItem(stateInfo =>
                {
                    using (InvoiceManager mgr = new InvoiceManager())
                    {
                        IEnumerable<int> items = (IEnumerable<int>)stateInfo;
                        var cipher = new CipherDecipherSrv(16);
                        String url = String.Format("{0}{1}", Uxnet.Web.Properties.Settings.Default.HostUrl, VirtualPathUtility.ToAbsolute("~/Published/B2CInvoiceCancelMailPage.aspx"));
                        foreach (var id in items)
                        {
                            try
                            {
                                var item = mgr.EntityList.Where(i => i.InvoiceID == id).First();
                                if (item.InvoiceCancellation != null)
                                {
                                    String.Format("{0}?{1}", url, cipher.cipher(id.ToString()))
                                        .MailWebPage(item.InvoiceBuyer.EMail, String.Format("{0}作廢電子發票開立郵件通知", item.Organization.CompanyName));
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.Warn(String.Format("作廢電子發票郵件通知客戶傳送失敗,ID:{0}", id));
                                Logger.Error(ex);
                            }
                        }
                    }
                }, invoiceID);
            }
        }


        public static string StringMask(string OrgString, int StartReplaceNO, int ReplaceLength, char ReplaceSymbol)
        {
            string newString = "";
            if (!String.IsNullOrEmpty(OrgString))
            {

                if (OrgString.Length >= StartReplaceNO - 1 + ReplaceLength)
                {
                    string _temp = OrgString.Trim();
                    newString = _temp.Substring(0, StartReplaceNO - 1) + new string(ReplaceSymbol, ReplaceLength) + _temp.Substring(StartReplaceNO - 1 + ReplaceLength);
                }
                else
                {
                    var chars = OrgString.ToArray();
                    for (int i = 1; i < chars.Length - 1; i++)
                        chars[i] = ReplaceSymbol;
                    newString = new string(chars);
                }
            }
            return newString;
        }
    }
}
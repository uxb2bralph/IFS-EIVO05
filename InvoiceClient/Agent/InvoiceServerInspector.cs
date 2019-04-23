using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;

using InvoiceClient.Helper;
using InvoiceClient.Properties;
using Model.Schema.TXN;
using Utility;
namespace InvoiceClient.Agent
{

    public class InvoiceServerInspector
    {
        private bool _isRunning;
        private DateTime _dateCounter;
        private int _invoiceCounter;
        private int _invoiceCancellationCounter;
        private int _allowanceCounter;
        private int _allowanceCancellationCounter;

        public InvoiceServerInspector()
        {
            initializeCounter();
        }

        private void initializeCounter()
        {
            _dateCounter = DateTime.Now;
            _invoiceCounter = 1;
            _invoiceCancellationCounter = 1;
            _allowanceCounter = 1;
            _allowanceCancellationCounter = 1;
        }

        public IEnumerable<XmlNode> GetIncomingInvoices()
        {
            WS_Invoice.eInvoiceService invSvc = InvoiceWatcher.CreateInvoiceService();

            try
            {
                Root token = this.CreateMessageToken("下載傳送大平台電子發票");

                XmlNode doc = invSvc.GetIncomingInvoices(token.ConvertToXml().Sign());
                if (doc != null)
                {
                    String storedPath = Settings.Default.DownloadDataInAbsolutePath ? Settings.Default.DownloadInvoiceFolder : Path.Combine(Settings.Default.InvoiceTxnPath, Settings.Default.DownloadInvoiceFolder);
                    storedPath.CheckStoredPath();

                    if (_dateCounter < DateTime.Today)
                    {
                        initializeCounter();
                    }
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.Encoding = Settings.Default.OutputEncodingWithoutBOM ? new UTF8Encoding() : Encoding.GetEncoding(Settings.Default.OutputEncoding);

                    var nodes = doc.SelectNodes("Response/Invoice");
                    foreach (XmlNode invNode in nodes)
                    {
                        String path = Path.Combine(storedPath, String.Format("A0401-{0:yyyyMMddHHmmssf}-{1:00000}.xml", _dateCounter, _invoiceCounter++));
                        invNode.Save(path, settings);
                    }

                    return nodes.Cast<XmlNode>();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return null;
        }

        public IEnumerable<XmlNode> GetIncomingInvoiceCancellations()
        {
            WS_Invoice.eInvoiceService invSvc = InvoiceWatcher.CreateInvoiceService();

            try
            {
                Root token = this.CreateMessageToken("下載傳送大平台作廢電子發票");

                XmlNode doc = invSvc.GetIncomingInvoiceCancellations(token.ConvertToXml().Sign());
                if (doc != null)
                {
                    String storedPath = Settings.Default.DownloadDataInAbsolutePath ? Settings.Default.DownloadInvoiceCancellationFolder : Path.Combine(Settings.Default.InvoiceTxnPath, Settings.Default.DownloadInvoiceCancellationFolder);
                    storedPath.CheckStoredPath();

                    if (_dateCounter < DateTime.Today)
                    {
                        initializeCounter();
                    }

                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.Encoding = Settings.Default.OutputEncodingWithoutBOM ? new UTF8Encoding() : Encoding.GetEncoding(Settings.Default.OutputEncoding);

                    var nodes = doc.SelectNodes("Response/CancelInvoice");
                    foreach (XmlNode invNode in nodes)
                    {
                        String path = Path.Combine(storedPath, String.Format("A0501-{0:yyyyMMddHHmmssf}-{1:00000}.xml", _dateCounter, _invoiceCancellationCounter++));
                        invNode.Save(path, settings);
                    }

                    return nodes.Cast<XmlNode>();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return null;
        }

        public IEnumerable<XmlNode> GetIncomingAllowances()
        {
            WS_Invoice.eInvoiceService invSvc = InvoiceWatcher.CreateInvoiceService();

            try
            {
                Root token = this.CreateMessageToken("下載傳送大平台電子發票折讓證單");

                XmlNode doc = invSvc.GetIncomingAllowances(token.ConvertToXml().Sign());
                if (doc != null)
                {
                    String storedPath = Settings.Default.DownloadDataInAbsolutePath ? Settings.Default.DownloadAllowanceFolder : Path.Combine(Settings.Default.InvoiceTxnPath, Settings.Default.DownloadAllowanceFolder);
                    storedPath.CheckStoredPath();

                    if (_dateCounter < DateTime.Today)
                    {
                        initializeCounter();
                    }
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.Encoding = Settings.Default.OutputEncodingWithoutBOM ? new UTF8Encoding() : Encoding.GetEncoding(Settings.Default.OutputEncoding);

                    var nodes = doc.SelectNodes("Response/Allowance");
                    foreach (XmlNode invNode in nodes)
                    {
                        String path = Path.Combine(storedPath, String.Format("B0401-{0:yyyyMMddHHmmssf}-{1:00000}.xml", _dateCounter, _allowanceCounter++));
                        invNode.Save(path, settings);
                    }

                    return nodes.Cast<XmlNode>();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return null;
        }

        public IEnumerable<XmlNode> GetIncomingAllowanceCancellations()
        {
            WS_Invoice.eInvoiceService invSvc = InvoiceWatcher.CreateInvoiceService();

            try
            {
                Root token = this.CreateMessageToken("下載傳送大平台作廢電子發票折讓證明");

                XmlNode doc = invSvc.GetIncomingAllowanceCancellations(token.ConvertToXml().Sign());
                if (doc != null)
                {
                    String storedPath = Settings.Default.DownloadDataInAbsolutePath ? Settings.Default.DownloadAllowanceCancellationFolder : Path.Combine(Settings.Default.InvoiceTxnPath, Settings.Default.DownloadAllowanceCancellationFolder);
                    storedPath.CheckStoredPath();

                    if (_dateCounter < DateTime.Today)
                    {
                        initializeCounter();
                    }

                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.Encoding = Settings.Default.OutputEncodingWithoutBOM ? new UTF8Encoding() : Encoding.GetEncoding(Settings.Default.OutputEncoding);

                    var nodes = doc.SelectNodes("Response/CancelAllowance");
                    foreach (XmlNode invNode in nodes)
                    {
                        String path = Path.Combine(storedPath, String.Format("B0501-{0:yyyyMMddHHmmssf}-{1:00000}.xml", _dateCounter, _allowanceCancellationCounter++));
                        invNode.Save(path, settings);
                    }

                    return nodes.Cast<XmlNode>();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return null;
        }

        public String ReceiveA1401()
        {
            WS_Invoice.eInvoiceService invSvc = InvoiceWatcher.CreateInvoiceService();

            try
            {
                Root token = this.CreateMessageToken("接收進項電子發票");
                String storedPath = Settings.Default.DownloadDataInAbsolutePath ? Settings.Default.DownloadInvoiceFolder : Path.Combine(Settings.Default.InvoiceTxnPath, Settings.Default.DownloadInvoiceFolder);
                storedPath = ValueValidity.GetDateStylePath(storedPath);
                bool hasNew = false;

                XmlDocument signedReq = token.ConvertToXml().Sign();
                XmlNode doc;
                while ((doc = invSvc.B2BReceiveA1401(signedReq)) != null)
                {
                    hasNew = true;
                    Model.Schema.TurnKey.A1401.Invoice invoice = doc.ConvertTo<Model.Schema.TurnKey.A1401.Invoice>();

                    doc.Save(Path.Combine(storedPath, String.Format("{0}(A1401).xml", invoice.Main.InvoiceNumber)));

                    XmlDocument resDoc = new XmlDocument();
                    resDoc.AppendChild(resDoc.ImportNode(doc, true));

                    invSvc.AcknowledgeReceiving(resDoc.Sign());
                }
                return hasNew ? storedPath : null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return null;
        }

        public String ReceiveB1401()
        {
            WS_Invoice.eInvoiceService invSvc = InvoiceWatcher.CreateInvoiceService();

            try
            {
                Root token = this.CreateMessageToken("接收銷項發票折讓證明");
                String storedPath = Settings.Default.DownloadDataInAbsolutePath ? Settings.Default.DownloadAllowanceFolder : Path.Combine(Settings.Default.InvoiceTxnPath, Settings.Default.DownloadAllowanceFolder);
                storedPath = ValueValidity.GetDateStylePath(storedPath);
                bool hasNew = false;

                XmlDocument signedReq = token.ConvertToXml().Sign();
                XmlNode doc;
                while ((doc = invSvc.B2BReceiveB1401(signedReq)) != null)
                {
                    hasNew = true;
                    Model.Schema.TurnKey.B1401.Allowance allowance = doc.ConvertTo<Model.Schema.TurnKey.B1401.Allowance>();

                    doc.Save(Path.Combine(storedPath, String.Format("{0}(B1401).xml", allowance.Main.AllowanceNumber)));

                    XmlDocument resDoc = new XmlDocument();
                    resDoc.AppendChild(resDoc.ImportNode(doc, true));

                    invSvc.AcknowledgeReceiving(resDoc.Sign());
                }
                return hasNew ? storedPath : null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return null;
        }

        public String ReceiveA0501()
        {
            WS_Invoice.eInvoiceService invSvc = InvoiceWatcher.CreateInvoiceService();

            try
            {
                Root token = this.CreateMessageToken("接收進項作廢電子發票");
                String storedPath = Settings.Default.DownloadDataInAbsolutePath ? Settings.Default.DownloadInvoiceCancellationFolder : Path.Combine(Settings.Default.InvoiceTxnPath, Settings.Default.DownloadInvoiceCancellationFolder);
                storedPath = ValueValidity.GetDateStylePath(storedPath);
                bool hasNew = false;

                XmlDocument signedReq = token.ConvertToXml().Sign();
                XmlNode doc;
                while ((doc = invSvc.B2BReceiveA1401(signedReq)) != null)
                {
                    hasNew = true;
                    Model.Schema.TurnKey.A0501.CancelInvoice cancelInvoice = doc.ConvertTo<Model.Schema.TurnKey.A0501.CancelInvoice>();

                    doc.Save(Path.Combine(storedPath, String.Format("{0}(A0501).xml", cancelInvoice.CancelInvoiceNumber)));

                    XmlDocument resDoc = new XmlDocument();
                    resDoc.AppendChild(resDoc.ImportNode(doc, true));

                    invSvc.AcknowledgeReceiving(resDoc.Sign());
                }
                return hasNew ? storedPath : null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return null;
        }

        public String ReceiveB0501()
        {
            WS_Invoice.eInvoiceService invSvc = InvoiceWatcher.CreateInvoiceService();

            try
            {
                Root token = this.CreateMessageToken("接收銷項作廢發票折讓證明");
                String storedPath = Settings.Default.DownloadDataInAbsolutePath ? Settings.Default.DownloadAllowanceCancellationFolder : Path.Combine(Settings.Default.InvoiceTxnPath, Settings.Default.DownloadAllowanceCancellationFolder);
                storedPath = ValueValidity.GetDateStylePath(storedPath);
                bool hasNew = false;

                XmlDocument signedReq = token.ConvertToXml().Sign();
                XmlNode doc;
                while ((doc = invSvc.B2BReceiveB0501(signedReq)) != null)
                {
                    hasNew = true;
                    Model.Schema.TurnKey.B0501.CancelAllowance cancelAllowance = doc.ConvertTo<Model.Schema.TurnKey.B0501.CancelAllowance>();

                    doc.Save(Path.Combine(storedPath, String.Format("{0}(B0501).xml", cancelAllowance.CancelAllowanceNumber)));

                    XmlDocument resDoc = new XmlDocument();
                    resDoc.AppendChild(resDoc.ImportNode(doc, true));

                    invSvc.AcknowledgeReceiving(resDoc.Sign());
                }
                return hasNew ? storedPath : null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return null;
        }        

        public String GetSaleInvoices()
        {
            WS_Invoice.eInvoiceService invSvc = InvoiceWatcher.CreateInvoiceService();

            try
            {
                Root token = this.CreateMessageToken("下載電子發票PDF");
                String storedPath = Settings.Default.DownloadDataInAbsolutePath ? Settings.Default.DownloadSaleInvoiceFolder : Path.Combine(Settings.Default.InvoiceTxnPath, Settings.Default.DownloadSaleInvoiceFolder);
               // storedPath = ValueValidity.GetDateStylePath(storedPath);
                bool hasNew = false;

                XmlDocument signedReq = token.ConvertToXml().Sign();
                string[] items = invSvc.ReceiveContentAsPDF(signedReq);

                ///資料格式:
                ///第一列:URL
                ///第二列:開立人統編,yyyy,mm,dd,發票號碼,InvoiceID,IsolationFolder
                ///...
                ///
                if (items != null && items.Length>1)
                {
                    String getPDFUrl = items[0];
                    hasNew = true;
                    using (WebClient wc = new WebClient())
                    {
                        for (int i = 1; i < items.Length; i++)
                        {
                            var invNo = items[i].Split(',');
                            String pdfPath = Path.Combine(storedPath, invNo[0], invNo[1], invNo[2]);
                            if(!String.IsNullOrEmpty(invNo[6]))
                            {
                                pdfPath = Path.Combine(pdfPath, invNo[6]);
                            }
                            pdfPath.CheckStoredPath();
                            String pdfFile = Path.Combine(pdfPath, String.Format("{0}{1}-{2}.pdf", invNo[1], invNo[2], invNo[4]));//ex:201403-FH78325348.pdf

                            token = this.CreateMessageToken("已下載電子發票PDF:" + invNo[4]);// 
                            signedReq = token.ConvertToXml().Sign();
                            var buf = signedReq.UploadData(getPDFUrl + "?" + invNo[5]);
                            if (buf != null && buf.Length > 0)
                            {
                                buf.SaveAs(pdfFile);
                                invSvc.DeleteTempForReceivePDF(signedReq, int.Parse(invNo[5]));
                            }
                            else
                            {
                                Logger.Warn("電子發票PDF:" + invNo[4] + "讀取失敗!!");
                            }

                            //XmlDocument resDoc = new XmlDocument();
                            // XmlNode doc = invSvc.B2BReceiveA1401(signedReq);
                            // resDoc.AppendChild(resDoc.ImportNode(token.ConvertToXml(), true));

                            //invSvc.AcknowledgeReceiving(token.ConvertToXml().Sign()); 
                            
                            //signedReq = token.ConvertToXml().Sign();
                        }
                    }
                }
                return hasNew ? storedPath : null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return null;
        }

        public void AcknowledgeServer()
        {
            WS_Invoice.eInvoiceService invSvc = InvoiceWatcher.CreateInvoiceService();

            try
            {
                Root token = this.CreateMessageToken("用戶端系統正在執行中");
                invSvc.AcknowledgeLivingReport(token.ConvertToXml().Sign());
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public Model.DataEntity.Organization GetRegisterdMember()
        {
            WS_Invoice.eInvoiceService invSvc = InvoiceWatcher.CreateInvoiceService();

            try
            {
                Root token = this.CreateMessageToken("讀取用戶端資料");
                XmlNode doc = invSvc.GetRegisteredMember(token.ConvertToXml().Sign());
                if (doc != null)
                {
                    return doc.DeserializeDataContract<Model.DataEntity.Organization>();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return null;
        }


        public Model.Schema.EIVO.BonusInvoiceRoot GetIncomingWinningInvoices()
        {
            WS_Invoice.eInvoiceService invSvc = InvoiceWatcher.CreateInvoiceService();

            try
            {
                Root token = this.CreateMessageToken("下載發票中獎清冊");

                XmlNode doc = invSvc.GetIncomingWinningInvoices(token.ConvertToXml().Sign());
                if (doc != null)
                {
                    String storedPath = Path.Combine(Settings.Default.InvoiceTxnPath, Settings.Default.DownloadWinningFolder);
                    storedPath.CheckStoredPath();

                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.Encoding = Settings.Default.OutputEncodingWithoutBOM ? new UTF8Encoding() : Encoding.GetEncoding(Settings.Default.OutputEncoding);

                    Model.Schema.EIVO.BonusInvoiceRoot result = doc.ConvertTo<Model.Schema.EIVO.BonusInvoiceRoot>();
                    String path = Path.Combine(storedPath, String.Format("{0:yyyyMMddHHmmssf}_BonusInvoice.xml", DateTime.Now));
                    doc.Attributes.RemoveAll();
                    doc.Save(path, settings);
                    return result;
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
            if (Settings.Default.IsAutoInvService && !_isRunning)
            {
                ThreadPool.QueueUserWorkItem(p =>
                {
                    while (Settings.Default.IsAutoInvService)
                    {
                        _isRunning = true;
                        GetIncomingInvoices();
                        GetIncomingInvoiceCancellations();
                        GetIncomingAllowances();
                        GetIncomingAllowanceCancellations();
                        GetIncomingWinningInvoices();
                        GetSaleInvoices();
                        if (!Settings.Default.DisableB2BAutoReceiving)
                        {
                            ReceiveA1401();
                            ReceiveB1401();
                            ReceiveA0501();
                            ReceiveB0501();
                        }

                        Thread.Sleep(Settings.Default.AutoInvServiceInterval > 0 ? Settings.Default.AutoInvServiceInterval * 60 * 1000 : 1800000);
                    }
                    _isRunning = false;
                });
            }
        }
    }
}

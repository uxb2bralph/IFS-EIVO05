using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Security.Cryptography.X509Certificates;

using Uxnet.Com.Security.UseCrypto;
using System.Text;

namespace eIVOCenter.Published
{
    public partial class SignXmlPage : System.Web.UI.Page
    {
        private XmlDocument _docResult;
        protected IEnumerable<X509Certificate2> _certs;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                initializeData();
            }
        }

        private void initializeData()
        {
            CertStoreName.DataSource = Enum.GetNames(typeof(StoreName));
            CertStoreName.DataBind();
            CertStoreLocation.DataSource = Enum.GetNames(typeof(StoreLocation));
            CertStoreLocation.DataBind();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.PreRender += new EventHandler(SignXmlPage_PreRender);
        }

        void SignXmlPage_PreRender(object sender, EventArgs e)
        {
            if (_docResult != null)
            {
                Response.Clear();
                Response.ContentType = "message/rfc822";
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "SignedContext.xml"));
                _docResult.Save(Response.OutputStream);
                Response.Flush();
                Response.End();

            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (XmlFile.HasFile)
            {
                XmlDocument docMsg = new XmlDocument();
//                docMsg.PreserveWhitespace = true;
                docMsg.Load(XmlFile.PostedFile.InputStream);

                X509Store certStore = new X509Store(
                    (StoreName)Enum.Parse(typeof(StoreName), CertStoreName.SelectedValue),
                    (StoreLocation)Enum.Parse(typeof(StoreLocation), CertStoreLocation.SelectedValue));
                certStore.Open(OpenFlags.ReadOnly);
                X509Certificate2 cert = null;
                foreach (X509Certificate2 signerCert in certStore.Certificates)
                {
                    if (signerCert.Subject.IndexOf(Subject.Text) >= 0)
                    {
                        CryptoUtility.SignXml(docMsg, CspName.Text,
                            Request[this.StorePass.UniqueID], signerCert);
                        _docResult = docMsg;
                        cert = signerCert;
                        break;
                    }
                }

                if (cert == null)
                {
                    lblMsg.Text = "找不到簽署者的憑證!!";
                }

                certStore.Close();

            }
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            lbViewCert.Visible = false;

            if (XmlSigFile.HasFile)
            {

                XmlDocument docMsg = new XmlDocument();
//                docMsg.PreserveWhitespace = true;
                docMsg.Load(XmlSigFile.PostedFile.InputStream);

                CryptoUtility ca = new CryptoUtility();
                if (ca.VerifyXmlSignature(docMsg))
                {
                    lblMsg.Text = ca.CA_Log.Subject;
                    lblMsg.ForeColor = System.Drawing.Color.Black;
                    lbViewCert.Visible = true;

                    X509Certificate SignerCertificate = ca.SignerCertificate;
                    this.ViewState["cert"] = new X509Certificate(SignerCertificate);
                }
                else
                {
                    lblMsg.Text = "驗簽失敗!! 請查閱log...";
                }
            }
        }

        protected void lbViewCert_Click(object sender, EventArgs e)
        {
            if (this.ViewState["cert"] != null)
            {
                X509Certificate cert = (X509Certificate)this.ViewState["cert"];
                byte[] certRaw = cert.GetRawCertData();

                Response.Clear();
                Response.ContentType = "message/rfc822";
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", "Signer.cer"));
                Response.AddHeader("Content-Length", certRaw.Length.ToString());

                Response.OutputStream.Write(certRaw, 0, certRaw.Length);

                Response.Flush();
                Response.End();

            }
        }

        protected void btnListCert_Click(object sender, EventArgs e)
        {
            X509Store certStore = new X509Store(
                (StoreName)Enum.Parse(typeof(StoreName), CertStoreName.SelectedValue),
                (StoreLocation)Enum.Parse(typeof(StoreLocation), CertStoreLocation.SelectedValue));
            certStore.Open(OpenFlags.ReadOnly);

            _certs = certStore.Certificates.Cast<X509Certificate2>();

            certStore.Close();
        }
    }
}

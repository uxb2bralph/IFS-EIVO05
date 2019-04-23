namespace Uxnet.Web.Module.Common
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Collections;

    using Utility;

    /// <summary>
    ///		DownloadingButton ���K�n�y�z�C
    /// </summary>
    public partial class MailButton : System.Web.UI.UserControl
    {

        private static Hashtable _Hash = new Hashtable();
        private IList _controlList = new ArrayList();

        public event EventHandler BeforeClick;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // �b�o�̩�m�ϥΪ̵{���X�H��l�ƺ���
            this._btnDownload.OnClientClick = "return sendMail();";
        }

        public static IEnumerable GetMailControlsBySessionID(string sessionID)
        {
            object obj = _Hash[sessionID];
            return (obj != null) ? (IEnumerable)obj : null;
        }

        public static void RemoveMailControlsBySessionID(string sessionID)
        {
            _Hash.Remove(sessionID);
        }

        public IList MailControls
        {
            get
            {
                return _controlList;
            }
        }

        public string MailSubject
        {
            get
            {
                return _btnDownload.CommandArgument;
            }
            set
            {
                _btnDownload.CommandArgument = value;
            }
        }

        public string ContentGeneratorUrl
        {
            get
            {
                return (string)this.ViewState["Url"];
            }
            set
            {
                this.ViewState["Url"] = value;
            }
        }

        public string SmtpServer
        {
            get
            {
                return (string)this.ViewState["smtpServer"];
            }
            set
            {
                this.ViewState["smtpServer"] = value;
            }
        }

        #region Web Form �]�p�u�㲣�ͪ��{���X
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: ���� ASP.NET Web Form �]�p�u��һݪ��I�s�C
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		�����]�p�u��䴩�ҥ�������k - �ФŨϥε{���X�s�边�ק�
        ///		�o�Ӥ�k�����e�C
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        protected void _btnDownload_Click(object sender, System.EventArgs e)
        {
            if (BeforeClick != null)
            {
                BeforeClick(this, new EventArgs());
            }

            if (_controlList.Count > 0 && !String.IsNullOrEmpty(Request.Form["mailButtonAddr"]))
            {

                _Hash.Remove(Session.SessionID);
                _Hash.Add(Session.SessionID, _controlList);
                sendMail();
                Response.Redirect(Request.ApplicationPath + "/goBack.htm");
                //				Server.Transfer(Request.Path);
            }
        }

        private void sendMail()
        {
            if (!String.IsNullOrEmpty(ContentGeneratorUrl) && !String.IsNullOrEmpty(SmtpServer))
            {
                using (PageMailGenerator pageMail = new Utility.PageMailGenerator())
                {
                    pageMail.MailMessage = String.Format("CDS���ĪA�Ȥ��� {0}({0:yyyy/M/d})",
                        _btnDownload.CommandArgument, DateTime.Now);
                    pageMail.SMTP_Server = SmtpServer;
                    pageMail.From = "cds_service@uxb2b.com";
                    pageMail.Sender = "cds_service@uxb2b.com";

                    ArrayList al = new ArrayList();
                    foreach (string addr in Request.Form["mailButtonAddr"].Split(',', ';'))
                    {
                        al.Add(addr);
                    }

                    pageMail.Recipient = al;
                    pageMail.PageUrl = String.Format("{0}?sessionID={1}", ContentGeneratorUrl, Session.SessionID);
                    pageMail.Publish();

                }
            }

        }

    }
}

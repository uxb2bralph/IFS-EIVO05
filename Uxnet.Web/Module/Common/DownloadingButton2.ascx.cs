namespace Uxnet.Web.Module.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.ComponentModel;
    using System.IO;
    using System.Text;

    /// <summary>
    ///		DownloadingButton ���K�n�y�z�C
    /// </summary>
    public partial class DownloadingButton2 : System.Web.UI.UserControl
    {

        protected List<Control> _controlList;

        public event EventHandler BeforeClick;


        protected void Page_Load(object sender, System.EventArgs e)
        {
            // �b�o�̩�m�ϥΪ̵{���X�H��l�ƺ���
            if (ScriptManager.GetCurrent(this.Page) != null)
            {
                ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(this);
            }
        }

        public List<Control> DownloadControls
        {
            get
            {
                if (_controlList == null)
                    _controlList = new List<Control>();
                return _controlList;
            }
        }

        [Bindable(true)]
        public string ContentGeneratorUrl
        {
            get;
            set;
        }

        [Bindable(true)]
        public String OutputFileName
        {
            get;
            set;
        }

        [Bindable(true)]
        public String OnClientClick
        {
            get
            {
                return _btnDownload.OnClientClick;
            }
            set
            { 
                _btnDownload.OnClientClick=value;
            }
        }


        public Button TheButton
        {
            get
            {
                return _btnDownload;
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

        protected virtual void _btnDownload_Click(object sender, System.EventArgs e)
        {
            OnBeforeClick();

            if (_controlList.Count > 0)
            {
                if (!String.IsNullOrEmpty(ContentGeneratorUrl))
                {
                    Response.Clear();
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "message/rfc822";
                    Response.AddHeader("Content-Disposition", !String.IsNullOrEmpty(OutputFileName) ? String.Format("attachment;filename={0}", Server.UrlEncode(OutputFileName))
                        : String.Format("attachment;filename={0:yyyy-MM-dd}.htm", DateTime.Today));

                    Page.Items["contentList"] = _controlList;

                    using (StreamWriter sw = new StreamWriter(Response.OutputStream, Encoding.UTF8))
                    {
                        Server.Execute(ContentGeneratorUrl, sw, true);
                        sw.Flush();
                    }

                    Response.End();
                }
            }
        }

        protected void OnBeforeClick()
        {
            if (BeforeClick != null)
            {
                BeforeClick(this, new EventArgs());
            }
        }
    }
}

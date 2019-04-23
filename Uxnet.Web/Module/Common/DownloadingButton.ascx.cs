namespace Uxnet.Web.Module.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;

	/// <summary>
	///		DownloadingButton ���K�n�y�z�C
	/// </summary>
	public partial class DownloadingButton : System.Web.UI.UserControl
	{

		protected static Hashtable _Hash = new Hashtable();
		protected	IList _controlList = new ArrayList();

        public event EventHandler BeforeClick;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �b�o�̩�m�ϥΪ̵{���X�H��l�ƺ���
		}

		public static IEnumerable GetDownloadControlsBySessionID(string sessionID)
		{
			object obj = _Hash[sessionID];
            return (obj!=null)?(IEnumerable)obj:null;
		}

		public static void RemoveDownloadControlsBySessionID(string sessionID)
		{
			_Hash.Remove(sessionID);
		}

		public IList DownloadControls
		{
			get
			{
				return _controlList;
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

                _Hash.Remove(Session.SessionID);
                _Hash.Add(Session.SessionID, _controlList);
                if (!String.IsNullOrEmpty(ContentGeneratorUrl))
                {
                    Server.Transfer(ContentGeneratorUrl);
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

namespace Uxnet.Web.Module.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
using System.ComponentModel;

	/// <summary>
	///		ReturnButton ���K�n�y�z�C
	/// </summary>
	public partial class ReturnButton : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �b�o�̩�m�ϥΪ̵{���X�H��l�ƺ���
		}

        [Bindable(true)]
        public bool UseReferrer
        {
            get;
            set;
        }

        [Bindable(true)]
        public String GoBackUrl
        {
            get
            {
                return btnGoBack.CommandArgument;
            }
            set
            {
                btnGoBack.CommandArgument = value;
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
			this.PreRender += new EventHandler(ReturnButton_PreRender);

		}
		#endregion

		private void ReturnButton_PreRender(object sender, EventArgs e)
		{
            if (UseReferrer)
			{
                if (!String.IsNullOrEmpty(GoBackUrl))
                {
                    btnGoBack.OnClientClick = String.Format("window.location.href = '{0}';", GoBackUrl);
                }
                else if (null != Request.UrlReferrer)
                {
                    btnGoBack.OnClientClick = String.Format("window.location.href = '{0}';", Request.UrlReferrer.AbsolutePath);
                }
                else
                {
                    btnGoBack.OnClientClick = "window.history.go(-1);";
                }
			}
			else
			{
                btnGoBack.OnClientClick = "window.history.go(-1);";
            }
		}
	}
}

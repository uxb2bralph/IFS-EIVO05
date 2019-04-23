namespace Uxnet.Web.Module.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using Utility;

	/// <summary>
	///		ExceptionHandler ���K�n�y�z�C
	/// </summary>
	public partial class ExceptionHandler : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �b�o�̩�m�ϥΪ̵{���X�H��l�ƺ���
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
			this.Page.Error += new EventHandler(ExceptionHandler_Error);
		}
		#endregion

		private void ExceptionHandler_Error(object sender, EventArgs e)
		{
			Exception ex = Server.GetLastError();
			if(!(ex is System.Threading.ThreadAbortException))
				Logger.Error(ex);
		}
	}
}

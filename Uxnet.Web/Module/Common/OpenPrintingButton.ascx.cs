namespace Uxnet.Web.Module.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
    using System.Text;
    using System.Collections.Generic;

	/// <summary>
	///		PrintingButton ���K�n�y�z�C
	/// </summary>
    public partial class OpenPrintingButton : System.Web.UI.UserControl
	{
		private bool _autoPrint;

        public event EventHandler BeforeClick;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �b�o�̩�m�ϥΪ̵{���X�H��l�ƺ���
            _btnPrintIt.OnClientClick = "printDocument(this);";
		}


		public bool AutoPrint
		{
			get
			{
				return _autoPrint;
			}
			set
			{
				_autoPrint = value;
			}
		}

        public Button btnPrint
        {
            get
            {
                return this._btnPrintIt;
            }
        }

        public string UrlToPrint
        {
            get
            {
                return (string)this.ViewState["urlToPrn"];
            }
            set
            {
                this.ViewState["urlToPrn"] = value;
            }
        }

        private List<Control> _printControls  = new List<Control>();
        public List<Control> PrintControls
        {
            get
            {
                return _printControls;
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
			this.PreRender += new EventHandler(PrintingButton_PreRender);

		}
		#endregion

		protected void _btnPrintIt_Click(object sender, System.EventArgs e)
		{
            if (BeforeClick != null)
            {
                BeforeClick(this, new EventArgs());
            }

            if (this.ViewState["urlToPrn"] != null)
            {
                Page.Items["PrintDoc"] = _printControls;
                Server.Transfer((string)this.ViewState["urlToPrn"]);
                //				Response.Redirect((string)this.ViewState["urlToPrn"]);
            }
        }

		private void PrintingButton_PreRender(object sender, EventArgs e)
		{
            if (!Page.ClientScript.IsClientScriptBlockRegistered(typeof(OpenPrintingButton), "printScript"))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"
	                var mainForm;
                    var prnWin;

	                function printDocument(elmt)
	                {
                        prnWin = window.open('','prnWin','toolbar=no,location=no,status=no,menubar=no,scrollbars=auto,resizable=no,alwaysRaised,dependent,titlebar=no,width=64,height=48');
                        
		                mainForm = elmt.form;
		                mainForm.target = 'prnWin';
		                prnWin.focus();
                        prnWin.onload = startPrint;
		                return true;
	                }

                    function startPrint()
                    {
                        prnWin.onafterprint = afterPrint;
                        prnWin.print();
                        afterPrint();
                    }
                	
	                function afterPrint()
	                {
                        if(prnWin!=null)
                        {
                            prnWin.close();
                        }
		                if(mainForm!=null)
		                {
			                mainForm.target = """";
		                }
	                }
				");

                Page.ClientScript.RegisterClientScriptBlock(typeof(OpenPrintingButton), "printScript", sb.ToString(), true);
            }


			if(!this.IsPostBack)
			{
				if(_autoPrint)
				{
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("autoPrint"))
                    {
                        this.Page.ClientScript.RegisterStartupScript(typeof(String), "autoPrint",
                            String.Format("document.all('{0}').click();", this._btnPrintIt.ClientID), true);
                    }
				}

			}


		}
	}
}

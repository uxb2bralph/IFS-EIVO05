﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Threading;

using Utility;
using eIVOCenter.Helper;

namespace eIVOCenter.SAM
{
    public partial class PrintAllowanceAsPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            createPDF();
        }

        private void createPDF()
        {
            String pdfFile = Server.CreateContentAsPDF("~/SAM/PrintAllowancePage.aspx", Session.Timeout);
            if (pdfFile != null)
            {
                Response.WriteFileAsDownload(pdfFile, String.Format("{0:yyyy-MM-dd}.pdf", DateTime.Today), true);
            }
            else
            {
                Response.Output.WriteLine("系統忙錄中，請稍後再試...");
                Response.End();
            }
        }
    }
}
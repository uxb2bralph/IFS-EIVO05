using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace eIVOCenter
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            eIVOGo.Module.SAM.SystemMonitorControl.StartUp();
            eIVOGo.Published.eInvoiceService.StartUp();
            eIVOCenter.Published.eInvoiceService.StartUp();

            Model.InvoiceManagement.EIVOPlatformFactory.Sign = eIVOCenter.Helper.AppSigner.Sign;
            Model.InvoiceManagement.EIVOPlatformFactory.SignCms = eIVOCenter.Helper.AppSigner.SignCms;
        }

        //protected void Application_Start(object sender, EventArgs e)
        //{

        //}

        //protected void Session_Start(object sender, EventArgs e)
        //{

        //}

        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{

        //}

        //protected void Application_AuthenticateRequest(object sender, EventArgs e)
        //{

        //}

        //protected void Application_Error(object sender, EventArgs e)
        //{

        //}

        //protected void Session_End(object sender, EventArgs e)
        //{

        //}

        //protected void Application_End(object sender, EventArgs e)
        //{

        //}
    }
}
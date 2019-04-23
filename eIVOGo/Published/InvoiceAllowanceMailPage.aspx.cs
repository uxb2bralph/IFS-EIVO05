using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Model.DataEntity;
using eIVOGo.Properties;
using Utility;

namespace eIVOGo.Published
{
    public partial class InvoiceAllowanceMailPage : System.Web.UI.Page
    {

        public int? AllowanceID { get; set; }

        protected InvoiceAllowance _item;
        protected Organization _buyer;
        protected void Page_Load(object sender, EventArgs e)
        {

            int allownceID;
            if (int.TryParse(Request["id"], out allownceID))
            {
                AllowanceID = allownceID;
            }
            else
            {
                String qs = Request.Params["QUERY_STRING"];
                if (!String.IsNullOrEmpty(qs))
                {
                    if (int.TryParse((new CipherDecipherSrv()).decipher(qs), out allownceID))
                    {
                        AllowanceID = allownceID;
                    }
                }
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.PreRender += new EventHandler(InvoiceAllowenceMailPage_PreRender);
        }

        void InvoiceAllowenceMailPage_PreRender(object sender, EventArgs e)
        {
            if (AllowanceID.HasValue)
            {
                var mgr = dsInv.CreateDataManager();
                _item = mgr.GetTable<InvoiceAllowance>().Where(i => i.AllowanceID==AllowanceID).FirstOrDefault();
                if (_item != null)
                {
                    mailView.Item = _item;
                    invRequest.HRef = String.Format("{0}{1}?{2}", Uxnet.Web.Properties.Settings.Default.HostUrl, VirtualPathUtility.ToAbsolute("~/Published/RequestInvoicePaper.aspx"), (new CipherDecipherSrv(16)).cipher(_item.InvoiceID.ToString()));
                    this.DataBind();
                }
            }
        }
    }
}
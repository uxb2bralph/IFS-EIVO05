﻿using System;
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
    public partial class WinningInvoiceMailPage : System.Web.UI.Page
    {
        public int? InvoiceID { get; set; }

        protected InvoiceItem _item;
        protected Organization _buyer;
        protected String _queryString;

        protected void Page_Load(object sender, EventArgs e)
        {
            int invoiceID;
            _queryString = Request.Params["QUERY_STRING"];
            if (int.TryParse((new CipherDecipherSrv()).decipher(_queryString), out invoiceID))
            {
                InvoiceID = invoiceID;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.PreRender += new EventHandler(InvoiceMailPage_PreRender);
        }

        void InvoiceMailPage_PreRender(object sender, EventArgs e)
        {
            if (InvoiceID.HasValue)
            {
                var mgr = dsInv.CreateDataManager();
                _item = mgr.EntityList.Where(i => i.InvoiceID == InvoiceID).FirstOrDefault();
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
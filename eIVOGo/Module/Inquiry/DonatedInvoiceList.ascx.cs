using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eIVOGo.Module.Inquiry
{
    public partial class DonatedInvoiceList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.PreRender += new EventHandler(DonatedInvoiceList_PreRender);
        }

        void DonatedInvoiceList_PreRender(object sender, EventArgs e)
        {
            rpList.DataSource = DataItems;
            rpList.DataBind();
        }

        public IEnumerable<_QueryItem> DataItems
        { get; set; }

    }
}
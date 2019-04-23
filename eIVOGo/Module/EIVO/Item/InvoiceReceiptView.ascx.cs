using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Locale;
using Model.DataEntity;
using Utility;
using System.Xml.Linq;
using System.Xml;

namespace eIVOGo.Module.EIVO.Item
{
    public partial class InvoiceReceiptView : System.Web.UI.UserControl
    {
        protected InvoiceItem _item;
        protected InvoiceBuyer _buyer;
        protected Organization _buyerOrg;
        protected char[] _totalAmtChar;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        [Bindable(true)]
        public InvoiceItem Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
                if (_item != null)
                {
                    _totalAmtChar = ((int)_item.InvoiceAmountType.TotalAmount.Value).GetChineseNumberSeries(8);
                    _buyer = _item.InvoiceBuyer;
                    if (_buyer != null && _buyer.BuyerID.HasValue)
                        _buyerOrg = _buyer.Organization;
                }
            }
        }

      

        protected string getTagValue(XElement ExtraRemark, string TagName)
        {
            var element = ExtraRemark.Element(TagName);
            if (element != null)
            {
                element = element.Element("Value");
            }

            return element != null ? element.Value : null;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.PreRender += new EventHandler(InvoiceReceiptView_PreRender);
            this.rpList.ItemDataBound += new RepeaterItemEventHandler(rpList_ItemDataBound);
        }

        void rpList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            { 
                case ListItemType.AlternatingItem:
                case ListItemType.Item:
                    InvoiceProductPrintView view = e.Item.FindControl("productView") as InvoiceProductPrintView;
                    if (view != null)
                        view.Item = ((InvoiceDetail)e.Item.DataItem).InvoiceProduct;
                    break;
            }
        }

        void InvoiceReceiptView_PreRender(object sender, EventArgs e)
        {
            if (_item != null)
            {
                rpList.DataSource = _item.InvoiceDetails;
                rpList.DataBind();
                this.DataBind();
            }
        }

        //public override void DataBind()
        //{
        //    if (_item != null)
        //    {
        //        rpList.DataSource = _item.InvoiceDetails;
        //        base.DataBind();
        //    }
        //}
        public string PrintFormat(int InvoiceType)
        {
            string Format = null;

            if (InvoiceType != null || InvoiceType!=0)
            {
                if (InvoiceType == (int)Naming.InvoiceTypeDefinition.三聯式)
                    Format = Convert.ToString((int)Naming.InvoiceTypeFormat.三聯式);
                if (InvoiceType == (int)Naming.InvoiceTypeDefinition.二聯式)
                    Format = Convert.ToString((int)Naming.InvoiceTypeFormat.二聯式);
                if (InvoiceType == (int)Naming.InvoiceTypeDefinition.二聯式收銀機)
                    Format =Convert.ToString( (int)Naming.InvoiceTypeFormat.二聯式收銀機);
                if (InvoiceType == (int)Naming.InvoiceTypeDefinition.特種稅額)
                    Format = Convert.ToString((int)Naming.InvoiceTypeFormat.特種稅額);
                if (InvoiceType == (int)Naming.InvoiceTypeDefinition.電子計算機)
                    Format =Convert.ToString( (int)Naming.InvoiceTypeFormat.電子計算機);
                if (InvoiceType == (int)Naming.InvoiceTypeDefinition.三聯式收銀機)
                    Format =Convert.ToString( (int)Naming.InvoiceTypeFormat.三聯式收銀機);
                if (InvoiceType == (int)Naming.InvoiceTypeDefinition.一般稅額)
                    Format = Convert.ToString((int)Naming.InvoiceTypeFormat.一般稅額);
            }
            return Format;
        }
    }
}
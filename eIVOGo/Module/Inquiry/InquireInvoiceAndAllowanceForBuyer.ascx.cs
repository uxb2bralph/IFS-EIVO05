using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Locale;
using Model.Security.MembershipManagement;
using Model.InvoiceManagement;
using Business.Helper;
using Model.DataEntity;
using Uxnet.Web.Module.Common;
using System.Linq.Expressions;
using Utility;

namespace eIVOGo.Module.Inquiry
{
    public partial class InquireInvoiceAndAllowanceForBuyer : InquireInvoiceAndAllowanceBasic
    {

        #region "Page Control Event"
        protected void rdbType_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdbType1.Checked == true)
            {
                this.ddlDevice.SelectedIndex = 0;
                this.ddlDevice.Visible = false;
                this.uxb2b.Visible = false;
                this.uxb2b1.Visible = false;
            }
            else
            {
                this.ddlDevice.Visible = true;
            }
        }

        protected void ddlDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlDevice.SelectedValue == "1")
            {
                this.uxb2b.Visible = true;
                this.uxb2b1.Visible = true;
            }
            else
            {
                this.uxb2b.Visible = false;
                this.uxb2b1.Visible = false;
            }
        }

        #endregion

        
        #region "Search Data"

        protected override Expression<Func<InvoiceItem, bool>> buildInvoiceItemQuery(Expression<Func<InvoiceItem, bool>> queryExpr)
        {
            queryExpr = queryExpr.And(i => i.InvoiceByHousehold.InvoiceUserCarrier.UID == _userProfile.UID);
            if (!String.IsNullOrEmpty(txtUxb2bBarCode.Text))
            {
                queryExpr = queryExpr.And(i => i.InvoiceByHousehold.InvoiceUserCarrier.CarrierNo == txtUxb2bBarCode.Text 
                    || i.InvoiceByHousehold.InvoiceUserCarrier.CarrierNo2 == txtUxb2bBarCode.Text);
            }
            return base.buildInvoiceItemQuery(queryExpr);
        }

        protected override Expression<Func<InvoiceAllowance, bool>> buildInvoiceAllowanceQuery(Expression<Func<InvoiceAllowance, bool>> queryExpr)
        {
//            return base.buildInvoiceAllowanceQuery(a => false);
            return a => false;
        }

        #endregion
        
    }    
}
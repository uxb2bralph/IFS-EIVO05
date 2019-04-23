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
    public partial class InquireInvoiceAndAllowanceForSeller : InquireInvoiceAndAllowanceBasic
    {

        protected override Expression<Func<InvoiceItem, bool>> buildInvoiceItemQuery(Expression<Func<InvoiceItem, bool>> queryExpr)
        {
            queryExpr = queryExpr.And(d => d.CDS_Document.DocumentOwner.OwnerID == _userProfile.CurrentUserRole.OrganizationCategory.CompanyID);
            return base.buildInvoiceItemQuery(queryExpr);
        }

        protected override Expression<Func<InvoiceAllowance, bool>> buildInvoiceAllowanceQuery(Expression<Func<InvoiceAllowance, bool>> queryExpr)
        {
            queryExpr = queryExpr.And(d => d.CDS_Document.DocumentOwner.OwnerID == _userProfile.CurrentUserRole.OrganizationCategory.CompanyID);
            return base.buildInvoiceAllowanceQuery(queryExpr);
        }


    }    
}
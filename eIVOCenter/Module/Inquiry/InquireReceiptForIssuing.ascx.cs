﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Business.Helper;
using eIVOGo.Module.Base;
using Model.DataEntity;
using Model.Security.MembershipManagement;
using Utility;
using Model.Locale;

namespace eIVOCenter.Module.Inquiry
{
    public partial class InquireReceiptForIssuing : InquireInvoiceForReceiving
    {

        protected override void buildQueryItem()
        {
            Expression<Func<ReceiptItem, bool>> queryExpr = i => i.SellerID == _userProfile.CurrentUserRole.OrganizationCategory.CompanyID && i.ReceiptCancellation == null;

            if (DateFrom.HasValue)
            {
                queryExpr = queryExpr.And(i => i.ReceiptDate >= DateFrom.DateTimeValue);
            }
            if (DateTo.HasValue)
            {
                queryExpr = queryExpr.And(i => i.ReceiptDate < DateTo.DateTimeValue.AddDays(1));
            }
            if (!String.IsNullOrEmpty(MasterID.SelectedValue))
            {
                queryExpr = queryExpr.And(i => i.BuyerID == int.Parse(MasterID.SelectedValue));
            }

            itemList.BuildQuery = table =>
            {
                return table.Where(d => d.DocType == (int)Naming.DocumentTypeDefinition.E_Receipt && d.CurrentStep == (int)Naming.B2BInvoiceStepDefinition.待開立)
                    .Join(table.Context.GetTable<ReceiptItem>().Where(queryExpr), d => d.DocID, i => i.ReceiptID, (d, i) => d).OrderByDescending(d => d.DocID);
            };

            if (itemList.Select().Count() > 0)
            {
                OnDone(null);
            }

        }
    }
}
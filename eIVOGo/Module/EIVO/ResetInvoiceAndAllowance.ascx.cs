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
using eIVOGo.Module.Inquiry;

namespace eIVOGo.Module.EIVO
{
    public partial class ResetInvoiceAndAllowance : InquireInvoiceAndAllowance
    {
        protected override void initializeData()
        {
            if (btnSearch.CommandArgument == "Query")
            {
                InvoiceAllowanceResetList allowanceListView;
                InvoiceItemResetList invoiceListView;
                switch (rdbSearchItem.SelectedIndex)
                {
                    case 0:
                        invoiceListView = (InvoiceItemResetList)this.LoadControl("InvoiceItemResetList.ascx");
                        invoiceListView.InitializeAsUserControl(this.Page);
                        invoiceListView.QueryExpr = buildInvoiceItemQuery(i => !i.CDS_Document.DocumentDispatches.Any(d => d.TypeID == (int)Naming.DocumentTypeDefinition.E_Invoice));
                        plResult.Controls.Add(invoiceListView);
                        break;
                    case 1:
                        allowanceListView = (InvoiceAllowanceResetList)this.LoadControl("InvoiceAllowanceResetList.ascx");
                        allowanceListView.InitializeAsUserControl(this.Page);
                        allowanceListView.QueryExpr = buildInvoiceAllowanceQuery(i => !i.CDS_Document.DocumentDispatches.Any(d => d.TypeID == (int)Naming.DocumentTypeDefinition.E_Allowance));
                        plResult.Controls.Add(allowanceListView);
                        break;
                    case 2:
                        invoiceListView = (InvoiceItemResetList)this.LoadControl("InvoiceCancellationResetList.ascx");
                        invoiceListView.InitializeAsUserControl(this.Page);
                        invoiceListView.QueryExpr = buildInvoiceItemQuery(i => i.InvoiceCancellation!=null && !i.CDS_Document.DocumentDispatches.Any(d => d.TypeID == (int)Naming.DocumentTypeDefinition.E_InvoiceCancellation));
                        plResult.Controls.Add(invoiceListView);
                        break;
                    case 3:
                        allowanceListView = (InvoiceAllowanceResetList)this.LoadControl("InvoiceAllowanceCancellationResetList.ascx");
                        allowanceListView.InitializeAsUserControl(this.Page);
                        allowanceListView.QueryExpr = buildInvoiceAllowanceQuery(i => i.InvoiceAllowanceCancellation!=null &&  !i.CDS_Document.DocumentDispatches.Any(d => d.TypeID == (int)Naming.DocumentTypeDefinition.E_AllowanceCancellation));
                        plResult.Controls.Add(allowanceListView);
                        break;
                    //case 4:
                    //    invoiceListView = (InvoiceItemResetList)this.LoadControl("InvoiceItemResetList.ascx");
                    //    invoiceListView.InitializeAsUserControl(this.Page);
                    //    invoiceListView.QueryExpr = buildInvoiceItemQuery(i => i.InvoiceCancellation == null && i.InvoiceWinningNumber != null);
                    //    plResult.Controls.Add(invoiceListView);
                    //    break;
                }
            }
        }
    }    
}
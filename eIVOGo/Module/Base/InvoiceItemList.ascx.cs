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

namespace eIVOGo.Module.Base
{
    public partial class InvoiceItemList : System.Web.UI.UserControl, IPostBackEventHandler
    {

        public Expression<Func<InvoiceItem, bool>> QueryExpr { get; set; }
        protected int? _totalRecordCount;
        protected decimal? _subtotal;

        public event EventHandler EmptyData;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            this.PreRender += new EventHandler(InvoiceItemQueryList_PreRender);
            dsInv.Select += new EventHandler<DataAccessLayer.basis.LinqToSqlDataSourceEventArgs<InvoiceItem>>(dsInv_Select);
        }

        void InvoiceItemQueryList_PreRender(object sender, EventArgs e)
        {
            if (gvEntity.BottomPagerRow != null)
            {
                PagingControl paging = (PagingControl)gvEntity.BottomPagerRow.Cells[0].FindControl("pagingList");
                paging.RecordCount = dsInv.CurrentView.LastSelectArguments.TotalRowCount;
                paging.CurrentPageIndex = gvEntity.PageIndex;
            }
        }

        void dsInv_Select(object sender, DataAccessLayer.basis.LinqToSqlDataSourceEventArgs<InvoiceItem> e)
        {
            if (QueryExpr != null)
            {
                e.Query = dsInv.CreateDataManager().EntityList.Where(QueryExpr).OrderByDescending(i => i.InvoiceID);
                _totalRecordCount = e.Query.Count();
                _subtotal =  e.Query.Sum(i => i.InvoiceAmountType.TotalAmount);
                if (_totalRecordCount.Value == 0)
                {
                    lblError.Visible = true;
                }
            }
            else
            {
                e.QueryExpr = i => false;
                lblError.Visible = true;
            }
        }

        protected void PageIndexChanged(object source, PageChangedEventArgs e)
        {
            gvEntity.PageIndex = e.NewPageIndex;
            gvEntity.DataBind();
        }

        protected object doEmptyDataHandler()
        {
            if (EmptyData != null)
            {
                EmptyData(this, new EventArgs());
            }
            this.gvEntity.Visible = false;
            return null;
        }

        #region IPostBackEventHandler Members

        public virtual void RaisePostBackEvent(string eventArgument)
        {
            if (eventArgument.StartsWith("S:"))
            {
                this.PNewInvalidInvoicePreview1.setDetail = eventArgument.Substring(2).Trim();
                this.PNewInvalidInvoicePreview1.Popup.Show();
            }
        }

        #endregion
    }    
}
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
    public partial class InvoiceTrackCodeList : System.Web.UI.UserControl, IPostBackEventHandler
    {

        public Expression<Func<InvoiceTrackCode, bool>> QueryExpr { get; set; }
        protected int? _totalRecordCount;

        protected void Page_Load(object sender, EventArgs e)
        {
            gvEntity.PageIndex = PagingControl.GetCurrentPageIndex(gvEntity, 0);
        }

        protected override void OnInit(EventArgs e)
        {
            this.PreRender += new EventHandler(InvoiceTrackCode_PreRender);
            dsInv.Select += new EventHandler<DataAccessLayer.basis.LinqToSqlDataSourceEventArgs<InvoiceTrackCode>>(dsInv_Select);
        }

        void InvoiceTrackCode_PreRender(object sender, EventArgs e)
        {
            if (gvEntity.BottomPagerRow != null)
            {
                PagingControl paging = (PagingControl)gvEntity.BottomPagerRow.Cells[0].FindControl("pagingList");
                paging.RecordCount = dsInv.CurrentView.LastSelectArguments.TotalRowCount;
                paging.CurrentPageIndex = gvEntity.PageIndex;
            }

            if (_totalRecordCount.HasValue && _totalRecordCount.Value == 0)
            {
                lblError.Visible = true;
            }
        }

        void dsInv_Select(object sender, DataAccessLayer.basis.LinqToSqlDataSourceEventArgs<InvoiceTrackCode> e)
        {
            if (QueryExpr != null)
            {
                e.Query = dsInv.CreateDataManager().EntityList.Where(QueryExpr);
                _totalRecordCount = e.Query.Count();
            }
            else
            {
                e.QueryExpr = i => false;
            }
        }
       
        public void BindData()
        {
            gvEntity.DataBind();
        }

        #region IPostBackEventHandler Members

        public virtual void RaisePostBackEvent(string eventArgument)
        {

        }

        #endregion

        protected void gvEntity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PagingControl paging = (PagingControl)e.Row.FindControl("pagingList");
                paging.RecordCount = dsInv.CurrentView.LastSelectArguments.TotalRowCount;
                paging.CurrentPageIndex = gvEntity.PageIndex;
            }
        }
    }    
}
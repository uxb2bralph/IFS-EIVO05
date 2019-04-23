using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.DataEntity;
using Model.Locale;
using Model.Security.MembershipManagement;
using Utility;

namespace eIVOGo.Module.Inquiry
{
    public partial class BonusReportList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                initializeData();
                ShowResult(false);
                border_gray.Visible = false;
                Div1.Visible = false;
            }
            PagingControl1.PageSize = 10;
            btnPrint.PrintControls.Add(border_gray);
        }
        protected void ShowResult(bool bolShow)
        {
            PagingControl1.Visible = bolShow;
            btnPrint.Visible = bolShow;
            Div2.Visible = bolShow;   
            border_gray.Visible = true;
            Div1.Visible = true;
            if (bolShow)
                NoData.Visible = false;
            else
                NoData.Visible = true;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            PagingControl1.PageIndexChanged += new Uxnet.Web.Module.Common.PageChangedEventHandler(PagingControl1_PageIndexChanged);
            btnPrint.BeforeClick += new EventHandler(btnPrint_BeforeClick);
        }
        void btnPrint_BeforeClick(object sender, EventArgs e)
        {
            bindData(false);
        }
        void PagingControl1_PageIndexChanged(object source, Uxnet.Web.Module.Common.PageChangedEventArgs e)
        {
            bindData(true);
        }

        private void initializeData()
        {            
            this.PagingControl1.Visible = false;
            PeriodFrom.DateFrom = DateTime.Now.AddMonths(-2);
            PeriodTo.DateFrom = PeriodFrom.DateFrom;
        }

        private void bindData(bool bPaging)
        {
            var mgr = dsInv.CreateDataManager();
            Expression<Func<InvoiceWinningNumber, bool>> queryExpr = w => true;
            if (!String.IsNullOrEmpty(SellerID.Selector.SelectedValue))
            {
                //minyu-0701
                queryExpr = queryExpr.And(w => 
                    (w.InvoiceItem.SellerID == int.Parse(SellerID.Selector.SelectedValue)) &&
                    (w.InvoiceItem.InvoiceCancellation == null));
            }

            if (PeriodFrom.SelectedDate.HasValue)
            {
                queryExpr = queryExpr.And(w => (w.Year == PeriodFrom.SelectedDate.Value.Year && w.MonthFrom >= PeriodFrom.SelectedDate.Value.Month) || w.Year > PeriodFrom.SelectedDate.Value.Year);
            }

            if (PeriodTo.SelectedDate.HasValue)
            {
                queryExpr = queryExpr.And(w => (w.Year == PeriodTo.SelectedDate.Value.Year && w.MonthFrom <= PeriodTo.SelectedDate.Value.Month) || w.Year < PeriodTo.SelectedDate.Value.Year);
            }


            IQueryable<InvoiceWinningNumber> invData = mgr.GetTable<InvoiceWinningNumber>().Where(queryExpr);
            var items = invData.GroupBy(w => w.InvoiceItem.SellerID);

            if (items.Count() > 0)
            {
                this.PagingControl1.RecordCount = items.Count();
                this.PagingControl1.Visible = true;

                rpList.DataSource = bPaging ? items.Skip(PagingControl1.CurrentPageIndex * PagingControl1.PageSize).Take(PagingControl1.PageSize) : items;
                //rpList.DataSource = items;
                rpList.DataBind();
                var wItems = mgr.GetTable<InvoiceWinningNumber>();
                litTotal.Text = String.Format("{0:##,###,###,###}", items.Sum(g => g.Count(i=> i.InvoiceItem.InvoiceCancellation !=null)));
                if (litTotal.Text == "") litTotal.Text = "0";
                litDonate.Text = String.Format("{0:##,###,###,###}", 
                    items.Sum(g => g.Where(w => (w.InvoiceItem.DonateMark == "1") && (w.InvoiceItem.InvoiceCancellation == null)).Count()));
                if (litDonate.Text == "") litDonate.Text = "0";
                ShowResult(true);
            }
            else
            {
                litTotal.Text = "0";
                litDonate.Text = "0";
                this.PagingControl1.Visible = false;
                ShowResult(false);
            }

        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            bindData(true);
        }
    }
}
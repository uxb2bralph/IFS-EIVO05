﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Locale;
using Model.Security.MembershipManagement;
using Model.InvoiceManagement;
using Business.Helper;
using Model.SCMDataEntity;
using Uxnet.Web.Module.Common;
using System.Linq.Expressions;
using Utility;
using Uxnet.Web.WebUI;

namespace eIVOGo.Module.SCM
{
    public partial class PurchaseOrderReturnedMangement : System.Web.UI.UserControl, IPostBackEventHandler
    {
        UserProfileMember _userProfile;

        protected void Page_Load(object sender, EventArgs e)
        {
            _userProfile = WebPageUtility.UserProfile;
            if (!Page.IsPostBack)
            {
                initializeData();
            }
        }

        private void initializeData()
        {
            var mgr = this.dsPOReturn.CreateDataManager();
            this.ddlWarehouse.Items.AddRange(mgr.GetTable<WAREHOUSE>().Select(wh => new ListItem(wh.WAREHOUSE_NAME, wh.WAREHOUSE_SN.ToString())).ToArray());
            this.ddlSupplier.Items.AddRange(mgr.GetTable<SUPPLIER>().Select(s => new ListItem(s.SUPPLIER_NAME, s.SUPPLIER_SN.ToString())).ToArray());
        }

        protected override void OnInit(EventArgs e)
        {
            this.PreRender+=new EventHandler(PurchaseOrderMangement_PreRender);
            this.dsPOReturn.Select += new EventHandler<DataAccessLayer.basis.LinqToSqlDataSourceEventArgs<PURCHASE_ORDER_RETURNED>>(PurchaseOrderReturnDataSource1_Select);
            this.RefuseDocument.Done += new EventHandler(RefuseDocument_Done);
        }

        void RefuseDocument_Done(object sender, EventArgs e)
        {
            this.gvEntity.DataBind();
            this.AjaxAlert("該筆資料已刪除!!");            
        }

        void PurchaseOrderReturnDataSource1_Select(object sender, DataAccessLayer.basis.LinqToSqlDataSourceEventArgs<PURCHASE_ORDER_RETURNED> e)
        {
            if (!String.IsNullOrEmpty(this.btnSearch.CommandArgument))
            {
                var mgr = this.dsPOReturn.CreateDataManager();

                IQueryable<PURCHASE_ORDER_RETURNED> po = mgr.EntityList.Where(p => p.PO_RETURNED_DELETE_STATUS == 0);

                if (this.ddlWarehouse.SelectedIndex != 0)
                {
                    po = po.Where(p => p.WAREHOUSE.WAREHOUSE_SN == long.Parse(this.ddlWarehouse.SelectedValue));
                }

                if (this.ddlSupplier.SelectedIndex != 0)
                {
                    po = po.Where(p => p.SUPPLIER_SN == long.Parse(this.ddlSupplier.SelectedValue));
                }

                if (!string.IsNullOrEmpty(this.txtPurchaseRetNO.Text))
                {
                    po = po.Where(p => p.PURCHASE_ORDER_RETURNED_NUMBER.Equals(this.txtPurchaseRetNO.Text));
                }

                if (!string.IsNullOrEmpty(this.txtBarCode.Text))
                {
                    po = po.Where(p => p.PURCHASE_ORDER_RETURNED_DETAILS.Where(pod => pod.PRODUCTS_DATA.PRODUCTS_BARCODE.Equals(this.txtBarCode.Text)).Count() > 0);
                }

                if (!string.IsNullOrEmpty(this.txtProdName.Text))
                {
                    po = po.Where(p => p.PURCHASE_ORDER_RETURNED_DETAILS.Where(pod => pod.PRODUCTS_DATA.PRODUCTS_NAME.Contains(this.txtProdName.Text)).Count() > 0);
                }

                if (!string.IsNullOrEmpty(this.DateFrom.TextBox.Text) && !string.IsNullOrEmpty(this.DateTo.TextBox.Text))
                {
                    po = po.Where(p => p.PO_RETURNED_DATETIME.Value >= this.DateFrom.DateTimeValue & p.PO_RETURNED_DATETIME.Value <= this.DateTo.DateTimeValue);
                }

                if (this.rdbCloseStatus.SelectedIndex != 0)
                {
                    po = po.Where(p => p.PO_RETURNED_STATUS == (this.rdbCloseStatus.SelectedIndex == 1 ? 1 : 0));
                }

                if (this.rdbDeleteStatus.SelectedIndex != 0)
                {
                    po = po.Where(p => p.PO_RETURNED_DELETE_STATUS == (this.rdbDeleteStatus.SelectedIndex == 1 ? 1 : 0));
                }

                e.Query = po;

                this.lblRowCount.Text = po.Count().ToString();
                this.lblTotalSum.Text = "0.00";
            }
            else
            {
                e.QueryExpr = p => false;
            }
        }

        #region "Button Event"
        protected void btnNewPurchaseRtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("PurchaseOrderReturned.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.divResult.Visible = true;
            this.btnSearch.CommandArgument = "Query";
            this.gvEntity.DataBind();
            if (this.gvEntity.Rows.Count > 0)
            {
                this.countTable.Visible = true;
                this.lblError.Visible = false;
            }
            else
            {
                this.lblError.Text = "查無資料!!";
                this.lblError.Visible = true;
                this.countTable.Visible = false;
            }
        }
        #endregion

        #region "Gridview Event"
        protected void PageIndexChanged(object source, PageChangedEventArgs e)
        {
            gvEntity.PageIndex = e.NewPageIndex;
            gvEntity.DataBind();
        }

        void PurchaseOrderMangement_PreRender(object sender, EventArgs e)
        {
            if (gvEntity.BottomPagerRow != null)
            {
                PagingControl paging = (PagingControl)gvEntity.BottomPagerRow.Cells[0].FindControl("pagingList");
                paging.RecordCount = this.dsPOReturn.CurrentView.LastSelectArguments.TotalRowCount;
                paging.CurrentPageIndex = gvEntity.PageIndex;
            }
        }
        #endregion

        #region IPostBackEventHandler Members
        public void RaisePostBackEvent(string eventArgument)
        {
            if (eventArgument.StartsWith("D:"))
            {
                this.RefuseDocument.DocID = int.Parse(eventArgument.Substring(2));
                this.RefuseDocument.Show();
            }
            else if (eventArgument.StartsWith("P:"))
            {
                this.PupopPORDetail1.setDetail = eventArgument.Substring(2);
                this.PupopPORDetail1.Popup.Show();
            }
            else if (eventArgument.StartsWith("S:"))
            {
                this.PupopDeleteReason1.DocID = int.Parse(eventArgument.Substring(2));
                this.PupopDeleteReason1.Show();
            }
        }
        #endregion
    }    
}

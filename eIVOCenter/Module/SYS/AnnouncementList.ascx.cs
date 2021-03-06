﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Uxnet.Web.Module.Common;
using Utility;
using Uxnet.Web.Module.SiteAction;

namespace eIVOCenter.Module.SYS
{
    public partial class AnnouncementList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            gvEntity.DataBound += new EventHandler(gvEntity_DataBound);
            AnnouncementItem.Done += new EventHandler(categoryItem_Done);
        }

        void categoryItem_Done(object sender, EventArgs e)
        {
            gvEntity.DataBind();
        }

        void gvEntity_DataBound(object sender, EventArgs e)
        {
            gvEntity.SetPageIndex("pagingList", dsEntity.CurrentView.LastSelectArguments.TotalRowCount);
        }


        protected void PageIndexChanged(object source, PageChangedEventArgs e)
        {
            gvEntity.PageIndex = e.NewPageIndex;
            gvEntity.DataBind();
        }

        protected void gvEntity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            { 
                case "Modify":
                    AnnouncementItem.QueryExpr = r => r.AnnID == int.Parse((String)e.CommandArgument);
                    AnnouncementItem.BindData();
                    break;
                case "Delete":
                    delete(int.Parse((String)e.CommandArgument));
                    break;
                case "Create":
                    AnnouncementItem.Clean();
                    AnnouncementItem.Show();
                    break;
                case "Download":
                    dumpMenu((String)e.CommandArgument);
                    break;
                //case "Online":
                //    Page.Items["menuPath"] = e.CommandArgument;
                //    Server.Transfer("~/SYS/EditMenuNodes.aspx");
                    //break;
            }
        }

        private void dumpMenu(string siteMenu)
        {
            String menuFile = Path.Combine(SiteMenuBar.MenuManager.StoredPath, siteMenu);
            if (File.Exists(menuFile))
            {
                Response.WriteFileAsDownload(menuFile);
            }

        }

        private void delete(int key)
        {
            dsEntity.CreateDataManager().DeleteAny(r => r.AnnID == key);
            gvEntity.DataBind();
            Response.Redirect(Request.RawUrl);
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            AnnouncementItem.Show();
        }

    }
}
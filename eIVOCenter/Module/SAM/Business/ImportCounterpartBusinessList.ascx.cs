using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eIVOCenter.Helper;
using Uxnet.Web.Module.Common;
using Model.DataEntity;
using Model.UploadManagement;
using Model.Locale;

namespace eIVOCenter.Module.SAM.Business
{
    public partial class ImportCounterpartBusinessList : System.Web.UI.UserControl
    {
        protected IUploadManager<ItemUpload<Organization>> _mgr;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.PreRender += new EventHandler(ImportCounterpartBusinessList_PreRender);
            _mgr = (IUploadManager<ItemUpload<Organization>>)modelItem.DataItem;
            this.Visible = _mgr != null;
        }

        public IUploadManager<ItemUpload<Organization>> UploadManager
        {
            get
            {
                return _mgr;
            }
            set
            {
                modelItem.DataItem = value;
                _mgr = value;
                this.Visible = true;
            }
        }

        void ImportCounterpartBusinessList_PreRender(object sender, EventArgs e)
        {
            if (_mgr == null)
            {
                this.Visible = false;
            }
            else
            {
                if (_mgr.IsValid)
                {
                    gvEntity.DataSource = _mgr.ItemList.Skip(pagingList.CurrentPageIndex * pagingList.PageSize).Take(pagingList.PageSize);
                    pagingList.RecordCount = _mgr.ItemCount;
                }
                else
                {
                    gvEntity.DataSource = _mgr.ErrorList.Skip(pagingList.CurrentPageIndex * pagingList.PageSize).Take(pagingList.PageSize);
                    pagingList.RecordCount = _mgr.ErrorList.Count;
                }

                gvEntity.DataBind();

                if (pagingList.RecordCount <= 0)
                {
                    pagingList.Visible = false;
                }
                else
                {
                    pagingList.Visible = true;
                }

            }
        }
    }
}
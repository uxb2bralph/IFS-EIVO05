﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.DataEntity;
using Model.Locale;
using eIVOGo.Module.Base;

namespace eIVOGo.Module.SAM.Business
{
    public partial class CompanyList : EntityItemList<EIVOEntityDataContext, Organization>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            doActivate.DoAction = arg =>
            {
                var mgr = dsEntity.CreateDataManager();
                var item = mgr.EntityList.Where(m => m.CompanyID == int.Parse(arg)).First();
                item.OrganizationStatus.CurrentLevel = (int)Naming.MemberStatusDefinition.Checked;
                mgr.SubmitChanges();
            };
            doCreate.DoAction = arg =>
            {
                modelItem.DataItem = null;
                Server.Transfer(ToEdit.TransferTo);
            };
            doEdit.DoAction = arg =>
            {
                modelItem.DataItem = int.Parse(arg);
                Server.Transfer(ToEdit.TransferTo);
            };
            doDelete.DoAction = arg =>
            {
                delete(arg);
            };
            doSetup.DoAction = arg =>
                {
                    modelItem.DataItem = int.Parse(arg);
                    socialWelfare.BindData();
                };
        }


        protected void delete(string keyValue)
        {
            var mgr = dsEntity.CreateDataManager();
            var item = mgr.EntityList.Where(m => m.CompanyID == int.Parse(keyValue)).First();
            item.OrganizationStatus.CurrentLevel = (int)Naming.MemberStatusDefinition.Mark_To_Delete;
            mgr.SubmitChanges();
        }
    }
}
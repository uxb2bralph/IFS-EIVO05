﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eIVOGo.Module.Base;
using Model.DataEntity;

namespace eIVOCenter.Module.EIVO.Action
{
    public partial class InvoiceProductItemList : EntityItemList<EIVOEntityDataContext, InvoiceProductItem>
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}
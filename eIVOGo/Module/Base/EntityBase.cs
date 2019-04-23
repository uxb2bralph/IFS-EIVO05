using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI.WebControls;

using DataAccessLayer.basis;
using Utility;
using Uxnet.Web.WebUI;
using Uxnet.Web.Module.DataModel;

namespace eIVOGo.Module.Base
{
    public abstract partial class EntityBase<T, TEntity> : EntityDataSource<T, TEntity>
        where T : DataContext, new()
        where TEntity : class, new()
    {

        protected TEntity _entity;

        protected virtual void loadEntity()
        {
            _entity = Select().FirstOrDefault();
        }

        public virtual void BindData()
        {
            loadEntity();
            this.DataBind();
        }

        public override void DataBind()
        {
            if (_entity != null)
            {
                base.DataBind();
            }
        }
    }
}
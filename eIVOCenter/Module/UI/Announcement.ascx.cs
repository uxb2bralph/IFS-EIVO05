using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.DataEntity;
namespace eIVOCenter.Module.UI
{
    public partial class Announcement : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
            protected override void OnInit(EventArgs e)
            {
                this.PreRender += new EventHandler(template_main_page_master_PreRender);
            }
            void template_main_page_master_PreRender(object sender, EventArgs e)
            {
                var item = dsEntity.CreateDataManager().GetTable<Announcement_REC>()
                        .Where(a => (a.StartDate <= DateTime.Now && a.EndDate >= DateTime.Now) || a.AlwaysShow == true);
                // this.AnnMessage.Text=string.Empty;
                // for(int i=0;i<item.ToList().Count();i++)
                //{
                //    this.AnnMessage.Text += item.ToList()[i].AnnMessage+"\t";
                //}
                rpList.DataSource = item;
                rpList.DataBind();
            }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eIVOGo.Module.EIVO;
using Model.InvoiceManagement;
using Model.DataEntity;
using Model.Security.MembershipManagement;
using Business.Helper;
using Model.Locale;

namespace eIVOGo.SAM
{
    public partial class PrintAllowancePage : System.Web.UI.Page
    {
        private UserProfileMember _userProfile;

        protected void Page_Load(object sender, EventArgs e)
        {
            _userProfile = WebPageUtility.UserProfile;
            initializeData();
        }

        protected virtual void initializeData()
        {
            //改DocumentPrintLog去紀錄列印資料
            //IEnumerable<int> items = Session["PrintDoc"] as IEnumerable<int>;
            //Session.Remove("PrintDoc");
            IEnumerable<int> items;
            String AllowancePrintView = null;
            using (InvoiceManager mgr = new InvoiceManager())
            {
                items = mgr.GetTable<DocumentPrintQueue>().Where(i => i.UID == _userProfile.UID & i.CDS_Document.DocType == (int)Naming.DocumentTypeDefinition.E_Allowance).Select(i => i.DocID).ToList();
                //if (items != null && items.Count() > 0)
                //{
                //    AllowancePrintView = mgr.GetTable<DocumentPrintQueue>().Where(i => i.UID == _userProfile.UID & i.CDS_Document.DocType == (int)Naming.DocumentTypeDefinition.E_Allowance).First().CDS_Document.InvoiceAllowance.InvoiceAllowanceSeller.Organization.OrganizationStatus.AllowancePrintView;
                //    if (String.IsNullOrEmpty(AllowancePrintView))
                //    {
                //        AllowancePrintView = "~/Module/EIVO/AllowancePrintView.ascx";
                //    }
                //}

                if (items != null && items.Count() > 0)
                {
                    AllowancePrintView finalView = null;

                    foreach (var item in items)
                    {
                        AllowancePrintView = mgr.GetTable<InvoiceAllowance>().Where(a => a.CDS_Document.DocID == item).FirstOrDefault().InvoiceAllowanceSeller.Organization.OrganizationStatus.AllowancePrintView;
                        if (String.IsNullOrEmpty(AllowancePrintView))
                        {
                            AllowancePrintView = "~/Module/EIVO/AllowancePrintView.ascx";
                        }

                        int allowanceID = item;
                        AllowancePrintView view = (AllowancePrintView)this.LoadControl(AllowancePrintView);
                        finalView = view;
                        view.InitializeAsUserControl(this.Page);
                        view.AllowanceID = allowanceID;
                        theForm.Controls.Add(view);
                    }

                    if (finalView != null)
                        finalView.IsFinal = true;
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Business.Helper;
using eIVOGo.Helper;
using eIVOGo.Module.Base;
using eIVOGo.Module.Common;
using eIVOGo.Properties;
using Model.DataEntity;
using Model.Locale;
using Model.Security.MembershipManagement;
using Utility;
using Uxnet.Web.WebUI;
using DataAccessLayer.basis;

namespace eIVOGo.Module.SAM
{
    public partial class EditOrganization : EditEntityItemBase<EIVOEntityDataContext, Organization>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.PreRender += new EventHandler(AddMember_PreRender);
            this.QueryExpr = m => m.CompanyID == (int?)modelItem.DataItem;
        }

        void AddMember_PreRender(object sender, EventArgs e)
        {
            if (_entity != null)
            {
                modelItem.DataItem = _entity.CompanyID;
            }
            else
            {
                this.SetToOutsourcingCS.Checked = true;
            }
        }

        protected override bool saveEntity()
        {
            var mgr = dsEntity.CreateDataManager();

            loadEntity();

            String receiptNo = ReceiptNo.Text.Trim();
            if (_entity == null || _entity.ReceiptNo != receiptNo)
            {
                if (mgr.GetTable<Organization>().Any(o => o.ReceiptNo == receiptNo))
                {
                    this.AjaxAlert("相同的企業統編已存在!!");
                    return false;
                }
            }

            if (String.IsNullOrEmpty(CategoryID.SelectedValue))
            {
                this.AjaxAlert("請設定公司類別!!");
                return false;
            }

            bool isNewItem = false;
            OrganizationCategory orgaCate = null;
            if (_entity == null)
            {
                _entity = new Organization
                    {
                        OrganizationStatus = new OrganizationStatus
                        {
                            CurrentLevel = (int)Naming.MemberStatusDefinition.Checked
                        }

                    };

                orgaCate = new OrganizationCategory
                {
                    Organization = _entity
                };

                mgr.EntityList.InsertOnSubmit(_entity);
                isNewItem = true;
            }
            else
            {
                orgaCate = _entity.OrganizationCategory.First();
            }

            orgaCate.CategoryID = int.Parse(CategoryID.SelectedValue);

            _entity.ReceiptNo = receiptNo;
            _entity.CompanyName = CompanyName.Text.Trim().InsteadOfNullOrEmpty(null);
            _entity.Addr = Addr.Text.Trim().InsteadOfNullOrEmpty(null);
            _entity.Phone = Phone.Text.Trim().InsteadOfNullOrEmpty(null);
            _entity.Fax = Fax.Text.Trim().InsteadOfNullOrEmpty(null);
            _entity.UndertakerName = UndertakerName.Text.Trim().InsteadOfNullOrEmpty(null);
            _entity.ContactName = ContactName.Text.Trim().InsteadOfNullOrEmpty(null);
            _entity.ContactTitle = ContactTitle.Text.Trim().InsteadOfNullOrEmpty(null);
            _entity.ContactPhone = ContactPhone.Text.Trim().InsteadOfNullOrEmpty(null);
            _entity.ContactMobilePhone = ContactMobilePhone.Text.Trim().InsteadOfNullOrEmpty(null);
            _entity.ContactEmail = ContactEmail.Text.Trim().InsteadOfNullOrEmpty(null);
            _entity.OrganizationStatus.SetToPrintInvoice = SetToPrintInvoice.Checked;
            _entity.OrganizationStatus.SetToOutsourcingCS = SetToOutsourcingCS.Checked;
            _entity.OrganizationStatus.InvoicePrintView = _entity.OrganizationStatus.SetToPrintInvoice.Value ? InvoicePrintView.Text : null;
            _entity.OrganizationStatus.AllowancePrintView = _entity.OrganizationStatus.SetToPrintInvoice.Value ? AllowancePrintView.Text : null;
            _entity.OrganizationStatus.AuthorizationNo = AuthorizationNo.Text.Trim().InsteadOfNullOrEmpty(null);

            if (!_entity.OrganizationValueCheck(this))
            {
                return false;
            }

            mgr.SubmitChanges();

            if (isNewItem)
            {
                createDefaultUser(mgr, orgaCate);
            }

            return true;
        }

        private void createDefaultUser(GenericManager<EIVOEntityDataContext, Organization> mgr, OrganizationCategory orgaCate)
        {
            var userProfile = new UserProfile
            {
                PID = _entity.ReceiptNo,
                Phone = _entity.Phone,
                EMail = _entity.ContactEmail,
                Address = _entity.Addr,
                UserProfileExtension = new UserProfileExtension
                {
                    IDNo = _entity.ReceiptNo
                },
                UserProfileStatus = new UserProfileStatus
                {
                    CurrentLevel = (int)Naming.MemberStatusDefinition.Wait_For_Check
                }
            };

            mgr.GetTable<UserRole>().InsertOnSubmit(new UserRole
            {
                RoleID = (int)Naming.RoleID.ROLE_SELLER,
                UserProfile = userProfile,
                OrganizationCategory = orgaCate
            });

            mgr.SubmitChanges();

            try
            {
                String.Format("{0}{1}?id={2}", Uxnet.Web.Properties.Settings.Default.HostUrl, VirtualPathUtility.ToAbsolute(Settings.Default.NotifyActivation), userProfile.UID)
                    .MailWebPage(userProfile.EMail, "電子發票系統 會員啟用認證信");
            }
            catch (Exception ex)
            {
                Logger.Warn("［電子發票系統 會員啟用認證信］傳送失敗,原因 => " + ex.Message);
                Logger.Error(ex);
            }
        }
    }
}
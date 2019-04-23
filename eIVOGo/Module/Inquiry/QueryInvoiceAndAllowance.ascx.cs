using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Locale;
using Model.Security.MembershipManagement;
using Model.InvoiceManagement;
using Business.Helper;
using Model.DataEntity;
using Uxnet.Web.Module.Common;
using System.Linq.Expressions;
using Utility;

namespace eIVOGo.Module.Inquiry
{
    public partial class QueryInvoiceAndAllowance : System.Web.UI.UserControl, IPostBackEventHandler
    {
        UserProfileMember _userProfile;

        public class dataType
        {
            public int InvoiceID;
            public DateTime? InvoiceDate;
            public string ChineseInvoiceDate;
            public string CompanyName;
            public string ReceiptNo;
            public string TrackCode;
            public string No;
            public Decimal? SalesAmount;
            public Decimal? TaxAmount;
            public Decimal? TotalAmount;
            public string check;
            public string Donation;
            public string DonationName;
            public string DonateMark;
            public string BuyerReceiptNo;
            public string googleID;
            public string SID;
            public IEnumerable<int> memo;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _userProfile = WebPageUtility.UserProfile;
            if (!Page.IsPostBack)
            {
                if (_userProfile.CurrentUserRole.RoleID == ((int)Naming.RoleID.ROLE_GUEST))
                {
                    this.rdbType1.Visible = false;
                    this.rdbType2.Visible = false;
                    this.lblDevice.Visible = true;
                    this.ddlDevice.Visible = true;
                }
                else if (_userProfile.CurrentUserRole.RoleID == ((int)Naming.RoleID.ROLE_SYS))
                {
                    this.divReceiptNo.Visible = true;
                }                

                if (_userProfile.CurrentUserRole.OrganizationCategory.CategoryID == (int)Naming.B2CCategoryID.Google台灣)
                {
                    this.lblName.Text = "GoogleID";
                    this.divGoogleID.Visible = true;
                }
                else if (_userProfile.CurrentUserRole.OrganizationCategory.CategoryID == (int)Naming.B2CCategoryID.商家發票自動配號)
                {
                    this.lblName.Text = "客戶ID";
                    this.divGoogleID.Visible = true;
                }

                SearchItem();
                LoadCompanyReceiptNo();
                LoadDevice();
                this.PrintingButton21.btnPrint.Text = "資料列印";
                this.PrintingButton21.btnPrint.CssClass = "btn";                
            }
            this.PrintingButton21.PrintControls.Add(this.gvEntity);
        }

        protected override void OnInit(EventArgs e)
        {
            gvEntity.PreRender += new EventHandler(gvEntity_PreRender);
            this.PrintingButton21.BeforeClick += new EventHandler(btnPrint_BeforeClick);
        }

        void btnPrint_BeforeClick(object sender, EventArgs e)
        {
            this.gvEntity.AllowPaging = false;
        }

        #region "Load Page Control Data"
        protected void SearchItem()
        {
            this.rdbSearchItem.Items.Clear();
            //if (_userProfile.CurrentUserRole.RoleID == ((int)Naming.RoleID.ROLE_GOOGLETW))
            //{
            //    this.rdbSearchItem.Items.Add(new ListItem("電子發票", "1"));
            //    this.rdbSearchItem.Items.Add(new ListItem("作廢電子發票", "3"));
            //    this.rdbSearchItem.Items.Add(new ListItem("中獎發票", "5"));
            //}
            //else
            //{
                this.rdbSearchItem.Items.Add(new ListItem("電子發票", "1"));
                this.rdbSearchItem.Items.Add(new ListItem("電子折讓單", "2"));
                this.rdbSearchItem.Items.Add(new ListItem("作廢電子發票", "3"));
                this.rdbSearchItem.Items.Add(new ListItem("作廢電子折讓單", "4"));
                this.rdbSearchItem.Items.Add(new ListItem("中獎發票", "5"));
            //}
            this.rdbSearchItem.SelectedIndex = 0;
        }

        protected void LoadCompanyReceiptNo()
        {
            using (InvoiceManager im = new InvoiceManager())
            {
                this.ddlReceiptNo.Items.AddRange(im.GetTable<Organization>().Where(o => o.OrganizationCategory.Any(c => c.CategoryID == (int)Naming.B2CCategoryID.Google台灣 || c.CategoryID == (int)Naming.B2CCategoryID.商家 || c.CategoryID == (int)Naming.B2CCategoryID.商家發票自動配號)).Select(o => new ListItem(o.ReceiptNo.Trim() + " " + o.CompanyName.Trim(), o.ReceiptNo.Trim())).ToArray());
            }
        }

        protected void LoadDevice()
        {
            this.ddlDevice.Items.Clear();
            this.ddlDevice.Items.Add(new ListItem("-請選擇-", "0"));
            this.ddlDevice.Items.Add(new ListItem("UXB2B條碼卡", "1"));
            this.ddlDevice.Items.Add(new ListItem("悠遊卡", "2"));
        }
        #endregion

        #region "Page Control Event"
        protected void rdbType_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdbType1.Checked == true)
            {
                this.ddlDevice.SelectedIndex = 0;
                this.ddlDevice.Visible = false;
                this.uxb2b.Visible = false;
                this.uxb2b1.Visible = false;
            }
            else
            {
                this.ddlDevice.Visible = true;
            }
        }

        protected void ddlReceiptNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.divResult.Visible = false;
            this.txtGoogleID.Text = "";
            using (InvoiceManager im = new InvoiceManager())
            {
                if (im.GetTable<OrganizationCategory>().Where(o => o.Organization.ReceiptNo.Equals(this.ddlReceiptNo.SelectedValue.Trim())).Any(c => c.CategoryID == (int)Naming.B2CCategoryID.Google台灣))
                {
                    this.lblName.Text="GoogleID";
                    this.divGoogleID.Visible = true;
                }
                else if (im.GetTable<OrganizationCategory>().Where(o => o.Organization.ReceiptNo.Equals(this.ddlReceiptNo.SelectedValue.Trim())).Any(c => c.CategoryID == (int)Naming.B2CCategoryID.商家發票自動配號))
                {
                    this.lblName.Text="客戶ID";
                    this.divGoogleID.Visible = true;
                }
                else
                {
                    this.lblName.Text = "";
                    this.divGoogleID.Visible = false;
                }
            }
        }

        protected void ddlDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlDevice.SelectedValue == "1")
            {
                this.uxb2b.Visible = true;
                this.uxb2b1.Visible = true;
            }
            else
            {
                this.uxb2b.Visible = false;
                this.uxb2b1.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.divResult.Visible = true;
            SearchData();
        }
        #endregion

        #region "Gridview Event"
        void gvEntity_PreRender(object sender, EventArgs e)
        {
            if (this.divResult.Visible == true)
            {
                SearchData();
                //if (_userProfile.CurrentUserRole.RoleID != ((int)Naming.RoleID.ROLE_GOOGLETW) && _userProfile.CurrentUserRole.RoleID != ((int)Naming.RoleID.ROLE_SYS))
                //{
                //    this.gvEntity.Columns[1].Visible = false;                    
                //}
                //else if (_userProfile.CurrentUserRole.RoleID == ((int)Naming.RoleID.ROLE_GOOGLETW))
                //{
                //    this.gvEntity.Columns[4].HeaderText = "發票號碼";
                //}
            }
        }        
        #endregion

        #region "Search Data"
        private void SearchData()
        {
            using (InvoiceManager im = new InvoiceManager())
            {                
                try
                {
                    IQueryable<dataType> data = null;
                    IQueryable<InvoiceItem> dataSrc = im.EntityList;
                    IQueryable<InvoiceAllowance> allowance = im.GetTable<InvoiceAllowance>();

                    //一般使用者僅能查詢屬於自己卡號的發票資訊,系統管理者則可以查詢全部
                    if (_userProfile.CurrentUserRole.RoleID != ((int)Naming.RoleID.ROLE_SYS))
                    {
                        if (_userProfile.CurrentUserRole.RoleID == (int)Naming.RoleID.ROLE_SELLER || _userProfile.CurrentUserRole.RoleID == (int)Naming.RoleID.ROLE_GOOGLETW)
                        {
                            if (this.rdbSearchItem.SelectedValue.Equals("1") | this.rdbSearchItem.SelectedValue.Equals("3") | this.rdbSearchItem.SelectedValue.Equals("5"))
                                dataSrc = dataSrc.Where(d => d.SellerID == _userProfile.CurrentUserRole.OrganizationCategory.CompanyID);
                            else
                                allowance = allowance.Where(d => d.InvoiceAllowanceSeller.SellerID == _userProfile.CurrentUserRole.OrganizationCategory.CompanyID);

                            if (_userProfile.CurrentUserRole.OrganizationCategory.CategoryID == (int)Naming.B2CCategoryID.Google台灣)
                            {
                                this.gvEntity.Columns[1].Visible = true;
                                //this.gvEntity.Columns[4].HeaderText = "發票號碼";
                            }
                            else if (_userProfile.CurrentUserRole.OrganizationCategory.CategoryID == (int)Naming.B2CCategoryID.商家發票自動配號)
                            {
                                this.gvEntity.Columns[1].Visible = true;
                                this.gvEntity.Columns[1].HeaderText = "客戶ID";
                            }
                        }
                        else
                        {
                            dataSrc = dataSrc.Where(i => i.InvoiceByHousehold.InvoiceUserCarrier.UID == _userProfile.UID);
                        }
                    }
                    else
                    {
                        this.gvEntity.Columns[1].Visible = true;
                        if (this.lblName.Text.Equals("GoogleID"))
                            this.gvEntity.Columns[1].HeaderText = "GoogleID";
                        else if (this.lblName.Text.Equals("客戶ID"))
                            this.gvEntity.Columns[1].HeaderText = "客戶ID";
                        else
                            this.gvEntity.Columns[1].HeaderText = "GoogleID/客戶ID";
                        
                    }

                    if (this.rdbSearchItem.SelectedValue.Equals("1"))
                    {
                        data = inqueryInvoiceItem(im, dataSrc, "1");
                    }
                    else if (this.rdbSearchItem.SelectedValue.Equals("2"))
                    {
                        data = inqueryAllowanceInvoiceItem(im, allowance, "2");
                    }
                    else if (this.rdbSearchItem.SelectedValue.Equals("3"))
                    {
                        data = inqueryInvoiceItem(im, dataSrc, "3");
                    }
                    else if (this.rdbSearchItem.SelectedValue.Equals("4"))
                    {
                        data = inqueryAllowanceInvoiceItem(im, allowance, "4");
                    }
                    else if (this.rdbSearchItem.SelectedValue.Equals("5"))
                    {
                        data = inqueryInvoiceItem(im, dataSrc, "5");
                    }

                    if (this.ddlReceiptNo.SelectedIndex>0)
                    {
                        data = data.Where(d => d.ReceiptNo.Equals(this.ddlReceiptNo.SelectedValue.Trim()));
                    }

                    if (!string.IsNullOrEmpty(this.txtGoogleID.Text))
                    {
                        data = data.Where(d => d.googleID.Equals(this.txtGoogleID.Text.Trim()));
                    }

                    if (!string.IsNullOrEmpty(this.DateFrom.TextBox.Text) & !string.IsNullOrEmpty(this.DateTo.TextBox.Text))
                    {
                        if (DateTime.Parse(this.DateFrom.TextBox.Text) > DateTime.Parse(this.DateTo.TextBox.Text))
                        {
                            DateTime tempDate;
                            tempDate = this.DateTo.DateTimeValue;
                            this.DateTo.DateTimeValue = this.DateFrom.DateTimeValue;
                            this.DateFrom.DateTimeValue = tempDate;
                        }
                        data = data.Where(d => d.InvoiceDate.Value.Date >= this.DateFrom.DateTimeValue.Date && d.InvoiceDate.Value.Date <= this.DateTo.DateTimeValue.Date);
                    }

                    if (!String.IsNullOrEmpty(this.invoiceNo.Text))
                    {
                        if (this.rdbSearchItem.SelectedValue.Equals("1") | this.rdbSearchItem.SelectedValue.Equals("3") | this.rdbSearchItem.SelectedValue.Equals("5"))
                        {
                            if (invoiceNo.Text.Trim().Length == 10)
                            {
                                String trackCode = invoiceNo.Text.Substring(0, 2);
                                String no = invoiceNo.Text.Substring(2);
                                data = data.Where(i => i.No == no && i.TrackCode == trackCode);
                            }
                            else
                            {
                                data = data.Where(i => i.No == invoiceNo.Text);
                            }
                        }
                        else if (this.rdbSearchItem.SelectedValue.Equals("2") | this.rdbSearchItem.SelectedValue.Equals("4"))
                        {
                            data = data.Where(i => i.No == invoiceNo.Text);
                        }
                    }
                    
                    if (data.Count() > 0)
                    {
                        if (this.gvEntity.AllowPaging)
                        {
                            int count = data.Count();
                            decimal total = data.Sum(d => d.TotalAmount) ?? 0;
                            this.lblError.Visible = false;
                            this.ResultTitle.Visible = true;
                            this.lblTotalSum.Text = total.ToString("0,0.00");
                            this.lblRowCount.Text = count.ToString();
                            this.gvEntity.PageIndex = PagingControl.GetCurrentPageIndex(this.gvEntity, 0);
                            //this.gvEntity.DataSource = data.OrderByDescending(d => d.InvoiceDate).ToList();
                            this.gvEntity.DataSource = data.OrderByDescending(d => d.InvoiceID).ToList();
                            this.gvEntity.DataBind();

                            PagingControl paging = (PagingControl)this.gvEntity.BottomPagerRow.Cells[0].FindControl("pagingIndex");
                            paging.RecordCount = count;
                            paging.CurrentPageIndex = this.gvEntity.PageIndex;
                        }
                        else
                        {
                            //this.gvEntity.DataSource = data.OrderByDescending(d => d.InvoiceDate).ToList();
                            this.gvEntity.DataSource = data.OrderByDescending(d => d.InvoiceID).ToList();
                            this.gvEntity.DataBind();
                        }
                        this.PrintingButton21.Visible = true;
                    }
                    else
                    {
                        this.lblError.Text = "查無資料!!";
                        this.lblError.Visible = true;
                        this.ResultTitle.Visible = false;
                        this.PrintingButton21.Visible = false;
                        this.lblTotalSum.Text = "0";
                        this.lblRowCount.Text = "0";
                        this.gvEntity.DataSource = null;
                        this.gvEntity.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    this.lblError.Text = "系統錯誤:" + ex.Message;
                }
            }
        }
        #endregion

        #region "Query Different Type Data"
        private IQueryable<dataType> inqueryInvoiceItem(InvoiceManager im, IQueryable<InvoiceItem> dataSrc, string type)
        {
            if (type.Equals("1"))
                dataSrc = dataSrc.Where(i => i.InvoiceCancellation == null);
            else if (type.Equals("3"))
                dataSrc = dataSrc.Where(i => i.InvoiceCancellation != null);
            else if (type.Equals("5"))
                dataSrc = dataSrc.Where(w => w.InvoiceWinningNumber != null && w.InvoiceCancellation == null);

            return from a in dataSrc
                   select new dataType
                   {
                       InvoiceID = a.InvoiceID,
                       InvoiceDate = a.InvoiceDate,
                       ChineseInvoiceDate = Utility.ValueValidity.ConvertChineseDateString(a.InvoiceDate),
                       CompanyName = a.Organization.CompanyName,
                       ReceiptNo = a.Organization.ReceiptNo,
                       TrackCode = a.TrackCode,
                       No = a.No,
                       SalesAmount = a.InvoiceAmountType.SalesAmount ?? 0,
                       TaxAmount = a.InvoiceAmountType.TaxAmount ?? 0,
                       TotalAmount = a.InvoiceAmountType.TotalAmount ?? 0,
                       check = im.GetTable<InvoiceWinningNumber>().Where(i => i.InvoiceID == a.InvoiceID).FirstOrDefault().UniformInvoiceWinningNumber.PrizeType,
                       DonateMark = a.DonateMark,
                       DonationName = im.GetTable<Organization>().Where(og => og.CompanyID == a.DonationID).FirstOrDefault().CompanyName,
                       BuyerReceiptNo = a.InvoiceBuyer.ReceiptNo.Equals("0000000000") ? "N/A" : a.InvoiceBuyer.ReceiptNo,
                       googleID = a.InvoiceBuyer.CustomerID,
                       memo = a.InvoiceDetails.Select(p=>p.ProductID),
                       SID=a.InvoicePurchaseOrder.OrderNo
                   };
        }

        private IQueryable<dataType> inqueryAllowanceInvoiceItem(InvoiceManager im, IQueryable<InvoiceAllowance> dataSrc, string type)
        {
            if (type.Equals("2"))
                dataSrc = dataSrc.Where(i => i.InvoiceAllowanceCancellation == null);
            else if (type.Equals("4"))
                dataSrc = dataSrc.Where(i => i.InvoiceAllowanceCancellation != null);

            return from a in dataSrc
                   select new dataType
                   {
                       InvoiceID = a.AllowanceID,
                       InvoiceDate = a.AllowanceDate,
                       ChineseInvoiceDate = Utility.ValueValidity.ConvertChineseDateString(a.AllowanceDate),
                       CompanyName = a.InvoiceAllowanceSeller.Organization.CompanyName,
                       ReceiptNo = a.InvoiceAllowanceSeller.Organization.ReceiptNo,
                       TrackCode = "",
                       No = a.AllowanceNumber,
                       SalesAmount = (a.TotalAmount - a.TaxAmount) ?? 0,
                       TaxAmount = a.TaxAmount ?? 0,
                       TotalAmount = a.TotalAmount ?? 0,
                       check = "N/A",
                       DonateMark = "0",
                       DonationName = "N/A",
                       BuyerReceiptNo = "N/A",
                       googleID = a.InvoiceItem.InvoiceBuyer.CustomerID,
                   };
        }
        #endregion

        public void RaisePostBackEvent(string eventArgument)
        {
            if (eventArgument.StartsWith("S:"))
            {
                this.PNewInvalidInvoicePreview1.setDetail = eventArgument.Substring(2).Trim();
                this.PNewInvalidInvoicePreview1.Popup.Show();
            }
        }

        protected string getRemark(object data)
        {
            if (data != null)
            {
                using (InvoiceManager im = new InvoiceManager())
                {
                    IEnumerable<int> items = ((IEnumerable<int>)data);
                    IEnumerable<String> dataList = im.GetTable<InvoiceProductItem>().Where(u => items.Contains((int)u.ProductID)).Select(u => u.Remark);
                    return String.Join("", dataList.ToArray());
                }
            }
            else
            {
                return "";
            }
        }
    }    
}

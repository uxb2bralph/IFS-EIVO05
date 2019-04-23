using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;
using Model.DataEntity;
using Model.Locale;
using Model.Security.MembershipManagement;
using Utility;
using System.Linq.Expressions;
using Business.Helper;

namespace eIVOGo.Module.Saler
{
    public partial class MaintainInvoiceList : System.Web.UI.UserControl
    {
        UserProfileMember _userProfile;

        public class ToDataInquiry
        {
            // Class members:
            // Property.
            public short qYear { get; set; }
            public short qPeriodNo { get; set; }
            public string qTrackCode { get; set; }
            public int qStartNo { get; set; }
            public int qEndNo { get; set; }
            public int qIntervalID { get; set; }
            public int NumberUsedCount { get; set; }  
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _userProfile = WebPageUtility.UserProfile;
            if (!this.IsPostBack)
            {
                initializeData();
                ShowResult(false);
                btnQuery.Visible = true;
                DIV3.Visible = false;
                TB2.Visible = false;
                DIV4.Visible = false;
                TB3.Visible = false;
                H1.Visible = false;
                REVStart.Text = THTitle.InnerText + "失敗!(發票號碼只能輸入字元0~9共8位數)";
                RFVStart.Text = THTitle.InnerText + "失敗!(發票號碼不能為空白)";
                REVEnd.Text = THTitle.InnerText + "失敗!(發票號碼只能輸入字元0~9共8位數)";
                RFVEnd.Text = THTitle.InnerText + "失敗!(發票號碼不能為空白)";
            }
            PagingControl1.PageSize = 10;
            //btnPrint.PrintControls.Add(border_gray);       
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //PagingControl1.PageIndexChanged += new Uxnet.Web.Module.Common.PageChangedEventHandler(PagingControl1_PageIndexChanged);
            //btnPrint.BeforeClick += new EventHandler(btnPrint_BeforeClick);
        }

        private void initializeData()
        {
            //this.PagingControl1.Visible = false;

            for (int i = -5; i < 6; i++)
            {
                SelsectYear.Items.Add((DateTime.Now.Year - (1911-i)).ToString("000")+"年");
                SelsectYear.Items[i + 5].Value = (DateTime.Now.Year + i).ToString();
                SelsectCodeYear.Items.Add((DateTime.Now.Year - (1911 - i)).ToString("000") + "年");
                SelsectCodeYear.Items[i + 5].Value = (DateTime.Now.Year + i).ToString();
            }
            SelsectYear.SelectedIndex = 5;
            for (int i = 1; i < 12; i = i+2)
            {
                SelsectPeriod.Items.Add(i.ToString("00") + "~" + (i+1).ToString("00")+"月");
                SelsectCodePeriod.Items.Add(i.ToString("00") + "~" + (i + 1).ToString("00") + "月");
            }
            SelsectPeriod.SelectedIndex = 0;
        }

        //protected void ShowEdit(string strTitle)
        //{
        //    border_gray.Visible = false;
        //    Div1.Visible = false;            
        //    QImg1.Visible = false;
        //    EditTitle.InnerText = strTitle;
        //}

        protected void ShowResult(bool bolShow)
        {
            DIV1.Visible = true;
            H1.Visible = true;
            lblEx.Visible = false;
            DIV4.Visible = false;
            TB3.Visible = false;
            PagingControl1.Visible = bolShow;
            DIV3.Visible = true;
            DIV2.Visible = true;
            DIV3_1.Visible = bolShow;
            TB2.Visible = true;
            //TB2_1.Visible = true;
            btnAdd.Visible = true;
            btnQuery.Visible = true;
            if (bolShow)
                NoData.Visible = false;
            else
                NoData.Visible = true;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            bindData(true);
        }

        private void bindData(bool bPaging)
        {
            var mgr = dsInv.CreateDataManager();
            Expression<Func<InvoiceNoInterval, bool>> queryExpr = w => true;
            if (_userProfile.CurrentUserRole.OrganizationCategory.CompanyID >0)
            {
                queryExpr = queryExpr.And(w => w.SellerID == _userProfile.CurrentUserRole.OrganizationCategory.CompanyID);
                if (SelsectYear.SelectedValue != "" && SelsectPeriod.SelectedValue != "")
                {
                    queryExpr = queryExpr.And(w => (w.InvoiceTrackCodeAssignment.InvoiceTrackCode.Year == short.Parse(SelsectYear.SelectedValue)
                        && w.InvoiceTrackCodeAssignment.InvoiceTrackCode.PeriodNo == SelsectPeriod.SelectedIndex + 1));
                }
            }
            else
            {
                return;
            }

            IQueryable<InvoiceNoInterval> invData = mgr.GetTable<InvoiceNoInterval>().Where(queryExpr);
            var items = invData.Select(w => new ToDataInquiry{
                qYear =  w.InvoiceTrackCodeAssignment.InvoiceTrackCode.Year, 
                qPeriodNo = w.InvoiceTrackCodeAssignment.InvoiceTrackCode.PeriodNo,
                qTrackCode = w.InvoiceTrackCodeAssignment.InvoiceTrackCode.TrackCode,
                qStartNo = w.StartNo,
                qEndNo = w.EndNo,
                qIntervalID = w.IntervalID,
                NumberUsedCount = w.InvoiceNoAssignments.Count()
            });

            if (items.Count() > 0)
            {
                this.PagingControl1.RecordCount = items.Count();
                this.PagingControl1.Visible = true;

                rpList.DataSource = bPaging ? items.Skip(PagingControl1.CurrentPageIndex * PagingControl1.PageSize).Take(PagingControl1.PageSize) : items;
                rpList.DataBind();                
                ShowResult(true);
            }
            else
            {
                //litTotal.Text = "0";
                //litDonate.Text = "0";
                this.PagingControl1.Visible = false;
                ShowResult(false);
            }

        }

        protected  string PeriodNo2String(short PeriodNo)
        {
            string sTmp = (PeriodNo * 2 - 1).ToString("00") + "~" + (PeriodNo * 2 ).ToString("00") + "月";
            return sTmp;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            H1.Visible = false;
            DIV1.Visible = false;
            DIV2.Visible = false;
            DIV3.Visible = false;
            //TB2_1.Visible = false;            
            DIV4.Visible = true;
            TB3.Visible = true;
            SelsectCodeYear.Enabled = true;
            SelsectCodePeriod.Enabled = true;
            SelsectCodeTrack.Enabled = true;
            SelsectCodeYear.SelectedIndex = SelsectYear.SelectedIndex;
            btnQuery.Visible = false;
            txtInvNoStar.Text = "";
            txtInvNoEnd.Text = "";
            int mPeriod;
            if ((DateTime.Now.Month % 2) > 0)
                mPeriod = (DateTime.Now.Month / 2) + 1;
            else
                mPeriod = DateTime.Now.Month / 2;

            SelsectCodePeriod.SelectedIndex = mPeriod-1;

            SelsectCodeTrackAddItems();
            
            THTitle.InnerText = "新增發票號碼";
            lblEx.Visible = false;
        }

        protected void SelsectCodeTrackAddItems()
        {
            SelsectCodeTrack.Items.Clear();
            lblExCode.Visible = false;
            var mgr = dsInv.CreateDataManager();
            Expression<Func<InvoiceTrackCode, bool>> queryExpr = w => true;
            queryExpr = queryExpr.And(w =>
                    (w.Year == short.Parse(SelsectCodeYear.SelectedValue) &&
                    (w.PeriodNo == SelsectCodePeriod.SelectedIndex + 1)));
            IQueryable<InvoiceTrackCode> invData = mgr.GetTable<InvoiceTrackCode>().Where(queryExpr);
            if (invData.Count() > 0)
                SelsectCodeTrack.Items.AddRange(invData.Select(w => new ListItem(w.TrackCode)).ToArray());
            else
                lblExCode.Visible = true;
        }
      
        protected void btnAddCode_Click(object sender, EventArgs e)
        {
            bool bolDo = true;
            string errString = "";          
            //檢查是否有非數字字元
            if (CharCheck(txtInvNoStar.Text) == false || CharCheck(txtInvNoEnd.Text) == false)
            {
                errString = "(不可以有非數字字元)";  
                bolDo = false;
            }
            //檢查號碼大小順序與差距(50)是否相符
            if (bolDo)
            {
                if (RangeCheck(txtInvNoStar.Text, txtInvNoEnd.Text) == false)
                {
                    errString = "(不符號碼大小順序與差距為50之原則)";  
                    bolDo = false;
                }

            }
            //檢查字軌是否可以使用
            if (bolDo)
            {
                if (RangeCheck(txtInvNoStar.Text, txtInvNoEnd.Text) == false)
                {
                    errString = "(不符號碼大小順序與差距為50之原則)";
                    bolDo = false;
                }

            }
            if (bolDo == false)
            {
                lblEx.Text = THTitle.InnerText + "失敗!!" + errString;
                lblEx.Visible = true;
                return;
            }
            else //當條件符合時
            {
                SelsectYear.SelectedIndex = SelsectCodeYear.SelectedIndex;
                if (THTitle.InnerText == "新增發票號碼") //新增資料                
                {
                    #region 新增發票號碼
                    var mgr = dsInv.CreateDataManager();
                    IQueryable<InvoiceTrackCode> invDataTrackCode;
                    Expression<Func<InvoiceTrackCode, bool>> queryExpr1 = w => true;
                    queryExpr1 = queryExpr1.And(w =>
                    (w.Year == short.Parse(SelsectCodeYear.SelectedValue) &&
                    (w.PeriodNo == SelsectCodePeriod.SelectedIndex + 1) &&
                    (w.TrackCode == SelsectCodeTrack.Text)));
                    invDataTrackCode = mgr.GetTable<InvoiceTrackCode>().Where(queryExpr1);

                    if (invDataTrackCode.Count() > 0)
                    {
                        IQueryable<InvoiceTrackCodeAssignment> invDataTrackCodeAssignment;
                        Expression<Func<InvoiceTrackCodeAssignment, bool>> queryExpr2 = w => true;
                        queryExpr2 = queryExpr2.And(w =>
                        (w.TrackID == invDataTrackCode.FirstOrDefault().TrackID) &&
                        (w.SellerID == _userProfile.CurrentUserRole.OrganizationCategory.CompanyID));
                        invDataTrackCodeAssignment = mgr.GetTable<InvoiceTrackCodeAssignment>().Where(queryExpr2);
                        //若無TrackID與SellerID之資料則新增InvoiceNoAssignment
                        if (invDataTrackCodeAssignment.Count() < 1)
                        {
                            mgr.GetTable<InvoiceTrackCodeAssignment>().InsertOnSubmit(new InvoiceTrackCodeAssignment
                            {
                                TrackID = invDataTrackCode.FirstOrDefault().TrackID,
                                SellerID = _userProfile.CurrentUserRole.OrganizationCategory.CompanyID
                            });
                            mgr.SubmitChanges();
                        }
                        IQueryable<InvoiceNoInterval> invDataNoInterval;
                        //檢查號碼落點未被使用
                        Expression<Func<InvoiceNoInterval, bool>> queryExpr3 = w => true;
                        //queryExpr3 = queryExpr3.And(w =>
                        //(w.TrackID == invDataTrackCode.FirstOrDefault().TrackID) &&
                        //(w.SellerID == _userProfile.CurrentUserRole.OrganizationCategory.CompanyID) &&
                        //(w.InvoiceTrackCodeAssignment.InvoiceTrackCode.TrackCode == SelsectCodeTrack.Text) &&
                        //((((w.StartNo <= (int.Parse(txtInvNoStar.Text)) && w.EndNo >= (int.Parse(txtInvNoStar.Text))) ||
                        //(w.StartNo <= (int.Parse(txtInvNoEnd.Text)) && w.EndNo >= (int.Parse(txtInvNoEnd.Text))))) ||
                        //(w.StartNo >= (int.Parse(txtInvNoStar.Text)) && w.EndNo <= (int.Parse(txtInvNoEnd.Text))))
                        //);
                        queryExpr3 = queryExpr3.And(w =>
                        (w.TrackID == invDataTrackCode.FirstOrDefault().TrackID) &&                        
                        (w.InvoiceTrackCodeAssignment.InvoiceTrackCode.TrackCode == SelsectCodeTrack.Text) &&
                        ((((w.StartNo <= (int.Parse(txtInvNoStar.Text)) && w.EndNo >= (int.Parse(txtInvNoStar.Text))) ||
                        (w.StartNo <= (int.Parse(txtInvNoEnd.Text)) && w.EndNo >= (int.Parse(txtInvNoEnd.Text))))) ||
                        (w.StartNo >= (int.Parse(txtInvNoStar.Text)) && w.EndNo <= (int.Parse(txtInvNoEnd.Text))))
                        );
                        invDataNoInterval = mgr.GetTable<InvoiceNoInterval>().Where(queryExpr3);
                        if (invDataNoInterval.Count() < 1)
                        {                        
                            if (!CheckNumber(invDataTrackCode.FirstOrDefault().TrackID, txtInvNoEnd.Text))
                            {
                                lblEx.Text = THTitle.InnerText + "失敗!!" + "違反序時序號原則該區段無法新增";
                                lblEx.Visible = true;
                                return;
                            }
                            //新增InvoiceNoInterval
                            mgr.GetTable<InvoiceNoInterval>().InsertOnSubmit(new InvoiceNoInterval
                            {
                                TrackID = invDataTrackCode.FirstOrDefault().TrackID,
                                SellerID = _userProfile.CurrentUserRole.OrganizationCategory.CompanyID,
                                StartNo = int.Parse(txtInvNoStar.Text),
                                EndNo = int.Parse(txtInvNoEnd.Text)
                            });
                            mgr.SubmitChanges();
                        }
                        else
                        {
                            errString = "(該區間之號碼已經被使用)";
                            bolDo = false;
                        }
                        if (bolDo)
                        {
                            SelsectYear.SelectedIndex = SelsectCodeYear.SelectedIndex;
                            SelsectPeriod.SelectedIndex = SelsectCodePeriod.SelectedIndex;
                            bindData(true);
                        }
                        else
                        {
                            lblEx.Text = THTitle.InnerText + "失敗!!" + errString;
                            lblEx.Visible = true;
                        }
                    }
                    #endregion
                }
                else
                {
                    #region 修改發票號碼
                    var mgr = dsInv.CreateDataManager();
                    //IQueryable<InvoiceNoInterval> invDataTrackCode;
                    Expression<Func<InvoiceNoInterval, bool>> queryExpr = w => true;
                    queryExpr = queryExpr.And(w => w.IntervalID == int.Parse(lblIntervalID.Text));
                    IQueryable<InvoiceNoInterval> invData = mgr.GetTable<InvoiceNoInterval>().Where(queryExpr);
                    if (invData.Count() > 0)
                    {
                        //var table = mgr.GetTable<InvoiceNoInterval>();
                        var item = invData.Where(queryExpr).FirstOrDefault();
                        item.StartNo = int.Parse(txtInvNoStar.Text);
                        item.EndNo = int.Parse(txtInvNoEnd.Text);
                        mgr.SubmitChanges();

                        SelsectYear.SelectedIndex = SelsectCodeYear.SelectedIndex;
                        SelsectPeriod.SelectedIndex = SelsectCodePeriod.SelectedIndex;
                        bindData(true);
                    }
                    #endregion
                }                
            }          
        }

        #region 檢查序時序號問題
        private bool CheckNumber(int intTrackID,string strEndNo)
        {
            var mgr = dsInv.CreateDataManager();
            Expression<Func<InvoiceNoInterval, bool>> queryExpr1 = w => true;
            queryExpr1 = queryExpr1.And(w => w.TrackID == intTrackID);
            IQueryable<InvoiceNoInterval> invData = mgr.GetTable<InvoiceNoInterval>().Where(queryExpr1);
            if (invData.Count() > 0)
            {
                Expression<Func<InvoiceNoAssignment, bool>> queryExpr2 = w => true;
                foreach (var tmpdata in invData)
                {
                    queryExpr2 = queryExpr2.And(w => w.IntervalID == tmpdata.IntervalID);
                    queryExpr2 = queryExpr2.And(w => w.InvoiceNo >= int.Parse(strEndNo));
                    IQueryable<InvoiceNoAssignment> invDataTemp = mgr.GetTable<InvoiceNoAssignment>().Where(queryExpr2);
                    if (invDataTemp.Count() > 0)
                        return false;
                }
                return true;
            }
            else
                return true;
        }
        #endregion

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtInvNoStar.Text = "";
            txtInvNoEnd.Text = "";
            lblEx.Visible = false;
        }

        protected bool CharCheck(string theStr)
        {
            bool bolAns = true;
            char[] theChar;
            theChar = theStr.ToCharArray();

            if (theChar.Length == 8)
            {
                for (int i = 0; i < theChar.Length; i++)
                {
                    if (theChar[i] < '0' || theChar[i] > '9')
                    {
                        bolAns = false;
                        break;
                    }
                }
            }
            else
                bolAns = false;

            return bolAns;
        }

        protected bool RangeCheck(string strStarNo, string strEndNo)
        {
            bool bolAns = true;
            int intStarNo = int.Parse(strStarNo);
            int intEndNo = int.Parse(strEndNo);
            if (intStarNo >= intEndNo)
                bolAns = false;
            if (((intEndNo-intStarNo+1)%50) > 0)
                bolAns = false;
            return bolAns;
        }


        protected void SelsectCodeYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelsectCodeTrackAddItems();
        }

        protected void SelsectCodePeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelsectCodeTrackAddItems();
        }

        protected void rpList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var mgr = dsInv.CreateDataManager();
            Expression<Func<InvoiceNoInterval, bool>> queryExpr = w => true;
            queryExpr = queryExpr.And(w => w.IntervalID == int.Parse(e.CommandArgument.ToString()));
            var table = mgr.GetTable<InvoiceNoInterval>();
            var item = table.Where(queryExpr).FirstOrDefault();

            if (e.CommandName == "Update")
            {
                H1.Visible = false;
                DIV1.Visible = false;
                DIV2.Visible = false;
                btnQuery.Visible = false;
                DIV3.Visible = false;
                DIV4.Visible = true;
                TB3.Visible = true;
                for (int i = 0; i < SelsectCodeYear.Items.Count; i++)
                {
                    if (item.InvoiceTrackCodeAssignment.InvoiceTrackCode.Year  == short.Parse(SelsectCodeYear.Items[i].Value))
                    {
                        SelsectCodeYear.SelectedIndex = i;
                        break;
                    }
                }
                SelsectCodePeriod.SelectedIndex = item.InvoiceTrackCodeAssignment.InvoiceTrackCode.PeriodNo - 1;

                SelsectCodeTrack.Items.Clear();
                SelsectCodeTrack.Items.Add(item.InvoiceTrackCodeAssignment.InvoiceTrackCode.TrackCode);

                SelsectCodeYear.Enabled = false;
                SelsectCodePeriod.Enabled = false;
                SelsectCodeTrack.Enabled = false;

                txtInvNoStar.Text = item.StartNo.ToString("00000000");
                txtInvNoEnd.Text = item.EndNo.ToString("00000000");
                lblIntervalID.Text = e.CommandArgument.ToString();
                lblEx.Visible = false;
                lblExCode.Visible = false;
                THTitle.InnerText = "修改發票號碼";
            }
            else if (e.CommandName == "Delete")
            {
                if (item != null)
                {
                    table.DeleteOnSubmit(item);
                    mgr.SubmitChanges();
                    bindData(true);
                }
            }
        }       

    }
}
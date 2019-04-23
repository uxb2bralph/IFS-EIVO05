using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Model.DataEntity;
using Model.Locale;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Utility;
using NPOI.HSSF.Util;
using System.Drawing;

namespace eIVOGo.Module.Inquiry
{
    public partial class DonateReport : System.Web.UI.UserControl
    {
        private List<_QueryItem> _items;
        private bool bPaging = true;
        private DonatedInvoiceList reportItem;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                initializeData();
                border_gray.Visible = false;
                Div2.Visible = false;
            }

            reportItem = rbReport.SelectedIndex == 0 ? (DonatedInvoiceList)this.LoadControl("DonatedInvoiceList.ascx") : (DonatedInvoiceList)this.LoadControl("DonatedWinningInvoiceList.ascx");
            phReport.Controls.Add(reportItem);
            ((UserControl)reportItem).InitializeAsUserControl(this.Page);

            btnPrint.PrintControls.Add(reportItem);
        }

        private void initializeData()
        {
            var mgr = dsInv.CreateDataManager();
            var orgItems = mgr.GetTable<Organization>();
            SellerID.Items.AddRange(orgItems.Where(
                o => o.OrganizationCategory.Any(
                    c => c.CategoryID == (int)Naming.CategoryID.COMP_E_INVOICE_B2C_SELLER))
                    .Select(o =>
                        new ListItem(String.Format("{0} {1}", o.ReceiptNo, o.CompanyName), o.CompanyID.ToString())).ToArray());
            AgencyID.Items.AddRange(mgr.GetTable<WelfareAgency>().Select(w => new ListItem(String.Format("{0} {1}", w.Organization.ReceiptNo, w.Organization.CompanyName), w.AgencyID.ToString())).ToArray());
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            btnQuery.CommandArgument = "Query";
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.PreRender += new EventHandler(DonateReport_PreRender);
            btnPrint.BeforeClick += new EventHandler(btnPrint_BeforeClick);
        }


        void btnPrint_BeforeClick(object sender, EventArgs e)
        {
            bPaging = false;
            DonateReport_PreRender(sender, e);
        }

        void DonateReport_PreRender(object sender, EventArgs e)
        {
            if (btnQuery.CommandArgument == "Query")
            {
                doQuery();
                if (pagingList.RecordCount > 0)
                {
                    reportItem.DataItems = bPaging ? _items.Skip(pagingList.PageSize * pagingList.CurrentPageIndex).Take(pagingList.PageSize) : _items;
                    pagingList.RecordCount = _items.Count;
                }
            }
        }

        private void doQuery()
        {
            var mgr = dsInv.CreateDataManager();
            IQueryable<WelfareAgency> agencies = AgencyID.SelectedIndex > 1 ?
                mgr.GetTable<WelfareAgency>().Where(w => w.AgencyID == int.Parse(AgencyID.SelectedValue)) :
                mgr.GetTable<WelfareAgency>();

            Expression<Func<InvoiceItem, bool>> invQuery = i => true;
            if (InvDateFrom.HasValue)
            {
                invQuery = invQuery.And(i => i.InvoiceDate >= InvDateFrom.DateTimeValue);
            }
            if (InvDateTo.HasValue)
            {
                invQuery = invQuery.And(i => i.InvoiceDate >= InvDateTo.DateTimeValue.AddDays(1));
            }
            if (SellerID.SelectedIndex > 0)
            {
                invQuery = invQuery.And(i => i.SellerID == int.Parse(SellerID.SelectedValue));
            }
            //minyu-0701
            invQuery = invQuery.And(i => i.InvoiceCancellation == null);

            var invItems = mgr.GetTable<InvoiceItem>().Where(invQuery);
            if (invItems.Count() > 0)
            {
                int dataCount = 0;
                if (AgencyID.SelectedIndex < 2)
                {
                    _items = new List<_QueryItem>();                    
                    var anonymousItems = invItems.Where(i => i.DonateMark == "1" && !i.DonationID.HasValue);
                    if (anonymousItems.Count() > 0)
                    {
                        foreach (var inv in anonymousItems)
                        {
                            dataCount++;
                            _items.Add(new _QueryItem
                            {
                                Agency = null,
                                InvoiceID = inv.InvoiceID,
                                Winnable = inv.InvoiceWinningNumber != null ? "是" : "否",
                                InvoiceNo = String.Format("{0}{1}***", inv.TrackCode, inv.No.Substring(0, inv.No.Length - 3))
                            });
                        }
                        _items.Add(new _QueryItem
                        {
                            Agency = null,
                            InvoiceCount = anonymousItems.Count(),
                            WinningInvoiceCount = anonymousItems.Count(i => i.InvoiceWinningNumber != null)
                        });
                        dataCount++;
                    }
                }
                if (AgencyID.SelectedIndex != 1)
                {
                    foreach (var a in agencies.Select(w => w.Organization).Where(o => o.DonatedInvoiceItems.Join(invItems, d => d.InvoiceID, i => i.InvoiceID, (d, i) => d.InvoiceID).Count() > 0))
                    {
                        foreach (var i in a.DonatedInvoiceItems)
                        {
                            dataCount++;
                            _items.Add(new _QueryItem
                            {
                                Agency = a,
                                InvoiceID = i.InvoiceID,
                                Winnable = i.InvoiceWinningNumber != null ? "是" : "否",
                                InvoiceNo = String.Format("{0}{1}***", i.TrackCode, i.No.Substring(0, i.No.Length - 3))
                            });
                        }
                        _items.Add(new _QueryItem
                        {
                            Agency = a,
                            InvoiceCount = a.DonatedInvoiceItems.Count,
                            WinningInvoiceCount = a.DonatedInvoiceItems.Count(i => i.InvoiceWinningNumber != null)
                        });
                        dataCount++;
                    }
                }
                //DisplayData(true);    //min-yu del 2011-05-26

                if (dataCount > 0)     //min-yu add 2011-05-26
                    DisplayData(true);
                else
                    DisplayData(false);
                pagingList.RecordCount = dataCount;
            }
            else
            {
                DisplayData(false);
                pagingList.RecordCount = 0;
            }

        }

        private void DisplayData(bool showYes)
        {
            if (showYes)
            {
                Div1.Visible = true;
                Div2.Visible = true;
                border_gray.Visible = true;
                table01.Visible = true;
                NoData.Visible = false;
            }
            else
            {
                Div1.Visible = false;
                Div2.Visible = true;
                border_gray.Visible = true;
                table01.Visible = false;
                NoData.Visible = true;
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            doQuery();
            OutExcelFile();
        }

        private void OutExcelFile()
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            try
            {
                // 新增試算表。
                Sheet sheet = workbook.CreateSheet("My Sheet");

                // 建立儲存格樣式。
                NPOI.SS.UserModel.Font font1 = workbook.CreateFont();
                //font1.Color = HSSFColor.BLUE.index;
                //font1.IsItalic = false;
                //font1.Underline = (byte)FontUnderlineType.NONE;
                font1.FontHeightInPoints = 12;

                //NPOI.SS.UserModel.Font font2 = workbook.CreateFont();
                //font2.Color = HSSFColor.BLACK.index;
                //font2.IsItalic = false;
                //font2.Underline = (byte)FontUnderlineType.NONE;
                //font2.FontHeightInPoints = 12;

                CellStyle style1 = workbook.CreateCellStyle();
                style1.SetFont(font1);
                style1.BorderBottom = CellBorderType.THIN;
                //style1.BottomBorderColor = HSSFColor.BLACK.index;
                style1.BorderLeft = CellBorderType.THIN;
                //style1.LeftBorderColor = HSSFColor.GREEN.index;
                style1.BorderRight = CellBorderType.THIN;
                //style1.RightBorderColor = HSSFColor.BLUE.index;
                style1.BorderTop = CellBorderType.THIN;
                //style1.TopBorderColor = HSSFColor.ORANGE.index;
                //style1.f

                //CellStyle style2 = workbook.CreateCellStyle();
                //style2.SetFont(font2);
                //style2.BorderBottom = CellBorderType.THIN;
                //style2.BottomBorderColor = HSSFColor.BLACK.index;
                //style2.BorderLeft = CellBorderType.DASH_DOT_DOT;
                //style2.LeftBorderColor = HSSFColor.GREEN.index;
                //style2.BorderRight = CellBorderType.HAIR;
                //style2.RightBorderColor = HSSFColor.BLUE.index;
                //style2.BorderTop = CellBorderType.THIN;
                //style2.TopBorderColor = HSSFColor.ORANGE.index;

                //CellStyle style3 = workbook.CreateCellStyle();
                //style3.SetFont(font2);
                //style3.BorderBottom = CellBorderType.THIN;
                //style3.BottomBorderColor = HSSFColor.BLACK.index;
                //style3.BorderLeft = CellBorderType.DASH_DOT_DOT;
                //style3.LeftBorderColor = HSSFColor.GREEN.index;
                //style3.BorderRight = CellBorderType.HAIR;
                //style3.RightBorderColor = HSSFColor.BLUE.index;
                //style3.BorderTop = CellBorderType.THIN;
                //style3.TopBorderColor = HSSFColor.ORANGE.index;
                //style3.FillBackgroundColor = HSSFColor.YELLOW.index;
                
                int i = 1;
                if (rbReport.SelectedIndex == 1)                
                    SetRow(sheet, style1, 0, "社福機構統編 ", "社福名稱 ", "發票號碼", "是否中獎");                   
                else                
                    SetRow(sheet, style1, 0, "社福機構統編 ", "社福名稱 ", "發票號碼");
                


                if (rbReport.SelectedIndex == 1)
                {
                    foreach (var row in _items)
                    {
                        if (row.InvoiceID.HasValue)
                            SetRow(sheet, style1, i, row.Agency.ReceiptNo, row.Agency.CompanyName, row.InvoiceNo, row.Winnable);
                        else
                            SetRow(sheet, style1, i, "", "", String.Format("總計 發票張數:{0:##,###,###,###,###}", row.InvoiceCount), String.Format("中獎總張數:{0:##,###,###,###,##0}", row.WinningInvoiceCount));
                        i++;
                    }
                }
                else
                {
                    foreach (var row in _items)
                    {
                        if (row.InvoiceID.HasValue)
                            SetRow(sheet, style1, i, row.Agency.ReceiptNo, row.Agency.CompanyName, row.InvoiceNo);
                        else
                            SetRow(sheet, style1, i, "", "", String.Format("總計 發票張數:{0:##,###,###,###,###}", row.InvoiceCount));
                        i++;
                    }
                }
                sheet.AutoSizeColumn(0);
                sheet.AutoSizeColumn(1);
                sheet.AutoSizeColumn(2);
                sheet.AutoSizeColumn(3);

                workbook.Write(ms);
                Page.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=Report.xls"));
                Page.Response.BinaryWrite(ms.ToArray());
                //Page.Response.End();             
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                workbook = null;
                ms.Close();
                ms.Dispose();
            }
        }

        private void SetRow(Sheet sheet, CellStyle xlStyle, int intRow, params string[] values)
        {
            NPOI.SS.UserModel.Row hsRow = (HSSFRow)sheet.CreateRow(intRow);
            for (int x = 0; x < values.Length; x++)
            {
                Cell cell = hsRow.CreateCell(x);
                cell.SetCellValue(values[x]);
                cell.CellStyle = xlStyle;
            }
        }
    }

    public class _QueryItem
    {
        public Organization Agency;
        public int? InvoiceID;
        public String InvoiceNo;
        public String Winnable;
        public int? InvoiceCount;
        public int? WinningInvoiceCount;
    }
}
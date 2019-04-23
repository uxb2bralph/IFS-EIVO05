
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.DataEntity;
using Model.Security.MembershipManagement;

using System.Collections;
using System.Reflection;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;


namespace eIVOGo.Module.Inquiry
{
    //public sealed class HttpResponse;
    public partial class DonateReportList : System.Web.UI.UserControl
    {
        public class ToDataInquiry
        {
            // Class members:
            // Property.
            public string No { get; set; }
            public string SellerID { get; set; }
            public string DonationID { get; set; }
            public string AgencyCode { get; set; }
            public string WAName { get; set; }
            public string DonateMark { get; set; }
            public string InvoCount { get; set; }
            public string WinningType { get; set; }
            public string InvoiceDate { get; set; }
        }

        IQueryable<ToDataInquiry> resultExcel ;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                initializeData();
                ShowResult(false);
            }
            PagingControl1.PageSize = 10;
            btnPrint.PrintControls.Add(border_gray);
        }

        protected void ShowResult(bool bolShow)
        {
            PagingControl1.Visible = bolShow;
            btnExcel.Visible = bolShow;
            btnPrint.Visible = bolShow;
            border_gray.Visible = bolShow;
            Div1.Visible = bolShow;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            PagingControl1.PageIndexChanged += new Uxnet.Web.Module.Common.PageChangedEventHandler(PagingControl1_PageIndexChanged);
            btnPrint.BeforeClick += new EventHandler(btnPrint_BeforeClick);
            //Uxnet.Web.Module.Common.PrintingButton2 btnPrint
        }

        void btnPrint_BeforeClick(object sender, EventArgs e)
        {
            bindData(false);
        }

        void PagingControl1_PageIndexChanged(object source, Uxnet.Web.Module.Common.PageChangedEventArgs e)
        {
            bindData(true);
        }


        private void initializeData()
        {

            var mgr = dsInv.CreateDataManager();
            var items1 = mgr.GetTable<Organization>().Select
                (o => new { CompanyData = o.ReceiptNo + " " + o.CompanyName, o.CompanyID, o.OrganizationCategory }).Where
                (p => p.OrganizationCategory.Any(c => c.CategoryID == 2));

            DropDownList1.DataSource = items1;
            DropDownList1.DataTextField = "CompanyData";
            DropDownList1.DataValueField = "CompanyID";
            DropDownList1.DataBind();
            DropDownList1.Items.Add("ALL");
            DropDownList1.SelectedValue = "ALL";

            var items2 = mgr.GetTable<Organization>().Select
                (o => new { CompanyData = o.ReceiptNo + " " + o.CompanyName, o.CompanyID, o.WelfareAgency.AgencyID }).Where
                (p => p.CompanyID == p.AgencyID);
            DropDownList2.DataSource = items2;
            DropDownList2.DataTextField = "CompanyData";
            DropDownList2.DataValueField = "CompanyID";
            DropDownList2.DataBind();
            DropDownList2.Items.Add("ALL");
            DropDownList2.SelectedValue = "ALL";
            this.PagingControl1.Visible = false;
        }


        protected void btnQuery_Click(object sender, EventArgs e)
        {
            bindData(true);
        }

        private void bindData(bool bPaging)
        {
            DateTime dtStar = DateTime.Now;
            DateTime dtEnd = DateTime.Now;
            bool selectDay = true;
            if (CalendarInputDatePicker1.TextBox.Text == "" || CalendarInputDatePicker2.TextBox.Text == "")
            {
                selectDay = false;
            }
            else
            {
                if (DateTime.Parse(CalendarInputDatePicker1.TextBox.Text) <= DateTime.Parse(CalendarInputDatePicker2.TextBox.Text))
                {
                    dtStar = DateTime.Parse(CalendarInputDatePicker1.TextBox.Text);
                    dtStar = dtStar.AddYears(1911);
                    dtEnd = DateTime.Parse(CalendarInputDatePicker2.TextBox.Text);
                    dtEnd = dtEnd.AddYears(1911);
                }
                else
                {
                    dtStar = DateTime.Parse(CalendarInputDatePicker2.TextBox.Text);
                    dtStar = dtStar.AddYears(1911);
                    dtEnd = DateTime.Parse(CalendarInputDatePicker1.TextBox.Text);
                    dtEnd = dtEnd.AddYears(1911);
                }
            }

            var DonationList = dsInv.CreateDataManager();
            var mgr = dsInv.CreateDataManager();
            IQueryable<viewDomationListCountGroup> invDataTmp;
            IQueryable<viewDomationListCountGroup> invData;
            if (DropDownList1.SelectedValue == "ALL")
                invData = mgr.GetTable<viewDomationListCountGroup>().Where(w => w.SellerID != null);
            else
                invData = mgr.GetTable<viewDomationListCountGroup>().Where(w => w.SellerID == int.Parse(DropDownList1.SelectedValue));

            invData = invData.Where(w => w.DonateMark == "1");

            if (DropDownList2.SelectedValue == "ALL")
                invData = invData.Where(w => w.DonationID != null);
            else
                invData = invData.Where(w => w.DonationID == int.Parse(DropDownList2.SelectedValue));

            trWinYesNo.Visible = false;

            if(selectDay)
                invData = invData.Where(w => (w.InvoiceDate >= dtStar && w.InvoiceDate < dtEnd));

            int DonationCount = invData.Where(w => w.WinningType != null).Count();
            //if (RadioButton2.Checked)
            //{
            //    invData = invData.Where(w => w.WinningType != null);
            //    trWinYesNo.Visible = true;
            //}
            var TmpItems2 = from invData0 in invData
                            select new ToDataInquiry
                            {
                                No = invData0.No,
                                SellerID = invData0.SellerID.ToString(),
                                DonationID = invData0.DonationID.ToString(),
                                AgencyCode = invData0.AgencyCode,
                                WAName = invData0.WAName,
                                DonateMark = invData0.DonateMark,
                                InvoCount = invData0.InvoCount,
                                WinningType = invData0.WinningType != null ? "是" : "否"
                            };
            if (selectDay)
                invDataTmp = mgr.GetTable<viewDomationListCountGroup>().Where(w => (w.InvoiceDate >= dtStar && w.InvoiceDate < dtEnd));
            else
                invDataTmp = mgr.GetTable<viewDomationListCountGroup>();

            if (RadioButton2.Checked)
                trWinYesNo.Visible = true;
            
            var TmpItems0 = invDataTmp.GroupBy(w => w.AgencyCode);
            var TmpItems1 = from DomationListCountGroupTemp in TmpItems0
                            select new ToDataInquiry
                            {
                                No = "",
                                SellerID = "",
                                DonationID = "",
                                AgencyCode = DomationListCountGroupTemp.Key + "tmp",
                                WAName = "",
                                DonateMark = DomationListCountGroupTemp.First().DonateMark,
                                InvoCount = DomationListCountGroupTemp.Count().ToString(),
                                WinningType = null
                            };
            var result = TmpItems1.Concat(TmpItems2).OrderBy(i => i.AgencyCode);
            resultExcel = result;

            litDonate.Visible = RadioButton2.Checked;
            if (TmpItems2.Count() > 0)
            {
                this.PagingControl1.RecordCount = result.Count();
                this.PagingControl1.Visible = true;
                //換頁與列印
                rpList.DataSource = bPaging ? result.Skip(PagingControl1.CurrentPageIndex * PagingControl1.PageSize).Take(PagingControl1.PageSize) : result;
                rpList.DataBind();


                if (String.Format("{0:##,###,###,###}", DonationCount) == "")
                    litDonate.Text = "中獎總張數： 0 ";
                else
                    litDonate.Text = "中獎總張數： " + String.Format("{0:##,###,###,###}", DonationCount);


                if (String.Format("{0:##,###,###,###}", TmpItems2.Count()) == "")
                    litTotal.Text = "發票張數： 0 ";
                else
                    litTotal.Text = "發票張數： " + String.Format("{0:##,###,###,###}", TmpItems2.Count());              
            }
            else
            {
                litTotal.Text = "發票張數： 0 ";
                litDonate.Text = "中獎總張數： 0 ";
                this.PagingControl1.Visible = false;
            }
            ShowResult(true);
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            //outFile();
            OutExcelFile();          
        }

        
        protected void outFile()
        {
            bindData(false);
            //export to excel
            Page.Response.Clear();
            Page.Response.Buffer = true;
            Page.Response.AddHeader("content-disposition", "attachment;filename=finance.xls");
            Page.Response.Charset = "";
            Page.Response.ContentType = "application/vnd.ms-excel"; 
            System.IO.StringWriter stringWrite = new System.IO.StringWriter(); 
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            rpList.RenderControl(htmlWrite);

            Page.Response.Write("<table>");
            Page.Response.Write("<tr>");
            Page.Response.Write("<th>社福代碼</th>");
            Page.Response.Write("<th>社福名稱</th>");
            Page.Response.Write("<th>發票號碼</th>");
            if(RadioButton2.Checked )
                Page.Response.Write("<th>是否中獎</th>");
            Page.Response.Write("</tr>");
            Page.Response.Write(stringWrite.ToString());
            Page.Response.Write("</table>"); 
            //Page.Response.Write(stringWrite.ToString());
            Page.Response.End(); 

        }
        
        /// 處理欲寫入資料方法 
        /// </summary> 
        /// 
        static void SetRow(HSSFSheet sheet,HSSFCellStyle xlStyle, int intRow, params string[] values)
        {            
            HSSFRow hsRow = (HSSFRow)sheet.CreateRow(intRow);            
            for (int x = 0; x < values.Length; x++)
            {
                hsRow.CreateCell(x);
                hsRow.Cells[x].SetCellValue(values[x]);
                hsRow.Cells[x].CellStyle = xlStyle;
            }
        }

        private void OutExcelFile()
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            try
            {                               
                // 新增試算表。
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("My Sheet");

                // 建立儲存格樣式。
                HSSFCellStyle style1 = (HSSFCellStyle)workbook.CreateCellStyle();
                style1.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.BLUE.index;
                style1.FillBackgroundColor = 100;
                style1.FillForegroundColor = 2;

                HSSFCellStyle style2 = (HSSFCellStyle)workbook.CreateCellStyle();
                style2.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.YELLOW.index2;
                //style2.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;

                int i = 1;
                if (RadioButton1.Checked)
                {
                    SetRow(sheet,style1, 0, "社福代碼 ", "社福名稱 ", "發票號碼");
                    foreach (var row in resultExcel)
                    {
                        if (row.AgencyCode.Substring(row.AgencyCode.Length - 3, 3) == "tmp")
                            row.AgencyCode = "捐贈張數 : " + row.InvoCount;
                        SetRow(sheet,style2, i, row.AgencyCode, row.WAName, row.No);
                        i++;
                    }
                }
                else
                {
                    SetRow(sheet,style1, 1, "社福代碼 ", "社福名稱 ", "發票號碼", "是否中獎");
                    foreach (var row in resultExcel)
                    {
                        if (row.AgencyCode.Substring(row.AgencyCode.Length - 3, 3) == "tmp")
                            row.AgencyCode = "捐贈張數 : " + row.InvoCount;
                        SetRow(sheet,style2, i, row.AgencyCode, row.WAName, row.No, row.WinningType);
                        i++;
                    }
                }
                
                workbook.Write(ms);
                Page.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=EmptyWorkbook.xls"));
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

        //private void OutExcelFile0()
        //{
        //    Application objExcel_App = new Application();
        //    Workbook objExcel_WB = (Workbook)objExcel_App.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
        //    Worksheet objExcel_WS = objExcel_WB.Worksheets[1] as Worksheet;
        //    Range objExcel_RG = null;

        //    try
        //    {
        //        objExcel_WS.Name = "捐贈中獎統計";

        //        // 設定第一列Excel內容

        //        ArrayList alRow = new ArrayList();
        //        object[] objRow1 = { "社福代碼 ", "社福名稱 ", "發票號碼"};
        //        object[] objRow2 = { "社福代碼 ", "社福名稱 ", "發票號碼", "是否中獎 " };
        //        string xslFileName = @"c:\0001.xls";
        //        if (RadioButton1.Checked)
        //        {
        //            alRow.Add(objRow1);
        //            objExcel_RG = objExcel_WS.get_Range("A1", "C1");
        //        }
        //        else
        //        {
        //            alRow.Add(objRow2);
        //            objExcel_RG = objExcel_WS.get_Range("A1", "D1");
        //        }

        //        objExcel_RG.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, objExcel_RG,
        //             alRow.ToArray(typeof(object)) as object[]);


        //        // 設定Excel格式
        //        objExcel_RG.Font.Bold = true;
        //        objExcel_RG.Font.Name = "Arial";
        //        objExcel_RG.Font.Size = 10;

        //        objExcel_RG.Font.Color = 255; //字型顏色
        //        objExcel_RG.Interior.Color =
        //        System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); //背景顏色
        //        objExcel_RG.VerticalAlignment = XlVAlign.xlVAlignTop; //垂直對齊
        //        objExcel_RG.HorizontalAlignment = XlHAlign.xlHAlignCenter;//水平對齊
        //        objExcel_RG.EntireRow.AutoFit(); //自動調整列高
        //        objExcel_RG.EntireColumn.AutoFit(); //自動調整欄寬

        //        int i = 2;
        //        if (RadioButton1.Checked)
        //        {
        //            xslFileName = "捐贈統計表";
        //            foreach (var row in resultExcel)
        //            {
        //                if (row.AgencyCode.Substring(row.AgencyCode.Length - 3, 3) == "tmp")
        //                    row.AgencyCode = "捐贈張數 : " + row.InvoCount;
        //                SetRow(objExcel_WS, i, row.AgencyCode, row.WAName, row.No);
        //                i++;
        //            }
        //        }
        //        else
        //        {
        //            foreach (var row in resultExcel)
        //            {
        //                xslFileName = "中獎統計表";
        //                if (row.AgencyCode.Substring(row.AgencyCode.Length - 3, 3) == "tmp")
        //                    row.AgencyCode = "捐贈張數 : " + row.InvoCount;
        //                SetRow(objExcel_WS, i, row.AgencyCode, row.WAName, row.No, row.WinningType);
        //                i++;
        //            }
        //        }
        //        //objExcel_WS.get_Range("A1", "B1").Merge(false); //設定A1:B1儲存格合併
        //        //objExcel_WS.get_Range("C:C", Type.Missing).NumberFormatLocal = "@";  //設定C欄儲存格格式為文字
        //        //objExcel_WS.get_Range("D:D", Type.Missing).NumberFormatLocal = "yyyy/MM/dd";  //設定D欄儲存格格式

        //        //string fileName = String.Format("{0}{1}.xlsx", Path.GetTempPath(), DateTime.Now.Ticks);
        //        //objExcel_App.ActiveWorkbook.SaveCopyAs(fileName);

        //        //objExcel_WB.SaveAs(xslFileName, XlFileFormat.xlExcel2, Type.Missing, Type.Missing, Type.Missing,
        //        //    Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing,
        //            //Type.Missing, Type.Missing);

        //        Response.AppendHeader("Content-Disposition", "attachment;filename=MassPay.xlsx");
        //        Response.ContentType = "application/ms-excel";
        //        Response.WriteFile(xslFileName);
        //        //rpList.r

        //        //Array.Clear(objColumnTemp, 0, objColumnTemp.Length);

        //        //objExcel_WS.SaveAs(xslFileName, Type.Missing, Type.Missing, Type.Missing
        //        //           , Type.Missing, Type.Missing, Type.Missing
        //        //                    , Type.Missing, Type.Missing);              
                
               


        //        //objExcel_App.Workbooks.Close();

        //        //objExcel_WB.SaveAs("", XlFileFormat.xlExcel2, Type.Missing, Type.Missing, Type.Missing,
        //        //    Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing,
        //        //    Type.Missing, Type.Missing);


        //    }
        //    catch (Exception exp)
        //    {
        //        //throw exp;
        //    }
        //    finally
        //    {
        //        objExcel_App.Quit();
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel_RG);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel_WS);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel_WB);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel_App);
        //        GC.Collect();
        //    }
        //}

        //private void ExcelResponse(object ExcelWork)
        //{
        //    string attachment = "attachment; filename=Employee.xls";
        //    Response.ClearContent();
        //    Response.AddHeader("content-disposition", attachment);
        //    Response.ContentType = "application/ms-excel";
        //    StringWriter stw = new StringWriter();
        //    HtmlTextWriter htextw = new HtmlTextWriter(stw);
            
        //    //gvEmployee.RenderControl(htextw);
        //    htextw = ExcelWork.
        //    Response.Write(stw.ToString());
        //    Response.End();
        //}


    }
}
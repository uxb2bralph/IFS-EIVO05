﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

using Model.DataEntity;
using Model.Locale;
using Model.Security.MembershipManagement;
using Utility;
using Model.UploadManagement;
using DataAccessLayer.basis;

namespace Model.InvoiceManagement
{
    public class CsvInvoiceUploadManager : CsvUploadManager<EIVOEntityDataContext, InvoiceItem, ItemUpload<InvoiceItem>>
    {
        public const String __Fields = "序號,日期,客戶ID,品名,單價,數量,金額,備註,對方統編,銷售額,稅額,含稅金額,名稱,聯絡人,地址,連絡電話,email";

        public enum FieldIndex
        {
            序號 = 0,
            日期 = 1,
            客戶ID = 2,
            品名 = 3,
            單價 = 4,
            數量 = 5,
            金額 = 6,
            備註 = 7,
            對方統編 = 8,
            銷售額 = 9,
            稅額 = 10,
            含稅金額 = 11,
            名稱 = 12,
            聯絡人 = 13,
            地址 = 14,
            連絡電話 = 15,
            email = 16
        }

        private int _sellerID;
        private TrackNoManager _trkMgr;
        private DateTime _uploadInvoiceDate;
        private Organization _seller;
        private InvoicePurchaseOrderUpload _uploadItem;

        public CsvInvoiceUploadManager(GenericManager<EIVOEntityDataContext> manager, int sellerID)
            : base(manager)
        {
            _sellerID = sellerID;
        }

        public CsvInvoiceUploadManager(int sellerID)
            : this(null, sellerID)
        {
        }

        protected override void initialize()
        {
            __COLUMN_COUNT = 17;   //序號,日期,客戶ID,品名,單價,數量,金額,對方統編,銷售額,稅額,含稅金額,名稱,聯絡人,地址,連絡電話,email
            _uploadInvoiceDate = DateTime.Now;
        }

        public override void ParseData(UserProfileMember userProfile, string fileName, Encoding encoding)
        {
            _uploadItem = new InvoicePurchaseOrderUpload
            {
                FilePath = fileName,
                UploadDate = DateTime.Now
            };

            _seller = this.GetTable<Organization>().Where(o => o.CompanyID == _sellerID).First();
            _trkMgr = new TrackNoManager(this, _sellerID);
            base.ParseData(userProfile, fileName, encoding);
        }

        protected override void doSave()
        {
            this.EntityList.InsertAllOnSubmit(_items.Where(i => i.Entity != null).Select(i => i.Entity));
            this.SubmitChanges();
        }

        private bool isB2C(String[] values)
        {
            return String.IsNullOrEmpty(values[(int)FieldIndex.對方統編]);
        }

        public Organization Seller
        {
            get
            {
                return _seller;
            }
        }


        protected override bool validate(ItemUpload<InvoiceItem> item)
        {
            String[] column = item.Columns;

            bool isB2C = this.isB2C(item.Columns);
            if (!isB2C && (column[(int)FieldIndex.對方統編].Length != 8 || !ValueValidity.ValidateString(column[(int)FieldIndex.對方統編], 20)))
            {
                item.Status = String.Join("、", item.Status, "對方統編格式錯誤");
                _bResult = false;
            }

            checkInputFields(item);

            ItemUpload<InvoiceItem> firstItem = _items.Where(i=>i.Columns[(int)FieldIndex.客戶ID] == item.Columns[(int)FieldIndex.客戶ID] 
                && i.Columns[(int)FieldIndex.序號] == item.Columns[(int)FieldIndex.序號]).FirstOrDefault();

            if (firstItem == null)
            {
                item.Entity = new InvoiceItem
                {
                    CDS_Document = new CDS_Document
                    {
                        DocDate = DateTime.Now,
                        DocType = (int)Naming.DocumentTypeDefinition.E_Invoice,
                        DocumentOwner = new DocumentOwner
                        {
                            OwnerID = _sellerID
                        }
                    },
                    DonateMark = "0",
                    InvoiceDate = _uploadInvoiceDate,
                    InvoiceType = 5,
                    SellerID = _sellerID,
                    RandomNo = ValueValidity.GenerateRandomCode(4),

                    //DonationID = donatory != null ? donatory.CompanyID : (int?)null,
                    InvoicePurchaseOrder = new InvoicePurchaseOrder
                    {
                        InvoicePurchaseOrderUpload = _uploadItem,
                        OrderNo = column[(int)FieldIndex.序號] + column[(int)FieldIndex.客戶ID]
                    },
                    InvoiceSeller = new InvoiceSeller
                    {
                        Name = _seller.CompanyName,
                        ReceiptNo = _seller.ReceiptNo,
                        Address = _seller.Addr,
                        ContactName = _seller.ContactName,
                        CustomerName = _seller.CompanyName,
                        EMail = _seller.ContactEmail,
                        Fax = _seller.Fax,
                        Phone = _seller.Phone,
                        PersonInCharge = _seller.UndertakerName,
                        SellerID = _seller.CompanyID
                    },
                    InvoiceBuyer = new InvoiceBuyer
                    {
                        CustomerID = column[2],
                        ReceiptNo = isB2C ? "0000000000" : column[(int)FieldIndex.對方統編],
                        Name = isB2C ? ValueValidity.GenerateRandomCode(4) : column[(int)FieldIndex.名稱],
                        CustomerName = column[(int)FieldIndex.名稱],
                        ContactName = column[(int)FieldIndex.聯絡人],
                        EMail = column[(int)FieldIndex.email],
                        Address = column[(int)FieldIndex.地址],
                        Phone = column[(int)FieldIndex.連絡電話]
                    },
                    Remark = column[(int)FieldIndex.備註].InsteadOfNullOrEmpty(null)
                };
                checkAmountValue(item);
                checkInvoiceNo(item);
                checkOrderNo(item);
                checkDetail(item, item);
            }
            else
            {
                checkFirstItem(firstItem, item);
                checkDetail(firstItem, item);
            }

            return _bResult;
        }

        private void checkFirstItem(ItemUpload<InvoiceItem> firstItem, ItemUpload<InvoiceItem> item)
        {
            if (item.Columns[(int)FieldIndex.對方統編] != firstItem.Columns[(int)FieldIndex.對方統編])
            {
                item.Status = String.Join("、", item.Status, "對方統編格式錯誤");
                _bResult = false;
            }
            if (item.Columns[(int)FieldIndex.銷售額] != firstItem.Columns[(int)FieldIndex.銷售額])
            {
                item.Status = String.Join("、", item.Status, "銷售額錯誤");
                _bResult = false;
            }
            if (item.Columns[(int)FieldIndex.稅額] != firstItem.Columns[(int)FieldIndex.稅額])
            {
                item.Status = String.Join("、", item.Status, "稅額錯誤");
                _bResult = false;
            }
            if (item.Columns[(int)FieldIndex.含稅金額] != firstItem.Columns[(int)FieldIndex.含稅金額])
            {
                item.Status = String.Join("、", item.Status, "含稅金額錯誤");
                _bResult = false;
            }
        }

        private void checkInvoiceNo(ItemUpload<InvoiceItem> item)
        {
            if (!_trkMgr.CheckInvoiceNo(item.Entity))
            {
                item.Status = String.Join("、", item.Status, "未設定發票字軌或發票號碼已用完");
                _bResult = false;
                _breakParsing = true;
            }
        }

        private void checkOrderNo(ItemUpload<InvoiceItem> item)
        {
            if (this.GetTable<InvoicePurchaseOrder>().Any(p => p.OrderNo == item.Entity.InvoicePurchaseOrder.OrderNo))
            {
                item.Status = String.Join("、", item.Status, "匯入資料重複");
                _bResult = false;
            }
        }


        private void checkDetail(ItemUpload<InvoiceItem> firstItem,ItemUpload<InvoiceItem> item)
        {
            String[] column = item.Columns;

            DateTime dateValue;
            if (!DateTime.TryParseExact(column[(int)FieldIndex.日期], "yyyy/M/d", CultureInfo.CurrentCulture, DateTimeStyles.None, out dateValue))
            {
                item.Status = String.Join("、", item.Status, "日期錯誤");
                _bResult = false;
            }

            decimal costAmt;
            decimal unitCost;
            int piece;

            if (!decimal.TryParse(column[(int)FieldIndex.金額], out costAmt))
            {
                item.Status = String.Join("、", item.Status, "金額錯誤");
                _bResult = false;
            }
            if (!decimal.TryParse(column[(int)FieldIndex.單價], out unitCost))
            {
                item.Status = String.Join("、", item.Status, "單價錯誤");
                _bResult = false;
            }
            if (!int.TryParse(column[(int)FieldIndex.數量], out piece))
            {
                item.Status = String.Join("、", item.Status, "數量錯誤");
                _bResult = false;
            }

            if (_bResult)
            {
                InvoiceProductItem productItem = new InvoiceProductItem
                {
                    InvoiceProduct = new InvoiceProduct { Brief = column[(int)FieldIndex.品名] },
                    CostAmount = costAmt,
                    Piece = piece,
                    UnitCost = unitCost,
                    TaxType = 1,
                    No = (short)firstItem.Entity.InvoiceDetails.Count,
                    Remark = column[(int)FieldIndex.備註].InsteadOfNullOrEmpty(null)
                };
                firstItem.Entity.InvoiceDetails.Add(new InvoiceDetail
                {
                    InvoiceProduct = productItem.InvoiceProduct
                });
            }
        }

        private void checkAmountValue(ItemUpload<InvoiceItem> item)
        {
            String[] column = item.Columns;
            decimal totalAmt;
            if (decimal.TryParse(column[(int)FieldIndex.含稅金額], out totalAmt))
            {
                decimal taxAmt, salesAmt;
                if (decimal.TryParse(column[(int)FieldIndex.稅額], out taxAmt))
                {
                    if (decimal.TryParse(column[(int)FieldIndex.銷售額], out salesAmt))
                    {
                        item.Entity.InvoiceAmountType = new InvoiceAmountType
                        {
                            TotalAmount = totalAmt,
                            SalesAmount = salesAmt,
                            TaxAmount = taxAmt,
                            TaxRate = 0.05m,
                            TaxType = 1,
                            TotalAmountInChinese = Utility.ValueValidity.MoneyShow(totalAmt)
                        };
                    }
                    else
                    {
                        item.Status = String.Join("、", item.Status, "銷售額錯誤");
                        _bResult = false;
                    }
                }
                else
                {
                    item.Status = String.Join("、", item.Status, "稅額錯誤");
                    _bResult = false;
                }
            }
            else
            {
                item.Status = String.Join("、", item.Status, "含稅金額錯誤");
                _bResult = false;
            }
        }

        private void checkInputFields(ItemUpload<InvoiceItem> item)
        {
            String[] column = item.Columns;
            if (column[(int)FieldIndex.序號].Length != 12)
            {
                item.Status = String.Join("、", item.Status, "序號非12位數");
                _bResult = false;
            }
            else if (!ValueValidity.ValidateString(column[(int)FieldIndex.序號], 20))
            {
                item.Status = String.Join("、", item.Status, "序號格式錯誤");
                _bResult = false;
            }

            if (String.IsNullOrEmpty(column[(int)FieldIndex.客戶ID]) /*|| !ValueValidity.ValidateString(column[2], 20)*/)
            {
                item.Status = String.Join("、", item.Status, "客戶ID格式錯誤");
                _bResult = false;
            }

            if (String.IsNullOrEmpty(column[(int)FieldIndex.品名]))
            {
                item.Status = String.Join("、", item.Status, "品項不得為空白");
                _bResult = false;
            }


            if (String.IsNullOrEmpty(column[(int)FieldIndex.名稱]))
            {
                item.Status = String.Join("、", item.Status, "名稱不得為空白");
                _bResult = false;
            }

            if (String.IsNullOrEmpty(column[(int)FieldIndex.聯絡人]))
            {
                item.Status = String.Join("、", item.Status, "聯絡人不得為空白");
                _bResult = false;
            }

            if (String.IsNullOrEmpty(column[(int)FieldIndex.地址]))
            {
                item.Status = String.Join("、", item.Status, "地址不得為空白");
                _bResult = false;
            }

            if (String.IsNullOrEmpty(column[(int)FieldIndex.連絡電話]))
            {
                item.Status = String.Join("、", item.Status, "連絡電話不得為空白");
                _bResult = false;
            }

            if (String.IsNullOrEmpty(column[(int)FieldIndex.email]) || !ValueValidity.ValidateString(column[(int)FieldIndex.email], 16))
            {
                item.Status = String.Join("、", item.Status, "Email格式不正確");
                _bResult = false;
            }
        }
    }

}

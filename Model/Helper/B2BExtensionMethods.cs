using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Schema.EIVO;
using Model.DataEntity;

namespace Model.Helper
{
    public static class B2BExtensionMethods
    {

        public static Model.Schema.TurnKey.A1401.Invoice CreateA1401(this InvoiceItem item)
        {
            var result = new Model.Schema.TurnKey.A1401.Invoice
            {
                Main = new Schema.TurnKey.A1401.Main
                {
                    Buyer = new Schema.TurnKey.A1401.MainBuyer
                    {
                        Address = item.InvoiceBuyer.Address,
                        CustomerNumber = item.InvoiceBuyer.CustomerID,
                        EmailAddress = item.InvoiceBuyer.EMail,
                        FacsimileNumber = item.InvoiceBuyer.Fax,
                        Identifier = item.InvoiceBuyer.ReceiptNo,
                        Name = item.InvoiceBuyer.Name,
                        PersonInCharge = item.InvoiceBuyer.PersonInCharge,
                        RoleRemark = item.InvoiceBuyer.RoleRemark,
                        TelephoneNumber = item.InvoiceBuyer.Phone
                    },
                    BuyerRemark = String.IsNullOrEmpty(item.BuyerRemark) ? Model.Schema.TurnKey.A1401.BuyerRemarkEnum.Item1 : (Model.Schema.TurnKey.A1401.BuyerRemarkEnum)int.Parse(item.BuyerRemark),
                    BuyerRemarkSpecified = !String.IsNullOrEmpty(item.BuyerRemark),
                    Category = item.Category,
                    CheckNumber = item.CheckNo,
                    CustomsClearanceMark = String.IsNullOrEmpty(item.CustomsClearanceMark) ? Model.Schema.TurnKey.A1401.CustomsClearanceMarkEnum.Item1 : (Model.Schema.TurnKey.A1401.CustomsClearanceMarkEnum)int.Parse(item.CustomsClearanceMark),
                    CustomsClearanceMarkSpecified = !String.IsNullOrEmpty(item.CustomsClearanceMark),
                    InvoiceType = (Schema.TurnKey.A1401.InvoiceTypeEnum)((int)item.InvoiceType),
                    //DonateMark = (Schema.TurnKey.A1401.DonateMarkEnum)(int.Parse(item.DonateMark)),
                    DonateMark = string.IsNullOrEmpty(item.DonateMark) ? Model.Schema.TurnKey.A1401.DonateMarkEnum.Item0 : (Schema.TurnKey.A1401.DonateMarkEnum)(int.Parse(item.DonateMark)),
                    GroupMark = item.GroupMark,
                    InvoiceDate = String.Format("{0:yyyyMMdd}", item.InvoiceDate),
                    InvoiceTimeSpecified = false,
                    InvoiceNumber = String.Format("{0}{1}", item.TrackCode, item.No),
                    MainRemark = item.Remark,
                    PermitNumber = item.PermitNumber,
                    PermitDate = item.PermitDate.HasValue ? String.Format("{0:yyyyMMdd}", item.PermitDate.Value) : null,
                    PermitWord = item.PermitWord,
                    RelateNumber = item.RelateNumber,
                    TaxCenter = item.TaxCenter,
                    Seller = new Schema.TurnKey.A1401.MainSeller
                    {
                        Address = item.InvoiceSeller.Address,
                        CustomerNumber = item.InvoiceSeller.CustomerID,
                        EmailAddress = item.InvoiceSeller.EMail,
                        FacsimileNumber = item.InvoiceSeller.Fax,
                        Identifier = item.InvoiceSeller.ReceiptNo,
                        Name = item.InvoiceSeller.Name,
                        PersonInCharge = item.InvoiceSeller.PersonInCharge,
                        RoleRemark = item.InvoiceSeller.RoleRemark,
                        TelephoneNumber = item.InvoiceSeller.Phone
                    }
                },
                Details = buildA1401Details(item),
                Amount = new Schema.TurnKey.A1401.Amount
                {
                    CurrencySpecified = false,
                    DiscountAmount = item.InvoiceAmountType.DiscountAmount.HasValue ? (long)item.InvoiceAmountType.DiscountAmount.Value : 0,
                    DiscountAmountSpecified = item.InvoiceAmountType.DiscountAmount.HasValue,
                    ExchangeRateSpecified = false,
                    OriginalCurrencyAmountSpecified = false,
                    SalesAmount = item.InvoiceAmountType.SalesAmount.HasValue ? (long)item.InvoiceAmountType.SalesAmount.Value : 0,
                    TaxAmount = item.InvoiceAmountType.TaxAmount.HasValue ? (long)item.InvoiceAmountType.TaxAmount.Value : 0,
                    TaxRate = item.InvoiceAmountType.TaxRate.HasValue ? item.InvoiceAmountType.TaxRate.Value : 0.05m,
                    TaxType = (Schema.TurnKey.A1401.TaxTypeEnum)((int)item.InvoiceAmountType.TaxType.Value),
                    TotalAmount = item.InvoiceAmountType.TotalAmount.HasValue ? (long)item.InvoiceAmountType.TotalAmount : 0
                }
            };

            return result;
        }

        private static Schema.TurnKey.A1401.DetailsProductItem[] buildA1401Details(InvoiceItem item)
        {
            List<Model.Schema.TurnKey.A1401.DetailsProductItem> items = new List<Schema.TurnKey.A1401.DetailsProductItem>();
            foreach (var detailItem in item.InvoiceDetails)
            {
                foreach (var productItem in detailItem.InvoiceProduct.InvoiceProductItem)
                {
                    items.Add(new Model.Schema.TurnKey.A1401.DetailsProductItem
                    {
                        Amount = productItem.CostAmount.HasValue ? productItem.CostAmount.Value : 0m,
                        Amount2 = productItem.CostAmount2.HasValue ? productItem.CostAmount2.Value : 0m,
                        Description = detailItem.InvoiceProduct.Brief.Length>256?
                        detailItem.InvoiceProduct.Brief.Substring(0,256):
                        detailItem.InvoiceProduct.Brief,
                        Quantity = productItem.Piece.HasValue ? productItem.Piece.Value : 0,
                        Quantity2 = productItem.Piece2.HasValue ? productItem.Piece2.Value : 0,
                        RelateNumber = productItem.RelateNumber,
                        Remark = productItem.Remark,
                        SequenceNumber = String.Format("{0:00}", productItem.No),
                        Unit = productItem.PieceUnit,
                        Unit2 = productItem.PieceUnit2,
                        UnitPrice = productItem.UnitCost.HasValue ? productItem.UnitCost.Value : 0,
                        UnitPrice2 = productItem.UnitCost2.HasValue ? productItem.UnitCost2.Value : 0
                    });
                }
            }
            return items.ToArray();
        }


        public static Model.Schema.TurnKey.A1101.Invoice CreateA1101(this InvoiceItem item)
        {
            var result = new Model.Schema.TurnKey.A1101.Invoice
            {
                Main = new Schema.TurnKey.A1101.Main
                {
                    Buyer = new Schema.TurnKey.A1101.MainBuyer
                    {
                        Address = item.InvoiceBuyer.Address,
                        CustomerNumber = item.InvoiceBuyer.CustomerID,
                        EmailAddress = item.InvoiceBuyer.EMail,
                        FacsimileNumber = item.InvoiceBuyer.Fax,
                        Identifier = item.InvoiceBuyer.ReceiptNo,
                        Name = item.InvoiceBuyer.Name,
                        PersonInCharge = item.InvoiceBuyer.PersonInCharge,
                        RoleRemark = item.InvoiceBuyer.RoleRemark,
                        TelephoneNumber = item.InvoiceBuyer.Phone
                    },
                    BuyerRemark = String.IsNullOrEmpty(item.BuyerRemark) ? Model.Schema.TurnKey.A1101.BuyerRemarkEnum.Item1 : (Model.Schema.TurnKey.A1101.BuyerRemarkEnum)int.Parse(item.BuyerRemark),
                    BuyerRemarkSpecified = !String.IsNullOrEmpty(item.BuyerRemark),
                    Category = item.Category,
                    CheckNumber = item.CheckNo,
                    CustomsClearanceMark = String.IsNullOrEmpty(item.CustomsClearanceMark) ? Model.Schema.TurnKey.A1101.CustomsClearanceMarkEnum.Item1 : (Model.Schema.TurnKey.A1101.CustomsClearanceMarkEnum)int.Parse(item.CustomsClearanceMark),
                    CustomsClearanceMarkSpecified = !String.IsNullOrEmpty(item.CustomsClearanceMark),
                    DonateMark = (Schema.TurnKey.A1101.DonateMarkEnum)(int.Parse(item.DonateMark)),
                    GroupMark = item.GroupMark,
                    InvoiceDate = String.Format("{0:yyyyMMdd}", item.InvoiceDate),
                    InvoiceTimeSpecified = false,
                    InvoiceNumber = String.Format("{0}{1}", item.TrackCode, item.No),
                    MainRemark = item.Remark,
                    PermitNumber = item.PermitNumber,
                    PermitDate = item.PermitDate.HasValue ? String.Format("{0:yyyyMMdd}", item.PermitDate.Value) : null,
                    PermitWord = item.PermitWord,
                    RelateNumber = item.RelateNumber,
                    TaxCenter = item.TaxCenter,
                    Seller = new Schema.TurnKey.A1101.MainSeller
                    {
                        Address = item.InvoiceSeller.Address,
                        CustomerNumber = item.InvoiceSeller.CustomerID,
                        EmailAddress = item.InvoiceSeller.EMail,
                        FacsimileNumber = item.InvoiceSeller.Fax,
                        Identifier = item.InvoiceSeller.ReceiptNo,
                        Name = item.InvoiceSeller.Name,
                        PersonInCharge = item.InvoiceSeller.PersonInCharge,
                        RoleRemark = item.InvoiceSeller.RoleRemark,
                        TelephoneNumber = item.InvoiceSeller.Phone
                    }
                },
                Details = buildA1101Details(item),
                Amount = new Schema.TurnKey.A1101.Amount {
                    CurrencySpecified = false,
                    DiscountAmount= item.InvoiceAmountType.DiscountAmount.HasValue?(long)item.InvoiceAmountType.DiscountAmount.Value:0,
                    DiscountAmountSpecified = item.InvoiceAmountType.DiscountAmount.HasValue,
                    ExchangeRateSpecified = false,
                    OriginalCurrencyAmountSpecified = false,
                    SalesAmount = item.InvoiceAmountType.SalesAmount.HasValue?(long)item.InvoiceAmountType.SalesAmount.Value:0,
                    TaxAmount = item.InvoiceAmountType.TaxAmount.HasValue?(long)item.InvoiceAmountType.TaxAmount.Value:0,
                    TaxRate = item.InvoiceAmountType.TaxRate.HasValue?item.InvoiceAmountType.TaxRate.Value:0m,
                    TaxType = (Schema.TurnKey.A1101.TaxTypeEnum)item.InvoiceAmountType.TaxType.Value
                }
            };

            return result;
        }

        private static Schema.TurnKey.A1101.DetailsProductItem[] buildA1101Details(InvoiceItem item)
        {
            List<Model.Schema.TurnKey.A1101.DetailsProductItem> items = new List<Schema.TurnKey.A1101.DetailsProductItem>();
            foreach (var detailItem in item.InvoiceDetails)
            {
                foreach (var productItem in detailItem.InvoiceProduct.InvoiceProductItem)
                {
                    items.Add(new Model.Schema.TurnKey.A1101.DetailsProductItem
                    {
                        Amount = productItem.CostAmount.HasValue ? productItem.CostAmount.Value : 0m,
                        Amount2 = productItem.CostAmount2.HasValue ? productItem.CostAmount2.Value : 0m,
                        Description = detailItem.InvoiceProduct.Brief.Length>256?
                        detailItem.InvoiceProduct.Brief.Substring(0,256):
                        detailItem.InvoiceProduct.Brief,
                        Quantity = productItem.Piece.HasValue ? productItem.Piece.Value : 0,
                        Quantity2 = productItem.Piece2.HasValue ? productItem.Piece2.Value : 0,
                        RelateNumber = productItem.RelateNumber,
                        Remark = productItem.Remark,
                        SequenceNumber = String.Format("{0:00}", productItem.No),
                        Unit = productItem.PieceUnit,
                        Unit2 = productItem.PieceUnit2,
                        UnitPrice = productItem.UnitCost.HasValue ? productItem.UnitCost.Value : 0,
                        UnitPrice2 = productItem.UnitCost2.HasValue ? productItem.UnitCost2.Value : 0
                    });
                }
            }
            return items.ToArray();
        }

        public static Model.Schema.TurnKey.B1101.Allowance CreateB1101(this InvoiceAllowance item)
        {
            var result = new Model.Schema.TurnKey.B1101.Allowance
            {
                Main = new Schema.TurnKey.B1101.Main
                {
                    AllowanceDate = String.Format("{0:yyyyMMdd}", item.AllowanceDate),
                    AllowanceNumber = item.AllowanceNumber,
                    AllowanceType = (Model.Schema.TurnKey.B1101.AllowanceTypeEnum)item.AllowanceType,
                    Buyer = new Schema.TurnKey.B1101.MainBuyer {
                        Address = item.InvoiceAllowanceBuyer.Address,
                        CustomerNumber = item.InvoiceAllowanceBuyer.CustomerID,
                        EmailAddress = item.InvoiceAllowanceBuyer.EMail,
                        FacsimileNumber = item.InvoiceAllowanceBuyer.Fax,
                        Identifier = item.InvoiceAllowanceBuyer.ReceiptNo,
                        Name = item.InvoiceAllowanceBuyer.Name,
                        PersonInCharge = item.InvoiceAllowanceBuyer.PersonInCharge,
                        TelephoneNumber = item.InvoiceAllowanceBuyer.Phone,
                        RoleRemark = item.InvoiceAllowanceBuyer.RoleRemark
                    },
                    Seller = new Schema.TurnKey.B1101.MainSeller {
                        Address = item.InvoiceAllowanceSeller.Address,
                        CustomerNumber = item.InvoiceAllowanceSeller.CustomerID,
                        EmailAddress = item.InvoiceAllowanceSeller.EMail,
                        FacsimileNumber = item.InvoiceAllowanceSeller.Fax,
                        Identifier = item.InvoiceAllowanceSeller.ReceiptNo,
                        Name = item.InvoiceAllowanceSeller.Name,
                        PersonInCharge = item.InvoiceAllowanceSeller.PersonInCharge,
                        TelephoneNumber = item.InvoiceAllowanceSeller.Phone,
                        RoleRemark = item.InvoiceAllowanceSeller.RoleRemark
                    }
                },
                Amount = new Model.Schema.TurnKey.B1101.Amount
                {
                    TaxAmount = item.TaxAmount.HasValue ? (long)item.TaxAmount.Value : 0,
                    TotalAmount = item.TotalAmount.HasValue ? (long)item.TotalAmount.Value : 0
                }
            };

            result.Details = item.InvoiceAllowanceDetails.Select(d => new Schema.TurnKey.B1101.DetailsProductItem
            {
                AllowanceSequenceNumber = d.InvoiceAllowanceItem.No.ToString(),
                Amount = d.InvoiceAllowanceItem.Amount.HasValue? d.InvoiceAllowanceItem.Amount.Value :0m,
                Amount2 = d.InvoiceAllowanceItem.Amount2.HasValue?d.InvoiceAllowanceItem.Amount2.Value:0m,
                OriginalInvoiceDate = String.Format("{0:yyyyMMdd}",item.InvoiceItem.InvoiceDate),
                OriginalDescription = d.InvoiceAllowanceItem.OriginalDescription,
                OriginalInvoiceNumber = String.Format("{0}{1}", item.InvoiceItem.TrackCode, item.InvoiceItem.No),
                Quantity = d.InvoiceAllowanceItem.Piece.HasValue ? d.InvoiceAllowanceItem.Piece.Value : 0.00000M,
                Quantity2 = d.InvoiceAllowanceItem.Piece2.HasValue?d.InvoiceAllowanceItem.Piece2.Value:0.00000M,
                Tax = (long)d.InvoiceAllowanceItem.Tax.Value,
                TaxType = (Schema.TurnKey.B1101.DetailsProductItemTaxType)d.InvoiceAllowanceItem.TaxType,
                Unit = d.InvoiceAllowanceItem.PieceUnit,
                UnitPrice = d.InvoiceAllowanceItem.UnitCost.HasValue ? d.InvoiceAllowanceItem.UnitCost.Value : 0
            }).ToArray();

            return result;
        }

        public static Model.Schema.TurnKey.B1401.Allowance CreateB1401(this InvoiceAllowance item)
        {
            var result = new Model.Schema.TurnKey.B1401.Allowance
            {
                Main = new Schema.TurnKey.B1401.Main
                {
                    AllowanceDate = String.Format("{0:yyyyMMdd}", item.AllowanceDate),
                    AllowanceNumber = item.AllowanceNumber,
                    AllowanceType = (Model.Schema.TurnKey.B1401.AllowanceTypeEnum)item.AllowanceType,
                    Buyer = new Schema.TurnKey.B1401.MainBuyer
                    {
                        Address = item.InvoiceAllowanceBuyer.Address,
                        CustomerNumber = item.InvoiceAllowanceBuyer.CustomerID,
                        EmailAddress = item.InvoiceAllowanceBuyer.EMail,
                        FacsimileNumber = item.InvoiceAllowanceBuyer.Fax,
                        Identifier = item.InvoiceAllowanceBuyer.ReceiptNo,
                        Name = item.InvoiceAllowanceBuyer.Name,
                        PersonInCharge = item.InvoiceAllowanceBuyer.PersonInCharge,
                        TelephoneNumber = item.InvoiceAllowanceBuyer.Phone,
                        RoleRemark = item.InvoiceAllowanceBuyer.RoleRemark
                    },
                    Seller = new Schema.TurnKey.B1401.MainSeller
                    {
                        Address = item.InvoiceAllowanceSeller.Address,
                        CustomerNumber = item.InvoiceAllowanceSeller.CustomerID,
                        EmailAddress = item.InvoiceAllowanceSeller.EMail,
                        FacsimileNumber = item.InvoiceAllowanceSeller.Fax,
                        Identifier = item.InvoiceAllowanceSeller.ReceiptNo,
                        Name = item.InvoiceAllowanceSeller.Name,
                        PersonInCharge = item.InvoiceAllowanceSeller.PersonInCharge,
                        TelephoneNumber = item.InvoiceAllowanceSeller.Phone,
                        RoleRemark = item.InvoiceAllowanceSeller.RoleRemark
                    }
                },
                Amount = new Model.Schema.TurnKey.B1401.Amount
                {
                    TaxAmount = item.TaxAmount.HasValue ? (long)item.TaxAmount.Value : 0,
                    TotalAmount = item.TotalAmount.HasValue ? (long)item.TotalAmount.Value : 0
                }
            };

            result.Details = item.InvoiceAllowanceDetails.Select(d => new Schema.TurnKey.B1401.DetailsProductItem
            {
                AllowanceSequenceNumber = d.InvoiceAllowanceItem.No.ToString(),
                Amount = d.InvoiceAllowanceItem.Amount.HasValue ? d.InvoiceAllowanceItem.Amount.Value : 0m,
                Amount2 = d.InvoiceAllowanceItem.Amount2.HasValue ? d.InvoiceAllowanceItem.Amount2.Value : 0m,
                OriginalSequenceNumber  = d.InvoiceAllowanceItem.OriginalSequenceNo.HasValue ?  d.InvoiceAllowanceItem.OriginalSequenceNo.Value .ToString () :"1",
                OriginalInvoiceDate = String.Format("{0:yyyyMMdd}", d.InvoiceAllowanceItem.InvoiceDate),
                OriginalDescription = d.InvoiceAllowanceItem.OriginalDescription,
                OriginalInvoiceNumber = d.InvoiceAllowanceItem.InvoiceNo,
                Quantity = d.InvoiceAllowanceItem.Piece.HasValue ? d.InvoiceAllowanceItem.Piece.Value : 0.00000M,
                Quantity2 = d.InvoiceAllowanceItem.Piece2.HasValue ? d.InvoiceAllowanceItem.Piece2.Value : 0.00000M,
                Tax = (long)d.InvoiceAllowanceItem.Tax.Value,
                TaxType = (Schema.TurnKey.B1401.DetailsProductItemTaxType)d.InvoiceAllowanceItem.TaxType,
                Unit = d.InvoiceAllowanceItem.PieceUnit,
                Unit2 = d.InvoiceAllowanceItem.PieceUnit2,
                UnitPrice2 = d.InvoiceAllowanceItem .UnitCost2 .HasValue ?d.InvoiceAllowanceItem.UnitCost2.Value : 0,
                UnitPrice = d.InvoiceAllowanceItem.UnitCost.HasValue ? d.InvoiceAllowanceItem.UnitCost.Value : 0
            }).ToArray();

            return result;
        }

        public static Model.Schema.TurnKey.A0201.CancelInvoice CreateA0201(this InvoiceItem item)
        {
            InvoiceCancellation cancelItem = item.InvoiceCancellation;

            if (cancelItem == null)
                return null;
            string CancelRemark = cancelItem.Remark;
            if (Encoding.UTF8.GetBytes(cancelItem.Remark).Length > 200)
                CancelRemark = ByteSubStr(cancelItem.Remark, 0, 200);
            var result = new Model.Schema.TurnKey.A0201.CancelInvoice
            {
                CancelDate = String.Format("{0:yyyyMMdd}", cancelItem.CancelDate.Value),
                BuyerId = item.InvoiceBuyer.ReceiptNo,
                CancelInvoiceNumber = cancelItem.CancellationNo,
                CancelTime = cancelItem.CancelDate.Value,
                InvoiceDate = String.Format("{0:yyyyMMdd}", item.InvoiceDate.Value),
                //Remark = cancelItem.Remark.Length > 200 ? cancelItem.Remark.Substring(0, 200) : cancelItem.Remark,
                Remark = CancelRemark,
                ReturnTaxDocumentNumber = cancelItem.ReturnTaxDocumentNo,
                SellerId = item.InvoiceSeller.ReceiptNo,
                CancelReason = string.IsNullOrEmpty(cancelItem.CancelReason) ?
                CancelRemark :
                Encoding.UTF8.GetBytes(cancelItem.CancelReason).Length > 20 ?
                ByteSubStr(cancelItem.CancelReason, 0, 20) : cancelItem.CancelReason
            };

            return result;
        }

        public static Model.Schema.TurnKey.A0501.CancelInvoice CreateA0501(this InvoiceItem item)
        {
            InvoiceCancellation cancelItem = item.InvoiceCancellation;

            if (cancelItem == null)
                return null;
            string CancelRemark= cancelItem.Remark;
            if (Encoding.UTF8.GetBytes(cancelItem.Remark).Length > 200)
                CancelRemark =ByteSubStr(cancelItem.Remark, 0, 200);
            var result = new Model.Schema.TurnKey.A0501.CancelInvoice
            {
                CancelDate = String.Format("{0:yyyyMMdd}", cancelItem.CancelDate.Value),
                BuyerId = item.InvoiceBuyer.ReceiptNo,
                CancelInvoiceNumber = cancelItem.CancellationNo,
                CancelTime = cancelItem.CancelDate.Value,
                InvoiceDate = String.Format("{0:yyyyMMdd}", item.InvoiceDate.Value),
                Remark =CancelRemark,
                ReturnTaxDocumentNumber = cancelItem.ReturnTaxDocumentNo,
                SellerId = item.InvoiceSeller.ReceiptNo,
                CancelReason =string.IsNullOrEmpty(cancelItem.CancelReason)?
                CancelRemark:
                Encoding.UTF8.GetBytes(cancelItem.CancelReason).Length > 20 ?
                ByteSubStr(cancelItem.CancelReason, 0, 20) : cancelItem.CancelReason
            };

            return result;
        }


        public static Model.Schema.TurnKey.B0201.CancelAllowance CreateB0201(this InvoiceAllowance item)
        {
            InvoiceAllowanceCancellation cancelItem = item.InvoiceAllowanceCancellation;

            if (cancelItem == null)
                return null;

            var result = new Model.Schema.TurnKey.B0201.CancelAllowance
            {
                AllowanceDate = String.Format("{0:yyyyMMdd}", item.AllowanceDate),
                CancelDate = String.Format("{0:yyyyMMdd}", cancelItem.CancelDate),
                CancelTime = cancelItem.CancelDate.Value,
                CancelAllowanceNumber = item.AllowanceNumber,
                Remark = cancelItem.Remark,
                BuyerId = item.InvoiceAllowanceBuyer.ReceiptNo,
                SellerId = item.InvoiceAllowanceSeller.ReceiptNo,
                CancelReason = cancelItem.Remark
            };

            return result;
        }

        public static Model.Schema.TurnKey.B0501.CancelAllowance CreateB0501(this InvoiceAllowance item)
        {
            InvoiceAllowanceCancellation cancelItem = item.InvoiceAllowanceCancellation;

            if (cancelItem == null)
                return null;

            var result = new Model.Schema.TurnKey.B0501.CancelAllowance
            {
                AllowanceDate = String.Format("{0:yyyyMMdd}", item.AllowanceDate),
                CancelDate = String.Format("{0:yyyyMMdd}", cancelItem.CancelDate),
                CancelTime = cancelItem.CancelDate.Value,
                CancelAllowanceNumber = item.AllowanceNumber,
                Remark = cancelItem.Remark,
                BuyerId = item.InvoiceAllowanceBuyer.ReceiptNo,
                SellerId = item.InvoiceAllowanceSeller.ReceiptNo,
                CancelReason = cancelItem.Remark
            };

            return result;
        }

        public static Model.Schema.TurnKey.A0401.Invoice CreateA0401(this InvoiceItem item)
        {
            var result = new Model.Schema.TurnKey.A0401.Invoice
            {
                Main = new Schema.TurnKey.A0401.Main
                {
                    Buyer = new Schema.TurnKey.A0401.MainBuyer
                    {
                        //Address = item.InvoiceBuyer.Address,
                        //CustomerNumber = item.InvoiceBuyer.CustomerID,
                        //EmailAddress = item.InvoiceBuyer.EMail,
                        //FacsimileNumber = item.InvoiceBuyer.Fax,
                        Identifier = item.InvoiceBuyer.ReceiptNo,
                        Name = item.InvoiceBuyer.Name,
                        //PersonInCharge = item.InvoiceBuyer.PersonInCharge,
                        //RoleRemark = item.InvoiceBuyer.RoleRemark,
                        //TelephoneNumber = item.InvoiceBuyer.Phone
                    },
                    BuyerRemark = String.IsNullOrEmpty(item.BuyerRemark) ? Model.Schema.TurnKey.A0401.MainBuyerRemark.Item1 : (Model.Schema.TurnKey.A0401.MainBuyerRemark)int.Parse(item.BuyerRemark),
                    BuyerRemarkSpecified = !String.IsNullOrEmpty(item.BuyerRemark),
                    Category = item.Category,
                    CheckNumber = item.CheckNo,
                    CustomsClearanceMark = String.IsNullOrEmpty(item.CustomsClearanceMark) ? Model.Schema.TurnKey.A0401.CustomsClearanceMarkEnum.Item1 : (Model.Schema.TurnKey.A0401.CustomsClearanceMarkEnum)int.Parse(item.CustomsClearanceMark),
                    CustomsClearanceMarkSpecified = !String.IsNullOrEmpty(item.CustomsClearanceMark),
                    InvoiceType = (Schema.TurnKey.A0401.InvoiceTypeEnum)((int)item.InvoiceType),
                    //DonateMark = (Schema.TurnKey.A1401.DonateMarkEnum)(int.Parse(item.DonateMark)),
                    DonateMark = string.IsNullOrEmpty(item.DonateMark) ? Model.Schema.TurnKey.A0401.DonateMarkEnum.Item0 : (Schema.TurnKey.A0401.DonateMarkEnum)(int.Parse(item.DonateMark)),
                    GroupMark = item.GroupMark,
                    InvoiceDate = String.Format("{0:yyyyMMdd}", item.InvoiceDate),
                    InvoiceTime = DateTime.Parse("00:00:00.0000000+08:00"),
                    InvoiceTimeSpecified = true,
                    InvoiceNumber = String.Format("{0}{1}", item.TrackCode, item.No),
                    MainRemark = item.Remark,
                    PermitNumber = item.PermitNumber,
                    PermitDate = item.PermitDate.HasValue ? String.Format("{0:yyyyMMdd}", item.PermitDate.Value) : null,
                    PermitWord = item.PermitWord,
                    RelateNumber = item.RelateNumber,
                    TaxCenter = item.TaxCenter,
                    Seller = new Schema.TurnKey.A0401.MainSeller
                    {
                        //Address = item.InvoiceSeller.Address,
                        //CustomerNumber = item.InvoiceSeller.CustomerID,
                        //EmailAddress = item.InvoiceSeller.EMail,
                        //FacsimileNumber = item.InvoiceSeller.Fax,
                        Identifier = item.InvoiceSeller.ReceiptNo,
                        Name = item.InvoiceSeller.Name,
                        //PersonInCharge = item.InvoiceSeller.PersonInCharge,
                        //RoleRemark = item.InvoiceSeller.RoleRemark,
                        //TelephoneNumber = item.InvoiceSeller.Phone
                    }
                },
                Details = buildA0401Details(item),
                Amount = new Schema.TurnKey.A0401.Amount
                {
                    CurrencySpecified = false,
                    DiscountAmount = item.InvoiceAmountType.DiscountAmount.HasValue ? (long)item.InvoiceAmountType.DiscountAmount.Value : 0,
                    DiscountAmountSpecified = item.InvoiceAmountType.DiscountAmount.HasValue,
                    ExchangeRateSpecified = false,
                    OriginalCurrencyAmountSpecified = false,
                    SalesAmount = item.InvoiceAmountType.SalesAmount.HasValue ? (long)item.InvoiceAmountType.SalesAmount.Value : 0,
                    TaxAmount = item.InvoiceAmountType.TaxAmount.HasValue ? (long)item.InvoiceAmountType.TaxAmount.Value : 0,
                    TaxRate = item.InvoiceAmountType.TaxRate.HasValue ? item.InvoiceAmountType.TaxRate.Value : 0.05m,
                    TaxType = (Schema.TurnKey.A0401.TaxTypeEnum)((int)item.InvoiceAmountType.TaxType.Value),
                    TotalAmount = item.InvoiceAmountType.TotalAmount.HasValue ? (long)item.InvoiceAmountType.TotalAmount : 0
                }
            };

            return result;
        }

        private static Schema.TurnKey.A0401.DetailsProductItem[] buildA0401Details(InvoiceItem item)
        {
            List<Model.Schema.TurnKey.A0401.DetailsProductItem> items = new List<Schema.TurnKey.A0401.DetailsProductItem>();
            foreach (var detailItem in item.InvoiceDetails)
            {
                foreach (var productItem in detailItem.InvoiceProduct.InvoiceProductItem)
                {
                    string remark = productItem.Remark;
                    if(!String.IsNullOrEmpty(productItem.Remark))
                    if (Encoding.UTF8.GetBytes(productItem.Remark).Length > 40)
                        remark = ByteSubStr(productItem.Remark, 0, 40);
                    items.Add(new Model.Schema.TurnKey.A0401.DetailsProductItem
                    {
                        Amount = productItem.CostAmount.HasValue ? productItem.CostAmount.Value : 0m,
                        Description = detailItem.InvoiceProduct.Brief.Length>256?
                        detailItem.InvoiceProduct.Brief.Substring(0,256):
                        detailItem.InvoiceProduct.Brief,
                        Quantity = productItem.Piece.HasValue ? productItem.Piece.Value : 0,
                        RelateNumber = productItem.RelateNumber,
                        Remark = remark,
                        SequenceNumber = String.Format("{0:00}", productItem.No),
                        Unit = productItem.PieceUnit,
                        UnitPrice = productItem.UnitCost.HasValue ? productItem.UnitCost.Value : 0,
                    });
                }
            }
            return items.ToArray();
        }

        public static Model.Schema.TurnKey.B0401.Allowance CreateB0401(this InvoiceAllowance item)
        {
            var result = new Model.Schema.TurnKey.B0401.Allowance
            {
                Main = new Schema.TurnKey.B0401.Main
                {
                    AllowanceDate = String.Format("{0:yyyyMMdd}", item.AllowanceDate),
                    AllowanceNumber = item.AllowanceNumber,
                    AllowanceType = (Model.Schema.TurnKey.B0401.AllowanceTypeEnum)item.AllowanceType,
                    Buyer = new Schema.TurnKey.B0401.MainBuyer
                    {
                        //Address = item.InvoiceAllowanceBuyer.Address,
                        //CustomerNumber = item.InvoiceAllowanceBuyer.CustomerID,
                        //EmailAddress = item.InvoiceAllowanceBuyer.EMail,
                        //FacsimileNumber = item.InvoiceAllowanceBuyer.Fax,
                        Identifier = item.InvoiceAllowanceBuyer.ReceiptNo,
                        Name = item.InvoiceAllowanceBuyer.Name,
                        //PersonInCharge = item.InvoiceAllowanceBuyer.PersonInCharge,
                        //TelephoneNumber = item.InvoiceAllowanceBuyer.Phone,
                        //RoleRemark = item.InvoiceAllowanceBuyer.RoleRemark
                    },
                    Seller = new Schema.TurnKey.B0401.MainSeller
                    {
                        //Address = item.InvoiceAllowanceSeller.Address,
                        //CustomerNumber = item.InvoiceAllowanceSeller.CustomerID,
                        //EmailAddress = item.InvoiceAllowanceSeller.EMail,
                        //FacsimileNumber = item.InvoiceAllowanceSeller.Fax,
                        Identifier = item.InvoiceAllowanceSeller.ReceiptNo,
                        Name = item.InvoiceAllowanceSeller.Name,
                        //PersonInCharge = item.InvoiceAllowanceSeller.PersonInCharge,
                        //TelephoneNumber = item.InvoiceAllowanceSeller.Phone,
                        //RoleRemark = item.InvoiceAllowanceSeller.RoleRemark
                    }
                },
                Amount = new Model.Schema.TurnKey.B0401.Amount
                {
                    TaxAmount = item.TaxAmount.HasValue ? (long)item.TaxAmount.Value : 0,
                    TotalAmount = item.TotalAmount.HasValue ? (long)item.TotalAmount.Value : 0
                }
            };

            result.Details = item.InvoiceAllowanceDetails.Select(d => new Schema.TurnKey.B0401.DetailsProductItem
            {
                AllowanceSequenceNumber = d.InvoiceAllowanceItem.No.ToString(),
                Amount = d.InvoiceAllowanceItem.Amount.HasValue ? d.InvoiceAllowanceItem.Amount.Value : 0m,
                OriginalSequenceNumber = d.InvoiceAllowanceItem.OriginalSequenceNo.HasValue ? d.InvoiceAllowanceItem.OriginalSequenceNo.Value.ToString() : "1",
                OriginalInvoiceDate = String.Format("{0:yyyyMMdd}", d.InvoiceAllowanceItem.InvoiceDate),
                OriginalDescription = d.InvoiceAllowanceItem.OriginalDescription,
                OriginalInvoiceNumber = d.InvoiceAllowanceItem.InvoiceNo,
                Quantity = d.InvoiceAllowanceItem.Piece.HasValue ? d.InvoiceAllowanceItem.Piece.Value : 0.00000M,
                Tax = (long)d.InvoiceAllowanceItem.Tax.Value,
                TaxType = (Schema.TurnKey.B0401.DetailsProductItemTaxType)d.InvoiceAllowanceItem.TaxType,
                Unit = d.InvoiceAllowanceItem.PieceUnit,
                UnitPrice = d.InvoiceAllowanceItem.UnitCost.HasValue ? d.InvoiceAllowanceItem.UnitCost.Value : 0
            }).ToArray();

            return result;
        }
        public static string ByteSubStr(string a_SrcStr, int a_StartIndex, int a_Cnt)
        {
            Encoding l_Encoding = Encoding.GetEncoding("utf-8", new EncoderExceptionFallback(), new DecoderReplacementFallback(""));
            byte[] l_byte = l_Encoding.GetBytes(a_SrcStr);// Encoding.UTF8.GetBytes(a_SrcStr);
            //byte[] l_byte = System.Text.Encoding.Default.GetBytes(a_SrcStr); 
            if (a_Cnt <= 0)
                return "";
            //若長度10 
            //若a_StartIndex傳入9 -> ok, 10 ->不行 
            if (a_StartIndex + 1 > l_byte.Length)
                return "";
            else
            {
                //若a_StartIndex傳入9 , a_Cnt 傳入2 -> 不行 -> 改成 9,1 
                if (a_StartIndex + a_Cnt > l_byte.Length)
                    a_Cnt = l_byte.Length - a_StartIndex;
            }
            return l_Encoding.GetString(l_byte, a_StartIndex, a_Cnt); // System.Text.Encoding.Default.GetString(l_byte, a_StartIndex, a_Cnt); 
        }
      
    }
}

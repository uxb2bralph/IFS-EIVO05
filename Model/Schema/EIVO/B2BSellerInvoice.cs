﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.42000
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此原始程式碼由 xsd 版本=4.6.1055.0 自動產生。
// 
namespace Model.Schema.EIVO.B2B {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class SellerInvoiceRoot {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Invoice")]
        public SellerInvoiceRootInvoice[] Invoice;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class SellerInvoiceRootInvoice {
        
        /// <remarks/>
        public string InvoiceNumber;
        
        /// <remarks/>
        public string InvoiceDate;
        
        /// <remarks/>
        public string InvoiceTime;
        
        /// <remarks/>
        public string SellerId;
        
        /// <remarks/>
        public string BuyerId;
        
        /// <remarks/>
        public string BuyerName;
        
        /// <remarks/>
        public string InvoiceType;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("InvoiceItem")]
        public SellerInvoiceRootInvoiceInvoiceItem[] InvoiceItem;
        
        /// <remarks/>
        public decimal SalesAmount;
        
        /// <remarks/>
        public byte TaxType;
        
        /// <remarks/>
        public decimal TaxAmount;
        
        /// <remarks/>
        public decimal TotalAmount;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> DiscountAmount;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DiscountAmountSpecified;
        
        /// <remarks/>
        public SellerInvoiceRootInvoiceCustomerDefined CustomerDefined;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class SellerInvoiceRootInvoiceInvoiceItem {
        
        /// <remarks/>
        public string Description;
        
        /// <remarks/>
        public decimal Quantity;
        
        /// <remarks/>
        public decimal Quantity2;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Quantity2Specified;
        
        /// <remarks/>
        public string Unit;
        
        /// <remarks/>
        public string Unit2;
        
        /// <remarks/>
        public decimal UnitPrice;
        
        /// <remarks/>
        public decimal UnitPrice2;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UnitPrice2Specified;
        
        /// <remarks/>
        public decimal Amount;
        
        /// <remarks/>
        public decimal Amount2;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Amount2Specified;
        
        /// <remarks/>
        public decimal SequenceNumber;
        
        /// <remarks/>
        public string Remark;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class SellerInvoiceRootInvoiceCustomerDefined {
        
        /// <remarks/>
        public string IsolationFolder;
    }
}

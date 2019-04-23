using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Model.Schema.MIG3_1;
using Model.Schema.TXN;

namespace Model.Schema.EIVO
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "A0101" /*AnonymousType = true*/)]
    public partial class RootResponseForA0101 : RootResponse
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Invoice", Order = 1)]
        public Model.Schema.EIVO.A0101.Invoice[] Invoice;
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "A0201" /*AnonymousType = true*/)]
    public partial class RootResponseForA0201 : RootResponse
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CancelInvoice", Order = 1)]
        public Model.Schema.EIVO.A0201.CancelInvoice[] CancelInvoice;
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    [XmlInclude(typeof(Model.Schema.EIVO.RootResponseForA0101))]
    public partial class RootA0101 : Root
    {

    }


    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    [XmlInclude(typeof(Model.Schema.EIVO.RootResponseForA0201))]
    public partial class RootA0201 : Root
    {

    }


    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "B0101" /*AnonymousType = true*/)]
    public partial class RootResponseForB0101 : RootResponse
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Allowance", Order = 1)]
        public Model.Schema.EIVO.B0101.Allowance[] Allowance;
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "B0201" /*AnonymousType = true*/)]
    public partial class RootResponseForB0201 : RootResponse
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CancelAllowance", Order = 1)]
        public Model.Schema.EIVO.B0201.CancelAllowance[] CancelAllowance;
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    [XmlInclude(typeof(Model.Schema.EIVO.RootResponseForB0101))]
    public partial class RootB0101 : Root
    {

    }


    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    [XmlInclude(typeof(Model.Schema.EIVO.RootResponseForB0201))]
    public partial class RootB0201 : Root
    {

    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "E0402" /*AnonymousType = true*/)]
    public partial class RootResponseForE0402 : RootResponse
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("BranchTrackBlank", Order = 1)]
        public Model.Schema.MIG3_1.E0402.BranchTrackBlank E0402BranchTrackBlank ;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    [XmlInclude(typeof(Model.Schema.EIVO.RootResponseForE0402))]
    public partial class RootE0402 : Root
    {

    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "E0402" /*AnonymousType = true*/)]
    public partial class RootResponseForBranchTrackBlank : RootResponse
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("BranchTrackBlank", Order = 1)]
        public Model.Schema.EIVO.E0402.BranchTrackBlank TrackBlank;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    [XmlInclude(typeof(Model.Schema.EIVO.RootResponseForBranchTrackBlank))]
    public partial class RootBranchTrackBlank : Root
    {

    }
}

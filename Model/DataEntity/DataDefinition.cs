using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.DataEntity
{
    public partial class DataDefinition
    {
    }

    public class NotifyToProcessID
    {
        public int? MailToID { get; set; }
        public Organization Seller { get; set; }
        public String itemNO { get; set; }
        public int? DocID { get; set; }
        public String Subject { get; set; }
    }

    public class NotifyMailInfo
    {
        public bool? isMail { get; set; }
        public InvoiceItem InvoiceItem { get; set; }
    }
}

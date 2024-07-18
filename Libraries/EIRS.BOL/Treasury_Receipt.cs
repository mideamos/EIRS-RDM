//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EIRS.BOL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Treasury_Receipt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Treasury_Receipt()
        {
            this.MAP_TreasuryReceipt_Settlement = new HashSet<MAP_TreasuryReceipt_Settlement>();
        }
    
        public long ReceiptID { get; set; }
        public string ReceiptRefNo { get; set; }
        public Nullable<long> ServiceBillID { get; set; }
        public Nullable<long> AssessmentID { get; set; }
        public Nullable<decimal> ReceiptAmount { get; set; }
        public Nullable<System.DateTime> ReceiptDate { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string Notes { get; set; }
        public Nullable<int> CancelledBy { get; set; }
        public string CancelNotes { get; set; }
        public string GeneratedPath { get; set; }
        public string SignedPath { get; set; }
        public Nullable<int> SignSourceID { get; set; }
        public string SignImgSrc { get; set; }
        public string DocumentUrl { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MAP_TreasuryReceipt_Settlement> MAP_TreasuryReceipt_Settlement { get; set; }
    }
}

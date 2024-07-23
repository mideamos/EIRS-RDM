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
    
    public partial class Revenue_Stream
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Revenue_Stream()
        {
            this.Assessment_Items = new HashSet<Assessment_Items>();
            this.MAP_Directorates_RevenueStream = new HashSet<MAP_Directorates_RevenueStream>();
            this.MDA_Service_Items = new HashSet<MDA_Service_Items>();
            this.Revenue_SubStream = new HashSet<Revenue_SubStream>();
            this.Payment_Account = new HashSet<Payment_Account>();
            this.MAP_TaxOffice_Target = new HashSet<MAP_TaxOffice_Target>();
            this.MAP_TaxOfficer_Target = new HashSet<MAP_TaxOfficer_Target>();
        }
    
        public int RevenueStreamID { get; set; }
        public string RevenueStreamName { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> EnableBillNotification { get; set; }
        public Nullable<int> NotificationPeriod { get; set; }
        public string EmailContent { get; set; }
        public string SMSContent { get; set; }
        public string BillTemplatePath { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assessment_Items> Assessment_Items { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MAP_Directorates_RevenueStream> MAP_Directorates_RevenueStream { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MDA_Service_Items> MDA_Service_Items { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Revenue_SubStream> Revenue_SubStream { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment_Account> Payment_Account { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MAP_TaxOffice_Target> MAP_TaxOffice_Target { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MAP_TaxOfficer_Target> MAP_TaxOfficer_Target { get; set; }
    }
}
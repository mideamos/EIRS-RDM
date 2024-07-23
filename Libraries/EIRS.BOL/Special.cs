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
    
    public partial class Special
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Special()
        {
            this.MAP_Special_AddressInformation = new HashSet<MAP_Special_AddressInformation>();
        }
    
        public int SpecialID { get; set; }
        public string SpecialRIN { get; set; }
        public string SpecialTaxPayerName { get; set; }
        public string TIN { get; set; }
        public Nullable<int> TaxOfficeID { get; set; }
        public Nullable<int> TaxPayerTypeID { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public string ContactName { get; set; }
        public string Description { get; set; }
        public Nullable<int> NotificationMethodID { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> TaxOfficerID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MAP_Special_AddressInformation> MAP_Special_AddressInformation { get; set; }
        public virtual Notification_Method Notification_Method { get; set; }
        public virtual TaxPayer_Types TaxPayer_Types { get; set; }
        public virtual Tax_Offices Tax_Offices { get; set; }
    }
}
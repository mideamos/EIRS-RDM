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
    
    public partial class MAP_API_Users_Rights
    {
        public long UAID { get; set; }
        public Nullable<int> APIID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<bool> APIAccess { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual MST_API MST_API { get; set; }
        public virtual MST_Users MST_Users { get; set; }
    }
}

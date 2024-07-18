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
    
    public partial class SystemUser
    {
        public int SystemUserID { get; set; }
        public string SystemUserName { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public Nullable<int> SystemRoleID { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public Nullable<int> FailedLoginCount { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual SystemRole SystemRole { get; set; }
    }
}

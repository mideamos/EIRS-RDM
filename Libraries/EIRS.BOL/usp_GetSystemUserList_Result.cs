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
    
    public partial class usp_GetSystemUserList_Result
    {
        public Nullable<int> SystemUserID { get; set; }
        public string SystemUserName { get; set; }
        public string UserPassword { get; set; }
        public string UserLogin { get; set; }
        public Nullable<int> SystemRoleID { get; set; }
        public string SystemRoleName { get; set; }
        public Nullable<bool> Active { get; set; }
        public string ActiveText { get; set; }
    }
}
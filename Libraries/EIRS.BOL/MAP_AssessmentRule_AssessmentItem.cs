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
    
    public partial class MAP_AssessmentRule_AssessmentItem
    {
        public int ARAIID { get; set; }
        public Nullable<int> AssessmentRuleID { get; set; }
        public Nullable<int> AssessmentItemID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual Assessment_Items Assessment_Items { get; set; }
        public virtual Assessment_Rules Assessment_Rules { get; set; }
    }
}
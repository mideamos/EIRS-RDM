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
    
    public partial class usp_GetAssessment_AssessmentRuleList_Result
    {
        public Nullable<long> AARID { get; set; }
        public Nullable<int> AssetTypeID { get; set; }
        public string AssetTypeName { get; set; }
        public Nullable<int> AssetID { get; set; }
        public string AssetRIN { get; set; }
        public Nullable<int> ProfileID { get; set; }
        public string ProfileDescription { get; set; }
        public Nullable<int> AssessmentRuleID { get; set; }
        public string AssessmentRuleName { get; set; }
        public Nullable<int> TaxYear { get; set; }
        public Nullable<decimal> AssessmentRuleAmount { get; set; }
        public Nullable<decimal> SettledAmount { get; set; }
    }
}

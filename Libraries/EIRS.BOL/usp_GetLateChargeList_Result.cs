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
    
    public partial class usp_GetLateChargeList_Result
    {
        public Nullable<int> LateChargeID { get; set; }
        public Nullable<int> RevenueStreamID { get; set; }
        public string RevenueStreamName { get; set; }
        public Nullable<int> TaxYear { get; set; }
        public Nullable<decimal> Penalty { get; set; }
        public Nullable<decimal> Interest { get; set; }
        public Nullable<bool> Active { get; set; }
        public string ActiveText { get; set; }
    }
}
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
    
    public partial class usp_GetTaxPayerReviewNotes_Result
    {
        public long TPRID { get; set; }
        public Nullable<int> ReviewStatusID { get; set; }
        public string ReviewStatusName { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> ReviewDate { get; set; }
        public Nullable<int> UserID { get; set; }
        public string ContactName { get; set; }
    }
}

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
    
    public partial class vw_PaymentAccount
    {
        public long PaymentAccountID { get; set; }
        public string PaymentRefNo { get; set; }
        public string PaymentDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string SettlementMethodName { get; set; }
        public Nullable<int> TaxPayerID { get; set; }
        public Nullable<int> TaxPayerTypeID { get; set; }
        public Nullable<int> SettlementMethodID { get; set; }
        public string TaxPayerName { get; set; }
        public string TransactionRefNo { get; set; }
        public Nullable<int> TaxOfficeID { get; set; }
        public Nullable<int> TaxOfficerID { get; set; }
        public Nullable<int> RevenueStreamID { get; set; }
    }
}
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
    
    public partial class usp_GetDAOutputList_Result
    {
        public Nullable<int> DAOutputID { get; set; }
        public Nullable<System.DateTime> transaction_date { get; set; }
        public string Taxpayer_RIN { get; set; }
        public string Taxpayer_TIN { get; set; }
        public string Total_Income { get; set; }
        public string Pension_Contribution_Declared { get; set; }
        public string NHF_Declared { get; set; }
        public string NHIS_Declared { get; set; }
        public string Life_Assurance { get; set; }
        public string PAYE_Income { get; set; }
        public string PAYE_Pension { get; set; }
        public string PAYE_NHF { get; set; }
        public string PAYE_NHIS { get; set; }
        public string Employee_PAYE_Contribution { get; set; }
        public string Share_of_Assessment { get; set; }
        public string Assessment_Year { get; set; }
        public string Start_Month { get; set; }
        public string tax_office { get; set; }
    }
}
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
    
    public partial class usp_SFTP_GetDataSubmissionList_Result
    {
        public string SubmissionRefNo { get; set; }
        public Nullable<System.DateTime> SubmissionDate { get; set; }
        public Nullable<int> DataSubmitterID { get; set; }
        public string DataSubmitterName { get; set; }
        public Nullable<int> TaxYear { get; set; }
        public Nullable<int> DataSubmissionTypeID { get; set; }
        public string DataSubmissionTypeName { get; set; }
        public string DocumentPath { get; set; }
        public Nullable<int> DataSubmissionID { get; set; }
    }
}

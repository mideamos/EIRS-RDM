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
    
    public partial class usp_GetTaxPayerAssetForTCC_Result
    {
        public Nullable<int> AssetTypeID { get; set; }
        public string AssetTypeName { get; set; }
        public Nullable<int> TaxPayerRoleID { get; set; }
        public string TaxPayerRoleName { get; set; }
        public Nullable<int> AssetID { get; set; }
        public string AssetLGA { get; set; }
        public string AssetRIN { get; set; }
        public string AssetName { get; set; }
        public Nullable<int> BuildingUnitID { get; set; }
        public string UnitNumber { get; set; }
        public Nullable<bool> Active { get; set; }
        public string ActiveText { get; set; }
        public Nullable<long> TPAID { get; set; }
    }
}
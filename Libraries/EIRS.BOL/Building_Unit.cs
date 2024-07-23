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
    
    public partial class Building_Unit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Building_Unit()
        {
            this.MAP_Building_BuildingUnit = new HashSet<MAP_Building_BuildingUnit>();
        }
    
        public int BuildingUnitID { get; set; }
        public string BuildingUnitRIN { get; set; }
        public string UnitNumber { get; set; }
        public Nullable<int> UnitPurposeID { get; set; }
        public Nullable<int> UnitFunctionID { get; set; }
        public Nullable<int> UnitOccupancyID { get; set; }
        public Nullable<int> SizeID { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual Size Size { get; set; }
        public virtual Unit_Function Unit_Function { get; set; }
        public virtual Unit_Occupancy Unit_Occupancy { get; set; }
        public virtual Unit_Purpose Unit_Purpose { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MAP_Building_BuildingUnit> MAP_Building_BuildingUnit { get; set; }
    }
}
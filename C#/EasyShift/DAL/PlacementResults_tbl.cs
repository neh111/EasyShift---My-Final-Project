//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlacementResults_tbl
    {
        public int result_id { get; set; }
        public int shift_id { get; set; }
        public int employee_id { get; set; }
        public int job_id { get; set; }
        public Nullable<int> statisfaction_level { get; set; }
        public System.DateTime placement_date { get; set; }
    
        public virtual Employee_tbl Employee_tbl { get; set; }
        public virtual Jobs_tbl Jobs_tbl { get; set; }
        public virtual Shift_tbl Shift_tbl { get; set; }
    }
}

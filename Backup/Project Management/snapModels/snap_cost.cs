using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Management.snapModels
{
    public class snap_cost
    {
        public int ProjectID { get; set; }
        public Nullable<int> TechEstimate { get; set; }
        public Nullable<int> DomainEstimate { get; set; }
        public Nullable<int> BAEstimate { get; set; }
        public Nullable<int> QCEstimate { get; set; }
        public Nullable<int> TechLeadEstimate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Management.snapModels
{
    public class snap_projectEstimate
    {
        public int ProjectID { get; set; }
        public int ModuleID { get; set; }
        public Nullable<int> TechDuration { get; set; }
        public Nullable<int> AnalystDuration { get; set; }
        public string TechResource { get; set; }
        public string AnalystResource { get; set; }
        public string DeveloperDuration { get; set; }
        public string BADuration { get; set; }
        public string QCDuration { get; set; }
        public string LeadDuration { get; set; }
        public string DomainDuration { get; set; }
        public string ModuleCost { get; set; }
    }
}
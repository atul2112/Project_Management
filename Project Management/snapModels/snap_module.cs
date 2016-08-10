using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Management.snapModels
{
    public class snap_module
    {
        public int ModuleID { get; set; }
        public string ModuleTitle { get; set; }
        public string ModuleWeight { get; set; }
        public string PercentComplete { get; set; }
        public string ModuleTeamInfo { get; set; }
        public Nullable<int> ProjectInfo { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ModuleDescription { get; set; }
        public string ModuleFiles { get; set; }
    }
}
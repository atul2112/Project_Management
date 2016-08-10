using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Management.Models
{
    public class moduleDashboard
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
        public Nullable<int> TotalDays { get; set; }
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
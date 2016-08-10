using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Management.Models
{
    public class ProjectDashboard
    {
        public int ModuleCount { get; set; }
        public int MomCount { get; set; }
        public int FileCount { get; set; }
        public List<ModulesData> Modules { get; set; }
    }

    public class ModulesData 
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
        public int TotalDays { get; set; }
    }
    //public class GanttData 
    //{
    //    public string startDate;
    //    public string endDate;
    //    public string index;
    //}
}

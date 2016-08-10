using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Management.snapModels
{
    public class snap_projectDetails
    {
        public int ProjectID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> DomainTeam { get; set; }
        public Nullable<int> DomainLead { get; set; }
        public string DomainLeadName { get; set; }
        public Nullable<int> TechLead { get; set; }
        public string TechLeadName { get; set; }
        public Nullable<int> QCLead { get; set; }
        public string QCLeadName { get; set; }
        public string DomainResource { get; set; }
        public string TechResource { get; set; }
        public string Modules { get; set; }
        public string EstimatedCost { get; set; }
        public string ProjectLogo { get; set; }
        public string ProjectDetails { get; set; }
        public string StartDate { get; set; }
        public string Duration { get; set; }
        public Nullable<int> TechTeam { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ProjectType { get; set; }
        public string ClientName { get; set; }
        public string TotalCost { get; set; }
        public string CurrencyType { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Management.Models
{
    public class ProjectModel
    {
        public int ProjectID { get; set; }

        public string ProjectName { get; set; }
        public string ProjectPath { get; set; }
        public string StartDate { get; set; }
        public string ProposalDate { get; set; }

        public string Client { get; set; }
        public string DomainName { get; set; }

        public string TeamName1 { get; set; }
        public string TeamName2 { get; set; }
        public string TeamName3 { get; set; }
        public string Resource1 { get; set; }
        public string Resource2 { get; set; }
        public string Resource3 { get; set; }
        public string Resource4 { get; set; }

        public string Resource5 { get; set; }
        public string Resource6 { get; set; }
        public string Resource7 { get; set; }
        public string Resource8 { get; set; }

        public string Resource9 { get; set; }
        
    }
}
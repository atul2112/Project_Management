using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Management.CommonFunctions
{
    public class ModuleClass
    {
        public List<CostDefination> lstCostEstimate { get; set; }
        public List<CostCalculation> lstCostCalculate { get; set; }
        public ModuleClass()
        {
            lstCostEstimate = new List<CostDefination>();

            lstCostCalculate = new List<CostCalculation>();
        }
    }

    

    public class CostDefination
    {
        public string Tech_Cost { get; set; }
        public string BA_Cost { get; set; }
        public string Lead_Cost { get; set; }
        public string Domain_Cost { get; set; }
        public string QC_Cost { get; set; }
    }

    public class CostCalculation
    {
        public string Module_Name { get; set; }
        public string Start_Date { get; set; }
        public string Tech_Duration { get; set; }
        public string BA_Duration { get; set; }
        public string Developer_Duration { get; set; }
        public string QC_Duration { get; set; }
        public string Lead_Duration { get; set; }
        public string Domain_Duration { get; set; }
        public string Project_Duration { get; set; }
        public string Module_Cost { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Project_Management.CommonFunctions;
using System.Text;

namespace Project_Management.Controllers
{
    public class ProjectDetailController : Controller
    {
        //
        // GET: /ProjectDetail/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void StoreModuleDetail(string ModuleCostDetails, string Flag)
        {
            if (Flag == "AP")
            {
                List<CostCalculation> lstCostCalculate = new List<CostCalculation>();
                dynamic jsonDe = JsonConvert.DeserializeObject(ModuleCostDetails);
                foreach (var item in jsonDe)
                {
                    System.DateTime start_dt = item.StartDate;
                    lstCostCalculate.Add(new CostCalculation()
                    {
                        Module_Name = item.ModuleName,
                        Start_Date = (start_dt - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString(),
                        Tech_Duration = item.TechDuration,
                        BA_Duration = item.BADuration
                    });

                }

                Session["ModuleCostCalculation"] = lstCostCalculate;
                
            }
            else if (Flag == "MD")
            {
                dynamic jsonDe = JsonConvert.DeserializeObject(ModuleCostDetails);
                using (test_pmEntities2 dc = new test_pmEntities2())
                {

                    int DefineProjectID = Convert.ToInt32(Session["session_pid"]);
                    //int moduleID = dc.tbl_ProjectEstimate.Where(x => x.ProjectID == DefineProjectID).Select(x => x.ModuleID).ToList().Max() + 1;
                    List<CostCalculation> lstCostCalculate = new List<CostCalculation>();
                    string CompltTechResource = null, CompltDomainResource = null;
                    if (dc.tbl_projectdetails.Where(x => x.ProjectID == DefineProjectID).Select(x => x.TechResource).First() != "N/A")
                        CompltTechResource = dc.tbl_projectdetails.Where(x => x.ProjectID == DefineProjectID).Select(x => x.TechResource).First();
                    if (dc.tbl_projectdetails.Where(x => x.ProjectID == DefineProjectID).Select(x => x.DomainResource).First() != "N/A")
                        CompltDomainResource = dc.tbl_projectdetails.Where(x => x.ProjectID == DefineProjectID).Select(x => x.DomainResource).First();

                    foreach (var item in jsonDe)
                    {

                        tbl_ProjectEstimate CostDetail = new tbl_ProjectEstimate();
                        tbl_module moduleDetails = new tbl_module();
                        var moduleID = dc.tbl_module.OrderByDescending(x => x.ModuleID).ToList().FirstOrDefault();
                        if (moduleID != null)
                        {
                            moduleDetails.ModuleID = moduleID.ModuleID + 1;
                        }
                        else
                        {
                            moduleDetails.ModuleID = 1;
                        }
                        //moduleDetails.ModuleID = moduleID;
                        moduleDetails.ProjectInfo = DefineProjectID;
                        System.DateTime start_dt = item.StartDate;
                        moduleDetails.StartDate = (start_dt - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
                        moduleDetails.ModuleTitle = item.ModuleName;
                        moduleDetails.ModuleWeight = "8.0";
                        moduleDetails.PercentComplete = "50";
                        moduleDetails.ModuleDescription = item.Description;

                        CostDetail.ProjectID = DefineProjectID;
                        CostDetail.ModuleID = moduleDetails.ModuleID;
                        int TD = item.TechDuration;
                        int BD = item.AnalystDuration;
                        CostDetail.TechDuration = TD;
                        CostDetail.AnalystDuration = BD;
                        CostDetail.TechResource = item.TechResource;
                        CostDetail.AnalystResource = item.AnalystResource;
                        CostDetail.DeveloperDuration = item.DeveloperDuration;
                        CostDetail.BADuration = item.BADuration;
                        CostDetail.QCDuration = item.QCDuration;
                        CostDetail.LeadDuration = item.LeadDuration;
                        CostDetail.DomainDuration = item.DomainDuration;
                        CostDetail.ModuleCost = item.ModuleCost;

                        if (CompltTechResource == null)
                            CompltTechResource = CostDetail.TechResource;
                        else
                            CompltTechResource = CompltTechResource + "," + CostDetail.TechResource;

                        if (CompltDomainResource == null)
                            CompltDomainResource = CostDetail.AnalystResource;
                        else
                            CompltDomainResource = CompltDomainResource + "," + CostDetail.AnalystResource;

                        lstCostCalculate.Add(new CostCalculation()
                        {
                            Module_Name = item.ModuleName,
                            Start_Date = item.StartDate,
                            Tech_Duration = item.TechDuration,
                            BA_Duration = item.AnalystDuration,
                            Developer_Duration = item.DeveloperDuration,
                            QC_Duration = item.QCDuration,
                            Lead_Duration = item.LeadDuration,
                            Domain_Duration = item.DomainDuration,
                            Project_Duration = item.ProjectDuration,
                            Module_Cost = item.ModuleCost
                        });

                        dc.tbl_ProjectEstimate.Add(CostDetail);
                        dc.tbl_module.Add(moduleDetails);
                        dc.SaveChanges();
                        //moduleID++;

                    }

                    string[] CompltTechResourceArray = CompltTechResource.Split(',');
                    CompltTechResourceArray = CompltTechResourceArray.Distinct().ToArray();
                    StringBuilder strBuild1=new StringBuilder();
                    for (int i = 0; i < CompltTechResourceArray.Length - 1; i++)
                    {
                        strBuild1.Append(CompltTechResourceArray[i]);
                        strBuild1.Append(",");
                    }
                    strBuild1.Append(CompltTechResourceArray[CompltTechResourceArray.Length - 1]);
                    CompltTechResource = strBuild1.ToString();

                    string[] CompltDomainResourceArray = CompltDomainResource.Split(',');
                    CompltDomainResourceArray = CompltDomainResourceArray.Distinct().ToArray();
                    StringBuilder strBuild2 = new StringBuilder();
                    for (int i = 0; i < CompltDomainResourceArray.Length - 1; i++)
                    {
                        strBuild2.Append(CompltDomainResourceArray[i]);
                        strBuild2.Append(",");
                    }
                    strBuild2.Append(CompltDomainResourceArray[CompltDomainResourceArray.Length - 1]);
                    CompltDomainResource = strBuild2.ToString();

                    dc.UpdateTechDomainResource(DefineProjectID, CompltTechResource, CompltDomainResource);

                    Session["ModuleExcelExportData"] = lstCostCalculate;
                }
                
                //return RedirectToAction("Home", "ModuleDashboard", new { id = Convert.ToInt32(Session["session_pid"]) });
               
            }
            else
            {
                
            }

            
           
        }

        public ActionResult StoreProjectCost(string ProjectCostDetails)
        {
            List<CostDefination> lstCostDefination = new List<CostDefination>();
            dynamic jsonDe = JsonConvert.DeserializeObject(ProjectCostDetails);

            lstCostDefination.Add(new CostDefination()
            {
                Tech_Cost = jsonDe.TC,
                BA_Cost = jsonDe.BAC,
                Lead_Cost = jsonDe.LC,
                Domain_Cost = jsonDe.DC,
                QC_Cost = jsonDe.QCC
            });



            Session["ProjectCostDefination"] = lstCostDefination;

            return View();
        }

        public ActionResult AddModuledetail(string ModuleCostDetails)
        {


            dynamic jsonDe = JsonConvert.DeserializeObject(ModuleCostDetails);
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                tbl_ProjectEstimate CostDetail = new tbl_ProjectEstimate();
                tbl_module moduleDetails = new tbl_module();
                int DefineProjectID = Convert.ToInt32(Session["session_pid"]);
                var moduleID = Convert.ToInt32(dc.tbl_ProjectEstimate.Where(x => x.ProjectID == Convert.ToInt32(Session["session_pid"])).OrderByDescending(x => x.ModuleID).ToList().Max());

                foreach (var item in jsonDe)
                {
                    moduleDetails.ModuleID = moduleID;
                    moduleDetails.ProjectInfo = DefineProjectID;
                    moduleDetails.StartDate = item.Start_Date;
                    moduleDetails.ModuleTitle = item.Module_Name;
                    moduleDetails.ModuleWeight = "8.0";
                    moduleDetails.PercentComplete = "50";
                    moduleDetails.ModuleDescription = item.Module_Name;

                    CostDetail.ProjectID = DefineProjectID;
                    CostDetail.ModuleID = moduleID;
                    CostDetail.TechDuration = Convert.ToInt32(item.Tech_Duration);
                    CostDetail.AnalystResource = Convert.ToInt32(item.BA_Duration);
                    dc.tbl_ProjectEstimate.Add(CostDetail);
                    dc.tbl_module.Add(moduleDetails);
                    moduleID++;


                }
            }




            return RedirectToAction("/Home/ModuleDashboard?ID=" + Session["session_pid"] + "");
        }



    }
}

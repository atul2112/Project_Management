using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Management.Models;
using System.IO;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Net;
using Project_Management.CommonFunctions;
using ClosedXML.Excel;
using Newtonsoft.Json;
using System.Threading;

namespace Project_Management.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return RedirectToAction("Home");
        }


        public ActionResult Home()
        {
            if (Session["UserDetails"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult PopupModule()
        {
            return View();
        }


        [ChildActionOnly]
        public ActionResult ShowProjects(tbl_projectdetails p)
        {
            List<snapModels.snap_projectDetails> pm = new List<snapModels.snap_projectDetails>();
            List<ProjectDetailsTbl> lpd = new List<ProjectDetailsTbl>();
            List<ProjectInfoTbl> lpi = new List<ProjectInfoTbl>();
            List<tbl_projectdetails> pd = new List<tbl_projectdetails>();
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                Userdetails UD = (Userdetails)Session["UserDetails"];
                var query = dc.tbl_projectdetails.OrderBy(x => x.StartDate).ToList();

                //.....................User Filter Logic..............//
                string projectIDs = null;
                var moduleQuery = dc.tbl_ProjectEstimate.ToList();
                foreach (var item in moduleQuery)
                {
                    if ((item.TechResource != null && item.TechResource.Contains(UD.UserID)) || (item.AnalystResource != null && item.AnalystResource.Contains(UD.UserID)))
                    {
                        if (projectIDs == null)
                            projectIDs = item.ProjectID.ToString();
                        else
                            projectIDs = projectIDs + "," + item.ProjectID.ToString();
                    }
                }
                if (projectIDs != null)
                {
                    string[] projectIDsArray = projectIDs.Split(',');
                    projectIDsArray = projectIDsArray.Distinct().ToArray();
                    StringBuilder strBuild = new StringBuilder();
                    for (int i = 0; i < projectIDsArray.Length - 1; i++)
                    {
                        strBuild.Append(projectIDsArray[i]);
                        strBuild.Append(",");
                    }
                    strBuild.Append(projectIDsArray[projectIDsArray.Length - 1]);
                    projectIDs = strBuild.ToString();
                }
                //......................................................//

                if (UD.RoleID != "6")
                {
                    foreach (var item in query)
                    {
                        //if (item.DomainResource.Contains(UD.UserID) || item.TechResource.Contains(UD.UserID) || item.CreatedBy.Contains(UD.UserID))
                        if ((projectIDs != null && projectIDs.Contains(item.ProjectID.ToString())) || item.CreatedBy.Contains(UD.UserID))
                        {
                            if (dc.tbl_projectdetails.AsEnumerable().Where(x => x.ProjectID == item.ProjectID).ToList().Count() != 0)
                            {
                                pm.Add(new snapModels.snap_projectDetails
                                {
                                    ProjectID = item.ProjectID
                                 ,
                                    Title = item.Title
                                 ,
                                    ProjectLogo = item.ProjectLogo
                                 ,
                                    CreatedBy = item.CreatedBy
                                 ,
                                    StartDate = item.StartDate
                                 ,
                                    ProjectType=item.ProjectType
                                 ,
                                    ClientName=item.ClientName
                                 ,
                                    Description = item.Description
                                 ,
                                    TechLeadName = (dc.UserInfoTbls.Where(x => x.UserID == item.TechLead).ToList().First()).FirstName
                                 ,
                                    DomainLeadName = (dc.UserInfoTbls.Where(x => x.UserID == item.DomainLead).ToList().First()).FirstName
                                 ,
                                    QCLeadName = (dc.UserInfoTbls.Where(x => x.UserID == item.QCLead).ToList().First()).FirstName

                                });
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in query)
                    {
                        if (dc.tbl_projectdetails.AsEnumerable().Where(x => x.ProjectID == item.ProjectID).ToList().Count() != 0)
                        {
                            pm.Add(new snapModels.snap_projectDetails
                            {
                                ProjectID = item.ProjectID
                             ,
                                Title = item.Title
                             ,
                                ProjectLogo = item.ProjectLogo
                             ,
                                CreatedBy = item.CreatedBy
                             ,
                                StartDate = item.StartDate
                             ,
                                ProjectType = item.ProjectType
                             ,
                                ClientName = item.ClientName
                             ,
                                Description = item.Description
                             ,
                                TechLeadName = (dc.UserInfoTbls.Where(x => x.UserID == item.TechLead).ToList().First()).FirstName
                             ,
                                DomainLeadName = (dc.UserInfoTbls.Where(x => x.UserID == item.DomainLead).ToList().First()).FirstName
                             ,
                                QCLeadName = (dc.UserInfoTbls.Where(x => x.UserID == item.QCLead).ToList().First()).FirstName

                            });
                        }
                    }
                }
            }
            if (Session["projectImgSession"] != null)
            {
                Session["projectImgSession"] = null;
            }

            return PartialView(pm);
        }

        [HttpPost]
        public ActionResult GetDomainTeam(string optional)
        {
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                List<TeamTbl> dquery = new List<TeamTbl>(); //To be returned
                var query = dc.TeamTbls.ToList();
                foreach (var v in query)
                {
                    bool b = v.TeamName.Contains("Tech");
                    if (!b)
                    {
                        dquery.Add(new TeamTbl { TeamID = v.TeamID, TeamName = v.TeamName });
                    }
                }
                ModelState.Clear();
                return Json(dquery, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetTechTeam(string optional)
        {
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                List<TeamTbl> dquery = new List<TeamTbl>(); //To be returned
                var query = dc.TeamTbls.ToList();
                foreach (var v in query)
                {
                    bool b = v.TeamName.Contains("Tech");
                    if (b)
                    {
                        dquery.Add(new TeamTbl { TeamID = v.TeamID, TeamName = v.TeamName });
                    }
                }
                ModelState.Clear();
                return Json(dquery, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost] //...Done
        public ActionResult GetDomainLead(string optional)
        {
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                var id = Int32.Parse(optional);
                List<UserInfoTbl> dquery = new List<UserInfoTbl>(); //To be returned
                var query = dc.UserInfoTbls.Where(x => x.TeamID == id && x.DesignationID == 6).ToList();
                foreach (var v in query)
                {
                    dquery.Add(new UserInfoTbl { UserID = v.UserID, Username = v.Username });
                }
                ModelState.Clear();
                return Json(dquery, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost] //...Done
        public ActionResult GetTechLead(string optional)
        {
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                var id = Int32.Parse(optional);
                List<UserInfoTbl> dquery = new List<UserInfoTbl>(); //To be returned
                var query = dc.UserInfoTbls.Where(x => x.TeamID == id && x.DesignationID == 3).ToList();
                foreach (var v in query)
                {
                    dquery.Add(new UserInfoTbl { UserID = v.UserID, Username = v.Username });
                }
                ModelState.Clear();
                return Json(dquery, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost] //...Done
        public ActionResult GetDomainResource()
        {
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                var id = Int32.Parse(Session["session_pid"].ToString());
                List<UserInfoTbl> dquery = new List<UserInfoTbl>(); //To be returned
                //var techTeamID = dc.tbl_projectdetails.Where(x => x.ProjectID == id).First().TechTeam;
                var domainTeamID = dc.tbl_projectdetails.Where(x => x.ProjectID == id).First().DomainTeam;
                var query = dc.UserInfoTbls.Where(x => x.TeamID == domainTeamID && x.DesignationID == 2).ToList();
                foreach (var v in query)
                {
                    dquery.Add(new UserInfoTbl { UserID = v.UserID, Username = v.Username });
                }
                ModelState.Clear();
                return Json(dquery, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost] //...Done
        public ActionResult GetTechResource()
        {
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                var id = Int32.Parse(Session["session_pid"].ToString());
                List<UserInfoTbl> dquery = new List<UserInfoTbl>(); //To be returned
                var techTeamID = dc.tbl_projectdetails.Where(x => x.ProjectID == id).First().TechTeam;
                //var domainTeamID = dc.tbl_projectdetails.Where(x => x.ProjectID == id).First().DomainTeam;
                var query = dc.UserInfoTbls.Where(x => x.TeamID == techTeamID && x.DesignationID == 1).ToList();
                foreach (var v in query)
                {
                    dquery.Add(new UserInfoTbl { UserID = v.UserID, Username = v.Username });
                }
                ModelState.Clear();
                return Json(dquery, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost] //To be Update
        public ActionResult GetQCLead(string optional)
        {
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                var id = Int32.Parse(optional);
                List<UserInfoTbl> dquery = new List<UserInfoTbl>(); //To be returned
                var query = dc.UserInfoTbls.Where(x => x.TeamID == id && x.DesignationID == 9).ToList();
                foreach (var v in query)
                {
                    dquery.Add(new UserInfoTbl { UserID = v.UserID, Username = v.Username });
                }
                ModelState.Clear();
                return Json(dquery, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddProject()
        {
            Session["dialog"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteProject(string id) //to be done
        {
            try
            {
                using (test_pmEntities2 dc = new test_pmEntities2())
                {
                    List<ProjectModel> dquery = new List<ProjectModel>();
                    dc.DeleteProject(Int32.Parse(id));
                    dc.DeleteMom(id);
                    dc.DeleteModule(Int32.Parse(id));
                    dc.DeleteCost(Int32.Parse(id));
                    dc.DeleteProjectEstimate(Int32.Parse(id));
                    dquery.Add(new ProjectModel { ProjectName = "success" });
                    return Json(dquery, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public ActionResult AddCost()
        {
            return View();
        }

        [HttpPost]
        public void UpdateProjectProfile(int id)
        {
            try
                {
                    var allowedExtensions = new[] { ".JPG", ".Jpg", ".png", ".jpg", ".jpeg" };
                    HttpFileCollectionBase files = (HttpFileCollectionBase)Session["projectImgSession"];
                    //for (var i = 0; i < files.Count; i++)
                    //{
                    //    HttpPostedFileBase file = files[i];
                    //    var filename = Path.GetFileName(file.FileName);
                    //    var ext = Path.GetExtension(file.FileName);
                    //    if (allowedExtensions.Contains(ext))
                    //    {
                    //        string name = Path.GetFileNameWithoutExtension(filename);
                    //        string newfilename = name + "_" + detail.ProjectID + ext;
                    //        var path = Path.Combine(Server.MapPath("~/Profiles"), newfilename);
                    //        detail.ProjectLogo = "/Profiles/" + newfilename;

                    //        file.SaveAs(path);
                    //        Session["dialog"] = "New Project added succesfully";
                    //        //return View();
                    //    }
                    //    else
                    //    {
                    //        ViewBag.Message = "Please choose only (.Jpg/.jpg/.png/jpeg) type file";
                    //        //return View();
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    //return
                }
        }

        [HttpPost]
        public void SaveProjectProfile()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    Session["projectImgSession"] = files;
                }
                catch (Exception ex)
                {
                    //return
                }
            }
            else
            {
                //return
            }
        }

        [HttpPost]
        public int SaveProject(string projectDetails)//(string title, string description, string type, int domainteam, int domainlead, int techteam, int techlead, int qclead, System.DateTime startdate)
        {
            int rpID = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    dynamic jsonDe = JsonConvert.DeserializeObject(projectDetails);
                    foreach (var detailItem in jsonDe)
                    {
                        using (test_pmEntities2 dc = new test_pmEntities2())
                        {
                            var DefineProjectID = 1;
                            var pid = dc.tbl_projectdetails.OrderByDescending(x => x.ProjectID).ToList().FirstOrDefault();
                            tbl_projectdetails detail = new tbl_projectdetails();
                            if (pid != null)
                            {
                                detail.ProjectID = pid.ProjectID + 1;
                                DefineProjectID = pid.ProjectID + 1;
                                
                            }
                            else
                            {
                                detail.ProjectID = DefineProjectID;

                            }
                            rpID = detail.ProjectID;
                            var allowedExtensions = new[] {".JPG", ".Jpg", ".png", ".jpg", ".jpeg" };

                            detail.Title = detailItem.Title;
                            detail.Description = detailItem.Description;
                            detail.DomainTeam = detailItem.DomainTeam;
                            detail.DomainLead = detailItem.DomainLead;
                            detail.TechLead = detailItem.TechLead;
                            detail.QCLead = detailItem.QCLead;
                            detail.DomainResource = "N/A";//fc["DomainResource"];
                            detail.TechResource = "N/A";//fc["TechResource"];
                            detail.ProjectType = detailItem.ProjectType;

                            detail.Modules = "N/A";// fc["Modules"];
                            detail.EstimatedCost = "N/A";// fc["EstimatedCost"];
                            detail.ProjectDetails = "N/A";// fc["ProjectDetails"];
                            if (detailItem.StartDate != "")
                            {
                                System.DateTime start_dt = detailItem.StartDate;
                                detail.StartDate = (start_dt - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();// fc["StartDate"];
                            }
                            else
                                detail.StartDate = "N/A";
                            detail.Duration = "N/A";// fc["Duration"];
                            detail.TechTeam = detailItem.TechTeam;
                            detail.CreatedDate = (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
                            Userdetails UD = (Userdetails)Session["UserDetails"];
                            detail.CreatedBy = UD.UserID;

                            detail.ClientName = detailItem.ClientName;
                            detail.TotalCost = detailItem.TotalCost;
                            detail.CurrencyType = detailItem.CurrencyType;

                            if (Session["ProjectCostDefination"] != null)
                            {
                                List<CostDefination> CostDefination = Session["ProjectCostDefination"] as List<CostDefination>;
                                tbl_Cost CostDetail = new tbl_Cost();
                                CostDetail.ProjectID = DefineProjectID;
                                CostDetail.TechEstimate = Convert.ToInt32(CostDefination[0].Tech_Cost);
                                CostDetail.BAEstimate = Convert.ToInt32(CostDefination[0].BA_Cost);
                                CostDetail.TechLeadEstimate = Convert.ToInt32(CostDefination[0].Lead_Cost);
                                CostDetail.DomainEstimate = Convert.ToInt32(CostDefination[0].Domain_Cost);
                                CostDetail.QCEstimate = Convert.ToInt32(CostDefination[0].QC_Cost);
                                dc.tbl_Cost.Add(CostDetail);
                            }

                            if (Session["projectImgSession"] != null)
                            {
                                HttpFileCollectionBase files = (HttpFileCollectionBase)Session["projectImgSession"];
                                for (var i = 0; i < files.Count; i++)
                                {
                                    HttpPostedFileBase file = files[i];
                                    var filename = Path.GetFileName(file.FileName);
                                    var ext = Path.GetExtension(file.FileName);
                                    if (allowedExtensions.Contains(ext))
                                    {
                                        string name = Path.GetFileNameWithoutExtension(filename);
                                        string newfilename = name + "_" + detail.ProjectID + ext;
                                        var path = Path.Combine(Server.MapPath("~/Profiles"), newfilename);
                                        detail.ProjectLogo = "/Profiles/" + newfilename;

                                        file.SaveAs(path);
                                        Session["dialog"] = "New Project added succesfully";
                                        //return View();
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Please choose only (.Jpg/.jpg/.png/jpeg) type file";
                                        //return View();
                                    }
                                }
                            }
                            else
                            {
                                detail.ProjectLogo = "/Profiles/NoImage.jpg";
                                dc.tbl_projectdetails.Add(detail);

                                Session["dialog"] = "New Project added succesfully";
                                //return View();
                            }

                            dc.tbl_projectdetails.Add(detail);
                            if (Session["ModuleCostCalculation"] != null)
                            {
                                List<CostCalculation> CostCalculation = Session["ModuleCostCalculation"] as List<CostCalculation>;
                                tbl_ProjectEstimate CostDetail = new tbl_ProjectEstimate();
                                tbl_module moduleDetails = new tbl_module();
                                int moduleID = 1;
                                foreach (var item in CostCalculation)
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
                                    CostDetail.AnalystDuration = Convert.ToInt32(item.BA_Duration);

                                    dc.tbl_ProjectEstimate.Add(CostDetail);
                                    dc.tbl_module.Add(moduleDetails);

                                    moduleID++;
                                    dc.SaveChanges();
                                }
                            }
                            else
                            {
                                dc.SaveChanges();
                            }
                        }
                    }
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
            }
            return rpID;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProject(FormCollection fc, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (test_pmEntities2 dc = new test_pmEntities2())
                    {
                        var DefineProjectID = 1;
                        var pid = dc.tbl_projectdetails.OrderByDescending(x => x.ProjectID).ToList().FirstOrDefault();
                        tbl_projectdetails detail = new tbl_projectdetails();
                        if (pid != null)
                        {
                            detail.ProjectID = pid.ProjectID + 1;
                            DefineProjectID = pid.ProjectID + 1;
                        }
                        else
                        {
                            detail.ProjectID = DefineProjectID;

                        }
                        var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", ".jpeg" };
                        detail.Title = fc["Title"];

                        detail.Description = fc["Description"];
                        detail.DomainTeam = Int32.Parse(fc["DomainTeam"]);
                        detail.DomainLead = Int32.Parse(fc["DomainLead"]);
                        detail.TechLead = Int32.Parse(fc["TechLead"]);
                        detail.QCLead = Int32.Parse(fc["QCLead"]);
                        detail.DomainResource = "N/A";//fc["DomainResource"];
                        detail.TechResource = "N/A";//fc["TechResource"];
                        detail.ProjectType = fc["ProjectType"];

                        detail.Modules = "N/A";// fc["Modules"];
                        detail.EstimatedCost = "N/A";// fc["EstimatedCost"];
                        detail.ProjectDetails = "N/A";// fc["ProjectDetails"];
                        System.DateTime sDate = DateTime.Parse(fc["StartDate"]);
                        detail.StartDate = (sDate - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();// fc["StartDate"];
                        detail.Duration = "N/A";// fc["Duration"];
                        detail.TechTeam = Int32.Parse(fc["TechTeam"]);
                        detail.CreatedDate = (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
                        Userdetails UD = (Userdetails)Session["UserDetails"];
                        detail.CreatedBy = UD.UserID;

                        if (Session["ProjectCostDefination"] != null)
                        {
                            List<CostDefination> CostDefination = Session["ProjectCostDefination"] as List<CostDefination>;
                            tbl_Cost CostDetail = new tbl_Cost();
                            CostDetail.ProjectID = DefineProjectID;
                            CostDetail.TechEstimate = Convert.ToInt32(CostDefination[0].Tech_Cost);
                            CostDetail.BAEstimate = Convert.ToInt32(CostDefination[0].BA_Cost);
                            CostDetail.TechLeadEstimate = Convert.ToInt32(CostDefination[0].Lead_Cost);
                            CostDetail.DomainEstimate = Convert.ToInt32(CostDefination[0].Domain_Cost);
                            CostDetail.QCEstimate = Convert.ToInt32(CostDefination[0].QC_Cost);
                            dc.tbl_Cost.Add(CostDetail);
                        }

                        if (file != null)
                        {
                            var filename = Path.GetFileName(file.FileName);
                            var ext = Path.GetExtension(file.FileName);
                            if (allowedExtensions.Contains(ext))
                            {
                                string name = Path.GetFileNameWithoutExtension(filename);
                                string newfilename = name + "_" + detail.ProjectID + ext;
                                var path = Path.Combine(Server.MapPath("~/Profiles"), newfilename);
                                detail.ProjectLogo = "/Profiles/" + newfilename;

                                file.SaveAs(path);
                                Session["dialog"] = "New Project added succesfully";
                                //return View();
                            }
                            else
                            {
                                ViewBag.Message = "Please choose only (.Jpg/.jpg/.png/jpeg) type file";
                                //return View();
                            }
                        }
                        else
                        {
                            detail.ProjectLogo = "/Profiles/NoImage.jpg";
                            dc.tbl_projectdetails.Add(detail);

                            Session["dialog"] = "New Project added succesfully";
                            //return View();
                        }

                        dc.tbl_projectdetails.Add(detail);
                        if (Session["ModuleCostCalculation"] != null)
                        {
                            List<CostCalculation> CostCalculation = Session["ModuleCostCalculation"] as List<CostCalculation>;
                            tbl_ProjectEstimate CostDetail = new tbl_ProjectEstimate();
                            tbl_module moduleDetails = new tbl_module();
                            int moduleID = 1;
                            foreach (var item in CostCalculation)
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
                                CostDetail.AnalystDuration = Convert.ToInt32(item.BA_Duration);

                                dc.tbl_ProjectEstimate.Add(CostDetail);
                                dc.tbl_module.Add(moduleDetails);

                                moduleID++;
                                dc.SaveChanges();
                            }
                        }
                        else
                        {
                            dc.SaveChanges();
                        }
                        return RedirectToAction("Home", "Home");


                    }
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
            }
            return View();
        }

        public ActionResult AddView(int id)  //
        {
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                ProjectDashboard obj = new ProjectDashboard();
                var pid = id.ToString();
                var query = dc.tbl_projectdetails.Where(x => x.ProjectID == id).ToList().First();
                var ModuleQuery = dc.tbl_module.Where(x => x.ProjectInfo == id).ToList();
                var MomQuery = dc.tbl_mom.Where(x => x.ProjectInfo == pid).ToList();
                obj.ModuleCount = ModuleQuery.Count;
                obj.MomCount = MomQuery.Count;
                int tildCount = 0;
                foreach (var item in ModuleQuery)
                {
                    if (item.ModuleFiles != null)
                    {
                        string File = item.ModuleFiles;
                        foreach (char c in File)
                        {
                            if (c == '~')
                                tildCount++;
                        }
                        tildCount++;
                    }
                }
                foreach (var item in MomQuery)
                {
                    if (item.MomFile != null)
                    {
                        string File = item.MomFile;
                        foreach (char c in File)
                        {
                            if (c == '~')
                                tildCount++;
                        }
                        tildCount++;
                    }
                }
                obj.FileCount = tildCount;
                List<ModulesData> mdobj = new List<ModulesData>();
                foreach (var item in ModuleQuery)
                {
                    ModulesData mobj = new ModulesData();
                    mobj.ModuleID = item.ModuleID;
                    mobj.ModuleTitle = item.ModuleTitle;
                    mobj.ModuleWeight = item.ModuleWeight;
                    mobj.PercentComplete = item.PercentComplete;
                    mobj.ModuleTeamInfo = item.ModuleTeamInfo;
                    mobj.ProjectInfo = item.ProjectInfo;
                    mobj.StartDate = item.StartDate;
                    mobj.EndDate = item.EndDate;
                    mobj.ModuleDescription = item.ModuleDescription;
                    mobj.ModuleFiles = item.ModuleFiles;
                    mobj.TotalDays = (Int32.Parse(dc.tbl_ProjectEstimate.Where(x => x.ModuleID == item.ModuleID && x.ProjectID == item.ProjectInfo).Select(x => x.TechDuration).ToList().First().ToString())) * 5 + (Int32.Parse(dc.tbl_ProjectEstimate.Where(x => x.ModuleID == item.ModuleID && x.ProjectID == item.ProjectInfo).Select(x => x.AnalystDuration).ToList().First().ToString())) * 5;
                    //obj.Modules.Add(mobj);
                    mdobj.Add(mobj);
                }
                obj.Modules = mdobj;
                Session["session_pid"] = id;
                Session["session_pname"] = query.Title;
                return View(obj);
            }
        }

        public ActionResult ModuleDashboard(int id)
        {
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                List<moduleDashboard> lst_obj = new List<moduleDashboard>();
                var query = dc.tbl_projectdetails.Where(x => x.ProjectID == id).ToList().First();
                var dquery = dc.tbl_module.Where(x => x.ProjectInfo == id).OrderBy(x => x.StartDate).ToList();
                Userdetails UD = (Userdetails)Session["UserDetails"];

                //.....................User Filter Logic..............//
                string moduleIDs = null;
                var moduleQuery = dc.tbl_ProjectEstimate.Where(x => x.ProjectID == id).ToList();
                foreach (var item in moduleQuery)
                {
                    if ((item.TechResource != null && item.TechResource.Contains(UD.UserID)) || (item.AnalystResource != null && item.AnalystResource.Contains(UD.UserID)))
                    {
                        if (moduleIDs == null)
                            moduleIDs = item.ModuleID.ToString();
                        else
                            moduleIDs = moduleIDs + "," + item.ModuleID.ToString();
                    }
                }
                if (moduleIDs != null)
                {
                    string[] moduleIDsArray = moduleIDs.Split(',');
                    moduleIDsArray = moduleIDsArray.Distinct().ToArray();
                    StringBuilder strBuild = new StringBuilder();
                    for (int i = 0; i < moduleIDsArray.Length - 1; i++)
                    {
                        strBuild.Append(moduleIDsArray[i]);
                        strBuild.Append(",");
                    }
                    strBuild.Append(moduleIDsArray[moduleIDsArray.Length - 1]);
                    moduleIDs = strBuild.ToString();
                }
                //......................................................//

                if (UD.DesignationID == "3" || UD.DesignationID == "5" || UD.DesignationID == "7" || UD.DesignationID == "8")
                {
                    foreach (var item in dquery)
                    {
                        moduleDashboard obj = new moduleDashboard();
                        obj.ModuleID = item.ModuleID;
                        obj.ModuleTitle = item.ModuleTitle;
                        obj.ModuleWeight = item.ModuleWeight;
                        obj.PercentComplete = item.PercentComplete;
                        obj.ModuleTeamInfo = item.ModuleTeamInfo;
                        obj.ProjectInfo = item.ProjectInfo;
                        obj.StartDate = item.StartDate;
                        obj.EndDate = item.EndDate;
                        obj.ModuleDescription = item.ModuleDescription;
                        obj.ModuleFiles = item.ModuleFiles;
                        var mquery = dc.tbl_ProjectEstimate.Where(x => x.ModuleID == obj.ModuleID).FirstOrDefault();
                        obj.TotalDays = (mquery.TechDuration + mquery.AnalystDuration) * 5;
                        obj.TechDuration = mquery.TechDuration;
                        obj.AnalystDuration = mquery.AnalystDuration;
                        obj.TechResource = mquery.TechResource;
                        obj.AnalystResource = mquery.AnalystResource;
                        obj.BADuration = mquery.BADuration;
                        obj.DeveloperDuration = mquery.DeveloperDuration;
                        obj.DomainDuration = mquery.DomainDuration;
                        obj.QCDuration = mquery.QCDuration;
                        obj.LeadDuration = mquery.LeadDuration;
                        obj.ModuleCost = mquery.ModuleCost;
                        lst_obj.Add(obj);
                    }
                }
                else
                {
                    foreach (var item in dquery)
                    {
                        if (moduleIDs != null && moduleIDs.Contains(item.ModuleID.ToString()))
                        {
                            moduleDashboard obj = new moduleDashboard();
                            obj.ModuleID = item.ModuleID;
                            obj.ModuleTitle = item.ModuleTitle;
                            obj.ModuleWeight = item.ModuleWeight;
                            obj.PercentComplete = item.PercentComplete;
                            obj.ModuleTeamInfo = item.ModuleTeamInfo;
                            obj.ProjectInfo = item.ProjectInfo;
                            obj.StartDate = item.StartDate;
                            obj.EndDate = item.EndDate;
                            obj.ModuleDescription = item.ModuleDescription;
                            obj.ModuleFiles = item.ModuleFiles;
                            var mquery = dc.tbl_ProjectEstimate.Where(x => x.ModuleID == obj.ModuleID).FirstOrDefault();
                            obj.TotalDays = (mquery.TechDuration + mquery.AnalystDuration) * 5;
                            obj.TechDuration = mquery.TechDuration;
                            obj.AnalystDuration = mquery.AnalystDuration;
                            obj.TechResource = mquery.TechResource;
                            obj.AnalystResource = mquery.AnalystResource;
                            obj.BADuration = mquery.BADuration;
                            obj.DeveloperDuration = mquery.DeveloperDuration;
                            obj.DomainDuration = mquery.DomainDuration;
                            obj.QCDuration = mquery.QCDuration;
                            obj.LeadDuration = mquery.LeadDuration;
                            obj.ModuleCost = mquery.ModuleCost;
                            lst_obj.Add(obj);
                        }
                    }
                }

                //Session["session_pid"] = id;
                //Session["session_pname"] = query.Title;
                return View(lst_obj);
            }
        }

        [HttpPost]
        public ActionResult GetModule(string pid)
        {
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                List<tbl_module> dquery = new List<tbl_module>();
                dquery.Add(new tbl_module { PercentComplete = "60%" });
                return Json(dquery, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddModule()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteModule(string id)
        {
            try
            {
                using (test_pmEntities2 dc = new test_pmEntities2())
                {
                    List<tbl_module> dquery = new List<tbl_module>();
                    dc.DeleteModuleByModuleID(Int32.Parse(id));
                    dc.DeleteProjectEstimateByModuleID(Int32.Parse(id));
                    dquery.Add(new tbl_module { ModuleTitle = "success" });
                    return Json(dquery, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult AddModule(string mTitle, string mWeight, string mList, string mDescription, System.DateTime sDate, System.DateTime eDate, string mPercent)
        {
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                List<tbl_module> dquery = new List<tbl_module>();
                var moduleID = dc.tbl_module.OrderByDescending(x => x.ModuleID).ToList().FirstOrDefault();
                tbl_module module = new tbl_module();
                if (moduleID != null)
                {
                    module.ModuleID = moduleID.ModuleID + 1;
                }
                else
                {
                    module.ModuleID = 1;
                }
                module.ModuleTitle = mTitle;
                module.ModuleWeight = mWeight;
                module.ModuleTeamInfo = mList;
                module.ModuleDescription = mDescription;
                module.StartDate = (sDate - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
                module.EndDate = (eDate - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
                module.PercentComplete = mPercent;
                module.ProjectInfo = Int32.Parse(Session["session_pid"].ToString());
                dquery.Add(new tbl_module { ModuleID = module.ModuleID, ModuleTitle = module.ModuleTitle, ModuleTeamInfo = module.ModuleTeamInfo, ProjectInfo = module.ProjectInfo });
                dc.tbl_module.Add(module);
                dc.SaveChanges();
                return Json(dquery, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult AddMom()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMom(String comment)
        {
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                List<tbl_mom> dquery = new List<tbl_mom>();
                var momID = dc.tbl_mom.OrderByDescending(x => x.MomID).ToList().FirstOrDefault();
                tbl_mom mom = new tbl_mom();
                if (momID != null)
                {
                    mom.MomID = (momID.MomID) + 1;
                }
                else
                {
                    mom.MomID = 1;
                }

                if (Session["momFileSession"] != null)
                {
                    HttpFileCollectionBase files = (HttpFileCollectionBase)Session["momFileSession"];
                    string filepath = null;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase momFile = files[i];
                        var filename = Path.GetFileName(momFile.FileName);
                        //var ext = Path.GetExtension(momFile.FileName);
                        //string newfilename = mom.MomID + ext;
                        string mappath = "~/ProjectDocs/" + Session["session_pid"].ToString() + "/" + mom.MomID.ToString();
                        bool dirExists = Directory.Exists(Server.MapPath(mappath));
                        if (!dirExists)
                            Directory.CreateDirectory(Server.MapPath(mappath));
                        var path = Path.Combine(Server.MapPath(mappath), filename);
                        if (filepath == null)
                            filepath = "/ProjectDocs/" + Session["session_pid"].ToString() + "/" + mom.MomID.ToString() + "/" + filename;
                        else
                            filepath = filepath + "~" + "/ProjectDocs/" + Session["session_pid"].ToString() + "/" + mom.MomID.ToString() + "/" + filename;
                        //mom.MomFile = "/ProjectDocs/" + Session["session_pid"].ToString() + "/" + mom.MomID.ToString() + "/" + filename;
                        momFile.SaveAs(path);
                        Session["momFileSession"] = null;
                    }
                    mom.MomFile = filepath;
                }
                else
                {
                    mom.MomFile = null;
                }
                mom.ProjectInfo = Session["session_pid"].ToString();
                mom.TimeStamp = (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
                mom.MomContent = comment.ToString();
                dc.tbl_mom.Add(mom);
                dc.SaveChanges();
                var query = dc.tbl_mom.Where(x => x.ProjectInfo == mom.ProjectInfo).OrderByDescending(x => x.TimeStamp).ToList();
                foreach (var v in query)
                {
                    string s = v.MomContent.Replace("?", "");
                    dquery.Add(new tbl_mom { MomID = v.MomID, ProjectInfo = v.ProjectInfo, TimeStamp = v.TimeStamp, MomContent = s, MomFile = v.MomFile });
                }
                ModelState.Clear();
                return Json(dquery, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetAllMom(String opt)
        {
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                List<tbl_mom> dquery = new List<tbl_mom>(); //To be returned
                var projectID = Session["session_pid"].ToString();
                var query = dc.tbl_mom.Where(x => x.ProjectInfo == projectID).OrderByDescending(x => x.TimeStamp).ToList();
                foreach (var v in query)
                {
                    string s = v.MomContent.Replace("?", "");
                    dquery.Add(new tbl_mom { MomID = v.MomID, ProjectInfo = v.ProjectInfo, TimeStamp = v.TimeStamp, MomContent = s, MomFile = v.MomFile });
                }
                ModelState.Clear();
                return Json(dquery, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DateFilter(System.DateTime startDate, System.DateTime endDate)
        {
            string startEpoch = (startDate - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
            string endEpoch = (endDate.AddDays(1) - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
            var projectID = Session["session_pid"].ToString();
            List<tbl_mom> dquery = new List<tbl_mom>();
            using (SqlConnection connection = new SqlConnection("Server=192.168.0.202;Database=test_pm;User Id=test_pm;Password=test.123"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM tbl_mom where TimeStamp>='" + startEpoch + "' and TimeStamp<='" + endEpoch + "' and ProjectInfo='" + projectID + "' order by TimeStamp desc", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string s = reader["MomContent"].ToString().Replace("?", "");
                        dquery.Add(new tbl_mom { MomID = Int32.Parse(reader["MomId"].ToString()), ProjectInfo = reader["ProjectInfo"].ToString(), TimeStamp = reader["TimeStamp"].ToString(), MomContent = s, MomFile = reader["MomFile"].ToString() });
                    }
                }

                return Json(dquery, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public void UploadFiles()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object 
                    //if (Session["momFileSession"] != null)
                    //{
                    //    List<HttpFileCollectionBase> fileList = (List<HttpFileCollectionBase>)Session["momFileSession"];
                    //    HttpFileCollectionBase files = Request.Files;
                    //    fileList.Add(files);
                    //    Session["momFileSession"] = fileList;
                    //}
                    //else
                    //{
                    List<HttpFileCollectionBase> fileList = new List<HttpFileCollectionBase>();
                    HttpFileCollectionBase files = Request.Files;
                    fileList.Add(files);
                    Session["momFileSession"] = files;
                    //}

                }
                catch (Exception ex)
                {
                    //return
                }
            }
            else
            {
                //return
            }
        }


        [HttpPost]
        public void StoreModuleID(string mid)
        {
            Session["ModuleIDSession"] = mid;
        }

        [HttpPost]
        public void UploadModlueFiles()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    using (test_pmEntities2 dc = new test_pmEntities2())
                    {
                        HttpFileCollectionBase files = Request.Files;
                        string filepath = null;
                        for (int i = 0; i < files.Count; i++)
                        {
                            HttpPostedFileBase moduleFile = files[i];
                            var filename = Path.GetFileName(moduleFile.FileName);
                            string mappath = "~/ProjectDocs/" + Session["session_pid"].ToString() + "/ModuleFiles/" + Session["ModuleIDSession"].ToString();
                            bool dirExists = Directory.Exists(Server.MapPath(mappath));
                            if (!dirExists)
                                Directory.CreateDirectory(Server.MapPath(mappath));
                            var path = Path.Combine(Server.MapPath(mappath), filename);
                            if (filepath == null)
                                filepath = "/ProjectDocs/" + Session["session_pid"].ToString() + "/ModuleFiles/" + Session["ModuleIDSession"].ToString() + "/" + filename;
                            else
                                filepath = filepath + "~" + "/ProjectDocs/" + Session["session_pid"].ToString() + "/ModuleFiles/" + Session["ModuleIDSession"].ToString() + "/" + filename;

                            moduleFile.SaveAs(path);
                        }
                        int mID = Int32.Parse(Session["ModuleIDSession"].ToString());
                        string f = dc.tbl_module.Where(x => x.ModuleID == mID).Select(x => x.ModuleFiles).ToList().First();
                        if (f != null)
                            filepath = f + "~" + filepath;
                        dc.UpdateModuleFile(Int32.Parse(Session["ModuleIDSession"].ToString()), filepath);
                    }
                }
                catch (Exception ex)
                {
                    //return
                }
            }
            else
            {
                //return
            }
        }

        [HttpPost]
        public ActionResult GetModuleFiles(string mid)
        {
            int ID = Int32.Parse(mid);
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                var query = dc.tbl_module.Where(x => x.ModuleID == ID).ToList();
                List<tbl_module> dquery = new List<tbl_module>();
                foreach (var q in query)
                {
                    string fileString = q.ModuleFiles;
                    int j = 0, startIndex = 0;
                    int tildCount = 0;
                    foreach (char c in fileString)
                    {
                        if (c == '~')
                            tildCount++;
                    }
                    tildCount++;
                    for (int i = 0; i <= fileString.Length; i++)
                    {
                        if (i != fileString.Length)
                        {
                            if (fileString[i] == '/')
                                j = i;
                            if (fileString[i] == '~' || i == fileString.Length)
                            {
                                dquery.Add(new tbl_module { ModuleID = Int32.Parse(mid), ModuleDescription = fileString.Substring(j + 1, i - j - 1), ModuleFiles = fileString.Substring(startIndex, i - startIndex) });
                                startIndex = i + 1;
                            }
                        }
                        else
                            dquery.Add(new tbl_module { ModuleID = Int32.Parse(mid), ModuleDescription = fileString.Substring(j + 1, i - j - 1), ModuleFiles = fileString.Substring(startIndex, i - startIndex) });
                    }
                }
                return Json(dquery, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetFiles(string momID)
        {
            int ID = Int32.Parse(momID);
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                var query = dc.tbl_mom.Where(x => x.MomID == ID).ToList();
                List<tbl_mom> dquery = new List<tbl_mom>();
                foreach (var q in query)
                {
                    string fileString = q.MomFile;
                    int j = 0, startIndex = 0;
                    int tildCount = 0;
                    foreach (char c in fileString)
                    {
                        if (c == '~')
                            tildCount++;
                    }
                    tildCount++;
                    for (int i = 0; i <= fileString.Length; i++)
                    {
                        if (i != fileString.Length)
                        {
                            if (fileString[i] == '/')
                                j = i;
                            if (fileString[i] == '~' || i == fileString.Length)
                            {
                                dquery.Add(new tbl_mom { MomID = Int32.Parse(momID), ProjectInfo = Session["session_pid"].ToString(), MomContent = fileString.Substring(j + 1, i - j - 1), MomFile = fileString.Substring(startIndex, i - startIndex), TimeStamp = q.TimeStamp });
                                startIndex = i + 1;
                            }
                        }
                        else
                            dquery.Add(new tbl_mom { MomID = Int32.Parse(momID), ProjectInfo = Session["session_pid"].ToString(), MomContent = fileString.Substring(j + 1, i - j - 1), MomFile = fileString.Substring(startIndex, i - startIndex), TimeStamp = q.TimeStamp });
                    }
                }
                return Json(dquery, JsonRequestBehavior.AllowGet);
            }
        }

        public void DownLoadFiles(string filename, string filepath)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename=" + filename);
                Response.TransmitFile(Server.MapPath(filepath));
                Response.End();
            }
            catch { 
            }
        }

        [HttpPost]
        public void DeleteFile(int moduleID, string filepath)
        {
            var path = Path.Combine(Server.MapPath(filepath));
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                string filesString = dc.tbl_module.Where(x => x.ModuleID == moduleID).Select(x => x.ModuleFiles).ToList().First();
                if (filesString.Contains(filepath))
                {
                    filesString = filesString.Replace(filepath, null);
                    if (filesString != "")
                    {
                        if (filesString[0] == '~')
                            filesString = filesString.Remove(0, 1);
                        if (filesString[filesString.Length - 1] == '~')
                            filesString = filesString.Remove(filesString.Length - 1, 1);
                        if (filesString.Contains("~~"))
                            filesString = filesString.Replace("~~", "~");
                        dc.UpdateModuleFile(moduleID, filesString);
                    }
                    if (filesString == "")
                        dc.UpdateModuleFile(moduleID, null);
                    
                }
            }
        }

        [HttpPost]
        public void DeleteMultipleFiles(string moduleFilesJSON)
        {
            dynamic jsonDe = JsonConvert.DeserializeObject(moduleFilesJSON);
            foreach (var item in jsonDe)
            {
                if (item.selected == "true")
                {
                    string filepath = item.filepath;
                    int moduleID = item.mid;
                    var path = Path.Combine(Server.MapPath(filepath));
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    using (test_pmEntities2 dc = new test_pmEntities2())
                    {
                        string filesString = dc.tbl_module.Where(x => x.ModuleID == moduleID).Select(x => x.ModuleFiles).ToList().First();
                        if (filesString.Contains(filepath))
                        {
                            filesString = filesString.Replace(filepath, null);
                            if (filesString != "")
                            {
                                if (filesString[0] == '~')
                                    filesString = filesString.Remove(0, 1);
                                if (filesString[filesString.Length - 1] == '~')
                                    filesString = filesString.Remove(filesString.Length - 1, 1);
                                if (filesString.Contains("~~"))
                                    filesString = filesString.Replace("~~", "~");
                                dc.UpdateModuleFile(moduleID, filesString);
                            }
                            if (filesString == "")
                                dc.UpdateModuleFile(moduleID, null);

                        }
                    }
                }
            }            
        }

        //.........For Showing File Table......//
        public ActionResult FilesGrid(string id) //id refer to project id
        {
            FileDashboard obj = new FileDashboard();
            var pID = Int32.Parse(id);
            //try
            //{
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                var projQuery = dc.tbl_projectdetails.Where(x => x.ProjectID == pID).FirstOrDefault();
                List<projects> projObj = new List<projects>();
                //projObj.Add(new projects { pFileName = "File 1", pFilePath = "File 1 Path" });
                //projObj.Add(new projects { pFileName = "File 2", pFilePath = "File 2 Path" });
                //projObj.Add(new projects { pFileName = "File 3", pFilePath = "File 3 Path" });
                //projObj.Add(new projects { pFileName = "File 4", pFilePath = "File 4 Path" });
                obj.pProject = projObj;

                var moduleQuery = dc.tbl_module.Where(x => x.ProjectInfo == pID).ToList();
                List<modules> moduleObj = new List<modules>();
                foreach (var item in moduleQuery)
                {
                    List<moduleData> md = new List<moduleData>();
                    string fileString = item.ModuleFiles;
                    int j = 0, startIndex = 0;
                    int tildCount = 0;
                    if (fileString != null)
                    {
                        foreach (char c in fileString)
                        {
                            if (c == '~')
                                tildCount++;
                        }
                        tildCount++;
                        for (int i = 0; i <= fileString.Length; i++)
                        {
                            if (i != fileString.Length)
                            {
                                if (fileString[i] == '/')
                                    j = i;
                                if (fileString[i] == '~' || i == fileString.Length)
                                {
                                    md.Add(new moduleData { mFileName = fileString.Substring(j + 1, i - j - 1), mPathName = fileString.Substring(startIndex, i - startIndex) });
                                    startIndex = i + 1;
                                }
                            }
                            else
                                md.Add(new moduleData { mFileName = fileString.Substring(j + 1, i - j - 1), mPathName = fileString.Substring(startIndex, i - startIndex) });
                        }
                        moduleObj.Add(new modules { mId = item.ModuleID, mName = item.ModuleTitle, mData = md });
                    }
                }
                obj.pModules = moduleObj;

                var momQuery = dc.tbl_mom.Where(x => x.ProjectInfo == id).ToList();
                List<moms> momObj = new List<moms>();
                foreach (var item in momQuery)
                {
                    List<momData> momd = new List<momData>(); ;
                    string fileString = item.MomFile;
                    int j = 0, startIndex = 0;
                    int tildCount = 0;
                    if (fileString != null)
                    {
                        foreach (char c in fileString)
                        {
                            if (c == '~')
                                tildCount++;
                        }
                        tildCount++;
                        for (int i = 0; i <= fileString.Length; i++)
                        {
                            if (i != fileString.Length)
                            {
                                if (fileString[i] == '/')
                                    j = i;
                                if (fileString[i] == '~' || i == fileString.Length)
                                {
                                    momd.Add(new momData { momFileName = fileString.Substring(j + 1, i - j - 1), momPathName = fileString.Substring(startIndex, i - startIndex) });
                                    startIndex = i + 1;
                                }
                            }
                            else
                                momd.Add(new momData { momFileName = fileString.Substring(j + 1, i - j - 1), momPathName = fileString.Substring(startIndex, i - startIndex) });
                        }
                        momObj.Add(new moms { momId = item.MomID, momsData = momd });
                    }
                }
                obj.pMoms = momObj;
            }
            //}
            //catch (Exception)
            //{

            //   throw;
            //}
            return View(obj);
        }

        //.........Excel Export..............//
        public void generateExcel()
        {
            try
            {
                //System.Data.DataTable dt = new System.Data.DataTable();
                //DataSet ds = (DataSet)HttpContext.Current.Session["Report"];
                //dt = ds.Tables[0];
                if (Session["ModuleExcelExportData"] != null)
                {
                    List<CostCalculation> ExcelData = Session["ModuleExcelExportData"] as List<CostCalculation>;
                    XLWorkbook wb = new XLWorkbook();
                    IXLWorksheet ws;
                    XLColor color = XLColor.Transparent;
                    ws = wb.AddWorksheet("Summary");

                    ws.ShowGridLines = false;
                    IXLRange rn;
                    int iRow = 0, iCol = 0, j = 0;
                    //ws.ColumnWidth = 25;
                    //ws.Column(1).Width = 50;

                    //.............Sheet Title.....................//
                    rn = ws.Range(iRow + 1, iCol + 1, iRow + 2, iCol + 20);
                    rn.Merge();
                    rn.Value = "Module Report";
                    rn.Style.Font.Bold = true;
                    rn.Style.Font.FontSize = 20;
                    rn.Style.Font.FontColor = XLColor.White;
                    rn.Style.Font.FontName = "Calibri";
                    rn.Style.Alignment.WrapText = true;
                    rn.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    rn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    rn.Style.Fill.BackgroundColor = XLColor.DarkCyan;
                    rn.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    rn.Style.Border.LeftBorderColor = XLColor.Black;
                    rn.Style.Border.RightBorderColor = XLColor.Black;
                    rn.Style.Border.TopBorderColor = XLColor.Black;
                    rn.Style.Border.BottomBorderColor = XLColor.Black;
                    ws.Column(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    //..............Sheet Headings....................//
                    string[] heads = new string[] { "Module Name", "Start Date", "Tech Duration", "BA Duration", "Developer Duration", "QC Duration", "Lead Duration", "Domain Duration", "Project Duration", "Module Cost" };
                    for (int i = 0; i < 10; i++)
                    {
                        rn = ws.Range(iRow + 3, iCol + j + 1, iRow + 4, iCol + j + 2);
                        rn.Merge();
                        rn.Value = heads[i];
                        rn.Style.Font.Bold = true;
                        rn.Style.Font.FontSize = 11;
                        rn.Style.Font.FontColor = XLColor.Black;
                        rn.Style.Font.FontName = "Calibri";
                        rn.Style.Alignment.WrapText = true;
                        rn.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        rn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        rn.Style.Fill.BackgroundColor = XLColor.LightGray;
                        rn.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        rn.Style.Border.LeftBorderColor = XLColor.Black;
                        rn.Style.Border.RightBorderColor = XLColor.Black;
                        rn.Style.Border.TopBorderColor = XLColor.Black;
                        rn.Style.Border.BottomBorderColor = XLColor.Black;
                        ws.Column(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        j = j + 2;
                    }

                    //.................Sheet Values........................//
                    foreach (var item in ExcelData)
                    {
                        List<string> sheetVal = new List<string>();
                        sheetVal.Add(item.Module_Name);
                        sheetVal.Add(item.Start_Date);
                        sheetVal.Add(item.Tech_Duration);
                        sheetVal.Add(item.BA_Duration);
                        sheetVal.Add(item.Developer_Duration);
                        sheetVal.Add(item.QC_Duration);
                        sheetVal.Add(item.Lead_Duration);
                        sheetVal.Add(item.Domain_Duration);
                        sheetVal.Add(item.Project_Duration);
                        sheetVal.Add(item.Module_Cost);
                        j = 0;
                        for (var i = 1; i <= 10; i++)
                        {
                            rn = ws.Range(iRow + 5, iCol + j + 1, iRow + 6, iCol + j + 2);
                            rn.Merge();
                            rn.Value = sheetVal[i - 1];
                            rn.Style.Font.Bold = false;
                            rn.Style.Font.FontSize = 10;
                            rn.Style.Font.FontColor = XLColor.Black;
                            rn.Style.Font.FontName = "Calibri";
                            rn.Style.Alignment.WrapText = true;
                            rn.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            rn.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            rn.Style.Fill.BackgroundColor = XLColor.White;
                            rn.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                            rn.Style.Border.LeftBorderColor = XLColor.Black;
                            rn.Style.Border.RightBorderColor = XLColor.Black;
                            rn.Style.Border.TopBorderColor = XLColor.Black;
                            rn.Style.Border.BottomBorderColor = XLColor.Black;
                            ws.Column(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            j = j + 2;
                        }
                        iRow = iRow + 2;
                    }
                    flushExcelWorkbook(wb, "Module_Summary");
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void flushExcelWorkbook(XLWorkbook wb, string sFileName)
        {
            try
            {
                string sTime = DateTime.Now.ToString("MMddyyHmmss");
                sFileName = sFileName + "_" + sTime;

                MemoryStream objMemoryStream = new MemoryStream();

                wb.SaveAs(objMemoryStream);
                byte[] buffer = objMemoryStream.ToArray();

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("Content-disposition", "attachment; filename=" + sFileName + ".xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
                Response.Flush();
                Response.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //GantChart String Builder//
        public string GetChartData()
        {
            using (test_pmEntities2 dc = new test_pmEntities2())
            {
                List<moduleDashboard> lst_obj = new List<moduleDashboard>();
                var id = Int32.Parse(Session["session_pid"].ToString());
                var dquery = dc.tbl_module.Where(x => x.ProjectInfo == id).ToList();

                foreach (var item in dquery)
                {
                    moduleDashboard obj = new moduleDashboard();
                    obj.ModuleID = item.ModuleID;
                    obj.ModuleTitle = item.ModuleTitle;
                    obj.ModuleWeight = item.ModuleWeight;
                    obj.PercentComplete = item.PercentComplete;
                    obj.ModuleTeamInfo = item.ModuleTeamInfo;
                    obj.ProjectInfo = item.ProjectInfo;
                    obj.StartDate = item.StartDate;
                    obj.EndDate = item.EndDate;
                    obj.ModuleDescription = item.ModuleDescription;
                    obj.ModuleFiles = item.ModuleFiles;
                    var mquery = dc.tbl_ProjectEstimate.Where(x => x.ModuleID == obj.ModuleID).FirstOrDefault();
                    obj.TotalDays = (mquery.TechDuration + mquery.AnalystDuration) * 5;
                    lst_obj.Add(obj);
                }
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(lst_obj);
                //FileStream fStrm;
                //StreamWriter wStrm;
                //TextWriter oldOut = Console.Out;
                //var filename = "jsonOutPut.txt";
                //string mappath = "~/ProjectDocs/";
                //bool dirExists = Directory.Exists(Server.MapPath(mappath));
                //if (!dirExists)
                //    Directory.CreateDirectory(Server.MapPath(mappath));
                //var path = Path.Combine(Server.MapPath(mappath), filename);
                //fStrm = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                //wStrm = new StreamWriter(fStrm);
                //Console.SetOut(wStrm);
                //Console.WriteLine(json);
                //Console.SetOut(oldOut);
                //wStrm.Close();
                //fStrm.Close();
                return (json);
            }
        }

        public ActionResult AddComment()
        {
            return View();
        }


    }
}

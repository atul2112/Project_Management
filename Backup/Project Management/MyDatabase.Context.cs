﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project_Management
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class test_pmEntities2 : DbContext
    {
        public test_pmEntities2()
            : base("name=test_pmEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<DesignationTbl> DesignationTbls { get; set; }
        public DbSet<ProjectDetailsTbl> ProjectDetailsTbls { get; set; }
        public DbSet<ProjectInfoTbl> ProjectInfoTbls { get; set; }
        public DbSet<RoleTbl> RoleTbls { get; set; }
        public DbSet<tbl_Cost> tbl_Cost { get; set; }
        public DbSet<tbl_detail> tbl_detail { get; set; }
        public DbSet<tbl_module> tbl_module { get; set; }
        public DbSet<tbl_moduleteam> tbl_moduleteam { get; set; }
        public DbSet<tbl_mom> tbl_mom { get; set; }
        public DbSet<tbl_team> tbl_team { get; set; }
        public DbSet<TeamTbl> TeamTbls { get; set; }
        public DbSet<UserInfoTbl> UserInfoTbls { get; set; }
        public DbSet<UserProjectTbl> UserProjectTbls { get; set; }
        public DbSet<tbl_comment> tbl_comment { get; set; }
        public DbSet<tbl_projectdetails> tbl_projectdetails { get; set; }
        public DbSet<tbl_ProjectEstimate> tbl_ProjectEstimate { get; set; }
    
        public virtual int tbl_Cost_Insert(Nullable<int> projectID, Nullable<int> techEstimate, Nullable<int> domainEstimate, Nullable<int> bAEstimate, Nullable<int> qCEstimate, Nullable<int> techLeadEstimate)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            var techEstimateParameter = techEstimate.HasValue ?
                new ObjectParameter("TechEstimate", techEstimate) :
                new ObjectParameter("TechEstimate", typeof(int));
    
            var domainEstimateParameter = domainEstimate.HasValue ?
                new ObjectParameter("DomainEstimate", domainEstimate) :
                new ObjectParameter("DomainEstimate", typeof(int));
    
            var bAEstimateParameter = bAEstimate.HasValue ?
                new ObjectParameter("BAEstimate", bAEstimate) :
                new ObjectParameter("BAEstimate", typeof(int));
    
            var qCEstimateParameter = qCEstimate.HasValue ?
                new ObjectParameter("QCEstimate", qCEstimate) :
                new ObjectParameter("QCEstimate", typeof(int));
    
            var techLeadEstimateParameter = techLeadEstimate.HasValue ?
                new ObjectParameter("TechLeadEstimate", techLeadEstimate) :
                new ObjectParameter("TechLeadEstimate", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("tbl_Cost_Insert", projectIDParameter, techEstimateParameter, domainEstimateParameter, bAEstimateParameter, qCEstimateParameter, techLeadEstimateParameter);
        }
    
        public virtual int tbl_module_Insert(Nullable<int> moduleID, string moduleTitle, string moduleWeight, string percentComplete, string moduleTeamInfo, Nullable<int> projectInfo, string startDate, string endDate, string moduleDescription, string moduleFiles)
        {
            var moduleIDParameter = moduleID.HasValue ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(int));
    
            var moduleTitleParameter = moduleTitle != null ?
                new ObjectParameter("ModuleTitle", moduleTitle) :
                new ObjectParameter("ModuleTitle", typeof(string));
    
            var moduleWeightParameter = moduleWeight != null ?
                new ObjectParameter("ModuleWeight", moduleWeight) :
                new ObjectParameter("ModuleWeight", typeof(string));
    
            var percentCompleteParameter = percentComplete != null ?
                new ObjectParameter("PercentComplete", percentComplete) :
                new ObjectParameter("PercentComplete", typeof(string));
    
            var moduleTeamInfoParameter = moduleTeamInfo != null ?
                new ObjectParameter("ModuleTeamInfo", moduleTeamInfo) :
                new ObjectParameter("ModuleTeamInfo", typeof(string));
    
            var projectInfoParameter = projectInfo.HasValue ?
                new ObjectParameter("ProjectInfo", projectInfo) :
                new ObjectParameter("ProjectInfo", typeof(int));
    
            var startDateParameter = startDate != null ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(string));
    
            var endDateParameter = endDate != null ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(string));
    
            var moduleDescriptionParameter = moduleDescription != null ?
                new ObjectParameter("ModuleDescription", moduleDescription) :
                new ObjectParameter("ModuleDescription", typeof(string));
    
            var moduleFilesParameter = moduleFiles != null ?
                new ObjectParameter("ModuleFiles", moduleFiles) :
                new ObjectParameter("ModuleFiles", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("tbl_module_Insert", moduleIDParameter, moduleTitleParameter, moduleWeightParameter, percentCompleteParameter, moduleTeamInfoParameter, projectInfoParameter, startDateParameter, endDateParameter, moduleDescriptionParameter, moduleFilesParameter);
        }
    
        public virtual int tbl_mom_Insert(Nullable<int> momID, string projectInfo, string timeStamp, string momContent, string momFile)
        {
            var momIDParameter = momID.HasValue ?
                new ObjectParameter("MomID", momID) :
                new ObjectParameter("MomID", typeof(int));
    
            var projectInfoParameter = projectInfo != null ?
                new ObjectParameter("ProjectInfo", projectInfo) :
                new ObjectParameter("ProjectInfo", typeof(string));
    
            var timeStampParameter = timeStamp != null ?
                new ObjectParameter("TimeStamp", timeStamp) :
                new ObjectParameter("TimeStamp", typeof(string));
    
            var momContentParameter = momContent != null ?
                new ObjectParameter("MomContent", momContent) :
                new ObjectParameter("MomContent", typeof(string));
    
            var momFileParameter = momFile != null ?
                new ObjectParameter("MomFile", momFile) :
                new ObjectParameter("MomFile", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("tbl_mom_Insert", momIDParameter, projectInfoParameter, timeStampParameter, momContentParameter, momFileParameter);
        }
    
        public virtual int tbl_projectdetail_Insert(string projectID, string title, string description, string domainInfo, string techTeamInfo, string managerInfo, string detailInfo, string clientInfo, string teamMemberInfo, string currentReleaseName, string estimatedCost, string projectHealthStatus, string commentInfo, string imgPath)
        {
            var projectIDParameter = projectID != null ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(string));
    
            var titleParameter = title != null ?
                new ObjectParameter("Title", title) :
                new ObjectParameter("Title", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var domainInfoParameter = domainInfo != null ?
                new ObjectParameter("DomainInfo", domainInfo) :
                new ObjectParameter("DomainInfo", typeof(string));
    
            var techTeamInfoParameter = techTeamInfo != null ?
                new ObjectParameter("TechTeamInfo", techTeamInfo) :
                new ObjectParameter("TechTeamInfo", typeof(string));
    
            var managerInfoParameter = managerInfo != null ?
                new ObjectParameter("ManagerInfo", managerInfo) :
                new ObjectParameter("ManagerInfo", typeof(string));
    
            var detailInfoParameter = detailInfo != null ?
                new ObjectParameter("DetailInfo", detailInfo) :
                new ObjectParameter("DetailInfo", typeof(string));
    
            var clientInfoParameter = clientInfo != null ?
                new ObjectParameter("ClientInfo", clientInfo) :
                new ObjectParameter("ClientInfo", typeof(string));
    
            var teamMemberInfoParameter = teamMemberInfo != null ?
                new ObjectParameter("TeamMemberInfo", teamMemberInfo) :
                new ObjectParameter("TeamMemberInfo", typeof(string));
    
            var currentReleaseNameParameter = currentReleaseName != null ?
                new ObjectParameter("CurrentReleaseName", currentReleaseName) :
                new ObjectParameter("CurrentReleaseName", typeof(string));
    
            var estimatedCostParameter = estimatedCost != null ?
                new ObjectParameter("EstimatedCost", estimatedCost) :
                new ObjectParameter("EstimatedCost", typeof(string));
    
            var projectHealthStatusParameter = projectHealthStatus != null ?
                new ObjectParameter("ProjectHealthStatus", projectHealthStatus) :
                new ObjectParameter("ProjectHealthStatus", typeof(string));
    
            var commentInfoParameter = commentInfo != null ?
                new ObjectParameter("CommentInfo", commentInfo) :
                new ObjectParameter("CommentInfo", typeof(string));
    
            var imgPathParameter = imgPath != null ?
                new ObjectParameter("ImgPath", imgPath) :
                new ObjectParameter("ImgPath", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("tbl_projectdetail_Insert", projectIDParameter, titleParameter, descriptionParameter, domainInfoParameter, techTeamInfoParameter, managerInfoParameter, detailInfoParameter, clientInfoParameter, teamMemberInfoParameter, currentReleaseNameParameter, estimatedCostParameter, projectHealthStatusParameter, commentInfoParameter, imgPathParameter);
        }
    
        public virtual int tbl_projectdetails_Insert(Nullable<int> projectID, string title, string description, Nullable<int> domainTeam, Nullable<int> domainLead, Nullable<int> techLead, Nullable<int> qCLead, string domainResource, string techResource, string modules, string estimatedCost, string projectLogo, string projectDetails, string startDate, string duration, Nullable<int> techTeam, string createdDate, string createdBy, string projectType, string clientName, string totalCost, string currencyType)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            var titleParameter = title != null ?
                new ObjectParameter("Title", title) :
                new ObjectParameter("Title", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var domainTeamParameter = domainTeam.HasValue ?
                new ObjectParameter("DomainTeam", domainTeam) :
                new ObjectParameter("DomainTeam", typeof(int));
    
            var domainLeadParameter = domainLead.HasValue ?
                new ObjectParameter("DomainLead", domainLead) :
                new ObjectParameter("DomainLead", typeof(int));
    
            var techLeadParameter = techLead.HasValue ?
                new ObjectParameter("TechLead", techLead) :
                new ObjectParameter("TechLead", typeof(int));
    
            var qCLeadParameter = qCLead.HasValue ?
                new ObjectParameter("QCLead", qCLead) :
                new ObjectParameter("QCLead", typeof(int));
    
            var domainResourceParameter = domainResource != null ?
                new ObjectParameter("DomainResource", domainResource) :
                new ObjectParameter("DomainResource", typeof(string));
    
            var techResourceParameter = techResource != null ?
                new ObjectParameter("TechResource", techResource) :
                new ObjectParameter("TechResource", typeof(string));
    
            var modulesParameter = modules != null ?
                new ObjectParameter("Modules", modules) :
                new ObjectParameter("Modules", typeof(string));
    
            var estimatedCostParameter = estimatedCost != null ?
                new ObjectParameter("EstimatedCost", estimatedCost) :
                new ObjectParameter("EstimatedCost", typeof(string));
    
            var projectLogoParameter = projectLogo != null ?
                new ObjectParameter("ProjectLogo", projectLogo) :
                new ObjectParameter("ProjectLogo", typeof(string));
    
            var projectDetailsParameter = projectDetails != null ?
                new ObjectParameter("ProjectDetails", projectDetails) :
                new ObjectParameter("ProjectDetails", typeof(string));
    
            var startDateParameter = startDate != null ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(string));
    
            var durationParameter = duration != null ?
                new ObjectParameter("Duration", duration) :
                new ObjectParameter("Duration", typeof(string));
    
            var techTeamParameter = techTeam.HasValue ?
                new ObjectParameter("TechTeam", techTeam) :
                new ObjectParameter("TechTeam", typeof(int));
    
            var createdDateParameter = createdDate != null ?
                new ObjectParameter("CreatedDate", createdDate) :
                new ObjectParameter("CreatedDate", typeof(string));
    
            var createdByParameter = createdBy != null ?
                new ObjectParameter("CreatedBy", createdBy) :
                new ObjectParameter("CreatedBy", typeof(string));
    
            var projectTypeParameter = projectType != null ?
                new ObjectParameter("ProjectType", projectType) :
                new ObjectParameter("ProjectType", typeof(string));
    
            var clientNameParameter = clientName != null ?
                new ObjectParameter("ClientName", clientName) :
                new ObjectParameter("ClientName", typeof(string));
    
            var totalCostParameter = totalCost != null ?
                new ObjectParameter("TotalCost", totalCost) :
                new ObjectParameter("TotalCost", typeof(string));
    
            var currencyTypeParameter = currencyType != null ?
                new ObjectParameter("CurrencyType", currencyType) :
                new ObjectParameter("CurrencyType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("tbl_projectdetails_Insert", projectIDParameter, titleParameter, descriptionParameter, domainTeamParameter, domainLeadParameter, techLeadParameter, qCLeadParameter, domainResourceParameter, techResourceParameter, modulesParameter, estimatedCostParameter, projectLogoParameter, projectDetailsParameter, startDateParameter, durationParameter, techTeamParameter, createdDateParameter, createdByParameter, projectTypeParameter, clientNameParameter, totalCostParameter, currencyTypeParameter);
        }
    
        public virtual int tbl_ProjectEstimate_Insert(Nullable<int> projectID, Nullable<int> moduleID, Nullable<int> techDuration, Nullable<int> analystDuration, string techResource, string analystResource, string developerDuration, string bADuration, string qCDuration, string leadDuration, string domainDuration, string moduleCost)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            var moduleIDParameter = moduleID.HasValue ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(int));
    
            var techDurationParameter = techDuration.HasValue ?
                new ObjectParameter("TechDuration", techDuration) :
                new ObjectParameter("TechDuration", typeof(int));
    
            var analystDurationParameter = analystDuration.HasValue ?
                new ObjectParameter("AnalystDuration", analystDuration) :
                new ObjectParameter("AnalystDuration", typeof(int));
    
            var techResourceParameter = techResource != null ?
                new ObjectParameter("TechResource", techResource) :
                new ObjectParameter("TechResource", typeof(string));
    
            var analystResourceParameter = analystResource != null ?
                new ObjectParameter("AnalystResource", analystResource) :
                new ObjectParameter("AnalystResource", typeof(string));
    
            var developerDurationParameter = developerDuration != null ?
                new ObjectParameter("DeveloperDuration", developerDuration) :
                new ObjectParameter("DeveloperDuration", typeof(string));
    
            var bADurationParameter = bADuration != null ?
                new ObjectParameter("BADuration", bADuration) :
                new ObjectParameter("BADuration", typeof(string));
    
            var qCDurationParameter = qCDuration != null ?
                new ObjectParameter("QCDuration", qCDuration) :
                new ObjectParameter("QCDuration", typeof(string));
    
            var leadDurationParameter = leadDuration != null ?
                new ObjectParameter("LeadDuration", leadDuration) :
                new ObjectParameter("LeadDuration", typeof(string));
    
            var domainDurationParameter = domainDuration != null ?
                new ObjectParameter("DomainDuration", domainDuration) :
                new ObjectParameter("DomainDuration", typeof(string));
    
            var moduleCostParameter = moduleCost != null ?
                new ObjectParameter("ModuleCost", moduleCost) :
                new ObjectParameter("ModuleCost", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("tbl_ProjectEstimate_Insert", projectIDParameter, moduleIDParameter, techDurationParameter, analystDurationParameter, techResourceParameter, analystResourceParameter, developerDurationParameter, bADurationParameter, qCDurationParameter, leadDurationParameter, domainDurationParameter, moduleCostParameter);
        }
    
        public virtual int tbl_WeekEstimate_Insert(Nullable<int> projectID, Nullable<int> moduleID, Nullable<int> techDuration, Nullable<int> bADuration, Nullable<int> qCDuration, Nullable<int> leadDuration, Nullable<int> domainDuration)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            var moduleIDParameter = moduleID.HasValue ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(int));
    
            var techDurationParameter = techDuration.HasValue ?
                new ObjectParameter("TechDuration", techDuration) :
                new ObjectParameter("TechDuration", typeof(int));
    
            var bADurationParameter = bADuration.HasValue ?
                new ObjectParameter("BADuration", bADuration) :
                new ObjectParameter("BADuration", typeof(int));
    
            var qCDurationParameter = qCDuration.HasValue ?
                new ObjectParameter("QCDuration", qCDuration) :
                new ObjectParameter("QCDuration", typeof(int));
    
            var leadDurationParameter = leadDuration.HasValue ?
                new ObjectParameter("LeadDuration", leadDuration) :
                new ObjectParameter("LeadDuration", typeof(int));
    
            var domainDurationParameter = domainDuration.HasValue ?
                new ObjectParameter("DomainDuration", domainDuration) :
                new ObjectParameter("DomainDuration", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("tbl_WeekEstimate_Insert", projectIDParameter, moduleIDParameter, techDurationParameter, bADurationParameter, qCDurationParameter, leadDurationParameter, domainDurationParameter);
        }
    
        public virtual int tbl_projectdetails_Delete(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("tbl_projectdetails_Delete", projectIDParameter);
        }
    
        public virtual int DeleteProject(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteProject", projectIDParameter);
        }
    
        public virtual int tbl_Cost_Delete(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("tbl_Cost_Delete", projectIDParameter);
        }
    
        public virtual int tbl_module_Delete(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("tbl_module_Delete", projectIDParameter);
        }
    
        public virtual int tbl_mom_Delete(string projectID)
        {
            var projectIDParameter = projectID != null ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("tbl_mom_Delete", projectIDParameter);
        }
    
        public virtual int tbl_ProjectEstimate_Delete(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("tbl_ProjectEstimate_Delete", projectIDParameter);
        }
    
        public virtual int DeleteMom(string projectID)
        {
            var projectIDParameter = projectID != null ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteMom", projectIDParameter);
        }
    
        public virtual int DeleteModule(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteModule", projectIDParameter);
        }
    
        public virtual int DeleteCost(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteCost", projectIDParameter);
        }
    
        public virtual int DeleteProjectEstimate(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteProjectEstimate", projectIDParameter);
        }
    
        public virtual int tbl_module_Delete_ModuleID_(Nullable<int> moduleID)
        {
            var moduleIDParameter = moduleID.HasValue ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("tbl_module_Delete_ModuleID_", moduleIDParameter);
        }
    
        public virtual int tbl_ProjectEstimate_Delete_ModuleID_(Nullable<int> moduleID)
        {
            var moduleIDParameter = moduleID.HasValue ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("tbl_ProjectEstimate_Delete_ModuleID_", moduleIDParameter);
        }
    
        public virtual int DeleteModuleByModuleID(Nullable<int> moduleID)
        {
            var moduleIDParameter = moduleID.HasValue ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteModuleByModuleID", moduleIDParameter);
        }
    
        public virtual int DeleteProjectEstimateByModuleID(Nullable<int> moduleID)
        {
            var moduleIDParameter = moduleID.HasValue ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteProjectEstimateByModuleID", moduleIDParameter);
        }
    
        public virtual int tbl_module_UpdateFile(Nullable<int> moduleID, string moduleFiles)
        {
            var moduleIDParameter = moduleID.HasValue ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(int));
    
            var moduleFilesParameter = moduleFiles != null ?
                new ObjectParameter("ModuleFiles", moduleFiles) :
                new ObjectParameter("ModuleFiles", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("tbl_module_UpdateFile", moduleIDParameter, moduleFilesParameter);
        }
    
        public virtual ObjectResult<tbl_module> UploadModuleFiles(Nullable<int> moduleID, string moduleFiles)
        {
            var moduleIDParameter = moduleID.HasValue ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(int));
    
            var moduleFilesParameter = moduleFiles != null ?
                new ObjectParameter("ModuleFiles", moduleFiles) :
                new ObjectParameter("ModuleFiles", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<tbl_module>("UploadModuleFiles", moduleIDParameter, moduleFilesParameter);
        }
    
        public virtual ObjectResult<tbl_module> UploadModuleFiles(Nullable<int> moduleID, string moduleFiles, MergeOption mergeOption)
        {
            var moduleIDParameter = moduleID.HasValue ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(int));
    
            var moduleFilesParameter = moduleFiles != null ?
                new ObjectParameter("ModuleFiles", moduleFiles) :
                new ObjectParameter("ModuleFiles", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<tbl_module>("UploadModuleFiles", mergeOption, moduleIDParameter, moduleFilesParameter);
        }
    
        public virtual int UpdateModuleFiles(Nullable<int> moduleID, string moduleFiles)
        {
            var moduleIDParameter = moduleID.HasValue ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(int));
    
            var moduleFilesParameter = moduleFiles != null ?
                new ObjectParameter("ModuleFiles", moduleFiles) :
                new ObjectParameter("ModuleFiles", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateModuleFiles", moduleIDParameter, moduleFilesParameter);
        }
    
        public virtual int UpdateModuleFile(Nullable<int> moduleID, string moduleFiles)
        {
            var moduleIDParameter = moduleID.HasValue ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(int));
    
            var moduleFilesParameter = moduleFiles != null ?
                new ObjectParameter("ModuleFiles", moduleFiles) :
                new ObjectParameter("ModuleFiles", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateModuleFile", moduleIDParameter, moduleFilesParameter);
        }
    
        public virtual int tbl_comment_Insert(Nullable<int> projectID, Nullable<int> commentID, string commentDate, Nullable<int> userID, string userName, string comment)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("projectID", projectID) :
                new ObjectParameter("projectID", typeof(int));
    
            var commentIDParameter = commentID.HasValue ?
                new ObjectParameter("commentID", commentID) :
                new ObjectParameter("commentID", typeof(int));
    
            var commentDateParameter = commentDate != null ?
                new ObjectParameter("commentDate", commentDate) :
                new ObjectParameter("commentDate", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(int));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            var commentParameter = comment != null ?
                new ObjectParameter("comment", comment) :
                new ObjectParameter("comment", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("tbl_comment_Insert", projectIDParameter, commentIDParameter, commentDateParameter, userIDParameter, userNameParameter, commentParameter);
        }
    
        public virtual ObjectResult<tbl_comment> InsertComment(Nullable<int> projectID, Nullable<int> commentID, string commentDate, Nullable<int> userID, string userName, string comment)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("projectID", projectID) :
                new ObjectParameter("projectID", typeof(int));
    
            var commentIDParameter = commentID.HasValue ?
                new ObjectParameter("commentID", commentID) :
                new ObjectParameter("commentID", typeof(int));
    
            var commentDateParameter = commentDate != null ?
                new ObjectParameter("commentDate", commentDate) :
                new ObjectParameter("commentDate", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(int));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            var commentParameter = comment != null ?
                new ObjectParameter("comment", comment) :
                new ObjectParameter("comment", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<tbl_comment>("InsertComment", projectIDParameter, commentIDParameter, commentDateParameter, userIDParameter, userNameParameter, commentParameter);
        }
    
        public virtual ObjectResult<tbl_comment> InsertComment(Nullable<int> projectID, Nullable<int> commentID, string commentDate, Nullable<int> userID, string userName, string comment, MergeOption mergeOption)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("projectID", projectID) :
                new ObjectParameter("projectID", typeof(int));
    
            var commentIDParameter = commentID.HasValue ?
                new ObjectParameter("commentID", commentID) :
                new ObjectParameter("commentID", typeof(int));
    
            var commentDateParameter = commentDate != null ?
                new ObjectParameter("commentDate", commentDate) :
                new ObjectParameter("commentDate", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(int));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            var commentParameter = comment != null ?
                new ObjectParameter("comment", comment) :
                new ObjectParameter("comment", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<tbl_comment>("InsertComment", mergeOption, projectIDParameter, commentIDParameter, commentDateParameter, userIDParameter, userNameParameter, commentParameter);
        }
    
        public virtual int tbl_projectdetails_UpdateTechDomainResource(Nullable<int> projectID, string techResource, string analystResource)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            var techResourceParameter = techResource != null ?
                new ObjectParameter("TechResource", techResource) :
                new ObjectParameter("TechResource", typeof(string));
    
            var analystResourceParameter = analystResource != null ?
                new ObjectParameter("AnalystResource", analystResource) :
                new ObjectParameter("AnalystResource", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("tbl_projectdetails_UpdateTechDomainResource", projectIDParameter, techResourceParameter, analystResourceParameter);
        }
    
        public virtual int UpdateTechDomainResource(Nullable<int> projectID, string techResource, string analystResource)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            var techResourceParameter = techResource != null ?
                new ObjectParameter("TechResource", techResource) :
                new ObjectParameter("TechResource", typeof(string));
    
            var analystResourceParameter = analystResource != null ?
                new ObjectParameter("AnalystResource", analystResource) :
                new ObjectParameter("AnalystResource", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateTechDomainResource", projectIDParameter, techResourceParameter, analystResourceParameter);
        }
    }
}

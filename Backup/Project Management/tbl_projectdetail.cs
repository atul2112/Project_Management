//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tbl_projectdetail
    {
        [Required(ErrorMessage = "Please Enter Project ID", AllowEmptyStrings = false)]
        [Display(Name = "Project ID")]
        public string ProjectID { get; set; }
        [Required(ErrorMessage = "Please Enter Project Title", AllowEmptyStrings = false)]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please Enter Description", AllowEmptyStrings = false)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please Enter Domain ID", AllowEmptyStrings = false)]
        [Display(Name = "Domain ID")]
        public string DomainInfo { get; set; }
        [Required(ErrorMessage = "Please Enter TechTeam ID", AllowEmptyStrings = false)]
        [Display(Name = "TechTeam ID")]
        public string TechTeamInfo { get; set; }
        [Required(ErrorMessage = "Please Enter Manager ID", AllowEmptyStrings = false)]
        [Display(Name = "Manager ID")]
        public string ManagerInfo { get; set; }
        [Required(ErrorMessage = "Please Enter Detail ID", AllowEmptyStrings = false)]
        [Display(Name = "Detail ID")]
        public string DetailInfo { get; set; }
        [Required(ErrorMessage = "Please Enter Client ID", AllowEmptyStrings = false)]
        [Display(Name = "Client ID")]
        public string ClientInfo { get; set; }
        [Required(ErrorMessage = "Please Enter Team Member ID", AllowEmptyStrings = false)]
        [Display(Name = "Team Member ID")]
        public string TeamMemberInfo { get; set; }
        [Required(ErrorMessage = "Please Enter Current Release Name", AllowEmptyStrings = false)]
        [Display(Name = "Current Release Name")]
        public string CurrentReleaseName { get; set; }
        [Required(ErrorMessage = "Please Enter Estimated Cost", AllowEmptyStrings = false)]
        [Display(Name = "Estimated Cost")]
        public string EstimatedCost { get; set; }
        [Required(ErrorMessage = "Please Enter Project Health Status", AllowEmptyStrings = false)]
        [Display(Name = "Project Health Status")]
        public string ProjectHealthStatus { get; set; }
        [Required(ErrorMessage = "Please Enter Comment ID", AllowEmptyStrings = false)]
        [Display(Name = "Comment ID")]
        public string CommentInfo { get; set; }
        public string ImgPath { get; set; }
    }
}
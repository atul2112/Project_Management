using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Management.Models
{
    public class ProjectEntityModel
    {
        public int RowNumber { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Image_Path { get; set; }

        public int WorkItemId { get; set; }
        public string WorkItemName { get; set; }

        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }
        public double Complete { get; set; }

        public int PriorityId { get; set; }
        public string PriorityName { get; set; }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }       
    }
}
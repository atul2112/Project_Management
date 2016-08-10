using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Management.snapModels
{
    public class snap_comment
    {
        public int projectID { get; set; }
        public int commentID { get; set; }
        public string commentDate { get; set; }
        public Nullable<int> userID { get; set; }
        public string userName { get; set; }
        public string comment { get; set; }
    }
}
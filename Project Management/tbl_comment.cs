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
    
    public partial class tbl_comment
    {
        public int projectID { get; set; }
        public int commentID { get; set; }
        public string commentDate { get; set; }
        public Nullable<int> userID { get; set; }
        public string userName { get; set; }
        public string comment { get; set; }
    }
}

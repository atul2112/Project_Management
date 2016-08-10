using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Management.snapModels
{
    public class snap_userInfo
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public int DesignationID { get; set; }
        public int TeamID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
    }
}
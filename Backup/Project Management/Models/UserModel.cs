using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Management.Models
{
    public class UserModel
    {
        public int UserID { get; set; }

        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public int DesID { get; set; }
        public string Designation { get; set; }

        public int TeamID { get; set; }
        public string TeamName { get; set; }

        public int UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        

    }
}
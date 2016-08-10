using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.IO;

namespace Project_Management.CommonFunctions
{
    public class LoginFunctions
    {
       
    }

    public class Userdetails
    {
        public string UserID;
        public string Username;
        public string FirstName;
        public string LastName;
        public string DesignationID;
        public string Designation;
        public string RoleID;
        public string Role;
        public string TeamID;
        public string Team;
        public string EmailID;

        
    }

    public class ModuleDetails
    {
        public string ModuleName;
        public string StartDate;
        public double TechDuration;
        public double BADuration;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Management.Models
{
    public class CommonFunctionsModel
    {
        public const string Session_ProjectDetails = "ProjectDetails";
        public static string GenerateRandomNumber()
        {
            string strPwdchar = "abcdefghijklmnopqrstuvwxyz0123456789#@$ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string strPwd = "";
            Random rnd = new Random();
            for (int i = 0; i <= 7; i++)
            {
                int iRandom = rnd.Next(0, strPwdchar.Length - 1);
                strPwd += strPwdchar.Substring(iRandom, 1);
            }
            return strPwd;
        }  
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Management.CommonFunctions;

namespace Project_Management.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Login()
        {
            Session["UserDetails"] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginInfo(UserInfoTbl u)
        {
          //this is to handle post login
            if (ModelState.IsValid) // check validity
            {

                using (test_pmEntities2 dc = new test_pmEntities2())
                {
                    var user = dc.UserInfoTbls.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if (user != null)
                    {
                        //Session["LoggedUserID"] = user.UserID.ToString();
                        //Session["LoggedUserFullname"] = user.FirstName.ToString() + " " + (user.LastName != null ? user.LastName.ToString() : "");

                        Userdetails UD = new Userdetails();
                        UD.UserID = user.UserID.ToString();
                        UD.Username = user.Username.ToString();
                        UD.FirstName = user.FirstName.ToString();
                        UD.LastName = (user.LastName != null ? user.LastName.ToString() : "");
                        UD.RoleID = user.RoleID.ToString();
                        UD.Role = dc.RoleTbls.Where(a => a.RoleID.Equals(user.RoleID)).Select(a=> a.RoleName).FirstOrDefault();
                        UD.DesignationID = user.DesignationID.ToString();
                        UD.Designation = dc.DesignationTbls.Where(a => a.DesID.Equals(user.DesignationID)).Select(a => a.DesName).FirstOrDefault();
                        UD.TeamID = user.TeamID.ToString();
                        UD.Team = dc.TeamTbls.Where(a => a.TeamID.Equals(user.TeamID)).Select(a => a.TeamName).FirstOrDefault();
                        UD.EmailID = user.EmailID.ToString();
                        

                        Session["UserDetails"] = UD;
                      
                        return RedirectToAction("Home", "Home");

                    }
                    else
                    {                       
                        TempData["Message"] = "Wrong Credentials, Check Login Details";
                        return RedirectToAction("Login");
                        
                    }
                }
            }
            return View(u);
        }

        public void blankFunction()
        {
            //asdfghjkl
        }

        public ActionResult LandingPage()
        {
            if (Session["LoggedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
       

    }
}

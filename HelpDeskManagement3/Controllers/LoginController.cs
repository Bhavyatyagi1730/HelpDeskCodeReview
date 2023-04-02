using Microsoft.Ajax.Utilities;
using Model;
using Service.Class;
using Service.Entities;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HelpDeskManagement3.Controllers
{

    public class LoginController : Controller
    {

        ILogin _login = null;
        public LoginController()
        {
            _login = new Login();
        }
        public ActionResult ShowLoginByCredential()
        {
       
            return View();
        } 
        [HttpPost]
        public ActionResult ShowLoginByCredential(LoginViewModel user)
        {
            if (ModelState.IsValid == true)
            {
                LoginViewModel obj = new LoginViewModel();
              
                obj = _login.GetinfoByUserCredentials(user.Email, user.Password);
          
                if (obj == null)
                {
                    ViewBag.ErrorMessage = "Login Failed!";
                    return View();
                }
                else
                {
                    Session["Id"] = obj.User_Id;
                    Session["username"] = obj.UserName;
                    Session["email"] = obj.Email;
                    Session["roletype"] = obj.RoleType;
                    Session["depID"] = obj.Dep_ID;
                    if (obj.RoleType == "null")
                    {
                        return View("Error");
                    }
                    if (obj.RoleType == "Admin" && obj.Email==user.Email && obj.Password==user.Password)
                    {  
                        return RedirectToAction("ShowAdminDashboard", "Dashboard");
                    }
                    else if(obj.RoleType == "User" && obj.Email == user.Email && obj.Password == user.Password)
                    {                    
                        return RedirectToAction("ShowUserDashboard", "UserDashboard");                    
                    }
                    else
                    {
                        return RedirectToAction("ShowLoginByCredential", "Login");
                    }
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            return RedirectToAction("ShowLoginByCredential", "Login");
        }
    }
}
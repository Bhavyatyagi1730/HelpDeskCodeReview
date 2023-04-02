using Microsoft.Ajax.Utilities;
using Model;
using Service.Class;
using Service.Entities;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;

namespace HelpDeskManagement3.Controllers
{
    public class DashboardController : Controller
    {
        ICreateUser _CreateUser = null;
        EmailValidation _EmailValidation = null;
        public DashboardController()
        {
            _CreateUser = new CreateUser();
            _EmailValidation = new EmailValidation();
        }
        // GET: Dashboard
        HelpDeskDBEntities DBContext = new HelpDeskDBEntities();


        public ActionResult ShowAdminDashboard()
        {
            try
            {
              
                string checkroletype = Session["roletype"]?.ToString() ?? "No RoleType";
                if (checkroletype == "Admin")
                {

                    return View();
                }
                else
                {
                    return RedirectToAction("ShowLoginByCredential", "Login");
                }
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }

        public ActionResult ShowUser()
        {
            var checkroletype = Session["roletype"].ToString();
            if (checkroletype == "Admin")
            {
                var Userdata = _CreateUser.GetAllUser();
                return View(Userdata);
            }
            return RedirectToAction("ShowLoginByCredential", "Login");
        }
        public ActionResult CreateNewUser()
        {

            var Checkroletype = Session["roletype"]?.ToString() ?? "No Roletype";
            if (Checkroletype == "Admin")
            {
                ViewBag.departmentdata = DBContext.Departments.ToList();
                ViewBag.roledata = DBContext.RoleTypes.ToList();
                return View();

            }
            return RedirectToAction("ShowLoginByCredential", "Login");

        }
        [HttpPost]
        public ActionResult CreateNewUser(HelpDesk newUser)
        {

            ViewBag.departmentdata = DBContext.Departments.ToList();
            ViewBag.roledata = DBContext.RoleTypes.ToList();

            if (ModelState.IsValid == true)
            {
                if (_EmailValidation.IsEmailUnique(newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email address already exists");

                }
                else
                {
                    int a = _CreateUser.createnewUser(newUser);
                    if (a > 0)
                    {
                        Session["InsertMessage"] = "<script>alert('Inserted !!)</script>";
                      
                        return RedirectToAction("ShowUser");
                    }
                    else
                    {
                        Session["InsertMessage"] = "<script>alert('not inserted !!)</script>";
                    }


                }


            }

            return View();
        }
        public ActionResult Edit(int id)
        {
            var CheckRoletype = Session["roletype"].ToString();
            if (CheckRoletype == "Admin")
            {
                var Userdata = _CreateUser.GetUserById(id);
                ViewBag.roledata = DBContext.RoleTypes.ToList();
                ViewBag.departmentdata = DBContext.Departments.ToList();
                return View(Userdata);

            }
            return RedirectToAction("ShowLoginByCredential", "Login");

        }
        [HttpPost]
        public ActionResult Edit(HelpDesk newUser)
        {
            int a = _CreateUser.UpdateUser(newUser);
            if (a > 0)
            {
                TempData["UpdateMessage"] = "<script>alert('Updated !!)</script>";
                return RedirectToAction("ShowUser");
            }
            else
            {
                TempData["UpdateMessage"] = "<script>alert('Not Updated !!)</script>";
            }

            return View();
        }
        public ActionResult Delete()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            int a = _CreateUser.DeleteUser(id);
            if (a > 0)
            {
                TempData["DeleteMessage"] = "<script>alert('Data is Deleted !!)</script>";
                return RedirectToAction("ShowUser");
            }
            else
            {
                TempData["DeleteMessage"] = "<script>alert('Not Deleted !!)</script>";
            }
            return View();
        }

    }
}
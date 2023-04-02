using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDeskManagement3.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        HelpDeskDBEntities dept = new HelpDeskDBEntities();
        public ActionResult DepartmentView()
        {
            var data = dept.Departments.ToList();
            return View(data);
        
        }

        public ActionResult AddDepartment()
        {

            return View();  
        }

        [HttpPost]
        public ActionResult AddDepartment(Department newDept)
        {

            if (ModelState.IsValid == true)
            {
                dept.Departments.Add(newDept);
                int a = dept.SaveChanges();
                if (a > 0)
                {
                    return RedirectToAction("DepartmentView");
                }
                else
                {
                    Session["InsertMessage"] = "<script>alert('not inserted !!)</script>";
                }
            }
            return View();
        }
    }
}
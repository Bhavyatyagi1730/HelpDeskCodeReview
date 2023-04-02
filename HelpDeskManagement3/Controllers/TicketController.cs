using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Interface;
using Service.Class;

namespace HelpDeskManagement3.Controllers
{ 
    public class TicketController : Controller
    {
        IShowTicket _ShowTicket = null;
        // GET: Ticket
        HelpDeskDBEntities DBContext = new HelpDeskDBEntities();
        public TicketController()
        {
            _ShowTicket = new ShowTicket();
        }
      
        public ActionResult ShowTicket()
        {
            string message = TempData["AdminUpdate"]?.ToString() ?? "No Ticket Created";
            ViewBag.Message = message;
            var admin = Session["roletype"]?.ToString() ?? "NO Roletype";
            if (admin == "Admin")
            {  
                var data = _ShowTicket.GetAllTicket();
                return View(data);

            }
            return RedirectToAction("ShowLoginByCredential", "Login");
           
        }
        public ActionResult AdminEditStatus(int id)
        {
            var admin = Session["roletype"]?.ToString() ?? "NO Roletype";
            if (admin == "Admin")
            {
                ViewBag.Department = DBContext.Departments.ToList();
                ViewBag.Status = DBContext.Status.ToList();
                var row = _ShowTicket.GetTicketById(id);
                return View(row);

            }
            return RedirectToAction("ShowLoginByCredential", "Login");

        }
        [HttpPost]
        public ActionResult AdminEditStatus(TicketTable newTicket)
        { 
            if (ModelState.IsValid == true)
            {
                int a = _ShowTicket.UpdateTicket(newTicket);
                if (a > 0)
                {
                    TempData["UpdateMessage"] = "<script>alert('Updated !!)</script>";
                    return RedirectToAction("ShowTicket");
                }
                else
                {
                    TempData["UpdateMessage"] = "<script>alert('Not Updated !!)</script>";
                }
            }
            return View();
        }
        public ActionResult AdminDeleteTask()
        {
            return View();
        } 
        [HttpPost]
        public ActionResult AdminDeleteTask(int id)
        {
            int a = _ShowTicket.DeleteTicket(id);
            if (a > 0)
            {
                TempData["DeleteMessage"] = "<script>alert('Data is Deleted !!)</script>";
                return RedirectToAction("ShowTicket");
            }
            else
            {
                TempData["DeleteMessage"] = "<script>alert('Not Deleted !!)</script>";
            }
            return View();
        }
        public ActionResult AdminShowDetails(int id)
        {
            var row = DBContext.TicketTables.Where(model => model.User_Id == id).FirstOrDefault();
            return View(row);
        }

        public ActionResult OpenTicket()
        {
            var admin = Session["roletype"]?.ToString() ?? "NO Roletype";
            if (admin == "Admin")
            {
                int statusId = 0;
                var openticketview = DBContext.TicketTables.Where(model => model.Status == statusId).ToList();
                return View(openticketview);

            }
            return RedirectToAction("ShowLoginByCredential", "Login");

        }
        public ActionResult ProgressTicket()
        {
            var admin = Session["roletype"]?.ToString() ?? "NO Roletype";
            if (admin == "Admin")
            {
                int statusId = 1;
                List<TicketTable> tickets = DBContext.TicketTables.Where(model => model.Status == statusId).ToList();
                return View(tickets);

            }
            return RedirectToAction("ShowLoginByCredential", "Login");
               

        }
        public ActionResult CloseTicket()
        {
            var admin = Session["roletype"]?.ToString() ?? "NO Roletype";
            if (admin == "Admin")
            {
                int statusId = 2;
                var ticketview = DBContext.TicketTables.Where(model => model.Status == statusId).ToList();
                return View(ticketview);

            }
            return RedirectToAction("ShowLoginByCredential", "Login");

        }
    }
    
}
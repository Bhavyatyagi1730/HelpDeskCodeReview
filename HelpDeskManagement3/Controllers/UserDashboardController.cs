using Model;
using Service.Class;
using Service.Interface;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace HelpDeskManagement3.Controllers
{
    public class UserDashboardController : Controller
    {
        HelpDeskDBEntities userData = new HelpDeskDBEntities();

        IEmail _email = null;
        ICreateUser _CreateUser = null;
        IShowTicket _showTicket = null;

        public UserDashboardController()
        {
            _email = new Email();
            _CreateUser = new CreateUser();
            _showTicket = new ShowTicket();
        }

        public ActionResult ShowUserDashboard()
        {
            var user = Session["roletype"].ToString();
            if (user == "User")
            {
                return View();

            }
            return RedirectToAction("ShowLoginByCredential", "Login");

        }

        public ActionResult ViewTicket()
        {
            var user = Session["roletype"].ToString();
            if (user == "User")
            {
                int value = Convert.ToInt32(Session["Id"]);
                var data = _showTicket.GetTicketByUserId(value);
                return View(data);
            }
            return RedirectToAction("ShowLoginByCredential", "Login");
        }
        public ActionResult CreateUserTicket()
        {
            var user = Session["roletype"].ToString();
            if (user == "User")
            {
                return View();
            }
            return RedirectToAction("ShowLoginByCredential", "Login");
        }

        [HttpPost]
        [ValidateInput(true)]
        public ActionResult CreateUserTicket(TicketTable newUser)
        {
            if (ModelState.IsValid == true)
            {
                int value = Convert.ToInt32(Session["Id"]);
                string name = (string)Session["username"];
                int departmentID = Convert.ToInt32(Session["depID"]);
                newUser.User_Id = value;
                newUser.CreatedBy = name;
                newUser.Dep_ID = departmentID;
                userData.TicketTables.Add(newUser);
                int a = _showTicket.userCreateTicket(newUser);
       
                _email.SendEmail("abhigautam.5678@gmail.com", "Ticket Status", "A new ticket with ID has been created !.");
                if (a > 0)
                {
                    TempData["Message"] = "Ticket Created Successfully";
                    TempData["AdminUpdate"] = "New Ticket Create succesfully!";
                    return RedirectToAction("ViewTicket");
                }
                else
                {
                    TempData["Message"] = "No Ticket Created";
                }

            }
            return View();
        }

        public ActionResult Delete()
        {
            var user = Session["roletype"].ToString();
            if (user == "User")
            {
                return View();

            }
            return RedirectToAction("ShowLoginByCredential", "Login");

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {

            int a = _showTicket.DeleteTicket(id);
            if (a > 0)
            {
                TempData["DeleteMessage"] = "<script>alert('Data is Deleted !!)</script>";
                return RedirectToAction("ViewTicket");
            }
            else
            {
                TempData["DeleteMessage"] = "<script>alert('Not Deleted !!)</script>";
            }
            return View();
        }
        public ActionResult Logout()
        {
            return RedirectToAction("ShowLoginByCredential", "Login");
        }
    }
}
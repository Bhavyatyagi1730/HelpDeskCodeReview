using Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace Service.Class
{
    public class ShowTicket : IShowTicket
    {
        HelpDeskDBEntities DBContext = new HelpDeskDBEntities();
        public TicketTable GetTicketById(int Id)
        {
          var row= DBContext.TicketTables.Where(model=>model.Ticket_Id==Id).FirstOrDefault();
           return row;
        }

        public List<TicketTable> GetTicketByUserId(int Id)
        {
            var row = DBContext.TicketTables.Where(model => model.User_Id == Id).ToList();
            return row;
        }


        public int UpdateTicket(TicketTable updateTicket)
        {
            var status = DBContext.Status.Where(m => m.Status_Id == updateTicket.Status).FirstOrDefault();
            //var department = DBContext.Departments.Where(m => m.Dep_ID == updateTicket.D).FirstOrDefault();
            updateTicket.Status = status.Status_Id;
            DBContext.Entry(updateTicket).State = System.Data.Entity.EntityState.Modified;
            return DBContext.SaveChanges();
        }

        public int CreateTicket(TicketTable newTicket)
        {
            DBContext.TicketTables.Add(newTicket);
            return DBContext.SaveChanges();
        }
        public int DeleteTicket(TicketTable product)
        {
            DBContext.TicketTables.Remove(product);
            return DBContext.SaveChanges();
        }
        public List<TicketTable> GetAllTicket()
        {
            var data = DBContext.TicketTables.ToList();
            return data;
        }

        //List<IShowTicket> IShowTicket.GetAllTicket()
        //{
        //    throw new NotImplementedException();
        //}

        public int DeleteTicket(int id)
        {
            TicketTable product = DBContext.TicketTables.Find(id);
            DBContext.TicketTables.Remove(product);
            return DBContext.SaveChanges();
        }

       

        public int userCreateTicket(TicketTable newTicketCreate)
        {
            
            string fileName = Path.GetFileNameWithoutExtension(newTicketCreate.ImageFile.FileName);
            string extension = Path.GetExtension(newTicketCreate.ImageFile.FileName);
            HttpPostedFileBase postedFile = newTicketCreate.ImageFile;
            int length = postedFile.ContentLength;
            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
            {
                fileName = fileName + extension;
                newTicketCreate.Image_path = "~/Images/" + fileName;
                fileName = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/"), fileName);
                newTicketCreate.ImageFile.SaveAs(fileName);

            }
           
            DBContext.TicketTables.Add(newTicketCreate);
            return DBContext.SaveChanges();
        }
    }
}

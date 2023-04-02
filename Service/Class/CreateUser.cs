using Model;
using Service.Entities;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace Service.Class
{
   public class CreateUser : ICreateUser
    {

        HelpDeskDBEntities DBContext = new HelpDeskDBEntities();
       
        public List<HelpDesk> GetAllUser()
        {
            var Userdata = DBContext.HelpDesks.ToList();
            return Userdata;

         
           
        }

        public HelpDesk GetUserById(int id)
        {
            var UserdataById = DBContext.HelpDesks.Where(model => model.User_Id == id).FirstOrDefault();
            return UserdataById;
        }

       

        public int UpdateUser(HelpDesk Updateusers)
        {
          
            var roleType = DBContext.RoleTypes.Where(m => m.RoleTypeID == Updateusers.RoleTypeID).FirstOrDefault();
            var department = DBContext.Departments.Where(m => m.Dep_ID == Updateusers.Dep_ID).FirstOrDefault();
            Updateusers.Roletype = roleType.RoleType_Disc;
            Updateusers.Dep_ID = department.Dep_ID;
            DBContext.Entry(Updateusers).State = EntityState.Modified;
            return DBContext.SaveChanges();
        }
        public int DeleteUser(int id)
        {
            var product = DBContext.HelpDesks.Find(id); 
            DBContext.HelpDesks.Remove(product);
            return DBContext.SaveChanges();
        }

       
     

        public int createnewUser(HelpDesk newUser)
        {
           
            var roleType = DBContext.RoleTypes.Where(m => m.RoleTypeID == newUser.RoleTypeID).FirstOrDefault();
            var department = DBContext.Departments.Where(m => m.Dep_ID ==newUser.Dep_ID).FirstOrDefault();
            newUser.Roletype = roleType.RoleType_Disc;
            newUser.Dep_ID = department.Dep_ID;
            DBContext.HelpDesks.Add(newUser);
            return DBContext.SaveChanges();
           

        }


        






    }
}

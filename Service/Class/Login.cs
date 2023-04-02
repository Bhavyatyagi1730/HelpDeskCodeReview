using Model;
using Service.Entities;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Class
{
    public class Login : ILogin
    {
        HelpDeskDBEntities db1 = new HelpDeskDBEntities();
        public LoginViewModel GetinfoByUserCredentials(string Email, string Password)
        {
            LoginViewModel user = new LoginViewModel();
              HelpDesk roledata = db1.HelpDesks.Where(m => m.Email == Email && m.Password == Password).FirstOrDefault();
            if (roledata != null)
            {
                user.User_Id = roledata.User_Id;
                user.Email = roledata.Email;
                user.Password = roledata.Password;
                user.RoleType = roledata.Roletype;
                user.UserName = roledata.UserName;
                user.Dep_ID = (int)roledata.Dep_ID;
            }
            return user;

        }
   


    }
}

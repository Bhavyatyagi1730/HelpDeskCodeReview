using Service.Entities;
using System;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICreateUser
    {
        List<HelpDesk> GetAllUser();
        HelpDesk GetUserById(int id);
        int createnewUser(HelpDesk newUser);
        int UpdateUser(HelpDesk Updateusers);
        int DeleteUser(int id);
       
    }
}

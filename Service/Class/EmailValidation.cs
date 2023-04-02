using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Class
{
    public class EmailValidation
    {
        HelpDeskDBEntities DBCOntext = new HelpDeskDBEntities();
        public bool IsEmailUnique(string email)
        {
            return DBCOntext.HelpDesks.Any(u => u.Email == email);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities
{
    public class LoginViewModel
    {
       
        public int User_Id { get; set; }
        //[Required(ErrorMessage = "Please enter UserName name.")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter Password.")]
        public string Password { get; set; }
       
        public string RoleType { get; set; }

        [Required(ErrorMessage = "Please enter Email Id.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        public int Dep_ID { get; set; }



    }
}

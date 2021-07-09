using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGS.Model
{
    public class RegistrationDetails
    {
        [Required(ErrorMessage = "Please Enter First Name")]
        public string first_name { get; set; }
        [Required(ErrorMessage = "Please Enter Last Name")]
        public string last_name { get; set; }

        public long phone_no { get; set; }
        
        public long alternate_phone_no { get; set; }
        [Required(ErrorMessage = "Please Enter Address")]
        public string address { get; set; }

        [Required(ErrorMessage = "Please Enter Email ID")]
        public string email_id { get; set; }
        [Required(ErrorMessage = "Please Enter User Name")]
        public string user_name { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        public string password { get; set; }

    }
}

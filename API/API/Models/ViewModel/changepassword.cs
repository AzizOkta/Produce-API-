using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModel
{
    public class changepassword
    {
        public int OTP { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
        public string confirmPass { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModel
{
    public class RegisterVM
    {
       
        public String FirstName { get; set; }     
        public String LastName { get; set; }
        public String Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public String Email { get; set; }
        public Gender Gender { get; set; }


        //acccount
        public String Password { get; set; }

        //University
        public String Degree { get; set; }
      

        public string GPA { get; set; }
        public int University_Id { get; set; }
     
    }
    public enum Gender
    {
        Male,
        Female
    }
}

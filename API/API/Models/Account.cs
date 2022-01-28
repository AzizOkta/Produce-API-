using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{

    [Table("TB_M_Account")]

public class Account
    {
        [Key]
        public string NIK { get; set; }
        public string Password { get; set; }

        public int otp { get; set; }
        public DateTime ExpiredTime { get; set; }
        public bool IsTrue { get; set; }


        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        [JsonIgnore]
        public virtual Profiling Profiling { get; set; }
        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
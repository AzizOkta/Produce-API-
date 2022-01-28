using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_AccountRole")]
    public class AccountRole
    {
        [ForeignKey("Role")]
        public int Id_Role { get; set; }

        [ForeignKey("Account")]
        public String Id_Account { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; }
    }
}

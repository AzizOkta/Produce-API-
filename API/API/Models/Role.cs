using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_Role")]
    public class Role
    {
        public int id { get; set; }
        public string nama { get; set; }
        public virtual ICollection<AccountRole> AccountRole { get; set; }
    }
}

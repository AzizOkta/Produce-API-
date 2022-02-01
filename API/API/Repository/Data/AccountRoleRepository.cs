using API.Context;
using API.Models;
using API.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
   
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, int>
    {
        private readonly MyContext Context;
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {
            this.Context = myContext;
        }

        public int SignManager(AccountRoleVM accountRoleVM)
        {
            AccountRole ar = new AccountRole()
            {
                Id_Role = 2,
                Id_Account = accountRoleVM.NIK
            };
            Context.AccountRoles.Add(ar);
            var result = Context.SaveChanges();

            return result;
        }
    }
}




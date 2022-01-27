using API.Context;
using API.Models;
using API.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext Context;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.Context = myContext;
        }

        public int Login(LoginVM loginVM)
        {
            var checkEmailPhone = Context.Employees.Where(e => e.Email == loginVM.Email || e.Phone == loginVM.Email)
                .FirstOrDefault();
            if (checkEmailPhone != null)
            {
                var getPassword = Context.Accounts.Where(e => e.NIK == checkEmailPhone.NIK).FirstOrDefault();
                if(BCrypt.Net.BCrypt.Verify(loginVM.Password, getPassword.Password))
                {
                    return 1;// login berhasil
                }
                else
                {
                    return 2;// checkEmail ditemukan password salah
                }
            }
            else
            {
                return 3;// email tidak ditemuakn
            }
          
        }

    }

}

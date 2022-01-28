using API.Base;
using API.Models;
using API.Models.ViewModel;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string> 
    {
        private readonly AccountRepository accountRepository;
        public AccountsController(AccountRepository accountRepository) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            var result = accountRepository.Login(loginVM);
            if (result != 0)
            {
                if (result == 2)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Password Salah!" });
                }
                else if (result == 3)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Akun tidak Terdafta!" });
                }
                else
                {
                    return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Berhasil Login" });
                }
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Gagal Login" });
            }
        }

        [Route("ForgetPass")]
        [HttpPost]
        public ActionResult<RegisterVM> ForgotPass (RegisterVM registerVM)
        {
            var hasil = accountRepository.ForgotPass(registerVM.Email);
            if(hasil == 1)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK ,message = "OTP Terkirim" });
            }
            else
            {
                return  StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Email Tidak Terdaftar" });
            }
        }

        [Route("ChangePassword")]
        [HttpPut]

        public ActionResult<changepassword>ChangePass(changepassword ChangePass)
        {
            var result = accountRepository.ChangePass(ChangePass);
            if(result == 1)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Password Berhasil di Ubah" });
            }
            else if (result == 2)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Pass dan Confirm Tidak Sama" });
            }
            else if (result == 3)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "OTP Sudah Digunakan" });
            }
            else if (result == 4)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Kode OTP Salah" });
            }
            else if (result == 5)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Kode OTP Expired" });
            }
            else 
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Email Tidak Terdaftar" });
            }
        }
    }

}

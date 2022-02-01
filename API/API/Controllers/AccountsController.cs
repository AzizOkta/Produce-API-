using API.Base;
using API.Context;
using API.Models;
using API.Models.ViewModel;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string> 
    {
        private readonly AccountRepository accountRepository;
        public IConfiguration _configuration;
        private readonly MyContext Context;

        public AccountsController(MyContext myContext, AccountRepository accountRepository, IConfiguration configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this._configuration = configuration;
            this.Context = myContext;
        }

        [Authorize]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT Berhasil");
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
                    var getUserData = Context.Employees.Where(e => e.Email == loginVM.Email || e.Phone == loginVM.Email).FirstOrDefault();
                    var getRole = Context.Roles.Where(r => r.AccountRole.Any(ar => ar.Account.NIK == getUserData.NIK)).ToList();

                    var claims = new List<Claim>
                    {
                        new Claim("Email", loginVM.Email) //payload
                    };

                    foreach (var item in getRole)
                    {
                        claims.Add(new Claim("roles", item.nama));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken
                    (
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signin
                    );
                    var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                    claims.Add(new Claim("TokenSecurity", idtoken.ToString()));
                    return StatusCode(200, new { status = HttpStatusCode.OK, idtoken, message = "Berhasil Login" });
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

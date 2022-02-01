using API.Base;
using API.Models;
using API.Models.ViewModel;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRolesController : BaseController<AccountRole,AccountRoleRepository, int>
    {
        public IConfiguration _configuration;
        private readonly AccountRoleRepository accountRoleRepository;
        public AccountRolesController(AccountRoleRepository accountRoleRepository, IConfiguration configuration) : base(accountRoleRepository)
        {
            this.accountRoleRepository = accountRoleRepository;
            this._configuration = configuration;
        }
       
        
        [Authorize(Roles = "Director")]
        [HttpPost("SignManager")]
        public ActionResult<AccountRoleVM> SignManager(AccountRoleVM accountRoleVM)
        {
            var result = accountRoleRepository.SignManager(accountRoleVM);
              return Ok(new { status = StatusCodes.Status200OK, result, message = "Berhasil Input Data Role!" });
                
        }
    }
}





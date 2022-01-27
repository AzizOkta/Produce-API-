using API.Base;
using API.Models;
using API.Models.ViewModel;
using API.Repository;
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
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>

    {
        private readonly EmployeeRepository employeeRepository;

        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost("{Register}")]
        public ActionResult<RegisterVM> Post(RegisterVM registerVM)
        {
            var result = employeeRepository.Register(registerVM);
            if (result != 0)
            {
                if (result == 4)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Email sudah digunakan!" });
                }
                else if (result == 5)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Nomor Telepon sudah digunakan!" });
                }
                else if (result == 6)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Email dan Nomor Telepon sudah digunakan!" });
                }
                else
                {
                    return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Data Berhasil ditambahkan" });
                }
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Gagal ditambahkan" });
            }
        }

        [Route("registerdata")]
        [HttpGet]

        public ActionResult<RegisterVM> GetRegisteredData()
        {
            var result = employeeRepository.GetRegisteredData();
            if (result != null)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Data Berhasil Ditampilkan" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Data Tidak Ditemukan" });  //ini kalau datanya harus kosong
            }
        }
    }
}







      
          


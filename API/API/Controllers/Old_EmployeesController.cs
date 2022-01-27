using API.Context;
using API.Models;
using API.Repository;
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
    public class Old_EmployeesController : ControllerBase
    {
        private OldEmployeeRepository employeeRepository;
        public Old_EmployeesController(OldEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost]
        public ActionResult Post(Employee employee)
        {
            var result = employeeRepository.Insert(employee);
            if (result != 0)
            {
                if (result == 2)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Email sudah digunakan!" });
                }
                else if (result == 3)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Nomor Telepon sudah digunakan!" });
                }
                else if (result == 4)
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
        [HttpPut]
        public ActionResult Put(Employee employee)
        {
            var result = employeeRepository.Update(employee);
            if (result != 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Bhasil DI Update" });
            }
            else
            {
                return StatusCode(404, new { StatusCode = HttpStatusCode.NotFound, result, message = "NIK Tidak ditemukan" });

            }
        }


        [HttpGet]
        public ActionResult Get()
        {
            var result = employeeRepository.Get();
            if (result.Count() > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Ditemukan" });
            }
            else
            {
                return StatusCode(404, new { StatusCode = HttpStatusCode.NotFound, result, message = "Data Tidak Ditemukan" });

            }
         
        }

        // [Route("getnik")]
        [HttpGet("{NIK}")]
        public ActionResult Get(Employee employee)
        {  
            var result = employeeRepository.Get(employee.NIK);
            if(result != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Ditemukan" });
            }
            else
            {
                return StatusCode(404, new { StatusCode = HttpStatusCode.NotFound, result, message = "Data Tidak Ditemukan" });
            }
           
        }

        /*  [Route("deletenik")]
          public ActionResult Delete(string NIK)
          {
              var result = employeeRepository.Delete(NIK);
              if (result >=0)
              {
                  return Ok("Berhasil");
              }
              else
              {
                  return BadRequest("Gagal");
              }
          }
  */

        [HttpDelete]
        public ActionResult Delete(Employee employee)
        {
            var result = employeeRepository.Delete(employee.NIK);
            if (result !=0)
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Berhasil Dihapus" });
            }
            else
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result, massage ="Data Tidak Berhasil DiHapus"});
            }
           
        }


    }
}



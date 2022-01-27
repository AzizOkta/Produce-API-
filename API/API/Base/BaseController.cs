using API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
    where Entity : class
    where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public ActionResult Post(Entity entity) //create
        {
            var result = repository.Insert(entity);

            if (result != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Data Berhasil ditambahkan" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Gagal ditambahkan" });
            }
        }


      [HttpGet("{key}")]
      //  [Route("getkey")]
        public ActionResult Get(Key key)
        {
            var result = repository.Get(key);
            if (result != null)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Data Berhasil Ditampilkan" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Data Tidak Ditemukan" });  //ini kalau datanya harus kosong
            }
        }



        [HttpGet]
        public ActionResult<Entity> Get()
        {
            var result = repository.Get();
            if (result.Count() > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Ditemukan" });
            }
            else
            {
                return StatusCode(404, new { StatusCode = HttpStatusCode.NotFound, result, message = "Data Tidak Ditemukan" });

            }
        }

        [HttpPut]
        public ActionResult Put(Entity entity) //update
        {
            var result = repository.Update(entity);
            //return Ok(result);
            if (result != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Data Berhasil Diubah" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Data Tidak Ditemukan" }); //belum
            }

        }

        [HttpDelete("{key}")]
        //[HttpDelete]
        public ActionResult Delete(Key key) //hapus
        {

            var result = repository.Delete(key);
            if (result != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Data Berhasil Dihapus" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Data Tidak Ditemukan" });
            }
        }
    }
}

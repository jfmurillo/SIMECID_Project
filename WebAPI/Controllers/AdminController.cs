﻿using CoreApp;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        //Siempre vamos a retornar 2 respuestas
        //200 --> OK
        //500 --> Internal server Error
        //Los retrieve trabajan con el verbo get de http
        [HttpGet]
        [Route("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            try
            {
                var am = new AdminManager();
                var adminList = am.RetrieveAll();
                return Ok(adminList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        [Route("RetrieveById")]
        public ActionResult RetrieveById(int adminId)
        {
            try
            {
                var am = new AdminManager();
                var admin = am.RetrieveById(adminId);

                if (admin != null)
                {
                    return Ok(admin);
                }
                else
                {
                    return NotFound("Admin not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost]
        [Route("Create")]
        public ActionResult Create(Admin admin)
        {
            try
            {
                var am = new AdminManager();
                am.Create(admin);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }



        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Admin admin)
        {
            try
            {
                var am = new AdminManager();
                am.Update(admin);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }



        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(Admin admin)
        {
            try
            {
                var am = new AdminManager();
                am.Delete(admin);
                return Ok(admin);


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

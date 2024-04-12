using CoreApp;
using DTO;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
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
                var um = new UserManager();
                var userList = um.RetrieveAll();
                return Ok(userList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        [Route("RetrieveById")]
        public ActionResult RetrieveById(int userId)
        {
            try
            {
                var um = new UserManager();
                var user = um.RetrieveById(userId);

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> CreateAsync(User user)
        {
            try
            {
                var um = new UserManager();
                um.Create(user);
                var em = new EmailManager();
                await em.SendEmail(user.Email);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }



        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(User user)
        {
            try
            {
                var um = new UserManager();
                um.Update(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(User user)
        {
            try
            {
                var um = new UserManager();
                um.Delete(user);
                return Ok(user);


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }



}

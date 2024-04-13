using CoreApp;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost] 
        [Route("Authenticate")]
        public ActionResult Authenticate([FromBody] LoginRequest request)
        {
            try
            {
                var um = new UserManager();
                var user = um.GetUserByEmail(request.Email);

                if (user != null && um.VerifyPassword(request.Password, user.Password))
                {
                    return Ok(new { message = "Login successful" });
                }
                else
                {
                    return Unauthorized("Invalid email or password");
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(ex + "Something went wrong");
            }
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

